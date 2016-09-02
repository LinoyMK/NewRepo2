using Journal.Public.DataAccess.ElasticSearch.ADO.Article;
using Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal;
using Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser;
using Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal;
using Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal;
using Nest;
using Search.Application.Abstract.Document;
using Search.Application.DTO.Search;
using Search.Application.DTO.Search.Internal;
using Search.DataAccess.ElasticSearch.Core.Repository;
using Search.Infrastructure.Configuration.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Search.Infrastructure.EnumTypes;

namespace Search.Application.Document
{
    public class FrontiersSearchDocumentService : IFrontiersSearchDocumentService
    {
        #region PRIVATE FIELDS

        private readonly IElasticSearchRepository _elasticSearchRepository;
        private readonly IFrontiersConfigurationManager _frontiersConfigurationManager;

        #endregion

        #region CONSTRUCTOR

        public FrontiersSearchDocumentService(IElasticSearchRepository elasticSearchRepository,
            IFrontiersConfigurationManager frontiersConfigurationManager)
        {
            _elasticSearchRepository = elasticSearchRepository;
            _frontiersConfigurationManager = frontiersConfigurationManager;
        }

        #endregion

        #region PUBLIC METHODS

        public ISearchDocumentDTO Search(string searchText, SearchType type, int skip, int top)
        {
            ISearchResponse<dynamic> searchResult = GetSearchResult(searchText, type, skip, top);
            return GetSearchDocumentDTO(searchResult);

        }

        public IEnumerable<ISearchSummaryDocumentDTO> GetSearchTypeSummary(string searchText, SearchType type)
        {
            ISearchResponse<dynamic> searchTypeSummary = GetSearchTypeSummaryResult(searchText, type);
            return GetSearchTypeStatisticsDocumentDTO(searchTypeSummary);
        }

        #endregion

        #region PRIVATE METHODS

       
        private ISearchResponse<dynamic> GetSearchResult(string searchText, SearchType type, int skip, int top) // TODO --> Need to test
        {

            var response = _elasticSearchRepository.Client.Search<dynamic>(s => s
               .Type(Types.Type(GetTypeNames(type)))
               .Skip(skip)
               .Take(top)
               .Query(qry => qry
                   .Bool(b => b
                   .Should(m => m.MultiMatch(mul => mul.Fields(ff => ff.Fields(GetSearchableFields())).Operator(Operator.And).Type(TextQueryType.BestFields).Query(searchText)))
                   ))
                .Source(source => source.Include(ff => ff.Fields(GetIncludeFields())))
                .Highlight(h => h.PreTags("<strong>").PostTags("</strong>").Fields(f => f.OnAll())) // TODO --> Highlight 
                );

            return response;
        }

        private ISearchResponse<dynamic> GetSearchTypeSummaryResult(string searchText, SearchType type) // TODO --> Need to test
        {
            var response = _elasticSearchRepository.Client.Search<dynamic>(s => s
              .Type(Types.Type(GetTypeNames(type)))
              .Query(qry => qry
                  .Bool(b => b
                  .Should(m => m.MultiMatch(mul => mul.Fields(ff => ff.Fields(GetSearchableFields())).Operator(Operator.And).Type(TextQueryType.BestFields).Query(searchText)))
                  ))
               .Source(false)
               .Aggregations(aggs => aggs.Terms("type_count", taggs => taggs.Field("_type")))
                );

            return response;
        }


        private ISearchDocumentDTO GetSearchDocumentDTO(ISearchResponse<dynamic> response)
        {
            ISearchDocumentDTO searchDocument = new SearchDocumentDTO();
            searchDocument.Results = new List<ISearchResultDocumentDTO>();

            searchDocument.Count = response.Total;

            if (response != null && response.Hits != null)
            {
                foreach (var hit in response.Hits)
                {
                    ISearchResultDocumentDTO result = new SearchResultDocumentDTO();
                    result.Score = hit.Score;

                    switch (hit.Type)
                    {
                        case "article":
                            ArticleAdo article = hit.Source;
                            Map_ArticleAdo_To_SearchResultDocumentDTO(article, result);
                            break;

                        case "research-topic":
                            Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.ResearchTopicAdo researchTopic = hit.Source;
                            Map_ResearchTopicAdo_To_SearchResultDocumentDTO(researchTopic, result);
                            break;

                        case "editorial-board-user":
                            EditorialBoardUserAdo editorialBoard = hit.Source;
                            Map_EditorialBoardUserAdo_To_SearchResultDocumentDTO(editorialBoard, result);
                            break;

                        case "journal":
                            Journal.Public.DataAccess.ElasticSearch.ADO.Journal.JournalAdo journal = hit.Source;
                            Map_JournalAdo_To_SearchResultDocumentDTO(journal, result);
                            break;

                    }

                    searchDocument.Results.Add(result);
                }
            }

            return searchDocument;
        }

        private IEnumerable<ISearchSummaryDocumentDTO> GetSearchTypeStatisticsDocumentDTO(ISearchResponse<dynamic> response)
        {
            IEnumerable<ISearchSummaryDocumentDTO> lstSearchTypeStatistics = null;
            if (response != null && response.Hits != null)
            {
                if (response.Aggregations != null && response.Aggregations.Any())
                {
                    if (response.Aggregations.ContainsKey("type_count"))
                    {
                        BucketAggregate aggsItems = (BucketAggregate)response.Aggregations["type_count"];

                        if (aggsItems != null)
                        {
                            lstSearchTypeStatistics = aggsItems.Items.Select(b => GetSearchTypeStatistics((KeyedBucket)b)).ToList();
                        }
                    }
                }
            }

            return lstSearchTypeStatistics;
        }

        private void Map_ResearchTopicAdo_To_SearchResultDocumentDTO(Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.ResearchTopicAdo researchTopic,
           ISearchResultDocumentDTO searchResult)
        {
            searchResult.Id = researchTopic.ResearchTopicId;
            searchResult.Type = SearchType.ResearchTopic.ToString();
            searchResult.Title = researchTopic.Title;
            searchResult.Subtitle = GetFormattedEditorNames(researchTopic.TopicEditors);
            searchResult.Description = GetTypeAndTopicStatus(researchTopic.IsOpenForSubmission);
            searchResult.Link = GetResearchTopicLink(researchTopic.ResearchTopicId);
        }

        private void Map_ArticleAdo_To_SearchResultDocumentDTO(ArticleAdo article,
          ISearchResultDocumentDTO searchResult)
        {
            searchResult.Id = article.ArticleId;
            searchResult.Title = article.Title;
            searchResult.Type = SearchType.Article.ToString();
            searchResult.Subtitle = GetFormattedAuthorNames(article.Authors);
            searchResult.Description = GetTypeAndJournalName(article.Journal); // Currently no section name for article.
            searchResult.Link = GetArticleLink(article.DOI);
        }

        private void Map_JournalAdo_To_SearchResultDocumentDTO(Journal.Public.DataAccess.ElasticSearch.ADO.Journal.JournalAdo journal,
            ISearchResultDocumentDTO searchResult) // TODO -->  Not considering the section now
        {
            searchResult.Id = journal.JournalId;
            searchResult.Type = SearchType.Journal.ToString();
            searchResult.Title = journal.Title;
            searchResult.Subtitle = journal.MissionStatement;
            searchResult.Description = GetTypeAndISSN(journal.ISSN);  // "Journal, Electronic ISSN: 1662-5161";
            searchResult.Link = GetJournalLink(journal.Slug);
        }

        private void Map_EditorialBoardUserAdo_To_SearchResultDocumentDTO(EditorialBoardUserAdo editorialBoardUser,
           ISearchResultDocumentDTO searchResult)
        {
            searchResult.Id = editorialBoardUser.UserId;
            searchResult.Type = SearchType.EditorialBoard.ToString();
            searchResult.Title = editorialBoardUser.FullName;
            searchResult.Subtitle = GetEditorialBoardUserAffiliation(editorialBoardUser.Affiliations);
            searchResult.Description = GetRoleAndJournal(editorialBoardUser.Roles);
            searchResult.Link = GetEditorialBoardUserLink(editorialBoardUser.UserId);
        }

        private string GetJournalLink(string slug)
        {
            return $"{_frontiersConfigurationManager.AddressSettings.JournalUIAddress}/journal/{slug}";
        }

        private string GetTypeAndISSN(string issn)
        {
            if (!string.IsNullOrWhiteSpace(issn))
            {
                return $"Journal, Electronic ISSN: {issn}";
            }

            return "Journal";
        }

        private string GetEditorialBoardUserLink(long userId)
        {
            return userId > 0 ? $"{_frontiersConfigurationManager.AddressSettings.LoopWebsiteUrl}/people/{userId}/overview" : string.Empty;
        }

        private string GetRoleAndJournal(IEnumerable<RoleAdo> roles)
        {
            string formattedRoleAndJournal = null;
            if (roles != null && roles.Any())
            {
                RoleAdo highestRole = roles.OrderBy(r => r.RoleId).FirstOrDefault();
                if (highestRole != null)
                {
                    Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal.JournalAdo firstJournal = highestRole.Journals.FirstOrDefault();
                    string journalName = (firstJournal?.Sections == null || !firstJournal.Sections.Any()) ? firstJournal?.Title : firstJournal.Sections.FirstOrDefault()?.Title;

                    if (string.IsNullOrWhiteSpace(journalName))
                    {
                        formattedRoleAndJournal = highestRole.Name;
                    }
                    else
                    {
                        formattedRoleAndJournal = $"{highestRole.Name}, {journalName}";
                    }

                }
            }

            return formattedRoleAndJournal;
        }

        private string GetEditorialBoardUserAffiliation(IEnumerable<Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal.AffiliationAdo> affiliations)
        {
            string formattedAffiliation = null;
            if (affiliations != null && affiliations.Any())
            {
                Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal.AffiliationAdo firstAff = affiliations.FirstOrDefault();
                formattedAffiliation = FormatAddress(firstAff.Organization, firstAff.City, firstAff.Country); //TODO
            }

            return formattedAffiliation;
        }

        private string FormatAffiliation(string organisation, string address)
        {
            string separator = string.Empty;

            if (!string.IsNullOrWhiteSpace(organisation) && !string.IsNullOrWhiteSpace(address))
            {
                separator = ", ";
            }

            return separator;
        }

        private string FormatAddress(string organization, string city, string country)
        {
            StringBuilder address = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(city))
            {
                address.Append(city);
            }

            if (!string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(country))
            {
                address.Append(", ");
                address.Append(country);
            }
            else
            {
                address.Append(country);
            }

            string formattedAddress = address.ToString();

            if (!string.IsNullOrWhiteSpace(organization))
            {
                if (!string.IsNullOrWhiteSpace(formattedAddress))
                {
                    formattedAddress = organization + ", " + formattedAddress;
                }
                else
                {
                    formattedAddress = organization;
                }
            }

            return formattedAddress;
        }

        private string GetResearchTopicLink(long researchTopicId)
        {
            return $"{_frontiersConfigurationManager.AddressSettings.JournalUIAddress}/researchtopic/{researchTopicId}";
        }

        private string GetTypeAndTopicStatus(bool isOpenForSubmission)
        {
            string status = isOpenForSubmission ? "submission open" : "submission closed";
            return $"ResearchTopic, {status}";
        }

        private string GetFormattedEditorNames(IEnumerable<TopicEditorAdo> topicEditors)
        {
            StringBuilder formattedEditorNames = new StringBuilder();
            IList<TopicEditorAdo> lstTopicEditors = topicEditors?.ToList();

            if (lstTopicEditors != null)
            {
                int counter = 0;
                foreach (TopicEditorAdo editor in lstTopicEditors)
                {
                    ++counter;

                    if (editor.UserId > 0)
                    {
                        formattedEditorNames.Append($"<a href='{_frontiersConfigurationManager.AddressSettings.LoopWebsiteUrl}/people/{editor.UserId}/overview' target='_blank'>{editor.FullName}</a>");
                    }
                    else
                    {
                        formattedEditorNames.Append(editor.FullName);
                    }

                    if (counter != lstTopicEditors.Count)
                    {
                        formattedEditorNames.Append(", ");
                    }
                }
            }

            return formattedEditorNames.ToString();
        }

        private ISearchSummaryDocumentDTO GetSearchTypeStatistics(KeyedBucket keyBucket)
        {
            return new SearchTypeStatisticsDocumentDTO()
            {
                Type = GetType(keyBucket.Key),
                Count = keyBucket.DocCount.Value
            };
        }

        private string GetType(string key)
        {
            string type = string.Empty;
            switch (key)
            {
                case "article":
                    type = SearchType.Article.ToString();
                    break;
                case "editorial-board-user":
                    type = SearchType.EditorialBoard.ToString();
                    break;
                case "research-topic":
                    type = SearchType.ResearchTopic.ToString();
                    break;
                case "journal":
                    type = SearchType.Journal.ToString();
                    break;
                case "author":
                    type = SearchType.Author.ToString();
                    break;
            }

            return type;
        }

        private string GetArticleLink(string doi)
        {
            return $"{_frontiersConfigurationManager.AddressSettings.JournalUIAddress}/article/{doi}";
        }

        private string GetTypeAndJournalName(Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal.JournalAdo journal)
        {
            return "Article, " + journal?.Title; // TODO --> Storing Only Journal name not Section name in ES
        }

        private string GetFormattedAuthorNames(IEnumerable<AuthorAdo> authors)
        {
            StringBuilder formattedAuthorNames = new StringBuilder();

            if (authors != null)
            {
                int counter = 0;
                foreach (AuthorAdo author in authors)
                {
                    ++counter;

                    if (author.UserId > 0)
                    {
                        formattedAuthorNames.Append($"<a href='{_frontiersConfigurationManager.AddressSettings.LoopWebsiteUrl}/people/{author.UserId}/overview' target='_blank'>{author.FullName}</a>");
                    }
                    else
                    {
                        formattedAuthorNames.Append(author.FullName);
                    }

                    if (counter != authors.Count())
                    {
                        formattedAuthorNames.Append(", ");
                    }
                }
            }

            return formattedAuthorNames.ToString();
        }

        private string[] GetIncludeFields()
        {
            List<string> lstFields = new List<string>();

            IList<string> lstCommonFields = new List<string>() { "Title", "Slug" };
            IList<string> lstArticleFields = new List<string>() { "ArticleId", "DOI", "Authors", "Journal" };
            IList<string> lstJournalFields = new List<string>() { "JournalId", "ISSN", "MissionStatement", "Sections.SectionId", "Sections.Title", "Sections.Slug", "Sections.MissionStatement", "Sections.ISSN" };
            IList<string> lstResearchTopicFields = new List<string>() { "TopicEditors.UserId", "TopicEditors.Affiliations", "TopicEditors.FullName", "ResearchTopicId", "IsOpenForSubmission" };
            IList<string> lstEditorialBoardUserFields = new List<string>() { "UserId", "FullName", "Affiliations", "Roles" };

            lstFields.AddRange(lstCommonFields);
            lstFields.AddRange(lstArticleFields);
            lstFields.AddRange(lstJournalFields);
            lstFields.AddRange(lstResearchTopicFields);
            lstFields.AddRange(lstEditorialBoardUserFields);

            return lstFields.ToArray();
        }

        private string[] GetSearchableFields()
        {
            List<string> fields = new List<string>();

            // DOI, Title, author list, AFFILATION OF AUTHORS, abstract, keywords, JOURNAL, SECTION(only storing the sectionId), EDITORS, REVIEWERS (full text?)
            IList<string> articleFields = new List<string>() { "DOI.search",
                "Authors.FullName.search", "Authors.Email.search", "Authors.Affiliations.Organization.search",
                "Abstract.search",
                "Keywords.Value.search",
                "Journal.Title.search", "Journal.Abbreviation.search",
                "Editor.Email.search", "Editor.Affiliations.Organization.search",
                "Reviewers.Email.search", "Reviewers.Affiliations.Organization.search" };

            // Title, ISSN, SECTIONS, missions statement

            IList<string> journalFields = new List<string>() { "ISSN.search", "MissionStatement.search", "Sections.Title.search", "Sections.MissionStatement.search" }; //TODO --> Sections  


            //Name, AFFILIATION, SPECIALTY(Expecting journal/section of editor Role), keywords

            IList<string> editorialBoardFields = new List<string>() { "FullName.search", "Affiliations.Organization.search", "Roles.Journals.Title.search", "Roles.Journals.Sections.Title", "Keywords.Value.search" };

            // Title, description, Topic Editors names, keywords
            IList<string> researchTopicFields = new List<string>() { "Description.search", "TopicEditors.FullName.search", "Keywords.Title.search" };


            fields.Add("Title.search");
            fields.AddRange(articleFields);
            fields.AddRange(journalFields);
            fields.AddRange(editorialBoardFields);
            fields.AddRange(researchTopicFields);

            return fields.ToArray();
        }

        private IEnumerable<TypeName> GetTypeNames(SearchType type)
        {
            IEnumerable<TypeName> lstTypeNames = null;

            switch (type)
            {
                case SearchType.All:
                    lstTypeNames = new List<TypeName>() {
                        typeof(ArticleAdo),
                        typeof(EditorialBoardUserAdo),
                        typeof(Journal.Public.DataAccess.ElasticSearch.ADO.Journal.JournalAdo),
                        typeof(Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.ResearchTopicAdo) };
                    break;

                case SearchType.Article:
                case SearchType.Author:
                    lstTypeNames = new List<TypeName>() { typeof(ArticleAdo) };
                    break;

                case SearchType.EditorialBoard:
                    lstTypeNames = new List<TypeName>() { typeof(EditorialBoardUserAdo) };
                    break;

                case SearchType.ResearchTopic:
                    lstTypeNames = new List<TypeName>() { typeof(Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.ResearchTopicAdo) };
                    break;

                case SearchType.Journal:
                    lstTypeNames = new List<TypeName>() { typeof(Journal.Public.DataAccess.ElasticSearch.ADO.Journal.JournalAdo) };
                    break;

                default:
                    lstTypeNames = new List<TypeName>() {
                        typeof(ArticleAdo),
                        typeof(EditorialBoardUserAdo),
                        typeof(Journal.Public.DataAccess.ElasticSearch.ADO.Journal.JournalAdo),
                        typeof(Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.ResearchTopicAdo) };
                    break;
            }

            return lstTypeNames;
        }

        #endregion

    }
}

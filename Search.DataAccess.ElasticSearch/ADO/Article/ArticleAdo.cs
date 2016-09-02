// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the ArticleAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal;
using Nest;
using SectionAdo = Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal.SectionAdo;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article
{
    [ElasticsearchType(Name = "article", IdProperty = "ArticleId")]
    public class ArticleAdo 
    {
        public long ArticleId { get; set; }
        public string Slug { get; set; }
        public string DOI { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public ArticleTypeAdo ArticleType { get; set; }
        public int StageId { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? RecentDate { get; set; }
        public IEnumerable<KeywordAdo> Keywords { get; set; }
        public bool Published { get; set; }
        public TaxonomyAdo Taxonomy { get; set; }
        public IEnumerable<AuthorAdo> Authors { get; set; }
        public IEnumerable<AuthorAdo> CorrespondingAuthors { get; set; }
        public EditorAdo Editor { get; set; }
        public IEnumerable<ReviewerAdo> Reviewers { get; set; }
        public JournalAdo Journal { get; set; }
        public SectionAdo Section { get; set; }
        public ResearchTopicAdo ResearchTopic { get; set; }
        public ImpactAdo Impact { get; set; }
        public ActivityAdo Activity { get; set; }
        public bool IsControversial { get; set; }

    }
}
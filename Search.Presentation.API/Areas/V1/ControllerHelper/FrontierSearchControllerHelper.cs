using Search.Application.DTO.Search;
using Search.Application.DTO.Search.Internal;
using Search.Infrastructure.Extension;
using Search.Presentation.API.Areas.V1.ViewModel;
using Search.Presentation.API.Areas.V1.ViewModel.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Search.Presentation.API.Areas.V1.ControllerHelper
{
    public static class FrontierSearchControllerHelper
    {
        #region PUBLIC METHODS

        public static SearchViewModel ToViewModel(this ISearchDocumentDTO searchDocumentDTO)
        {
            return searchDocumentDTO == null ?
                null :
                new SearchViewModel()
                {
                    Count = searchDocumentDTO.Count,
                    FormattedCount = searchDocumentDTO.Count.ToKiloFormat(),
                    Results = searchDocumentDTO.Results.ToViewModel()
                };
        }

        public static IEnumerable<SearchTypeStatisticsViewModel> ToViewModel(this IEnumerable<ISearchSummaryDocumentDTO> lstSearchTypeStatistics)
        {
            return lstSearchTypeStatistics == null || !lstSearchTypeStatistics.Any() ?
                null :
                lstSearchTypeStatistics.Select(m => m.ToViewModel());
        }

        #endregion

        #region PRIVATE METHODS

        private static SearchTypeStatisticsViewModel ToViewModel(this ISearchSummaryDocumentDTO searchTypeStatistics)
        {
            return searchTypeStatistics == null ?
                null :
                new SearchTypeStatisticsViewModel()
                {
                    Count = searchTypeStatistics.Count,
                    FormattedCount = searchTypeStatistics.Count.ToKiloFormat(),
                    Type = searchTypeStatistics.Type
                };

        }

        private static IEnumerable<SearchResultViewModel> ToViewModel(this IEnumerable<ISearchResultDocumentDTO> lstSearchResultDocumentDTO)
        {
            return lstSearchResultDocumentDTO == null || !lstSearchResultDocumentDTO.Any() ?
                null :
                lstSearchResultDocumentDTO.Select(m => m.ToViewModel());
        }

        private static SearchResultViewModel ToViewModel(this ISearchResultDocumentDTO searchResultDocumentDTO)
        {
            return searchResultDocumentDTO == null ?
                null :
                new SearchResultViewModel()
                {
                    Id = searchResultDocumentDTO.Id,
                    Type = searchResultDocumentDTO.Type,
                    Score = searchResultDocumentDTO.Score,
                    Title = searchResultDocumentDTO.Title,
                    Subtitle = searchResultDocumentDTO.Subtitle,
                    Description = searchResultDocumentDTO.Description,
                    Link = searchResultDocumentDTO.Link
                };
        }

        #endregion
    }
}
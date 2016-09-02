using Search.Presentation.API.Areas.V1.ViewModel.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Search.Presentation.API.Areas.V1.ViewModel
{
    public class SearchViewModel
    {
        [Key]
        public long Count { get; set; }

        public string FormattedCount { get; set; }

        public IEnumerable<SearchResultViewModel> Results { get; set; }

        public IEnumerable<SearchTypeStatisticsViewModel> Types { get; set; }
    }
}

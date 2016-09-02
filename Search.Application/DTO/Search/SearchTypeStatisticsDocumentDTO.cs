namespace Search.Application.DTO.Search.Internal
{
    public class SearchTypeStatisticsDocumentDTO : ISearchSummaryDocumentDTO
    {
        public string Type { get; set; }

        public long Count { get; set; }
    }
}

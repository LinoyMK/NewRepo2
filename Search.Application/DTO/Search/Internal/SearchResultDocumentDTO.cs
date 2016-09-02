namespace Search.Application.DTO.Search.Internal
{
    public class SearchResultDocumentDTO : ISearchResultDocumentDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Score { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }
    }
}

namespace Search.Application.DTO.Search.Internal
{
    public interface ISearchResultDocumentDTO
    {
        long Id { get; set; }

        string Type { get; set; }

        double Score { get; set; }

        string Title { get; set; }

        string Subtitle { get; set; }

        string Description { get; set; }

        string Link { get; set; }

    }
}

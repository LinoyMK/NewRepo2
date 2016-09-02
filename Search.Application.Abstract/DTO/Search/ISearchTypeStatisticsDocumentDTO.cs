namespace Search.Application.DTO.Search.Internal
{
    public interface ISearchSummaryDocumentDTO
    {
        string Type { get; set; }

        long Count { get; set; }
    }
}

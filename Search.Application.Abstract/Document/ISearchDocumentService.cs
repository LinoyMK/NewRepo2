using Search.Application.DTO.Search;
using Search.Application.DTO.Search.Internal;
using System.Collections.Generic;
using static Search.Infrastructure.EnumTypes;

namespace Search.Application.Abstract.Document
{
    public interface ISearchDocumentService
    {
        ISearchDocumentDTO Search(string searchText, SearchType type, int index, int size);

        IEnumerable<ISearchSummaryDocumentDTO> GetSearchTypeSummary(string searchText, SearchType type);
    }
}

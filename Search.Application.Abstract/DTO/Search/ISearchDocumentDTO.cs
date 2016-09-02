using Search.Application.DTO.Search.Internal;
using System.Collections.Generic;

namespace Search.Application.DTO.Search
{
    public interface ISearchDocumentDTO
    {
        long Count { get; set; }

        IList<ISearchResultDocumentDTO> Results { get; set; }
    }
}

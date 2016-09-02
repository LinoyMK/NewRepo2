using Search.Application.DTO.Search.Internal;
using System.Collections.Generic;

namespace Search.Application.DTO.Search
{
    public class SearchDocumentDTO : ISearchDocumentDTO
    {
        public long Count { get; set; }

        public IList<ISearchResultDocumentDTO> Results { get; set; }
    }
}

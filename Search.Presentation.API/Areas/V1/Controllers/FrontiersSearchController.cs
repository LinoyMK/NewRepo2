using Search.Application.Abstract.Document;
using Search.Application.DTO.Search;
using Search.Application.DTO.Search.Internal;
using Search.Infrastructure.Extension;
using Search.Presentation.API.Areas.V1.ControllerHelper;
using Search.Presentation.API.Areas.V1.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using static Search.Infrastructure.EnumTypes;

namespace Search.Presentation.API.Areas.V1.Controllers
{
    [ODataRoutePrefix("search")]
    public class FrontiersSearchController : ODataController
    {
        #region PRIVATE FIELDS

        private readonly IFrontiersSearchDocumentService _frontiersSearchService;

        #endregion

        #region CONSTRUCTOR

        public FrontiersSearchController(IFrontiersSearchDocumentService frontiersSearchService)
        {
            _frontiersSearchService = frontiersSearchService;
        }

        #endregion

        #region PUBLIC METHODS

        [HttpGet]
        [ODataRoute("get.frontiers(type={type})")]
        // [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.)]  // ? AllowedQueryOptions.Search is not present
        // http://192.168.1.124:8888/api/v1/search/get.frontiers(type='All')?$search=test 
        public IHttpActionResult GetFrontiersResult([FromODataUri]string type, ODataQueryOptions<SearchViewModel> options)
        {

            SearchType searchType = SearchType.All;
            try
            {
                searchType = type.ParseEnum<SearchType>();
            }
            catch (Exception)
            {
                return BadRequest($"search type {type} is not found");
            }

            IEnumerable<KeyValuePair<string, string>> queryParameters = options.Request.GetQueryNameValuePairs();
            var searchData = queryParameters.Where(m => m.Key == "$search");

            if (searchData == null || !searchData.Any())
            {
                return BadRequest("$search query is mandatory");
            }

            int skip = options.Skip == null ? 0 : options.Skip.Value;
            int top = options.Top == null ? 20 : options.Top.Value;
            string searchText = searchData.FirstOrDefault().Value;

            ISearchDocumentDTO searchResult = _frontiersSearchService.Search(searchText, searchType, skip, top);
            SearchViewModel searchViewModel = searchResult.ToViewModel();

            if (searchType == SearchType.All)
            {
                IEnumerable<ISearchSummaryDocumentDTO> typeSummary = _frontiersSearchService.GetSearchTypeSummary(searchText, searchType);
                searchViewModel.Types = typeSummary.ToViewModel();
            }

            return Ok(searchViewModel);
        }

        #endregion
    }
}

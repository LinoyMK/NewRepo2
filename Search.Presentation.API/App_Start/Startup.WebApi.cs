#region  Frontiers File Header
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Frontiers">
//   © 2007 - 2016 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
#endregion

using Microsoft.OData.Edm;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;

namespace Search.Presentation.API
{
    /// <summary>
    /// Startup Partial Implementation.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Web Api Implementation.
        /// </summary>
        private static class WebApi
        {
            /// <summary>
            /// Gets the configuration.
            /// </summary>
            /// <param name="odataModel">The odata model.</param>
            /// <returns></returns>
            public static HttpConfiguration GetConfig(IEdmModel odataModel)
            {
                var config = new HttpConfiguration();
                config.MapHttpAttributeRoutes();

                var pathHandler = new DefaultODataPathHandler();
                var routingConventions = ODataRoutingConventions.CreateDefaultWithAttributeRouting(config, odataModel);
                var batchHandler = new DefaultODataBatchHandler(new HttpServer(config));

                config.MapODataServiceRoute(
                    routeName: "ODataService",
                    routePrefix: "api/v1",
                    model: odataModel,
                    pathHandler: pathHandler,
                    routingConventions: routingConventions,
                    batchHandler: batchHandler);
                config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
                config.EnsureInitialized();

                // XML JSON

                var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
                config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

                return config;
            }

        }
    }
}

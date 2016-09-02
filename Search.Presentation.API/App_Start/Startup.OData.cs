using Microsoft.OData.Edm;
using Search.Presentation.API.Areas.V1.ViewModel;
using Search.Presentation.API.Areas.V1.ViewModel.Internal;
using System;
using System.Text;
using System.Web.OData.Builder;

namespace Search.Presentation.API
{
    public partial class Startup
    {
        private static class OData
        {
            #region CONSTANTS

            private const string ACTION_FUNCTION_NAMESPACE = "action";
            private const string GET_FUNCTION_NAMESPACE = "get";


            #endregion

            public static IEdmModel CreateModel()
            {
                var modelBuilder = new ODataConventionModelBuilder();
                modelBuilder.EnableLowerCamelCase();

                #region ENTITY TYPE

                var searchEntity = modelBuilder.EntityType<SearchViewModel>();
                searchEntity.Name = Camelize(searchEntity.Name);
                searchEntity.Namespace = "search";
                searchEntity.HasKey(entity => entity.Count);

                #endregion

                #region COMPLEX TYPE

                modelBuilder.ComplexType<SearchResultViewModel>();

                #endregion

                #region ENITY SET

                modelBuilder.EntitySet<SearchViewModel>("search");

                #endregion

                #region FUNCTIONS

                FunctionConfiguration frontiersResult = modelBuilder.EntityType<SearchViewModel>().Collection.Function("frontiers");
                frontiersResult.Parameter<string>("type");
                frontiersResult.Namespace = GET_FUNCTION_NAMESPACE;
                frontiersResult.ReturnsFromEntitySet<SearchViewModel>("search");
                frontiersResult.Namespace = GET_FUNCTION_NAMESPACE;

                #endregion


                return modelBuilder.GetEdmModel();

            }

            private static string Camelize(string text)
            {
                if (text == null)
                {
                    throw new ArgumentNullException(nameof(text));
                }

                if (text.Length == 0)
                {
                    return text;
                }

                var stringBuilder = new StringBuilder(text);
                stringBuilder[0] = char.ToLowerInvariant(text[0]);

                return stringBuilder.ToString();
            }
        }
    }
}

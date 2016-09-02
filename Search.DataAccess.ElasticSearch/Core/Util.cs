using System.Configuration;

namespace Search.DataAccess.ElasticSearch.Core
{
    /// <summary>
    /// Internal miscellaneous utility functions.
    /// </summary>
    internal static class Util
    {
        /// <summary>
        /// The default key MongoRepository will look for in the App.config or Web.config file.
        /// </summary>
        private const string DefaultConnectionStringName = "ElasticSearchServerSettings";

        /// <summary>
        /// 
        /// </summary>
        private const string DefaultIndexAlias = "ElasticSearch:IndexAlias";

        /// <summary>
        /// Retrieves the default connectionstring from the App.config or Web.config file.
        /// </summary>
        /// <returns>Returns the default connectionstring from the App.config or Web.config file.</returns>
        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[DefaultConnectionStringName].ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultIndexAlias()
        {
            return ConfigurationManager.AppSettings.Get(DefaultIndexAlias);
        }
    }
}

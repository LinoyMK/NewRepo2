using Nest;
using System;

namespace Search.DataAccess.ElasticSearch.Core.RepositoryManager
{
    public class ElasticSearchRepositoryManager : IElasticSearchRepositoryManager
    {
        public ElasticClient Client { get; private set; }

        private readonly string _indexAlias;

        public ElasticSearchRepositoryManager()
            : this((string)Util.GetDefaultConnectionString(), (string)Util.GetDefaultIndexAlias())
        {
        }

        public ElasticSearchRepositoryManager(string connectionString, string indexAlias)
        {
            _indexAlias = indexAlias;

            var node = new Uri(connectionString);

            var settings = new ConnectionSettings(
                node
            );

            Client = new ElasticClient(settings);
        }




    }

}

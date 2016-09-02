using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.DataAccess.ElasticSearch.Core.Repository
{
    public class ElasticSearchRepository : IElasticSearchRepository
    {
        private ElasticClient _client;

        public IElasticClient Client => new Lazy<IElasticClient>(() => _client).Value;

        private readonly string _indexAlias;

        public ElasticSearchRepository()
            : this((string)Util.GetDefaultConnectionString(), (string)Util.GetDefaultIndexAlias())
        {
        }

        public ElasticSearchRepository(string connectionString, string indexAlias)
        {
            _indexAlias = indexAlias;

            var node = new Uri(connectionString);

            var settings = new ConnectionSettings(node);
            settings.DefaultIndex(_indexAlias);

            _client = new ElasticClient(settings);
        }

        protected TDataObject Find<TDataObject>(SearchDescriptor<TDataObject> searchDescriptor) where TDataObject : class
        {
            IEnumerable<TDataObject> documents = Client.Search<TDataObject>(searchDescriptor).Documents;

            IList<TDataObject> dataObjects = documents as IList<TDataObject> ?? documents.ToList();
            if (dataObjects.Any())
                return dataObjects.First();

            return default(TDataObject);
        }

        protected async Task<TDataObject> FindAsync<TDataObject>(SearchDescriptor<TDataObject> searchDescriptor) where TDataObject : class
        {
            ISearchResponse<TDataObject> response = await Client.SearchAsync<TDataObject>(searchDescriptor);
            IEnumerable<TDataObject> documents = response.Documents;

            IList<TDataObject> dataObjects = documents as IList<TDataObject> ?? documents.ToList();
            if (dataObjects.Any())
                return dataObjects.First();

            return default(TDataObject);
        }

        public virtual TDataObject Find<TDataObject>(long id) where TDataObject : class
        {
            IGetResponse<TDataObject> response = Client.Get<TDataObject>(id);
            return response.Source;
        }

        public virtual async Task<TDataObject> FindAsync<TDataObject>(long id) where TDataObject : class
        {
            IGetResponse<TDataObject> response = await Client.GetAsync<TDataObject>(id);
            return response.Source;
        }
    }
}

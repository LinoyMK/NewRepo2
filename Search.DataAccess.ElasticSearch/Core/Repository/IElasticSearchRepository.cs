using Nest;

namespace Search.DataAccess.ElasticSearch.Core.Repository
{
    public interface IElasticSearchRepository
    {
        IElasticClient Client { get; }
    }
}

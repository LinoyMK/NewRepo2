using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal
{
    public class DomainAdo
    {
        [Number(Name = "DomainId")]
        public int DomainId { get; set; }
    }
}

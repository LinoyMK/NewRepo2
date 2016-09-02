using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal
{
    public class SpecialityAdo
    {
        [Number(Name = "SpecialityId")]
        public int SpecialityId { get; set; }
    }
}

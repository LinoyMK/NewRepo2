using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal
{
    public class FieldAdo
    {
        [Number(Name = "FieldId")]
        public int FieldId { get; set; }
    } 
}

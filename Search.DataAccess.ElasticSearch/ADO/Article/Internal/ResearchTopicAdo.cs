using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal
{
    public class ResearchTopicAdo
    {
        [Number(Name = "ResearchTopicId")]
        public int ResearchTopicId { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }
    }
}

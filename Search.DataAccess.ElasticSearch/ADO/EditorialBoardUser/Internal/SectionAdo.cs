using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
    public class SectionAdo
    {
        [Number(Name = "SectionId")]
        public int SectionId { get; set; }

        [String(Name = "Title", Analyzer = "title_analyzer")]
        public string Title { get; set; }
    }
}

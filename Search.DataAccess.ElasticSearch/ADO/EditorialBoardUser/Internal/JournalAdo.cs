using System.Collections;
using System.Collections.Generic;
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
    public class JournalAdo
    {
        [Number(Name = "JournalId")]
        public int JournalId { get; set; }

        [String(Name = "Title", Analyzer = "title_analyzer")]
        public string Title { get; set; }

        [Nested(Name = "Sections")]
        public IEnumerable<SectionAdo> Sections { get; set; }
    }
}

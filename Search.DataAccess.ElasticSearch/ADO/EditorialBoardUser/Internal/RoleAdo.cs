using System.Collections.Generic;
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
    public class RoleAdo
    {
        [Number(Name = "RoleId")]
        public int RoleId { get; set; }

        [String(Name = "Code")]
        public string Code { get; set; }

        [String(Name = "Name")]
        public string Name { get; set; }

        [Nested(Name = "Journals")]
        public IEnumerable<JournalAdo> Journals { get; set; }
    }
}

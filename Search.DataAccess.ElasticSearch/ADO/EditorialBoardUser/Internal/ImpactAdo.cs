using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
    public class ImpactAdo
    {
        [Number(Name = "Views")]
        public long Views { get; set; }

        [Number(Name = "Publications")]
        public long Publications { get; set; }
    }
}

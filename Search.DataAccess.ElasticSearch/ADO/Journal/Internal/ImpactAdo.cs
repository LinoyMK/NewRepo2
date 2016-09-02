using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class ImpactAdo
    {
        [Number(Name = "Views")]
        public long Views { get; set; }
    }
}

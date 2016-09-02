
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal
{
    public class ImpactAdo
    {
        [Number(Name = "Views")]
        public long Views { get; set; }

        [Number(Name = "PMCViews")]
        public long PMCViews { get; set; }

        [Number(Name = "FrontiersViews")]
        public long FrontiersViews { get; set; }

        [Number(Name = "Downloads")]
        public long Downloads { get; set; }

        [Number(Name = "PMCDownloads")]
        public long PMCDownloads { get; set; }

        [Number(Name = "Citations")]
        public long Citations { get; set; }

        [Number(Name = "ScopusCitations")]
        public long ScopusCitations { get; set; }

        [Number(Name = "CrossRefCitations")]
        public long CrossRefCitations { get; set; }
    }
}

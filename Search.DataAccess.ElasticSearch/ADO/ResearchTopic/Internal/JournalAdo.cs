// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JournalAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the JournalAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal
{
    public class JournalAdo
    {
        [Number(Name = "JournalId")]
        public int JournalId { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }

        [Nested(Name = "Sections")]
        public IEnumerable<SectionAdo> Sections { get; set; }
    }
}
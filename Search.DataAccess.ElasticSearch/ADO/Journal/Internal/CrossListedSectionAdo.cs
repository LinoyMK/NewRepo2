// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrossListedSectionAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the CrossListedSectionAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class CrossListedSectionAdo
    {
        public CrossListedSectionAdo()
        {
            CrossListedJournalInfo = new List<JournalInfoAdo>();
        }

        [Number(Name = "SectionId")]
        public int SectionId { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }

        [Boolean(Name = "Online")]
        public bool Online { get; set; }

        [Nested(Name = "CrossListedJournalInfo")]
        public IEnumerable<JournalInfoAdo> CrossListedJournalInfo { get; set; }
    }
}

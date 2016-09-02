// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JournalInfoAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the JournalInfoAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class JournalInfoAdo
    {
        [Number(Name = "JournalId")]
        public int JournalId { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }

        [String(Name = "Slug")]
        public string Slug { get; set; }

        [Boolean(Name = "Online")]
        public bool Online { get; set; }

        [Boolean(Name = "IsPrimary")]
        public bool IsPrimary { get; set; }

        [String(Name = "TwitterAccount")]
        public string TwitterAccount { get; set; }
    }
}

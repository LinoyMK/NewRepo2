// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JournalRepositoryAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the JournalRepositoryAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class JournalRepositoryAdo
    {
        [Number(Name = "RepositoryId")]
        public int RepositoryId { get; set; }

        [String(Name = "Name")]
        public string Name { get; set; }
    }
}

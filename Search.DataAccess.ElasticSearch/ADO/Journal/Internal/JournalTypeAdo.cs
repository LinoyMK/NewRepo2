// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JournalTypeMDO.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the JournalTypeMDO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class JournalTypeAdo 
    {
        [Number(Name = "JournalTypeId")]
        public int JournalTypeId { get; set; }

        [String(Name = "Name")]
        public string Name { get; set; }
    }
}

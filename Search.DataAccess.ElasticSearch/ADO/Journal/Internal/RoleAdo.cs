// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleMDO.cs" company="Frontiers">
//   © 2007 - 2014 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the RoleMDO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class RoleAdo
    {
        [Number(Name = "RoleId")]
        public int RoleId { get; set; }

        [String(Name = "Code", Analyzer = "keyword_analyzer")]
        public string Code { get; set; }

        [String(Name = "Name", Analyzer = "keyword_analyzer")]
        public string Name { get; set; }
    }
}

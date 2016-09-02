// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImpactAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the ImpactAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal
{
    public class ImpactAdo
    {
        [Number(Name = "Views")]
        public long Views { get; set; }
    }
}

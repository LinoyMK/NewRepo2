// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the SectionMDO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal
{
    public class SectionAdo
    {
        [Number(Name = "SectionId")]
        public int SectionId { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }
    }
}

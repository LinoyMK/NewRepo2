// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the ActivityMDO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal
{
    public class ActivityAdo
    {
        [Number(Name = "Likes")]
        public long Likes { get; set; }

        [Number(Name = "Shares")]
        public long Shares { get; set; }

        [Number(Name = "Comments")]
        public long Comments { get; set; }

        [Number(Name = "FacebookShares")]
        public long FacebookShares { get; set; }

        [Number(Name = "TwitterShares")]
        public long TwitterShares { get; set; }

        [Number(Name = "GooglePlusShares")]
        public long GooglePlusShares { get; set; }

        [Number(Name = "LinkedInShares")]
        public long LinkedInShares { get; set; }

        [Number(Name = "OtherShares")]
        public long OtherShares { get; set; }
    }
}

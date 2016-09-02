// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivityAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the ActivityAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
    public class ActivityAdo
    {
        [Number(Name = "Followers")]
        public long Followers { get; set; }
    }
}

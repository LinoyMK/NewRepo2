// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeywordAdo.cs" company="Frontiers">
//   © 2007 - 2016 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the KeywordAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal
{
    public class KeywordAdo
    {
        [Number(Name = "KeywordId")]
        public int KeywordId { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }
    }
}

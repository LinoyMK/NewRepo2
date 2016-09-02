// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeywordAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the KeywordAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
   public class KeywordAdo
    {
       [Number(Name = "KeywordId")]
       public int KeywordId { get; set; }

       [String(Name = "Value")]
       public string Value { get; set; }
    }
}

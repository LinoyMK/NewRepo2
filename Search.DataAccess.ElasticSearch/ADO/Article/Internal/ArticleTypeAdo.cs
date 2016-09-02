// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArticleTypeAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the ArticleTypeAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Article.Internal
{
    public class ArticleTypeAdo
    {
        [Number(Name = "ArticleTypeId" )]
        public int ArticleTypeId { get; set; }

        [String(Name = "Name")]
        public string Name { get; set; }
    }
}

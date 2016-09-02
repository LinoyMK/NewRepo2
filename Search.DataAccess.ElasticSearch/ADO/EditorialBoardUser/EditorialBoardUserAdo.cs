// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorialBoardUserAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the EditorialBoardUserAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal;
using Nest;
using System.Collections.Generic;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser
{
    [ElasticsearchType(Name = "editorial-board-user", IdProperty = "UserId")]
    public class EditorialBoardUserAdo 
    {
        [Number(Name = "UserId")]
        public long UserId { get; set; }

        [String(Name = "Email")]
        public string Email { get; set; }

        [String(Name = "FirstName")]
        public string FirstName { get; set; }

        [String(Name = "MiddleName")]
        public string MiddleName { get; set; }

        [String(Name = "LastName")]
        public string LastName { get; set; }

        [String(Name = "FullName")]
        public string FullName { get; set; }

        [String(Name = "ShortName")]
        public string ShortName { get; set; }

        [String(Name = "Roles")]
        public IEnumerable<RoleAdo> Roles { get; set; }

        [Nested(Name = "Affiliations")]
        public IEnumerable<AffiliationAdo> Affiliations { get; set; }

        [Object(Name = "Impact")]
        public ImpactAdo Impact { get; set; }

        [Object(Name = "Activity")]
        public ActivityAdo Activity { get; set; }

        [Nested(Name = "Keywords")]
        public IEnumerable<KeywordAdo> Keywords { get; set; }
    }
}

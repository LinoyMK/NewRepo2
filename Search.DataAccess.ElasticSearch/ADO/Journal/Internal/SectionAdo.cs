// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SectionAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the SectionAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class SectionAdo
    {
        [Number(Name = "SectionId")]
        public int SectionId { get; set; }

        [String(Name = "Slug")]
        public string Slug { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }

        [Boolean(Name = "Online")]
        public bool Online { get; set; }

        [String(Name = "PMCID")]
        public string PMCID { get; set; }

        [String(Name = "About")]
        public string About { get; set; }

        [String(Name = "Email")]
        public string Email { get; set; }

        [String(Name = "MissionStatement")]
        public string MissionStatement { get; set; }

        [Boolean(Name = "OpenForSubmission")]
        public bool OpenForSubmission { get; set; }

        [Nested(Name = "ArticleTypes")]
        public IEnumerable<ArticleTypeAdo> ArticleTypes { get; set; }

        [Nested(Name = "Taxonomies")]
        public IEnumerable<TaxonomyAdo> Taxonomies { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JournalAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the JournalAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal;
using Nest;
using System.Collections.Generic;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal
{
    [ElasticsearchType(Name = "journal", IdProperty = "JournalId")]
    public class JournalAdo 
    {
        [Number(Name = "JournalId")]
        public long JournalId { get; set; }

        [String(Name = "Slug")]
        public string Slug { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }

        [String(Name = "Abbreviation")]
        public string Abbreviation { get; set; }

        [String(Name = "ShortName")]
        public string ShortName { get; set; }

        [String(Name = "ISSN")]
        public string ISSN { get; set; }

        [Boolean(Name = "Online")]
        public bool Online { get; set; }

        [Boolean(Name = "OpenForSubmission")]
        public bool OpenForSubmission { get; set; }

        [String(Name = "ImpactFactor")]
        public string ImpactFactor { get; set; }

        [Object(Name = "JournalType")]
        public JournalTypeAdo JournalType { get; set; }

        [String(Name = "PMCID")]
        public string PMCID { get; set; }

        [String(Name = "About")]
        public string About { get; set; }

        [String(Name = "Email")]
        public string Email { get; set; }

        [String(Name = "MissionStatement")]
        public string MissionStatement { get; set; }

        [String(Name = "TwitterAccount")]
        public string TwitterAccount { get; set; }

        [Nested(Name = "Sections")]
        public IEnumerable<SectionAdo> Sections { get; set; }  // Contains Sections as well as cross-listed sections

        [Nested(Name = "CrossListedSections")]
        public IEnumerable<CrossListedSectionAdo> CrossListedSections { get; set; }  // Contains only cross-listed sections

        [Nested(Name = "Repositories")]
        public IEnumerable<JournalRepositoryAdo> Repositories { get; set; }

        [Nested(Name = "ArticleTypes")]
        public IEnumerable<ArticleTypeAdo> ArticleTypes { get; set; }

        [Nested(Name = "Taxonomies")]
        public IEnumerable<TaxonomyAdo> Taxonomies { get; set; }
    }
}

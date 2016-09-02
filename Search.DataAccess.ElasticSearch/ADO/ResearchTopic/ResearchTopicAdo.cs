// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResearchTopicAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the ResearchTopicAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal;
using Nest;
using System;
using System.Collections.Generic;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic
{
    [ElasticsearchType(Name = "research-topic", IdProperty = "ResearchTopicId")]
    public class ResearchTopicAdo 
    {
        [Number(Name = "ResearchTopicId")]
        public long ResearchTopicId { get; set; }

        [String(Name = "Slug")]
        public string Slug { get; set; }

        [String(Name = "Title")]
        public string Title { get; set; }

        [String(Name = "Description")]
        public string Description { get; set; }

        [Date(Name = "AbstractSubmissionDeadline")]
        public DateTime? AbstractSubmissionDeadline { get; set; }

        [Date(Name = "ManuscriptSubmissionDeadline")]
        public DateTime? ManuscriptSubmissionDeadline { get; set; }

        [Date(Name = "ManuscriptSubmissionExtendedDeadline")]
        public DateTime? ManuscriptSubmissionExtendedDeadline { get; set; }

        [Date(Name = "SuggestedOn")]
        public DateTime? SuggestedOn { get; set; }

        [Date(Name = "InPreparationSince")]
        public DateTime? InPreparationSince { get; set; }

        [Date(Name = "OnlineSince")]
        public DateTime? OnlineSince { get; set; }

        [Date(Name = "ClosedOn")]
        public DateTime? ClosedOn { get; set; }

        [Boolean(Name = "IsOpenForSubmission")]
        public bool IsOpenForSubmission { get; set; }

        [Boolean(Name = "IsOpenForAbstractSubmission")]
        public bool IsOpenForAbstractSubmission { get; set; }

        [Boolean(Name = "IsOpenForManuscriptSubmission")]
        public bool IsOpenForManuscriptSubmission { get; set; }

        [Boolean(Name = "IsEbookAvailable")]
        public bool IsEbookAvailable { get; set; }

        [Number(Name = "StageId")]
        public int StageId { get; set; }

        [Nested(Name = "TopicEditors")]
        public IEnumerable<TopicEditorAdo> TopicEditors { get; set; }

        [Nested(Name = "Journals")]
        public IEnumerable<JournalAdo> Journals { get; set; }

        [Object(Name = "Impact")]
        public ImpactAdo Impact { get; set; }

        [Object(Name = "Activity")]
        public ActivityAdo Activity { get; set; }

        [Nested(Name = "Keywords")]
        public IEnumerable<KeywordAdo> Keywords { get; set; }
    }
}

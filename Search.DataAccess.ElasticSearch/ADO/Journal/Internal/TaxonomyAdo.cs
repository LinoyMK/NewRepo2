// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaxonomyAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the TaxonomyAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class TaxonomyAdo
    {
        [Object(Name = "Domain")]
        public DomainAdo Domain { get; set; }

        [Object(Name = "Field")]
        public FieldAdo Field { get; set; }

        [Object(Name = "Speciality")]
        public SpecialityAdo Speciality { get; set; }
    }
}

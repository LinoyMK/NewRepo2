// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the DomainAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class DomainAdo
    {
       [Number(Name = "DomainId")]
       public int DomainId { get; set; }
    }
}

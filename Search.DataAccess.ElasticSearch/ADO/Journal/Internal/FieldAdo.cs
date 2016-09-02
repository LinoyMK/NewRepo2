// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldAdo.cs" company="Frontiers">
//   © 2007 - 2015 Frontiers Media S.A. All Rights Reserved
// </copyright>
// <summary>
//   Defines the FieldAdo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.Journal.Internal
{
    public class FieldAdo
    {
       [Number(Name = "FieldId")]
       public int FieldId { get; set; }
    } 
}

using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.EditorialBoardUser.Internal
{
    public class AffiliationAdo
    {
        [String(Name = "Department")]
        public string Department { get; set; }

        [String(Name = "Organization")]
        public string Organization { get; set; }

        [String(Name = "AddressLine1")]
        public string AddressLine1 { get; set; }

        [String(Name = "AddressLine2")]
        public string AddressLine2 { get; set; }

        [String(Name = "AddressLine3")]
        public string AddressLine3 { get; set; }

        [String(Name = "City")]
        public string City { get; set; }

        [String(Name = "PostalCode")]
        public string PostalCode { get; set; }

        [String(Name = "State")]
        public string State { get; set; }

        [String(Name = "Country")]
        public string Country { get; set; }

        [String(Name = "Position")]
        public string Position { get; set; }
    }
}

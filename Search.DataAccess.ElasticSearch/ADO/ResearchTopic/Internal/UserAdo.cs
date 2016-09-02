using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Journal.Public.DataAccess.ElasticSearch.ADO.ResearchTopic.Internal
{
    public class UserAdo
    {
        public UserAdo()
        {
            Affiliations = new List<AffiliationAdo>();
        }

        [Number(Name = "UserId")]
        public int UserId { get; set; }

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

        [Nested(Name = "Affiliations")]
        public IEnumerable<AffiliationAdo> Affiliations { get; set; }

    }
}
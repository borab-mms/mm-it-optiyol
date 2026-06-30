using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MMCustomerInfo
{
    public class Address
    {
        public long id { get; set; }
        public string additional_information { get; set; }
        public string street_concatenated { get; set; }
        public bool active { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string postal_code { get; set; }
    }
    public class legal_agreement
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
    }    
    public class external_reference
    {
        public int id { get; set; }
        public string reference_type { get; set; }
        public string reference_value { get; set; }
        public bool deleted { get; set; }
        public long created { get; set; }
    }
    public class communication_profile
    {
        public bool active { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
    public class CustomerInfoResponseModel
    {
        public string type { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string salutation { get; set; }
        public List<Address> addresses { get; set; }
        public List<external_reference> external_references { get; set; }
        public List<legal_agreement> legal_agreements { get; set; }
        public List<communication_profile> communication_profiles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MMCustomerInfo
{
    public class CustomerInfoSummaryModel
    {
        public string type { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string salutation { get; set; }
        public Address addresses { get; set; }
        public bool Loyalty { get; set; }
        public List<ClubCard> ClubCards { get; set; }
        public List<CommunicationProfile> CommunicationProfiles { get; set; }
    }
    public class ClubCard
    {
        public string clubCard { get; set; }
        public bool active { get; set; }
        public DateTime createdDate { get; set; }
    }
    public class CommunicationProfile
    {
        public bool active { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
}

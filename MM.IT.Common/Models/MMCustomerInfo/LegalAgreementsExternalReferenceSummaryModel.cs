using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MMCustomerInfo
{
    public class LegalAgreementsExternalReferenceSummaryModel
    {
        public List<legal_agreement> legal_agreements { get; set; }
        public List<external_reference> external_references { get; set; }
    }
}

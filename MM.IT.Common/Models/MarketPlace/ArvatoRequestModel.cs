using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MarketPlace
{

    public class ArvatoRequestModel
    {
        public string accountCode { get; set; }
        public List<OrderNumber> orders { get; set; }
        public bool ifManualCancel { get; set; }
        public int integrationHeaderId { get; set; }
        public bool continueOnError { get; set; }
    }
    public class OrderNumber
    {
        public string orderNumber { get; set; }
        public string cancellationReason { get; set; }
    }
}

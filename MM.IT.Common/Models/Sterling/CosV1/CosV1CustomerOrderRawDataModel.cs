using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling.CosV1
{
    public class CosV1CustomerOrderRawDataModel
    {
        public List<CustomerOrderItemModel> customer_orders { get; set; }
        public page_metadata page_metadata { get; set; }
    }
    public class page_metadata
    {
        public bool has_next { get; set; }
    }
}

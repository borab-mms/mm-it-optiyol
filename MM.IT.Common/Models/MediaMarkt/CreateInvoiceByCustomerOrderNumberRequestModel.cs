using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MediaMarkt
{
    public class CreateInvoiceByCustomerOrderNumberRequestModel
    {
        public string CustomerOrderNumbers { get; set; }
    }
    public class CreateInvoiceByCustomerOrderNumberRequestItemModel
    {
        public int CustomerOrderNumber { get; set; }
    }
}

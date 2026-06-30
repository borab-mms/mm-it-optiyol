using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{
    public class DifferenceOrdersOfCosV1FromCosV3Model
    {
        public List<DifferenceOrdersOfCosV1FromCosV3ItemModel> CustomerOrderNumbers { get; set; }
        public DateTime LastModified { get; set; }
    }
    public class DifferenceOrdersOfCosV1FromCosV3ItemModel
    {
        public string CustomerOrderNumber { get; set; }
    }
}

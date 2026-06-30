using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using MM.IT.Common.Models.MarketPlace;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.DigitalCard
{
    public class SerialCodeInEpayRequestModel
    {
        public string CustomerOrderNumber { get; set; }
        public string LineItemID { get; set; }
    }
}

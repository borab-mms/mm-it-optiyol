using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.DigitalCard
{
    public class CreataSerialCodeInEpayResponseModel
    {
        public DateTime SendingDate { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MediaMarkt
{
    public class CreateInvoiceByCustomerOrderNumberResponseModel
    {
        [JsonIgnore]
        public bool IsSuccess { get; set; }
        [JsonIgnore]
        public int Code { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common
{

    public class OrderResultModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Version { get; set; } = "v.1";
        [JsonIgnore]
        public int Code { get; set; }
        public T Data { get; set; }
        public List<OrderResultErrorModel> errors { get; set; }
    }
    public class OrderResultErrorModel
    {
        public int code { get; set; }
        public string error { get; set; }
        public string orderHeadId { get; set; }
    }
    public class OrderResultModel : OrderResultModel<object> { }
}

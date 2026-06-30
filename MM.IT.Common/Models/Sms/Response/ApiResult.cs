using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sms.Response
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public int Code { get; set; }

        public T Data { get; set; }
    }

    public class ApiResult : ApiResult<object> { }
}

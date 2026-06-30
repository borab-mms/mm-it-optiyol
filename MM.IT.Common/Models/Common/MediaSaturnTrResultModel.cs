using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Common
{
    public class MediaSaturnTrResultModel<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string version { get; set; }
        public int code { get; set; }
        public T data { get; set; }
        public List<MediaSaturnTrResultErrorModel> errors { get; set; }
    }
    public class MediaSaturnTrResultErrorModel
    {
        public int code { get; set; }
        public string error { get; set; }
    }
    public class MediaSaturnTrResultModel : MediaSaturnTrResultModel<object> { }
}

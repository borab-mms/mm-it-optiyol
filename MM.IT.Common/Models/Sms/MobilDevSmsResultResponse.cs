using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MM.IT.Common.Models.Sms
{
    [XmlRoot(ElementName = "MainReportRoot")]
    public class MobilDevSmsResultRequest
    {
        [XmlElement(ElementName = "UserName")]
        public string Username { get; set; }


        [XmlElement(ElementName = "PassWord")]
        public string Password { get; set; }


        [XmlElement(ElementName = "Action")]
        public int Action { get; set; }
        [XmlElement(ElementName = "MsgID")]
        public string ProccessId { get; set; }
    }
    public class MobilDevSmsResultResponse
    {
        public string ProccessId { get; set; }
        public string GSM { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public string SmsCount { get; set; }
        public string MessageType { get; set; }
        public string RecipientType { get; set; }
        public string IYSBrandCode { get; set; }
        public string Encoding { get; set; }
    }

}

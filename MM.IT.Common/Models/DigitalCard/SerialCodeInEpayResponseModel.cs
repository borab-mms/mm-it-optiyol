using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.DigitalCard
{
    [Serializable()]
    public class SerialCodeInEpayResponseModel
    {
        [System.Xml.Serialization.XmlElement("TERMINALID")]
        public string TERMINALID { get; set; }
        [System.Xml.Serialization.XmlElement("LOCALDATETIME")]
        public string LOCALDATETIME { get; set; }
        [System.Xml.Serialization.XmlElement("SERVERDATETIME")]
        public string SERVERDATETIME { get; set; }
        [System.Xml.Serialization.XmlElement("TXID")]
        public string TXID { get; set; }
        [System.Xml.Serialization.XmlElement("HOSTTXID")]
        public string HOSTTXID { get; set; }
        [System.Xml.Serialization.XmlElement("AMOUNT")]
        public decimal AMOUNT { get; set; }
        [System.Xml.Serialization.XmlElement("RESULT")]
        public string RESULT { get; set; }
        [System.Xml.Serialization.XmlElement("RESULTTEXT")]
        public string RESULTTEXT { get; set; }
        [System.Xml.Serialization.XmlElement("PINCREDENTIALS")]
        public PINCREDENTIALITEM PINCREDENTIALS { get; set; }
        public string ResponseData { get; set; }

    }
    public class PINCREDENTIALITEM
    {
        [System.Xml.Serialization.XmlElement("PIN")]
        public string PIN { get; set; }
        [System.Xml.Serialization.XmlElement("SERIAL")]
        public string SERIAL { get; set; }
    }
}

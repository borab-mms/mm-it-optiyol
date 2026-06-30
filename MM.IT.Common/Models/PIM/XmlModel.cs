using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace MM.IT.Common.Models.PIM
{
    public class IgnoreNamespaceXmlTextReader : XmlTextReader
    {
        public IgnoreNamespaceXmlTextReader(TextReader reader) : base(reader)
        {
        }

        public override string NamespaceURI => "";
    }

    [XmlRoot(ElementName = "rss")]
    [XmlType("rss")]
    public class XmlModel
    {
        public channel channel { get; set; }
    }

    public class channel
    {
        [XmlElement("item")]
        public List<item> items { get; set; }
    }

    public class item
    {
        [XmlElement("ean")]
        public string ean { get; set; }

        [XmlElement("stock")]
        public string stock { get; set; }

        [XmlElement("article")]
        public string article { get; set; }

        [XmlElement("salesPrice")]
        public string salesPrice { get; set; }

        [XmlElement("brand")]
        public string brand { get; set; }
    }
}

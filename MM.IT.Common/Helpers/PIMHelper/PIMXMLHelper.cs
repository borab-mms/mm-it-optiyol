using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using MM.IT.Common.Models.PIM;

namespace MM.IT.Common.Helpers.PIMHelper
{
    public static class PIMXMLHelper
    {
        public static XmlModel GetXMLData(string xmlUrl)
        {

            try
            {
                WebProxy myproxy = new WebProxy("http://ciscowsa.media-saturn.com:80/");
                myproxy.BypassProxyOnLocal = false;
                myproxy.UseDefaultCredentials = false;

                WebRequest apiRequest = WebRequest.Create(xmlUrl);
                //apiRequest.Proxy = myproxy;

                HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();

                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    string xmlOutput;
                    using (StreamReader srr = new StreamReader(apiResponse.GetResponseStream()))
                        xmlOutput = srr.ReadToEnd();

                    var sr = new StringReader(xmlOutput);
                    var xmlSerializer = new XmlSerializer(typeof(XmlModel));
                    var xmlModel = (XmlModel)xmlSerializer.Deserialize(new IgnoreNamespaceXmlTextReader(sr));
                    return xmlModel;
                }
                return new XmlModel();
            }
            catch (Exception ex)
            {
                return new XmlModel();
            }

        }
    }
}

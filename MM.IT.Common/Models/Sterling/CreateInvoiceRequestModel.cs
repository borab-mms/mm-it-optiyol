using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sterling
{
    public class CreateInvoiceRequestModel
    {
        public SendMultiServiceHeader sendMultiServiceHeader { get; set; }
        public List<SendMultiServiceLine> sendMultiServiceLines { get; set; }
    }
    public class SendMultiServiceHeader
    {
        public string projectCode { get; set; }
        public string orderNo { get; set; }
        public string companyCode { get; set; }
        public string storeName { get; set; }
        public string orderType { get; set; }
        public string expectedDateIssue { get; set; }
        public string expectedDateDelivery { get; set; }
        public string textMessage { get; set; }
        public string fromStockLocation { get; set; }
        public string referenceNr { get; set; }
        public string companyName1 { get; set; }
        public string companyName2 { get; set; }
        public string companyAddress1 { get; set; }
        public string companyAddress2 { get; set; }
        public string companyAddress3 { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string postalCode { get; set; }
        public string email { get; set; }
        public string taxAddress { get; set; }
        public string taxOffice { get; set; }
        public string telephone1 { get; set; }
        public string telephone2 { get; set; }
        public string carrier { get; set; }
    }
    public class SendMultiServiceLine
    {
        public string orderPosNo { get; set; }
        public string articleCode { get; set; }
        public string uom { get; set; }
        public int expectedQuantity { get; set; }
        public string stockLocation { get; set; }
        public string textMessageLine { get; set; }
    }


}

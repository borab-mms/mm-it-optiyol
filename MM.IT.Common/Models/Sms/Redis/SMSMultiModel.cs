using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sms.Redis
{
    public class SMSMultiModel
    {
        public int SMSSuccessfullID { get; set; }
        public int SMSErrorID { get; set; }
        public int ChannelId { get; set; }
        public string SendType { get; set; }
        public string MessageDescription { get; set; }
        public string Numbers { get; set; }
        public string Mesgbody { get; set; }
        public DateTime SDate { get; set; }
        public DateTime? EDate { get; set; }
        public string MessageType { get; set; }
        public string RecipientType { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string UserInfo { get; set; }
        public string OrderNumber { get; set; }
        public int CharacterQuantity { get; set; }
        public int SMSQuantity { get; set; }
        public int SMSStatus { get; set; }
        public string SMSType { get; set; }
        public bool IsSecretData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

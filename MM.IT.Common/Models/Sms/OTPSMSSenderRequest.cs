using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Sms
{
    public class OTPSMSSenderRequest
    {
        //[Required(ErrorMessage = "Mesgbody alanı boş bırakılamaz!")]
        public string Mesgbody { get; set; }
        //[Required(ErrorMessage = "Numbers alanı boş bırakılamaz!")]
        public string Numbers { get; set; }
        //[Required(ErrorMessage = "ChannelCode alanı boş bırakılamaz!")]
        public string ChannelCode { get; set; }
        public string MessageDescription { get; set; }
        //public string MessageType { get; set; }
        ///// <summary>
        ///// B : Bireysel T: Tacir MessageType C ise Zorunlu
        ///// </summary>
        //public string RecipientType { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string UserInfo { get; set; }
        public string OrderNumber { get; set; }
        public bool IsSecretData { get; set; }
    }
}

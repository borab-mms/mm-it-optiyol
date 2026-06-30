using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using MM.IT.Common.Models.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MMCustomerInfo
{
    public class AuthRequestModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

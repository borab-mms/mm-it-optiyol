using FluentValidation;
using MM.IT.Common.Models.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.MMCustomerInfo
{
    public class CustomerInfoRequestModel
    {
        public string result_mode { get; set; }
        public string search_type { get; set; }
        public string referenceType { get; set; }
        public string referenceValue { get; set; }
        public string Mobile { get; set; }
        public string email { get; set; }
        public bool isAllClubCard { get; set; }
    }
}

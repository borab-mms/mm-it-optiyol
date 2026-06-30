using MM.IT.Common.Models.Common;
using MM.IT.Core.Services.Base;
using MM.IT.Core.Services.Interfaces;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services
{
    public class ESBService : BaseService, IESBService
    {
        public ESBService(IServiceProvider serviceProvider):base(serviceProvider) 
        {
            //    
        }
        public Task<ServiceResultModel<string>> GetESBAsync()
        {
            throw new NotImplementedException();
        }
    }
}

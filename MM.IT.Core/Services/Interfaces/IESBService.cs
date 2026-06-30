using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.MarketPlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.Interfaces
{
    public interface IESBService
    {
        Task<ServiceResultModel<string>> GetESBAsync();

    }
}

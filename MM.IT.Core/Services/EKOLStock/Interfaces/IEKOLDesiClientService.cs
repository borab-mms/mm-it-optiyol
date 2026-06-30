using MM.IT.Common.Models.EKOLStock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.EKOLStock.Interfaces
{
    public interface IEKOLDesiClientService
    {
        Task<List<EKOLDesiModel>> PostXmlAsync(string url, HttpContent contentPost);
    }
}

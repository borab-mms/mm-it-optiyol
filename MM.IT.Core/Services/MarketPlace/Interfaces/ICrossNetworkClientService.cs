using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Services.MarketPlace.Interfaces
{
    public interface ICrossNetworkClientService
    {
        Task<T> PostAsync<T>(string url, HttpContent contentPost);
    }
}

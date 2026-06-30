using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Helpers.HttpClientHelper.Interfaces
{
    public interface IHttpClientHelper
    {
        Task<T> GetDataWithBasicAuthAsync<T>(string baseUrl, string username, string password, string url);
    }
}

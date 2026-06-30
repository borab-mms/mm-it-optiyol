using MM.IT.Common.Enums;
using MM.IT.Common.Models.Sterling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.RedisAdaptor.Interfaces
{
    public interface IRedisAdapter
    {
        Task<ScanResultModel> ScanAsync(string pattern, int pageSize);
        Task<List<CustomerOrderRedisModel>> ScanAndGetListsAsync(string pattern, int keyBatchSize = 100, int listPageSize = 500);
    }
}

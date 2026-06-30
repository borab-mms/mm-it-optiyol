using MM.IT.Data.Entities.MMDFS;
using MM.IT.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces;
public interface IMMDFSRepositoryWrapper
{
    IGenericRepository<EfCoreMMDFSSqlProvider, MEPTblOnlineSalesNumberEntity> MEPTblOnlineSalesNumberRepository { get; }

}

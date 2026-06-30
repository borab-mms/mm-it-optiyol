using MM.IT.Data.Entities.MMDFS;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;

public class MMDFSRepositoryWrapper : IMMDFSRepositoryWrapper
{
    private readonly IUnitOfWork<EfCoreMMDFSSqlProvider> _unitOfWork;

    public MMDFSRepositoryWrapper(IUnitOfWork<EfCoreMMDFSSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IGenericRepository<EfCoreMMDFSSqlProvider, MEPTblOnlineSalesNumberEntity> MEPTblOnlineSalesNumberRepository => _unitOfWork.GetRepository<MEPTblOnlineSalesNumberEntity>();
}
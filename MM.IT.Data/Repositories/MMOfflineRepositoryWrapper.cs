using MM.IT.Data.Entities.MMOffline;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;
/// <summary>
/// IMMOfflineRepositoryWrapper şartlarını barındıran RepositoryWrapper Nesnesi
/// </summary>
public class MMOfflineRepositoryWrapper : IMMOfflineRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreMMOfflineSqlProvider> _unitOfWork;

    public MMOfflineRepositoryWrapper(IUnitOfWork<EFCoreMMOfflineSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region WWS Generic Repositories

    public IGenericRepository<EFCoreMMOfflineSqlProvider, WWSSFSPickupReportEntity> WWS_SFSPickup_Repository => _unitOfWork.GetRepository<WWSSFSPickupReportEntity>();

    #endregion
}
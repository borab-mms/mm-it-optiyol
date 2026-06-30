using MM.IT.Data.Entities.MMLogistics;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;

/// <summary>
/// IMMLogisticsRepositoryWrapper şartlarını barındıran RepositoryWrapper Nesnesi
/// </summary>
public class MMLogisticsRepositoryWrapper : IMMLogisticsRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreMMLogisticsSqlProvider> _unitOfWork;

    public MMLogisticsRepositoryWrapper(IUnitOfWork<EFCoreMMLogisticsSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region EKOLStock Repositories

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, EKOLStockEntity> EKOLStockRepository => _unitOfWork.GetRepository<EKOLStockEntity>();

    #endregion

    #region Optiyol

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolNotificationEntity> OptiyolNotificationsRepository => _unitOfWork.GetRepository<OptiyolNotificationEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderArrivedEntity> OptiyolOrderArrivedsRepository => _unitOfWork.GetRepository<OptiyolOrderArrivedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCanceledEntity> OptiyolOrderCanceledsRepository => _unitOfWork.GetRepository<OptiyolOrderCanceledEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCanceledWithItemListEntity> OptiyolOrderCanceledWithItemListsRepository => _unitOfWork.GetRepository<OptiyolOrderCanceledWithItemListEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCanceledWithItemsEntity> OptiyolOrderCanceledWithItemsRepository => _unitOfWork.GetRepository<OptiyolOrderCanceledWithItemsEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedEntity> OptiyolOrderCompletedsRepository => _unitOfWork.GetRepository<OptiyolOrderCompletedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedWithItemEntity> OptiyolOrderCompletedWithItemRepository => _unitOfWork.GetRepository<OptiyolOrderCompletedWithItemEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedWithItemLotNumbersEntity> OptiyolOrderCompletedWithItemLotNumbersRepository => _unitOfWork.GetRepository<OptiyolOrderCompletedWithItemLotNumbersEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedWithItemsEntity> OptiyolOrderCompletedWithItemsRepository => _unitOfWork.GetRepository<OptiyolOrderCompletedWithItemsEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderReturnedEntity> OptiyolOrderReturnedsRepository => _unitOfWork.GetRepository<OptiyolOrderReturnedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderUndoCanceledEntity> OptiyolOrderUndoCanceledsRepository => _unitOfWork.GetRepository<OptiyolOrderUndoCanceledEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouteFinishedEntity> OptiyolRouteFinishedsRepository => _unitOfWork.GetRepository<OptiyolRouteFinishedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouteLoadListCompletedEntity> OptiyolRouteLoadListCompletedsRepository => _unitOfWork.GetRepository<OptiyolRouteLoadListCompletedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouteLoadListCompletedItemsEntity> OptiyolRouteLoadListCompletedItemsRepository => _unitOfWork.GetRepository<OptiyolRouteLoadListCompletedItemsEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRoutePlannedEntity> OptiyolRoutePlannedsRepository => _unitOfWork.GetRepository<OptiyolRoutePlannedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouterStartedEntity> OptiyolRouterStartedsRepository => _unitOfWork.GetRepository<OptiyolRouterStartedEntity>();

    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouterStartedOrdersEntity> OptiyolRouterStartedOrdersRepository => _unitOfWork.GetRepository<OptiyolRouterStartedOrdersEntity>();
    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRawDataEntity> OptiyolRawDataRepository => _unitOfWork.GetRepository<OptiyolRawDataEntity>();
    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderPickupVirtualCompletedEntity> OptiyolOrderPickupVirtualCompletedRepository => _unitOfWork.GetRepository<OptiyolOrderPickupVirtualCompletedEntity>();
    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderStatusEntity> OptiyolOrderStatusRepository => _unitOfWork.GetRepository<OptiyolOrderStatusEntity>();
    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolStatusEntity> OptiyolStatusRepository => _unitOfWork.GetRepository<OptiyolStatusEntity>();
    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolBarcodeHeadEntity> OptiyolBarcodeHeadRepository => _unitOfWork.GetRepository<OptiyolBarcodeHeadEntity>();
    public IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolBarcodeYearIdLogsEntity> OptiyolBarcodeYearIdLogsRepository => _unitOfWork.GetRepository<OptiyolBarcodeYearIdLogsEntity>();
    #endregion
}

using MM.IT.Data.Entities.MMLogistics;
using MM.IT.Data.Providers;
using MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces;
/// <summary>
/// Constructor injection kullanımını basitleştirmek için tüm entity repository tanımlamalarını içerir. 
/// Kullanılmak istenen IGenericRepository'den türeyecek repository bu kısımda tanımlanmalı.
/// DB:MMLogistics
/// </summary>
public interface IMMLogisticsRepositoryWrapper
{
    #region EKOLStock Repositories

    IGenericRepository<EFCoreMMLogisticsSqlProvider, EKOLStockEntity> EKOLStockRepository { get; }

    #endregion

    #region Optiyol

    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolNotificationEntity> OptiyolNotificationsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderArrivedEntity> OptiyolOrderArrivedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCanceledEntity> OptiyolOrderCanceledsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCanceledWithItemListEntity> OptiyolOrderCanceledWithItemListsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedEntity> OptiyolOrderCompletedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedWithItemEntity> OptiyolOrderCompletedWithItemRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedWithItemLotNumbersEntity> OptiyolOrderCompletedWithItemLotNumbersRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCompletedWithItemsEntity> OptiyolOrderCompletedWithItemsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderReturnedEntity> OptiyolOrderReturnedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderUndoCanceledEntity> OptiyolOrderUndoCanceledsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouteFinishedEntity> OptiyolRouteFinishedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouteLoadListCompletedEntity> OptiyolRouteLoadListCompletedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouteLoadListCompletedItemsEntity> OptiyolRouteLoadListCompletedItemsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRoutePlannedEntity> OptiyolRoutePlannedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouterStartedEntity> OptiyolRouterStartedsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRouterStartedOrdersEntity> OptiyolRouterStartedOrdersRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolRawDataEntity> OptiyolRawDataRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderPickupVirtualCompletedEntity> OptiyolOrderPickupVirtualCompletedRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderStatusEntity> OptiyolOrderStatusRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolStatusEntity> OptiyolStatusRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolOrderCanceledWithItemsEntity> OptiyolOrderCanceledWithItemsRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolBarcodeHeadEntity> OptiyolBarcodeHeadRepository { get; }
    IGenericRepository<EFCoreMMLogisticsSqlProvider, OptiyolBarcodeYearIdLogsEntity> OptiyolBarcodeYearIdLogsRepository { get; }

    #endregion
}
using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Entities.MMONLINE.Sterling;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces;

/// <summary>
/// Constructor injection kullanımını basitleştirmek için tüm entity repository tanımlamalarını içerir. 
/// Kullanılmak istenen IGenericRepository'den türeyecek repository bu kısımda tanımlanmalı.
/// DB:Sterling
/// </summary>
public interface ISterlingRepositoryWrapper
{
    #region Sterling Generic Repositories
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingGlobalLastUpdatedDataEntity> SterlingGlobalLastUpdatedDataRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderHeadEntity> SterlingOrderHeadRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderAddressEntity> SterlingOrderAddressRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataStatusListEntity> SterlingMasterDataStatusListRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingRawDataEntity> SterlingRawDataRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataSellerEntity> SterlingMasterDataSellerRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemEntity> SterlingOrderItemRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemHoldsEntity> SterlingOrderItemHoldRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemPriceEntity> SterlingOrderItemPriceRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemsPriceAdjustmentEntity> SterlingOrderItemsPriceAdjustmentRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemsShippingCostsDetailEntity> SterlingOrderItemsShippingCostsDetailRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemsStateQuantityEntity> SterlingOrderItemsStateQuantityRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentEntity> SterlingOrderFulfillmentRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentItemEntity> SterlingOrderFulfillmentItemRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentOrderShipmentEntity> SterlingOrderFulfillmentOrderShipmentRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderNoteEntity> SterlingOrderNoteRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderPaymentEntity> SterlingOrderPaymentRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderPaymentPartEntity> SterlingOrderPaymentPartRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderRequestedFulfillmentEntity> SterlingOrderRequestedFulfillmentRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderRequestedFulfillmentItemEntity> SterlingOrderRequestedFulfillmentItemRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderReturnChargeEntity> SterlingOrderReturnChargeRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderReturnEntity> SterlingOrderReturnRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderReturnItemEntity> SterlingOrderReturnItemRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalDetailAmountEntity> SterlingOrderTotalDetailAmountRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalDetailEntity> SterlingOrderTotalDetailRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalDetailVatEntity> SterlingOrderTotalDetailVatRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalEntity> SterlingOrderTotalRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentOrderShipmentHandlingUnitEntity> SterlingOrderFulfillmentOrderShipmentHandlingUnitRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentOrderShipmentHandlingUnitItemEntity> SterlingOrderFulfillmentOrderShipmentHandlingUnitItemRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataSmsContentMatchEntity> SterlingMasterDataSmsContentMatchRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataPgBasedSmsMatchEntity> SterlingMasterDataPgBasedSmsMatchRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingProcessSendToFSPEntity> SterlingProcessSendToFSPRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingProcessShipmentDataEntity> SterlingProcessShipmentDataRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingMissingDataOrderEntity> SterlingMissingDataOrderRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderAddressComponentEntity> SterlingOrderAddressComponentRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingCOSv3TransitionEntity> SterlingCOSv3TransitionRepository { get; }
    IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderComponentAttributeEntity> SterlingOrderComponentAttributeRepository { get; }
    #endregion
}

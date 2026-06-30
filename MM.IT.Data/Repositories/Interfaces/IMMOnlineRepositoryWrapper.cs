using MM.IT.Data.Entities.MEX;
using MM.IT.Data.Entities.MMONLINE;
using MM.IT.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces;

/// <summary>
/// Constructor injection kullanımını basitleştirmek için tüm entity repository tanımlamalarını içerir. 
/// Kullanılmak istenen IGenericRepository'den türeyecek repository bu kısımda tanımlanmalı.
/// DB:MMOnline
/// </summary>
public interface IMMOnlineRepositoryWrapper
{
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXRawDataEntity> MEXRawDataRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXFlowProblemEntity> MEXFlowProblemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentHeaderEntity> MEXPaymentHeaderRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentItemEntity> MEXPaymentItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentPaymentEntity> MEXPaymentPaymentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentPaymentCardEntity> MEXPaymentPaymentCardRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentPaymentPosEntity> MEXPaymentPaymentPosRepository { get; }

    #region MarketPlaces
    IGenericRepository<EFCoreMMOnlineSqlProvider, ReturnDemandEntity> ReturnDemandRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, ReturnDemandStatuEntity> ReturnDemandStatuRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, ReturnRejectionReasonEntity> ReturnRejectionReasonRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, SaleChannelEntity> SaleChannelRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, SalesChannelOrderStatuEntity> SalesChannelOrderStatuRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, OrderHeadEntity> OrderHeadRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, SalesChannelReturnStatuEntity> SalesChannelReturnStatuRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, OrderItemEntity> OrderItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, CustomerEntity> CustomerRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, T800ExcludeListEntity> T800ExcludeListRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, VatKeyEntity> VatKeyRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, ESBEntity> ESBRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, ESBLogEntity> ESBLogRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, OrderUpdateHistoryEntity> OrderUpdateHistoryRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, RawCancellationDataEntity> RawCancellationDataRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, BackorderTrackingEntity> BackorderTrackingRepository { get; }

    #endregion

    #region DigitalCars
    IGenericRepository<EFCoreMMOnlineSqlProvider, DCHeadEntity> DCHeadRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, DCItemEntity> DCItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, DCTransactionResultEntity> DCTransactionResultRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, DCErrorEntity> DCErrorRepository { get; }

    #endregion

    #region FOM
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMRawDataEntity> FOMRawDataRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderHeadEntity> FOMOrderHeadRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemEntity> FOMOrderItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderCustomersEntity> FOMOrderCustomersRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderAddressEntity> FOMOrderAddressRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMLCAndCargoMatchEntity> FOMLCAndCargoMatchRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderPaymentEntity> FOMOrderPaymentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderDiscountEntity> FOMOrderDiscountRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderChargesEntity> FOMOrderChargesRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemRelationsEntity> FOMOrderItemRelationsRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderReturnItemEntity> FOMOrderReturnItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemReturnStatusEntity> FOMOrderItemReturnStatusRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemPriceEntity> FOMOrderItemPriceRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemWarrantyEntity> FOMOrderItemWarrantyRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemDiscountEntity> FOMOrderItemDiscountRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemChargeEntity> FOMOrderItemChargeRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemFulFillmentEntity> FOMOrderItemFulFillmentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemFulFillmentDeliveryEntity> FOMOrderItemFulFillmentDeliveryRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemFulFillmentDeliveryPackageEntity> FOMOrderItemFulFillmentDeliveryPackageRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentEntity> FOMOrderShipmentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentLineEntity> FOMOrderShipmentLineRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderHeadEntity> FOMHistoryOrderHeadRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderAddressEntity> FOMHistoryOrderAddressRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderCustomersEntity> FOMHistoryOrderCustomersRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderPaymentEntity> FOMHistoryOrderPaymentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderDiscountEntity> FOMHistoryOrderDiscountRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemEntity> FOMHistoryOrderItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderReturnItemEntity> FOMHistoryOrderReturnItemRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemReturnStatusEntity> FOMHistoryOrderItemReturnStatusRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemPriceEntity> FOMHistoryOrderItemPriceRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemFulFillmentDeliveryEntity> FOMHistoryOrderItemFulFillmentDeliveryRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemChargeEntity> FOMHistoryOrderItemChargeRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemDiscountEntity> FOMHistoryOrderItemDiscountRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemWarrantyEntity> FOMHistoryOrderItemWarrantyRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemFulFillmentDeliveryPackageEntity> FOMHistoryOrderItemFulFillmentDeliveryPackageRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderShipmentEntity> FOMHistoryOrderShipmentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderShipmentLineEntity> FOMHistoryOrderShipmentLineRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, PGSMSEntity> PGSMSRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMArchiveCheckEntity> FOMArchiveCheckRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMTimeStampKeyEntity> FOMTimeStampKeyRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMDeliveryMethodEntity> FOMDeliveryMethodRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderChargesEntity> FOMHistoryOrderChargesRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemFulFillmentEntity> FOMHistoryOrderItemFulFillmentRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemRelationsEntity> FOMHistoryOrderItemRelationsRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMT800SendLogEntity> FOMT800SendLogRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMSellerEntity> FOMSellerRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMPaymentTypeEntity> FOMPaymentTypeRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentStatusEntity> FOMOrderShipmentStatusRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentTypesEntity> FOMOrderShipmentTypesRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMStatusListEntity> FOMStatusListRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMSenderMpShippingStatusLogEntity> FOMSenderMpShippingStatusLogRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMGlobalLastUpdatedDataEntity> FOMGlobalLastUpdatedDataRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMVCRExcludeEntity> FOMVCRExcludeRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, FOMMissingDataOrderEntity> FOMMissingDataOrderRepository { get; }

    #endregion

    #region CancellationProcess
    IGenericRepository<EFCoreMMOnlineSqlProvider, CancellationProcessReasonsForCancellationEntity> CancellationProcessReasonsForCancellationRepository { get; }
    IGenericRepository<EFCoreMMOnlineSqlProvider, CancellationProcessCancellationRequestEntity> CancellationProcessCancellationRequestRepository { get; }

    #endregion
}

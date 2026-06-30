using MM.IT.Data.Entities.MEX;
using MM.IT.Data.Entities.MMONLINE;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;


public class MMOnlineRepositoryWrapper : IMMOnlineRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreMMOnlineSqlProvider> _unitOfWork;

    public MMOnlineRepositoryWrapper(IUnitOfWork<EFCoreMMOnlineSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXRawDataEntity> MEXRawDataRepository => _unitOfWork.GetRepository<MEXRawDataEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXFlowProblemEntity> MEXFlowProblemRepository => _unitOfWork.GetRepository<MEXFlowProblemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentHeaderEntity> MEXPaymentHeaderRepository => _unitOfWork.GetRepository<MEXPaymentHeaderEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentItemEntity> MEXPaymentItemRepository => _unitOfWork.GetRepository<MEXPaymentItemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentPaymentEntity> MEXPaymentPaymentRepository => _unitOfWork.GetRepository<MEXPaymentPaymentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentPaymentCardEntity> MEXPaymentPaymentCardRepository => _unitOfWork.GetRepository<MEXPaymentPaymentCardEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, MEXPaymentPaymentPosEntity> MEXPaymentPaymentPosRepository => _unitOfWork.GetRepository<MEXPaymentPaymentPosEntity>();

    #region MarketPlaces
    public IGenericRepository<EFCoreMMOnlineSqlProvider, ReturnDemandEntity> ReturnDemandRepository => _unitOfWork.GetRepository<ReturnDemandEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, ReturnDemandStatuEntity> ReturnDemandStatuRepository => _unitOfWork.GetRepository<ReturnDemandStatuEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, ReturnRejectionReasonEntity> ReturnRejectionReasonRepository => _unitOfWork.GetRepository<ReturnRejectionReasonEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, SaleChannelEntity> SaleChannelRepository => _unitOfWork.GetRepository<SaleChannelEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, SalesChannelOrderStatuEntity> SalesChannelOrderStatuRepository => _unitOfWork.GetRepository<SalesChannelOrderStatuEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, OrderHeadEntity> OrderHeadRepository => _unitOfWork.GetRepository<OrderHeadEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, SalesChannelReturnStatuEntity> SalesChannelReturnStatuRepository => _unitOfWork.GetRepository<SalesChannelReturnStatuEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, OrderItemEntity> OrderItemRepository => _unitOfWork.GetRepository<OrderItemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, CustomerEntity> CustomerRepository => _unitOfWork.GetRepository<CustomerEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, T800ExcludeListEntity> T800ExcludeListRepository => _unitOfWork.GetRepository<T800ExcludeListEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, VatKeyEntity> VatKeyRepository => _unitOfWork.GetRepository<VatKeyEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, ESBEntity> ESBRepository => _unitOfWork.GetRepository<ESBEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, ESBLogEntity> ESBLogRepository => _unitOfWork.GetRepository<ESBLogEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, OrderUpdateHistoryEntity> OrderUpdateHistoryRepository => _unitOfWork.GetRepository<OrderUpdateHistoryEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, RawCancellationDataEntity> RawCancellationDataRepository => _unitOfWork.GetRepository<RawCancellationDataEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, BackorderTrackingEntity> BackorderTrackingRepository => _unitOfWork.GetRepository<BackorderTrackingEntity>();


    #endregion

    #region DigitalCard
    public IGenericRepository<EFCoreMMOnlineSqlProvider, DCHeadEntity> DCHeadRepository => _unitOfWork.GetRepository<DCHeadEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, DCItemEntity> DCItemRepository => _unitOfWork.GetRepository<DCItemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, DCTransactionResultEntity> DCTransactionResultRepository => _unitOfWork.GetRepository<DCTransactionResultEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, DCErrorEntity> DCErrorRepository => _unitOfWork.GetRepository<DCErrorEntity>();

    #endregion

    #region FOM
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMRawDataEntity> FOMRawDataRepository => _unitOfWork.GetRepository<FOMRawDataEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderHeadEntity> FOMOrderHeadRepository => _unitOfWork.GetRepository<FOMOrderHeadEntity>();

    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemEntity> FOMOrderItemRepository => _unitOfWork.GetRepository<FOMOrderItemEntity>();

    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderCustomersEntity> FOMOrderCustomersRepository => _unitOfWork.GetRepository<FOMOrderCustomersEntity>();

    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderAddressEntity> FOMOrderAddressRepository => _unitOfWork.GetRepository<FOMOrderAddressEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMLCAndCargoMatchEntity> FOMLCAndCargoMatchRepository => _unitOfWork.GetRepository<FOMLCAndCargoMatchEntity>();

    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderPaymentEntity> FOMOrderPaymentRepository => _unitOfWork.GetRepository<FOMOrderPaymentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderDiscountEntity> FOMOrderDiscountRepository => _unitOfWork.GetRepository<FOMOrderDiscountEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderChargesEntity> FOMOrderChargesRepository => _unitOfWork.GetRepository<FOMOrderChargesEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemRelationsEntity> FOMOrderItemRelationsRepository => _unitOfWork.GetRepository<FOMOrderItemRelationsEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderReturnItemEntity> FOMOrderReturnItemRepository => _unitOfWork.GetRepository<FOMOrderReturnItemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemReturnStatusEntity> FOMOrderItemReturnStatusRepository => _unitOfWork.GetRepository<FOMOrderItemReturnStatusEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemPriceEntity> FOMOrderItemPriceRepository => _unitOfWork.GetRepository<FOMOrderItemPriceEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemWarrantyEntity> FOMOrderItemWarrantyRepository => _unitOfWork.GetRepository<FOMOrderItemWarrantyEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemDiscountEntity> FOMOrderItemDiscountRepository => _unitOfWork.GetRepository<FOMOrderItemDiscountEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemChargeEntity> FOMOrderItemChargeRepository => _unitOfWork.GetRepository<FOMOrderItemChargeEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemFulFillmentEntity> FOMOrderItemFulFillmentRepository => _unitOfWork.GetRepository<FOMOrderItemFulFillmentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemFulFillmentDeliveryEntity> FOMOrderItemFulFillmentDeliveryRepository => _unitOfWork.GetRepository<FOMOrderItemFulFillmentDeliveryEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderItemFulFillmentDeliveryPackageEntity> FOMOrderItemFulFillmentDeliveryPackageRepository => _unitOfWork.GetRepository<FOMOrderItemFulFillmentDeliveryPackageEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentEntity> FOMOrderShipmentRepository => _unitOfWork.GetRepository<FOMOrderShipmentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentLineEntity> FOMOrderShipmentLineRepository => _unitOfWork.GetRepository<FOMOrderShipmentLineEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderHeadEntity> FOMHistoryOrderHeadRepository => _unitOfWork.GetRepository<FOMHistoryOrderHeadEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderAddressEntity> FOMHistoryOrderAddressRepository => _unitOfWork.GetRepository<FOMHistoryOrderAddressEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderCustomersEntity> FOMHistoryOrderCustomersRepository => _unitOfWork.GetRepository<FOMHistoryOrderCustomersEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderPaymentEntity> FOMHistoryOrderPaymentRepository => _unitOfWork.GetRepository<FOMHistoryOrderPaymentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderDiscountEntity> FOMHistoryOrderDiscountRepository => _unitOfWork.GetRepository<FOMHistoryOrderDiscountEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemEntity> FOMHistoryOrderItemRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderReturnItemEntity> FOMHistoryOrderReturnItemRepository => _unitOfWork.GetRepository<FOMHistoryOrderReturnItemEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemReturnStatusEntity> FOMHistoryOrderItemReturnStatusRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemReturnStatusEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemPriceEntity> FOMHistoryOrderItemPriceRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemPriceEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemWarrantyEntity> FOMHistoryOrderItemWarrantyRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemWarrantyEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemDiscountEntity> FOMHistoryOrderItemDiscountRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemDiscountEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemChargeEntity> FOMHistoryOrderItemChargeRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemChargeEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemFulFillmentDeliveryEntity> FOMHistoryOrderItemFulFillmentDeliveryRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemFulFillmentDeliveryEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemFulFillmentDeliveryPackageEntity> FOMHistoryOrderItemFulFillmentDeliveryPackageRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemFulFillmentDeliveryPackageEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderShipmentEntity> FOMHistoryOrderShipmentRepository => _unitOfWork.GetRepository<FOMHistoryOrderShipmentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderShipmentLineEntity> FOMHistoryOrderShipmentLineRepository => _unitOfWork.GetRepository<FOMHistoryOrderShipmentLineEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, PGSMSEntity> PGSMSRepository => _unitOfWork.GetRepository<PGSMSEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMArchiveCheckEntity> FOMArchiveCheckRepository => _unitOfWork.GetRepository<FOMArchiveCheckEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMTimeStampKeyEntity> FOMTimeStampKeyRepository => _unitOfWork.GetRepository<FOMTimeStampKeyEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMDeliveryMethodEntity> FOMDeliveryMethodRepository => _unitOfWork.GetRepository<FOMDeliveryMethodEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderChargesEntity> FOMHistoryOrderChargesRepository => _unitOfWork.GetRepository<FOMHistoryOrderChargesEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemFulFillmentEntity> FOMHistoryOrderItemFulFillmentRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemFulFillmentEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMHistoryOrderItemRelationsEntity> FOMHistoryOrderItemRelationsRepository => _unitOfWork.GetRepository<FOMHistoryOrderItemRelationsEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMT800SendLogEntity> FOMT800SendLogRepository => _unitOfWork.GetRepository<FOMT800SendLogEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMSellerEntity> FOMSellerRepository => _unitOfWork.GetRepository<FOMSellerEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMPaymentTypeEntity> FOMPaymentTypeRepository => _unitOfWork.GetRepository<FOMPaymentTypeEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentStatusEntity> FOMOrderShipmentStatusRepository => _unitOfWork.GetRepository<FOMOrderShipmentStatusEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMOrderShipmentTypesEntity> FOMOrderShipmentTypesRepository => _unitOfWork.GetRepository<FOMOrderShipmentTypesEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMStatusListEntity> FOMStatusListRepository => _unitOfWork.GetRepository<FOMStatusListEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMSenderMpShippingStatusLogEntity> FOMSenderMpShippingStatusLogRepository => _unitOfWork.GetRepository<FOMSenderMpShippingStatusLogEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMGlobalLastUpdatedDataEntity> FOMGlobalLastUpdatedDataRepository => _unitOfWork.GetRepository<FOMGlobalLastUpdatedDataEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMVCRExcludeEntity> FOMVCRExcludeRepository => _unitOfWork.GetRepository<FOMVCRExcludeEntity>();
    public IGenericRepository<EFCoreMMOnlineSqlProvider, FOMMissingDataOrderEntity> FOMMissingDataOrderRepository => _unitOfWork.GetRepository<FOMMissingDataOrderEntity>();

    #endregion

    #region CancellationProcess
    public IGenericRepository<EFCoreMMOnlineSqlProvider, CancellationProcessReasonsForCancellationEntity> CancellationProcessReasonsForCancellationRepository => _unitOfWork.GetRepository<CancellationProcessReasonsForCancellationEntity>();

    public IGenericRepository<EFCoreMMOnlineSqlProvider, CancellationProcessCancellationRequestEntity> CancellationProcessCancellationRequestRepository => _unitOfWork.GetRepository<CancellationProcessCancellationRequestEntity>();

    #endregion
}

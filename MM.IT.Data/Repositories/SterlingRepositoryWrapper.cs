using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Entities.MMONLINE.Sterling;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;
public class SterlingRepositoryWrapper : ISterlingRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreSterlingSqlProvider> _unitOfWork;

    public SterlingRepositoryWrapper(IUnitOfWork<EFCoreSterlingSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region FOM Generic Repositories

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingGlobalLastUpdatedDataEntity> SterlingGlobalLastUpdatedDataRepository => _unitOfWork.GetRepository<SterlingGlobalLastUpdatedDataEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderHeadEntity> SterlingOrderHeadRepository => _unitOfWork.GetRepository<SterlingOrderHeadEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderAddressEntity> SterlingOrderAddressRepository => _unitOfWork.GetRepository<SterlingOrderAddressEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataStatusListEntity> SterlingMasterDataStatusListRepository => _unitOfWork.GetRepository<SterlingMasterDataStatusListEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingRawDataEntity> SterlingRawDataRepository => _unitOfWork.GetRepository<SterlingRawDataEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataSellerEntity> SterlingMasterDataSellerRepository => _unitOfWork.GetRepository<SterlingMasterDataSellerEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemEntity> SterlingOrderItemRepository => _unitOfWork.GetRepository<SterlingOrderItemEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemHoldsEntity> SterlingOrderItemHoldRepository => _unitOfWork.GetRepository<SterlingOrderItemHoldsEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemPriceEntity> SterlingOrderItemPriceRepository => _unitOfWork.GetRepository<SterlingOrderItemPriceEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemsPriceAdjustmentEntity> SterlingOrderItemsPriceAdjustmentRepository => _unitOfWork.GetRepository<SterlingOrderItemsPriceAdjustmentEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemsShippingCostsDetailEntity> SterlingOrderItemsShippingCostsDetailRepository => _unitOfWork.GetRepository<SterlingOrderItemsShippingCostsDetailEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderItemsStateQuantityEntity> SterlingOrderItemsStateQuantityRepository => _unitOfWork.GetRepository<SterlingOrderItemsStateQuantityEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentEntity> SterlingOrderFulfillmentRepository =>_unitOfWork.GetRepository<SterlingOrderFulfillmentEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentItemEntity> SterlingOrderFulfillmentItemRepository =>_unitOfWork.GetRepository<SterlingOrderFulfillmentItemEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentOrderShipmentEntity> SterlingOrderFulfillmentOrderShipmentRepository =>_unitOfWork.GetRepository<SterlingOrderFulfillmentOrderShipmentEntity>();


    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderNoteEntity> SterlingOrderNoteRepository =>_unitOfWork.GetRepository<SterlingOrderNoteEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderPaymentEntity> SterlingOrderPaymentRepository =>_unitOfWork.GetRepository<SterlingOrderPaymentEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderPaymentPartEntity> SterlingOrderPaymentPartRepository =>_unitOfWork.GetRepository<SterlingOrderPaymentPartEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderRequestedFulfillmentEntity> SterlingOrderRequestedFulfillmentRepository =>_unitOfWork.GetRepository<SterlingOrderRequestedFulfillmentEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderRequestedFulfillmentItemEntity> SterlingOrderRequestedFulfillmentItemRepository =>_unitOfWork.GetRepository<SterlingOrderRequestedFulfillmentItemEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderReturnChargeEntity> SterlingOrderReturnChargeRepository =>_unitOfWork.GetRepository<SterlingOrderReturnChargeEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderReturnEntity> SterlingOrderReturnRepository =>_unitOfWork.GetRepository<SterlingOrderReturnEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderReturnItemEntity> SterlingOrderReturnItemRepository =>_unitOfWork.GetRepository<SterlingOrderReturnItemEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalDetailAmountEntity> SterlingOrderTotalDetailAmountRepository =>_unitOfWork.GetRepository<SterlingOrderTotalDetailAmountEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalDetailEntity> SterlingOrderTotalDetailRepository =>_unitOfWork.GetRepository<SterlingOrderTotalDetailEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalDetailVatEntity> SterlingOrderTotalDetailVatRepository =>_unitOfWork.GetRepository<SterlingOrderTotalDetailVatEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderTotalEntity> SterlingOrderTotalRepository =>_unitOfWork.GetRepository<SterlingOrderTotalEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentOrderShipmentHandlingUnitEntity> SterlingOrderFulfillmentOrderShipmentHandlingUnitRepository => _unitOfWork.GetRepository<SterlingOrderFulfillmentOrderShipmentHandlingUnitEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderFulfillmentOrderShipmentHandlingUnitItemEntity> SterlingOrderFulfillmentOrderShipmentHandlingUnitItemRepository => _unitOfWork.GetRepository<SterlingOrderFulfillmentOrderShipmentHandlingUnitItemEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataSmsContentMatchEntity> SterlingMasterDataSmsContentMatchRepository => _unitOfWork.GetRepository<SterlingMasterDataSmsContentMatchEntity>();

    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingMasterDataPgBasedSmsMatchEntity> SterlingMasterDataPgBasedSmsMatchRepository => _unitOfWork.GetRepository<SterlingMasterDataPgBasedSmsMatchEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingProcessSendToFSPEntity> SterlingProcessSendToFSPRepository => _unitOfWork.GetRepository<SterlingProcessSendToFSPEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingProcessShipmentDataEntity> SterlingProcessShipmentDataRepository => _unitOfWork.GetRepository<SterlingProcessShipmentDataEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingMissingDataOrderEntity> SterlingMissingDataOrderRepository => _unitOfWork.GetRepository<SterlingMissingDataOrderEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderAddressComponentEntity> SterlingOrderAddressComponentRepository => _unitOfWork.GetRepository<SterlingOrderAddressComponentEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingCOSv3TransitionEntity> SterlingCOSv3TransitionRepository => _unitOfWork.GetRepository<SterlingCOSv3TransitionEntity>();
    public IGenericRepository<EFCoreSterlingSqlProvider, SterlingOrderComponentAttributeEntity> SterlingOrderComponentAttributeRepository => _unitOfWork.GetRepository<SterlingOrderComponentAttributeEntity>();


    #endregion
}

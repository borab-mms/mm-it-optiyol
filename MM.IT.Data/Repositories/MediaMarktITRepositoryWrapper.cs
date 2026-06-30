using MM.IT.Data.Entities.MediaMarktIT;
using MM.IT.Data.Entities.MEX;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;

public class MediaMarktITRepositoryWrapper : IMediaMarktITRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreMediaMarktITSqlProvider> _unitOfWork;

    public MediaMarktITRepositoryWrapper(IUnitOfWork<EFCoreMediaMarktITSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSAutomaticContentEntity> SMSAutomaticContentRepository => _unitOfWork.GetRepository<SMSAutomaticContentEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSHeadEntity> SMSHeadRepository => _unitOfWork.GetRepository<SMSHeadEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSHeadNewEntity> SMSHeadNewRepository => _unitOfWork.GetRepository<SMSHeadNewEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSSuccessfullEntity> SMSSuccessfullRepository => _unitOfWork.GetRepository<SMSSuccessfullEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSErrorEntity> SMSErrorRepository => _unitOfWork.GetRepository<SMSErrorEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSChannelEntity> SMSChannelRepository => _unitOfWork.GetRepository<SMSChannelEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSSendingLOGEntity> SMSSendingLOGRepository => _unitOfWork.GetRepository<SMSSendingLOGEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSOrderContentMatchEntity> SMSOrderContentMatchRepository => _unitOfWork.GetRepository<SMSOrderContentMatchEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MMITStoreStockEntity> MMITStoreStockRepository => _unitOfWork.GetRepository<MMITStoreStockEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, EmailContentEntity> EmailContentRepository => _unitOfWork.GetRepository<EmailContentEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MMITT800StoreStockEntity> MMITT800StoreStockRepository => _unitOfWork.GetRepository<MMITT800StoreStockEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MMITT601StoreStockEntity> MMITT601StoreStockRepository => _unitOfWork.GetRepository<MMITT601StoreStockEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, AyenSoftLogEntity> AyenSoftLogRepository => _unitOfWork.GetRepository<AyenSoftLogEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MEXOrderHeadEntity> MEXOrderHeadRepository => _unitOfWork.GetRepository<MEXOrderHeadEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MEXOrderItemEntity> MEXOrderItemRepository => _unitOfWork.GetRepository<MEXOrderItemEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MEXOrderPaymentEntity> MEXOrderPaymentRepository => _unitOfWork.GetRepository<MEXOrderPaymentEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MMProductsEntity> MMProductsRepository => _unitOfWork.GetRepository<MMProductsEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, OIDeliveryMethodEntity> OIDeliveryMethodRepository => _unitOfWork.GetRepository<OIDeliveryMethodEntity>();
    public IGenericRepository<EFCoreMediaMarktITSqlProvider, MMOTPaymentTypeEntity> MMOTPaymentTypeRepository => _unitOfWork.GetRepository<MMOTPaymentTypeEntity>();

}
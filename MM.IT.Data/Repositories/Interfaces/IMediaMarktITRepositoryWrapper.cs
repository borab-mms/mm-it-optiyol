using MM.IT.Data.Entities.MediaMarktIT;
using MM.IT.Data.Entities.MEX;
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
/// DB:MediaMarktIT
/// </summary>
public interface IMediaMarktITRepositoryWrapper
{
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSAutomaticContentEntity> SMSAutomaticContentRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSHeadEntity> SMSHeadRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSHeadNewEntity> SMSHeadNewRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSSuccessfullEntity> SMSSuccessfullRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSErrorEntity> SMSErrorRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSChannelEntity> SMSChannelRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSSendingLOGEntity> SMSSendingLOGRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, SMSOrderContentMatchEntity> SMSOrderContentMatchRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MMITStoreStockEntity> MMITStoreStockRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, EmailContentEntity> EmailContentRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MMITT800StoreStockEntity> MMITT800StoreStockRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MMITT601StoreStockEntity> MMITT601StoreStockRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, AyenSoftLogEntity> AyenSoftLogRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MEXOrderHeadEntity> MEXOrderHeadRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MEXOrderItemEntity> MEXOrderItemRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MEXOrderPaymentEntity> MEXOrderPaymentRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MMProductsEntity> MMProductsRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, OIDeliveryMethodEntity> OIDeliveryMethodRepository { get; }
    IGenericRepository<EFCoreMediaMarktITSqlProvider, MMOTPaymentTypeEntity> MMOTPaymentTypeRepository { get; }
}

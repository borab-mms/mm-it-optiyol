using MM.IT.Data.Entities.MMIT;
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
/// DB:MMIT
/// </summary>
/// 
public interface IMMITRepositoryWrapper
{
    IGenericRepository<EFCoreMMITSqlProvider, VCRInvoiceHeadEntity> VCRInvoiceHeadRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRCustomersEntity> VCRCustomerRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRLogEntity> VCRLogRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRPaymentEntity> VCRPaymentRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRProductEntity> VCRProductRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRSalesDocEntity> VCRSalesDocRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRTotalDiscountEntity> VCRTotalDiscountRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRTotalVatEntity> VCRTotalVatRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, VCRExcludeEntity> VCRExcludeRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, ApiChannelEntity> ApiChannelRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, ApiProjectEntity> ApiProjectRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, ApiAndChannelEntity> ApiAndChannelRepository { get; }
    IGenericRepository<EFCoreMMITSqlProvider, ApiListEntity> ApiListRepository { get; }
}

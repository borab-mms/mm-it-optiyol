using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Providers;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces;

/// <summary>
/// Constructor injection kullanımını basitleştirmek için tüm entity repository tanımlamalarını içerir. 
/// Kullanılmak istenen IGenericRepository'den türeyecek repository bu kısımda tanımlanmalı.
/// DB:FOM
/// </summary>
public interface IFOMRepositoryWrapper
{
    #region FOM Generic Repositories
    IGenericRepository<EFCoreFomSqlProvider, FomOrderHeadEntity> FOMOrderHeadRepository { get; }
    IGenericRepository<EFCoreFomSqlProvider, OrderItemFulFillmentEntity> OrderItemFulFillmentRepository { get; }
    IGenericRepository<EFCoreFomSqlProvider, EKOLOrderHeadEntity> EKOLOrderHeadRepository { get; }
    IGenericRepository<EFCoreFomSqlProvider, FomOrderItemEntity> FomOrderItemRepository { get; }
    IGenericRepository<EFCoreFomSqlProvider, FomArvatoHeadEntity> FomArvatoHeadRepository { get; }
    #endregion
}
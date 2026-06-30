using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;


public class FOMRepositoryWrapper : IFOMRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreFomSqlProvider> _unitOfWork;

    public FOMRepositoryWrapper(IUnitOfWork<EFCoreFomSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region FOM Generic Repositories

    public IGenericRepository<EFCoreFomSqlProvider, FomOrderHeadEntity> FOMOrderHeadRepository => _unitOfWork.GetRepository<FomOrderHeadEntity>();
    public IGenericRepository<EFCoreFomSqlProvider, OrderItemFulFillmentEntity> OrderItemFulFillmentRepository => _unitOfWork.GetRepository<OrderItemFulFillmentEntity>();
    public IGenericRepository<EFCoreFomSqlProvider, EKOLOrderHeadEntity> EKOLOrderHeadRepository => _unitOfWork.GetRepository<EKOLOrderHeadEntity>();
    public IGenericRepository<EFCoreFomSqlProvider, FomOrderItemEntity> FomOrderItemRepository => _unitOfWork.GetRepository<FomOrderItemEntity>();
    public IGenericRepository<EFCoreFomSqlProvider, FomArvatoHeadEntity> FomArvatoHeadRepository => _unitOfWork.GetRepository<FomArvatoHeadEntity>();

    #endregion
}

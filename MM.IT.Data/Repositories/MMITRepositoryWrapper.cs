using MM.IT.Data.Entities.MMIT;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories
{
    public class MMITRepositoryWrapper : IMMITRepositoryWrapper
    {
        private readonly IUnitOfWork<EFCoreMMITSqlProvider> _unitOfWork;
        public MMITRepositoryWrapper(IUnitOfWork<EFCoreMMITSqlProvider> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IGenericRepository<EFCoreMMITSqlProvider, VCRInvoiceHeadEntity> VCRInvoiceHeadRepository => _unitOfWork.GetRepository<VCRInvoiceHeadEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRCustomersEntity> VCRCustomerRepository => _unitOfWork.GetRepository<VCRCustomersEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRLogEntity> VCRLogRepository => _unitOfWork.GetRepository<VCRLogEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRPaymentEntity> VCRPaymentRepository => _unitOfWork.GetRepository<VCRPaymentEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRProductEntity> VCRProductRepository => _unitOfWork.GetRepository<VCRProductEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRSalesDocEntity> VCRSalesDocRepository => _unitOfWork.GetRepository<VCRSalesDocEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRTotalDiscountEntity> VCRTotalDiscountRepository => _unitOfWork.GetRepository<VCRTotalDiscountEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, VCRTotalVatEntity> VCRTotalVatRepository => _unitOfWork.GetRepository<VCRTotalVatEntity>();
        public IGenericRepository<EFCoreMMITSqlProvider, VCRExcludeEntity> VCRExcludeRepository => _unitOfWork.GetRepository<VCRExcludeEntity>();

        public IGenericRepository<EFCoreMMITSqlProvider, ApiChannelEntity> ApiChannelRepository => _unitOfWork.GetRepository<ApiChannelEntity>();
        public IGenericRepository<EFCoreMMITSqlProvider, ApiProjectEntity> ApiProjectRepository => _unitOfWork.GetRepository<ApiProjectEntity>();
        public IGenericRepository<EFCoreMMITSqlProvider, ApiAndChannelEntity> ApiAndChannelRepository => _unitOfWork.GetRepository<ApiAndChannelEntity>();
        public IGenericRepository<EFCoreMMITSqlProvider, ApiListEntity> ApiListRepository => _unitOfWork.GetRepository<ApiListEntity>();
    }
}

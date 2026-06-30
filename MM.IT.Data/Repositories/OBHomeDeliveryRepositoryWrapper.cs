using MM.IT.Data.Entities.OBHomeDelivery;
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
    public class OBHomeDeliveryRepositoryWrapper : IOBHomeDeliveryRepositoryWrapper
    {
        private readonly IUnitOfWork<EFCoreOBHomeDeliverySqlProvider> _unitOfWork;
        public OBHomeDeliveryRepositoryWrapper(IUnitOfWork<EFCoreOBHomeDeliverySqlProvider> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDSalesDocumentHeadEntity> HDSalesDocumentHeadRepository => _unitOfWork.GetRepository<HDSalesDocumentHeadEntity>();

        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, KarturOrderLinkEntity> KarturOrderLinkRepository => _unitOfWork.GetRepository<KarturOrderLinkEntity>();

        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, SFSBorusanHeadEntity> SFSBorusanHeadRepository => _unitOfWork.GetRepository<SFSBorusanHeadEntity>();

        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, SFSBorusanStatusEntity> SFSBorusanStatusRepository => _unitOfWork.GetRepository<SFSBorusanStatusEntity>();

        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, YurticiHeadEntity> YurticiHeadRepository => _unitOfWork.GetRepository<YurticiHeadEntity>();

        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, YurticiLogEntity> YurticiLogRepository => _unitOfWork.GetRepository<YurticiLogEntity>();
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeLocationEntity> HDBarcodeLocationRepository => _unitOfWork.GetRepository<HDBarcodeLocationEntity>();
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeEKOLEntity> HDBarcodeEKOLRepository => _unitOfWork.GetRepository<HDBarcodeEKOLEntity>();
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDSalesDocumentHeadCurrentEntity> HDSalesDocumentHeadCurrentRepository => _unitOfWork.GetRepository<HDSalesDocumentHeadCurrentEntity>();
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDSalesDocumentItemCurrentEntity> HDSalesDocumentItemCurrentRepository => _unitOfWork.GetRepository<HDSalesDocumentItemCurrentEntity>();
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeHeadEntity> HDBarcodeHeadRepository => _unitOfWork.GetRepository<HDBarcodeHeadEntity>();
        public IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeLogEntity> HDBarcodeLogRepository => _unitOfWork.GetRepository<HDBarcodeLogEntity>();
    }
}

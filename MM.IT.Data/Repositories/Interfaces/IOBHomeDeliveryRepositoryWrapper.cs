using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Entities.OBHomeDelivery;
using MM.IT.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces
{
    /// <summary>
    /// Constructor injection kullanımını basitleştirmek için tüm entity repository tanımlamalarını içerir. 
    /// Kullanılmak istenen IGenericRepository'den türeyecek repository bu kısımda tanımlanmalı.
    /// DB:OBHomeDelivery
    /// </summary>
    public interface IOBHomeDeliveryRepositoryWrapper
    {
        #region OBHomeDelivery Generic Repositories
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDSalesDocumentHeadEntity> HDSalesDocumentHeadRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, KarturOrderLinkEntity> KarturOrderLinkRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, SFSBorusanHeadEntity> SFSBorusanHeadRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, SFSBorusanStatusEntity> SFSBorusanStatusRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, YurticiHeadEntity> YurticiHeadRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, YurticiLogEntity> YurticiLogRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeLocationEntity> HDBarcodeLocationRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeEKOLEntity> HDBarcodeEKOLRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDSalesDocumentHeadCurrentEntity> HDSalesDocumentHeadCurrentRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDSalesDocumentItemCurrentEntity> HDSalesDocumentItemCurrentRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeHeadEntity> HDBarcodeHeadRepository { get; }
        IGenericRepository<EFCoreOBHomeDeliverySqlProvider, HDBarcodeLogEntity> HDBarcodeLogRepository { get; }
        #endregion
    }
}

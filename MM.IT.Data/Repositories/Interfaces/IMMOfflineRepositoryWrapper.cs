using MM.IT.Data.Entities.MMOffline;
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
/// DB:MMOffline - WWS
/// </summary>
public interface IMMOfflineRepositoryWrapper
{
    #region Master Data Generic Repositories
    IGenericRepository<EFCoreMMOfflineSqlProvider, WWSSFSPickupReportEntity> WWS_SFSPickup_Repository { get; }

    #endregion
}
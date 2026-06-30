using MM.IT.Data.Entities.MasterData;
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
/// DB:MasterData
/// </summary>
public interface IMasterDataRepositoryWrapper
{
    #region Master Data Generic Repositories

    IGenericRepository<EFCoreMasterDataSqlProvider, DesiEntity> DesiRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataSTRStoreEntity> STRStoreRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTArticleEntity> ARTArticleRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTBrandEntity> ARTBrandRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTDepartmentEntity> ARTDepartmentRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataZCCityEntity> ZCCityRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataZCDistrictEntity> ZCDistrictRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataZCNeighborhoodEntity> ZCNeighborhoodRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataMMProductEntity> MMProductRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTProductGroupEntity> MMProductGroupRepository { get; }
    IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataProductEntity> MasterDataProductRepository { get; }

    #endregion
}

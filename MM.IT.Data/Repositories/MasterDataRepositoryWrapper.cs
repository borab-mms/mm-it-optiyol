using MM.IT.Data.Entities.MasterData;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;


/// <summary>
/// IMasterDataRepositoryWrapper şartlarını barındıran RepositoryWrapper Nesnesi
/// </summary>
public class MasterDataRepositoryWrapper : IMasterDataRepositoryWrapper
{
    private readonly IUnitOfWork<EFCoreMasterDataSqlProvider> _unitOfWork;

    public MasterDataRepositoryWrapper(IUnitOfWork<EFCoreMasterDataSqlProvider> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region Master Data Generic Repositories

    public IGenericRepository<EFCoreMasterDataSqlProvider, DesiEntity> DesiRepository => _unitOfWork.GetRepository<DesiEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataSTRStoreEntity> STRStoreRepository => _unitOfWork.GetRepository<MasterDataSTRStoreEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTArticleEntity> ARTArticleRepository => _unitOfWork.GetRepository<MasterDataARTArticleEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTBrandEntity> ARTBrandRepository => _unitOfWork.GetRepository<MasterDataARTBrandEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTDepartmentEntity> ARTDepartmentRepository => _unitOfWork.GetRepository<MasterDataARTDepartmentEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataZCCityEntity> ZCCityRepository => _unitOfWork.GetRepository<MasterDataZCCityEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataZCDistrictEntity> ZCDistrictRepository => _unitOfWork.GetRepository<MasterDataZCDistrictEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataZCNeighborhoodEntity> ZCNeighborhoodRepository => _unitOfWork.GetRepository<MasterDataZCNeighborhoodEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataMMProductEntity> MMProductRepository => _unitOfWork.GetRepository<MasterDataMMProductEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataARTProductGroupEntity> MMProductGroupRepository => _unitOfWork.GetRepository<MasterDataARTProductGroupEntity>();
    public IGenericRepository<EFCoreMasterDataSqlProvider, MasterDataProductEntity> MasterDataProductRepository => _unitOfWork.GetRepository<MasterDataProductEntity>();

    #endregion
}
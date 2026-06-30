using AutoMapper;
using Microsoft.Extensions.Localization;
using MM.IT.Common.Models.MasterData;
using MM.IT.Common.Resources;
using MM.IT.Data.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.MapperAdapter.Profiles;

/// <summary>
/// MasterDataDB -> AutoMapper için kullanılacak RuleSet'lerin tanımlandığı nesne.
/// </summary>
public class MasterDataAutoMapperProfile : Profile
{
    /// <summary>
    /// Constructor
    /// </summary>
    public MasterDataAutoMapperProfile(IStringLocalizer<SharedResources> stringLocalizer)
    {
        CreateMap<MasterDataSTRStoreEntity, MasterDataSTRStoreModel>().ReverseMap();
    }
}
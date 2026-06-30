using AutoMapper;
using MM.IT.Common.Models.MediaMarkt;
using MM.IT.Data.Entities.MediaMarktIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.MapperAdapter.Profiles
{
    /// <summary>
    /// MediaMartkIT DB -> AutoMapper için kullanılacak RuleSet'lerin tanımlandığı nesne.
    /// </summary>
    public class MMITAutoMapperProfile : Profile
    {
        public MMITAutoMapperProfile()
        {
            CreateMap<MMITStoreStockEntity, StoreStockModel>().ReverseMap(); 

        }
    }
}
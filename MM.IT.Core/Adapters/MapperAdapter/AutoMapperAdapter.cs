using AutoMapper;
using Microsoft.Extensions.Localization;
using MM.IT.Common.Resources;
using MM.IT.Core.Adapters.MapperAdapter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.MapperAdapter;

/// <summary>
/// IMapperAdapter şartlarını barındıran AutoMapperAdapter Nesnesi
/// </summary>
public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;

    public AutoMapperAdapter(IStringLocalizer<SharedResources> stringLocalizer)
    {
        //INFO: Map sırasında kullanılacak kural setlerini tanımlar.
        var mappingConfig = new MapperConfiguration(mc =>
        {
            // mc.AddProfile(new AutoMapperProfile(stringLocalizer));
        });

        _mapper = mappingConfig.CreateMapper();
    }

    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map<TSource, TDestination>(source, destination);
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
    {
        return _mapper.ProjectTo<TDestination>(source, parameters, membersToExpand);
    }
}
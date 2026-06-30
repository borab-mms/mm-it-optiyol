using AutoMapper;
using MM.IT.Core.Adapters.MapperAdapter.Interfaces;
using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.MapperAdapter;

/// <summary>
/// Auto Mapper Extension Methodları İçerir
/// </summary>
public static class MapperAdapterExtensions
{
    private static IMapperAdapter _mapperAdapter;

    /// <summary>
    /// Startup Function -> Constructor Like -> App Startup
    /// </summary>
    /// <param name="mapperAdapter"></param>
    public static void Configure(IMapperAdapter mapperAdapter)
    {
        _mapperAdapter = mapperAdapter;
    }


    /// <summary>
    /// Elde edilmek istenen model tipini ve kaynak datayı alarak, kaynak datayı map kurallarına göre istenen formata çevirir ve döndürür.               
    /// <returns>TDestination: Generic Type -> Map sonucu istenilen model</returns>
    public static TDestination Map<TDestination>(this object source)
    {
        return _mapperAdapter.Map<TDestination>(source);
    }

    /// <summary>
    /// Elde edilmek istenen model tipini ve kaynak datayı alarak, kaynak datayı map kurallarına göre istenen formata çevirir ve döndürür.               
    /// <returns>TDestination: Generic Type -> Map sonucu istenilen model</returns>
    public static TDestination Map<TDestination>(this object source, TDestination destination)
    {
        return _mapperAdapter.Map(source, destination);
    }

    /// <summary>
    /// Dtolardan Entity'lere map edilirken navigation propertyler maplenmesin diye                
    /// <returns>IMappingExpression<TSource, TDestination> --> Automapper custom expression</returns>
    public static IMappingExpression<TSource, TDestination> IgnoreAllNavigationProperties<TSource, TDestination>(
           this IMappingExpression<TSource, TDestination> expression)
    {
        var destinationType = typeof(TDestination);
        foreach (var property in destinationType.GetProperties())
        {
            var descriptor = TypeDescriptor.GetProperties(destinationType)[property.Name];
            var hasDataMemberAttribute = descriptor.Attributes.OfType<ForeignKeyAttribute>().Any();
            if (hasDataMemberAttribute)
                expression.ForMember(property.Name, opt => opt.Ignore());
        }

        return expression;
    }

    /// <summary>
    /// Dtolardan Entity'lere map edilirken navigation propertyler maplenmesin diye                
    /// <returns>IMappingExpression<TSource, TDestination> --> Automapper custom expression</returns>
    public static IMappingExpression<TSource, TDestination> IgnoreAuditableProperties<TSource, TDestination>(
           this IMappingExpression<TSource, TDestination> expression) where TDestination : IAuditableEntity
    {
        expression.ForMember("CreatedBy", opt => opt.Ignore());
        expression.ForMember("CreatedByUser", opt => opt.Ignore());
        expression.ForMember("CreatedDate", opt => opt.Ignore());
        expression.ForMember("UpdatedBy", opt => opt.Ignore());
        expression.ForMember("UpdatedByUser", opt => opt.Ignore());
        expression.ForMember("UpdatedDate", opt => opt.Ignore());

        return expression;
    }

    /// <summary>
    /// Dtolardan Entity'lere map edilirken navigation propertyler maplenmesin diye                
    /// <returns>IMappingExpression<TSource, TDestination> --> Automapper custom expression</returns>
    public static IMappingExpression<TSource, TDestination> IgnoreDeactivableProperties<TSource, TDestination>(
           this IMappingExpression<TSource, TDestination> expression) where TDestination : IDeactivableEntity
    {
        expression.ForMember("IsActive", opt => opt.Ignore());

        return expression;
    }

    /// <summary>
    /// Dtolardan Entity'lere map edilirken navigation propertyler maplenmesin diye                
    /// <returns>IMappingExpression<TSource, TDestination> --> Automapper custom expression</returns>
    public static IMappingExpression<TSource, TDestination> IgnoreSoftDeletableProperties<TSource, TDestination>(
           this IMappingExpression<TSource, TDestination> expression) where TDestination : ISoftDeletableEntity
    {
        expression.ForMember("IsDeleted", opt => opt.Ignore());

        return expression;
    }

    /// <summary>
    /// IQueryable Mapping Extension Methodu
    /// </summary>
    /// <returns>TDestination: Generic Type -> Map sonucu istenilen model</returns>
    public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable query, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
    {
        return _mapperAdapter.ProjectTo<TDestination>(query, parameters, membersToExpand);
    }
}
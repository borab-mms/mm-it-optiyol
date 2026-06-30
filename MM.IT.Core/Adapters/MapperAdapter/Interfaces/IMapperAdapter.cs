using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Core.Adapters.MapperAdapter.Interfaces;

/// <summary>
/// Mapper Adapter Interface Tanımı
/// </summary>
public interface IMapperAdapter
{
    /// <summary>
    /// Elde edilmek istenen model tipini ve kaynak datayı alarak, kaynak datayı map kurallarına göre istenen formata çevirir ve döndürür.        
    /// </summary>
    /// <typeparam name="TDestination">TDestination: Generic Type -> Map sonucu istenilen model tipi</typeparam>
    /// <param name="source">Object: Map için kullanılacak kaynak data</param>
    /// <returns>TDestination: Generic Type -> Map sonucu istenilen model</returns>
    TDestination Map<TDestination>(object source);

    /// <summary>
    /// Elde edilmek istenen model tipini ve kaynak tipini aynı zamanda model ve kaynak datasını da alarak, 
    /// kaynak datayı map kurallarına göre istenen formata çevirir ve döndürür.        
    /// </summary>
    /// <typeparam name="TSource">TSource: Generic Type -> Map'lenmesi için gönderilen kaynak data tipi</typeparam>
    /// <typeparam name="TDestination">TDestination: Generic Type -> Map sonucu istenilen model tipi</typeparam>
    /// <param name="source">TSource: Generic Type -> Map'lenmesi için gönderilen kaynak data</param>
    /// <param name="destination">TDestination: Generic Type -> Map sonucu istenilen model</param>
    /// <returns>TDestination: Generic Type -> Map sonucu istenilen model</returns>
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

    /// <summary>
    /// Elde edilmek istenen model tipini ve kaynak için kullanılacak query bilgisini alarak, 
    /// query sonucu elde edilen kaynak datayı map kurallarına göre istenen formata çevirir ve döndürür.  
    /// Bağlantılı alt nesneleri de map'ler.
    /// </summary>
    /// <typeparam name="TDestination">TDestination: Generic Type -> Map sonucu istenilen model tipi</typeparam>
    /// <param name="source">IQueryable: Map için kullanılacak kaynak data query</param>
    /// <param name="parameters"></param>
    /// <param name="membersToExpand"></param>
    /// <returns>TDestination: Generic Type -> Map sonucu istenilen model</returns>
    IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.Interfaces;

/// <summary>
/// Entity için dummy Interface. Tüm Entity sınıfları bu interface'den türemelidir.
/// Dependency Injection ile Entity nesnesine erişim sağlanmak için kullanıldı.
/// Ayrıca Generic Repository gibi nesnelerde kullanım kontrolü için kullanıldı.
/// </summary>
public interface IEntityUpperCaseIDEntity
{

}

/// <summary>
/// Primary Key barındıran Entity Interface
/// </summary>
/// <typeparam name="TPrimaryKey">Generic Primary Key</typeparam>
public interface IEntityUpperCaseIDEntity<TPrimaryKey> : IEntity
{
    /// <summary>
    /// Object: Primary Key
    /// </summary>
    public TPrimaryKey ID { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.Interfaces;

/// <summary>
/// Tenant bilgisine sahip olması gereken Entity'ler için kullanılan Interface
/// </summary>
public interface IWithTenantEntity
{
    /// <summary>
    /// TenantEntity: Id -> ForeignKey
    /// </summary>
    public int TenantId { get; set; }
}

/// <summary>
/// Tenant bilgisine sahip olabilecek Entity'ler için kullanılan Interface
/// </summary>
public interface IWithNullableTenantEntity
{
    /// <summary>
    /// TenantEntity: Id -> ForeignKey
    /// </summary>
    public int? TenantId { get; set; }
}
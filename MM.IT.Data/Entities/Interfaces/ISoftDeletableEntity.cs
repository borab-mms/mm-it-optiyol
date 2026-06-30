using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.Interfaces;

/// <summary>
/// Soft Silinebilen Entityler için kullanılan Interface
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    /// Silindi Mi Bilgisi
    /// </summary>
    public bool IsDeleted { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.Interfaces;

/// <summary>
/// Audit datası barındıran Entityler için kullanılan Interface
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Entity oluşturulma tarih bilgisini saklar.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Entity oluşturan kişi bilgisini saklar. string: User Primary Key
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// Entity düzenleme tarih bilgisini saklar.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Entity düzenleyen kişi bilgisini saklar. string: User Primary Key
    /// </summary>
    public Guid? UpdatedBy { get; set; }
}
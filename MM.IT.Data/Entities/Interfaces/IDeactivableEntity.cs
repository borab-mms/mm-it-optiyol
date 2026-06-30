using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.Interfaces;

/// <summary>
/// Aktif/Pasif datası barındıran Entityler için kullanılan Interface
/// </summary>
public interface IDeactivableEntity
{
    /// <summary>
    /// Aktif Mi Bilgisi
    /// </summary>
    public bool IsActive { get; set; }
}
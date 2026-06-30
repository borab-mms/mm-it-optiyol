using MM.IT.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Entities.MMOffline.Interfaces;
/// <summary>
/// Entity için dummy Interface. WWS Entity sınıfları bu interface'den türemelidir.
/// </summary>
public interface IWWSEntity : IEntity
{
    /// <summary>
    /// Mağaza SAP CODE Bilgisi
    /// </summary>
    public string SAP_CODE { get; set; }
}

/// <summary>
/// Entity için dummy Interface. WWS Entity sınıfları bu interface'den türemelidir.
/// Son Güncellenme verisini içerir.
/// </summary>
public interface IWWSEntityWithUpdatedDate : IWWSEntity
{
    /// <summary>
    /// Kaydın Güncellenme Tarihi
    /// </summary>
    public DateTime INTEGRATION_UPDATED_DATE { get; set; }
}


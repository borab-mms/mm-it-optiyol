using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Models.Configs;

/// <summary>
/// Db Connection Config Nesnesi -> app.config: ConnectionStrings eşleniği
/// Db Connection ayarları bu nesnede tanımlanır.
/// Bilgilerin değişikliği için app.config içinde değişiklik yapılabilir.
/// </summary>
public class ConnectionConfigModel
{
    /// <summary>
    /// Default Connection String : SQL Server
    /// </summary>
    public string DefaultConnection { get; set; }

    /// <summary>
    /// MMOnline Connection String : SQL Server
    /// </summary>
    public string MMOnlineConnection { get; set; }

    /// <summary>
    /// MediaMarktIT Connection String : SQL Server
    /// </summary>
    public string MediaMarktITConnection { get; set; }

    /// <summary>
    /// OB_FOM Connection String : SQL Server
    /// </summary>
    public string OBFomConnection { get; set; }

    /// <summary>
    /// MM_LOGISTICS Connection String : SQL Server
    /// </summary>
    public string MMLogisticsConnection { get; set; }

    /// <summary>
    /// MasterData Connection String : SQL Server
    /// </summary>
    public string MasterDataConnection { get; set; }

    /// <summary>
    /// MMIT Connection String : SQL Server
    /// </summary>
    public string MMITConnection { get; set; }

    /// <summary>
    /// MMOffline Destination Connection String : SQL Server
    /// </summary>
    public string MMOfflineConnection { get; set; }

    /// <summary>
    /// OBHomeDeliveryConnection Destination Connection String : SQL Server
    /// </summary>
    public string OBHomeDeliveryConnection { get; set; }

    /// <summary>
    /// MMDFS Connection String : SQL Server
    /// </summary>
    public string MMDFSConnection { get; set; }

    /// <summary>
    /// Sterling Connection String : SQL Server
    /// </summary>
    public string SterlingConnection { get; set; }
}

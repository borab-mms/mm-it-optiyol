using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Constants;

/// <summary>
/// Uygulama için kullanılacak Rol Belirleyiciler sabit nesnesi
/// </summary>
public static class RoleConstants
{
    /// <summary>
    /// Administrator Rol Sabiti
    /// </summary>
    public const string Administrator = "Administrator";

    /// <summary>
    /// Modül Yöneticisi Rol Sabiti
    /// </summary>
    public const string ModuleManager = "ModuleManager";


    #region MasterData
    /// <summary>
    /// MasterData Modülü Insan Kaynakları Personeli Rol Sabiti
    /// </summary>
    public const string MasterDataHumanResources = "MasterData.HumanResources";
    #endregion

    #region Store
    /// <summary>
    /// Store Modülü Stok Takibi Çağrı Merkezi Personeli Rol Sabiti
    /// </summary>
    public const string StoreStockInquiryCallCenter = "Store.StockInquiryCallCenter";

    /// <summary>
    /// Store Modülü Stok Takibi Uzman Personeli Rol Sabiti
    /// </summary>
    public const string StoreStockInquiryExpert = "Store.StockInquiryExpert";
    #endregion
}
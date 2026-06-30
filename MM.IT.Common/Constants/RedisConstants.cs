using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Constants;

/// <summary>
/// Cache için Kullanılacak Key'ler Sabit Nesnesi
/// </summary>
public static class RedisConstants
{
    /// <summary>
    /// Tüm Cache Key'lerinin Başına Konan Cache Sabiti.
    /// </summary>
    public const string Prefix = "MM.IT.Cache.";

    /// <summary>
    /// Diller için Kullanılan Cache Sabiti
    /// </summary>
    public const string Languages = "Languages";

    /// <summary>
    /// Tenant'lar için Kullanılan Cache Sabiti
    /// </summary>
    public const string Tenants = "Tenants";

    /// <summary>
    /// Moduller için Kullanlan Cache Sabiti
    /// </summary>
    public const string Modules = "Modules";

    /// <summary>
    /// Actions için Kullanılan Cache Sabiti
    /// </summary>
    public const string Actions = "Actions";

    /// <summary>
    /// Menuler için Kullanılan Cache Sabiti
    /// </summary>
    public const string MenuItems = "MenuItems.{0}";

    /// <summary>
    /// Kullanıcıya Tanımlı Actions için Kullanılan Cache Sabiti
    /// </summary>
    public const string UserActions = "UserActions.{0}";


    #region Logistic Cost
    /// <summary>
    /// Logistic Cost Hesaplamaları için Kullanlan Cache Sabiti
    /// </summary>
    public const string LogisticCostCalcualtion = "LogisticCostCalculation.{0}.{1}";

    /// <summary>
    /// Logistic Cost S2S Hesaplamaları için Kullanlan Cache Sabiti
    /// </summary>
    public const string LogisticCostS2SCalcualtion = "LogisticCostS2SCalculation.{0}.{1}";
    #endregion

    #region Demo Discount
    /// <summary>
    /// Demo Discount Hesaplamaları için Kullanılan Cache Sabiti
    /// </summary>
    public const string DemoDiscountCalculations = "DemoDiscount.Calculations.{0}";

    #endregion
}

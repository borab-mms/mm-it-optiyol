using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Constants;
/// <summary>
/// Uygulama için kullanılacak Sabit Id'lerin tutulacağı Sabit Nesnesi
/// </summary>
public static class PrimaryKeyConstants
{
    #region Tenant
    /// <summary>
    /// Default Tenant Id Sabiti
    /// </summary>
    public const int TenantDefault = 1;
    #endregion

    #region User
    /// <summary>
    /// UserEntity.Id -> Seed User Primary Key
    /// </summary>
    public const string UserSeed = "63a5182f-6f97-4383-819e-51a1f287f3c7";

    /// <summary>
    /// Entegrasyon Kullanıcı Sabiti. Entegrasyon Tarafından Data Oluşturulunca Bu Sabit Kullanılacak.
    /// </summary>
    public const string UserIntegration = "7e5a2e70-3625-4a96-8f02-63b3da48480b";

    /// <summary>
    /// Giriş yapmayan Kullanıcı Sabiti. Anonymous Oluşturulan Data Bu Sabit'i Kullanacak.
    /// </summary>
    public const string UserAnonymous = "25fce967-ef88-4a33-b786-990e76368b24";

    /// <summary>
    /// Eventbus Kullanıcı Sabiti. Event ile Oluşturulan Data Bu Sabit'i Kullanacak.
    /// </summary>
    public const string UserEventBus = "9c233a09-96ca-4d60-94fa-51ba0137ecf6";

    /// <summary>
    /// Identity Server Admin Kullanıcı Sabiti.
    /// </summary>
    public const string UserIdentityServerAdmin = "4d51b53f-0d22-4118-9ac3-6802e0dcd92c";

    /// <summary>
    /// Identity Server - Prod Admin Kullanıcı Sabiti.
    /// </summary>
    public const string UserIdentityServerAdminProduction = "0EADBAB3-E110-460D-84EB-2D4241E8E2EA";

    /// <summary>
    /// EKOLStocs Sabiti.
    /// </summary>
    public const string EKOLStocs = "0EADBAB3-E110-460D-84EB-2D4241E8E2EA";

    #endregion
}

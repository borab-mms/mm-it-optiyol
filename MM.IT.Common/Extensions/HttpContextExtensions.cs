using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MM.IT.Common.Constants;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Common.Extensions;

/// <summary>
/// Http Context Extension Metodları
/// </summary>
public static class HttpContextExtensions
{
    public static TenantModel GetCurrentTenant(this HttpContext httpContext)
    {
        if (httpContext == null || !httpContext.Items.ContainsKey(CommonConstants.TenantKey))
        {
            return null;
        }

        return httpContext.Items[CommonConstants.TenantKey] as TenantModel;
    }
    public static void HandleResponse(this HttpContext httpContext, ContextResultModel input, bool forceResponseAsJson = false)
    {
        var appConfigs = httpContext.RequestServices.GetService(typeof(IOptions<AppConfigModel>)) as IOptions<AppConfigModel>;

        var isAjax = httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        if (isAjax || forceResponseAsJson)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = input.Code;
            httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ApiResultModel
            {
                Code = httpContext.Response.StatusCode,
                Message = input.Message,
                //Provider = appConfigs.Value.Provider.Name,
                Version = appConfigs.Value.Provider.Version
            }));
        }
        else
        {
            var currentTenant = httpContext.GetCurrentTenant();
            input.RedirectUrl = currentTenant != null ? $"{appConfigs.Value.BasePath}/{currentTenant.Identifier}{input.RedirectUrl}" : $"{appConfigs.Value.BasePath}{input.RedirectUrl}";
            httpContext.Response.Redirect(input.RedirectUrl);
        }
    }
    public static UserModel GetCurrentUser(this HttpContext httpContext)
    {
        if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
        {
            return null;
        }

        var user = httpContext.Items[Constants.CommonConstants.UserKey] as UserModel;

        return user;
    }
    public static bool IsInRole(this HttpContext httpContext, string role)
    {
        if (httpContext == null || !httpContext.Items.ContainsKey(CommonConstants.UserKey))
        {
            return false;
        }

        var user = httpContext.GetCurrentUser();

        if (user.Authorization == null || user.Authorization.Roles == null)
        {
            return false;
        }

        return user.Authorization.Roles.Any(p => p == role);
    }
    public static PrimeNodeChannelModel GetPrimeNodeChannel(this HttpContext httpContext)
    {
        return httpContext == null ? (PrimeNodeChannelModel)null : httpContext.Items[(object)"PrimeNode.Channel"] as PrimeNodeChannelModel;
    }
}

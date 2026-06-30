using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MM.IT.Common.Models.Common;
using MM.IT.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MM.IT.Data.Entities.MMIT;
using Microsoft.EntityFrameworkCore;
using MM.IT.Common.Options;
using MM.IT.Core.Services.Auth.Security.Jwt;

namespace MM.IT.Core.Api.Attributes;

public class PrimeNodeAuthorizeAttribute : Attribute, IAuthorizationFilter, IFilterMetadata
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        ILogger<PrimeNodeAuthorizeAttribute> requiredService1 = context.HttpContext.RequestServices.GetRequiredService<ILogger<PrimeNodeAuthorizeAttribute>>();
        ControllerActionDescriptor actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        PrimeNodeAuthorizeAttribute customAttribute = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<PrimeNodeAuthorizeAttribute>();
        if (((IEnumerable<object>)actionDescriptor.MethodInfo.GetCustomAttributes(true)).Any<object>((Func<object, bool>)(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)))) || ((IEnumerable<object>)actionDescriptor.ControllerTypeInfo.GetCustomAttributes(true)).Any<object>((Func<object, bool>)(a => a.GetType().Equals(typeof(AllowAnonymousAttribute)))) || customAttribute == null)
            return;
        string header = (string)context.HttpContext.Request.Headers["Authorization"];
        if (string.IsNullOrWhiteSpace(header))
        {
            string message = "Authorization header is missing. Please include a valid Authorization header in your request.";
            AuthorizationFilterContext authorizationFilterContext = context;
            ServiceResultModel serviceResultModel = new ServiceResultModel();
            serviceResultModel.Code = 401;
            serviceResultModel.Message = message;
            JsonResult jsonResult = new JsonResult((object)serviceResultModel);
            authorizationFilterContext.Result = (IActionResult)jsonResult;
            requiredService1.LogWarning(message);
        }
        else
        {
            string token = header.Replace("Bearer", "").Trim();
            if (string.IsNullOrWhiteSpace(token))
            {
                string message = "Bearer token is missing or invalid. Ensure the Authorization header contains a valid token.";
                AuthorizationFilterContext authorizationFilterContext = context;
                ServiceResultModel serviceResultModel = new ServiceResultModel();
                serviceResultModel.Code = 401;
                serviceResultModel.Message = message;
                JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                authorizationFilterContext.Result = (IActionResult)jsonResult;
                requiredService1.LogWarning(message);
            }
            else
            {
                ClaimsPrincipal principal = this.GetPrincipal(token, context);
                if (principal == null)
                {
                    string message = "Invalid token. The provided token could not be validated.";
                    AuthorizationFilterContext authorizationFilterContext = context;
                    ServiceResultModel serviceResultModel = new ServiceResultModel();
                    serviceResultModel.Code = 401;
                    serviceResultModel.Message = message;
                    JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                    authorizationFilterContext.Result = (IActionResult)jsonResult;
                    requiredService1.LogWarning(message);
                }
                else
                {
                    IMMITRepositoryWrapper requiredService2 = context.HttpContext.RequestServices.GetRequiredService<IMMITRepositoryWrapper>();
                    string username = principal.Identity.Name;
                    ApiChannelEntity channel = requiredService2.ApiChannelRepository.GetQuery().AsNoTracking<ApiChannelEntity>().FirstOrDefault<ApiChannelEntity>((Expression<Func<ApiChannelEntity, bool>>)(a => a.UserName.Equals(username)));
                    if (channel == null)
                    {
                        string message = "Unauthorized access. The user does not exist or is not associated with a valid channel.";
                        AuthorizationFilterContext authorizationFilterContext = context;
                        ServiceResultModel serviceResultModel = new ServiceResultModel();
                        serviceResultModel.Code = 403;
                        serviceResultModel.Message = message;
                        JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                        authorizationFilterContext.Result = (IActionResult)jsonResult;
                        requiredService1.LogWarning(message);
                    }
                    else if (!channel.IsActive)
                    {
                        string message = "Access denied. The user’s channel is inactive.";
                        AuthorizationFilterContext authorizationFilterContext = context;
                        ServiceResultModel serviceResultModel = new ServiceResultModel();
                        serviceResultModel.Code = 403;
                        serviceResultModel.Message = message;
                        JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                        authorizationFilterContext.Result = (IActionResult)jsonResult;
                        requiredService1.LogWarning(message);
                    }
                    else
                    {
                        context.HttpContext.Items.TryAdd<object, object>((object)"PrimeNode.Channel", (object)new PrimeNodeChannelModel()
                        {
                            Id = channel.Id,
                            ChannelName = channel.ChannelName,
                            EmailAddress = channel.EmailAddress,
                            UserName = channel.UserName
                        });
                        PrimeNodeCoreApiApplicationOptions primeNodeCoreApiApplicationOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<PrimeNodeCoreApiApplicationOptions>>().Value;
                        MethodInfo methodInfo = actionDescriptor.MethodInfo;
                        string actionNamespace = $"{methodInfo.DeclaringType.Namespace}.{methodInfo.DeclaringType.Name}.{methodInfo.Name}";
                        ApiListEntity actionDb = requiredService2.ApiListRepository.GetQuery().AsNoTracking<ApiListEntity>().FirstOrDefault<ApiListEntity>((Expression<Func<ApiListEntity, bool>>)(p => p.NamespacePath == actionNamespace && p.ProjectId == primeNodeCoreApiApplicationOptions.ProjectId));
                        if (actionDb == null)
                        {
                            string message = "The requested API action is not registered in the system.";
                            AuthorizationFilterContext authorizationFilterContext = context;
                            ServiceResultModel serviceResultModel = new ServiceResultModel();
                            serviceResultModel.Code = 404;
                            serviceResultModel.Message = message;
                            JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                            authorizationFilterContext.Result = (IActionResult)jsonResult;
                            requiredService1.LogWarning(message);
                        }
                        else if (!actionDb.IsActive)
                        {
                            string message = "The requested API action is currently inactive.";
                            AuthorizationFilterContext authorizationFilterContext = context;
                            ServiceResultModel serviceResultModel = new ServiceResultModel();
                            serviceResultModel.Code = 403;
                            serviceResultModel.Message = message;
                            JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                            authorizationFilterContext.Result = (IActionResult)jsonResult;
                            requiredService1.LogWarning(message);
                        }
                        else
                        {
                            ApiAndChannelEntity andChannelEntity = requiredService2.ApiAndChannelRepository.GetQuery().AsNoTracking<ApiAndChannelEntity>().FirstOrDefault<ApiAndChannelEntity>((Expression<Func<ApiAndChannelEntity, bool>>)(p => p.ApiId == actionDb.Id && p.ChannelId == channel.Id));
                            if (andChannelEntity == null)
                            {
                                string message = "Access to the requested API is not allowed for the current channel.";
                                AuthorizationFilterContext authorizationFilterContext = context;
                                ServiceResultModel serviceResultModel = new ServiceResultModel();
                                serviceResultModel.Code = 403;
                                serviceResultModel.Message = message;
                                JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                                authorizationFilterContext.Result = (IActionResult)jsonResult;
                                requiredService1.LogWarning(message);
                            }
                            else
                            {
                                if (andChannelEntity.IsActive)
                                    return;
                                string message = "The requested API-channel association is inactive.";
                                AuthorizationFilterContext authorizationFilterContext = context;
                                ServiceResultModel serviceResultModel = new ServiceResultModel();
                                serviceResultModel.Code = 403;
                                serviceResultModel.Message = message;
                                JsonResult jsonResult = new JsonResult((object)serviceResultModel);
                                authorizationFilterContext.Result = (IActionResult)jsonResult;
                                requiredService1.LogWarning(message);
                            }
                        }
                    }
                }
            }
        }
    }
    private ClaimsPrincipal GetPrincipal(string token, AuthorizationFilterContext context)
    {
        try
        {
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            if (!(securityTokenHandler.ReadToken(token) is JwtSecurityToken))
                return (ClaimsPrincipal)null;
            TokenOptionsModel internalTokenOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<TokenOptionsModel>>().Value;
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = internalTokenOptions.Issuer,
                ValidAudience = internalTokenOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = (SecurityKey)new SymmetricSecurityKey(Encoding.UTF8.GetBytes(internalTokenOptions.SecurityKey))
            };
            return securityTokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);
        }
        catch
        {
            return (ClaimsPrincipal)null;
        }
    }
}
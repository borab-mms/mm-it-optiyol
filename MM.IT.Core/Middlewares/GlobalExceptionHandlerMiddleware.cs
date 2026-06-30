using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Common.Extensions;
using Microsoft.Extensions.Hosting;

namespace MM.IT.Core.Middlewares;

/// <summary>
/// Net Core için kullanılan Global Exception Handler Middleware nesnesi
/// </summary>
public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly IOptions<AppConfigModel> _options;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger,
        IStringLocalizer<SharedResources> stringLocalizer,
        IOptions<AppConfigModel> options,
        IWebHostEnvironment environment)
    {
        _logger = logger;
        _stringLocalizer = stringLocalizer;
        _options = options;
        _environment = environment;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError("Something went wrong: {@Exception}", exception);

            context.HandleResponse(new ContextResultModel
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = _environment.IsProduction() ?
                    _stringLocalizer["Global.InternalServerError"] :
                    exception.Message,
                RedirectUrl = "/error?status=500"
            }, forceResponseAsJson: _options.Value.IsAPI);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Common;
using MM.IT.Common.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Common.Extensions;
using MM.IT.Core.Attributes;

namespace MM.IT.Core.Middlewares;
/// <summary>
/// Environment Validation Middleware nesnesi: Her request için route içerisinde tenant bilgisi aranır var ise 
/// Action'lara ilgili attr eklendiğinde talep edilen environment içinde değil ise işlemi iptal edip response döner.
/// </summary>
public class EnvironmentHandlerMiddleware : IMiddleware
{
    private readonly IOptions<AppConfigModel> _options;
    private readonly IWebHostEnvironment _env;

    public EnvironmentHandlerMiddleware(IOptions<AppConfigModel> options,
        IWebHostEnvironment env)
    {
        _options = options;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpointFeature = context.Features.Get<IEndpointFeature>();

        if (endpointFeature != null &&
            endpointFeature.Endpoint != null &&
            endpointFeature.Endpoint.Metadata.Any(p => p is EnvironmentValidationAttribute))
        {
            var environmentAttr = endpointFeature.Endpoint.Metadata.GetMetadata<EnvironmentValidationAttribute>();

            var excludedEnvironments = environmentAttr.Exclude?
                .Split(",")
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .ToArray();

            if (excludedEnvironments.Any() && excludedEnvironments.Contains(_env.EnvironmentName))
            {
                context.HandleResponse(new ContextResultModel
                {
                    Code = StatusCodes.Status404NotFound,
                    //TODO: Resource
                    Message = $"This Action Not Active in Current Environment.",
                    RedirectUrl = $"/error?status={StatusCodes.Status405MethodNotAllowed}"
                }, forceResponseAsJson: _options.Value.IsAPI);

                return;
            }

            var includedEnvironments = environmentAttr.Include?
                .Split(",")
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Trim())
                .ToArray();

            if (includedEnvironments.Any() && !includedEnvironments.Contains(_env.EnvironmentName))
            {
                context.HandleResponse(new ContextResultModel
                {
                    Code = StatusCodes.Status404NotFound,
                    //TODO: Resource
                    Message = $"This Action Not Active in Current Environment.",
                    RedirectUrl = $"/error?status={StatusCodes.Status405MethodNotAllowed}"
                }, forceResponseAsJson: _options.Value.IsAPI);

                return;
            }

            if (!excludedEnvironments.Any() && !includedEnvironments.Any())
            {
                context.HandleResponse(new ContextResultModel
                {
                    Code = StatusCodes.Status404NotFound,
                    //TODO: Resource
                    Message = $"This Action Not Active in Current Environment.",
                    RedirectUrl = $"/error?status={StatusCodes.Status405MethodNotAllowed}"
                }, forceResponseAsJson: _options.Value.IsAPI);

                return;
            }
        }

        await next(context);
    }
}

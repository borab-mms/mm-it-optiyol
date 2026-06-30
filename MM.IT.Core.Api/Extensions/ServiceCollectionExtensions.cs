using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using MM.IT.Common.Constants;
using MM.IT.Common.Models.Base.Interfaces;
using MM.IT.Common.Resources;
using MM.IT.Core.Converters;
using MM.IT.Core.Filters;
using MM.IT.Core.Services.Auth;
using MM.IT.Core.Services.Auth.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using FluentValidation.AspNetCore;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using MM.IT.Core.Extensions;
using MM.IT.Core.Services.Auth.Security.Jwt;
using MM.IT.Core.Services.EKOLStock.Interfaces;
using MM.IT.Core.Services.EKOLStock;
using MM.IT.Core.Services.MediaMarkt.Interfaces;
using MM.IT.Core.Services.MediaMarkt;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using MM.IT.Core.Services.MarketPlace;
using MM.IT.Common.Helpers.HttpClientHelper.Interfaces;
using MM.IT.Common.Helpers.HttpClientHelper;
using MM.IT.Core.Services.MMCustomerInfo.Interfaces;
using MM.IT.Core.Services.MMCustomerInfo;
using MM.IT.Core.Services.Interfaces;
using MM.IT.Core.Services.DigitalCard.Interfaces;
using MM.IT.Core.Services.DigitalCard;
using MM.IT.Core.Services.SMS.Interfaces;
using MM.IT.Core.Services.SMS;
//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using MM.IT.Core.Services.PIM.Interfaces;
using MM.IT.Core.Services.PIM;
using MM.Optiyol.Api.Services.Optiyol.Interfaces;
using MM.Optiyol.Api.Services.Optiyol;
using MM.IT.Common.Options;
using MM.IT.Core.Adapters.IntegrationAdapter.Interfaces;
using MM.IT.Core.Adapters.IntegrationAdapter;

namespace MM.IT.Core.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseDefaultApiServiceCollection(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.UseBaseServiceCollection(configuration, environment);

        services.ConfigureConfigs(configuration);

        services.ConfigureIntegrationAdapters();

        services.ConfigureBusinessServices();

        services.AddEndpointsApiExplorer();

        services.ConfigureSwagger(configuration);

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.AddControllers(options => options.Filters.Add<ValidateModelFilter>())
                .AddDataAnnotationsLocalization();

        services.AddFluentValidationAutoValidation(p =>
        {
            p.DisableDataAnnotationsValidation = true;
        }).AddFluentValidationClientsideAdapters()
        .AddFluentValidationRulesToSwagger();

        services.AddValidatorsFromAssemblyContaining<IPmModelValidator>();

        services.ConfigureEventBus(configuration);

        services.ConfigureAuthentication(configuration);

        services.ConfigureHealthCheck(configuration, environment);
        services.ConfigureHttpClients(configuration);
        services.AddLogging(buider => buider.AddFile(configuration.GetSection("Logging")));

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });


        return services;
    }

    public static IServiceCollection ConfigureConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        //INFO: Config tanımları MM.Platform.Common.Models.Configs ve app.config içerisinde yapılıp bu kısımda eklenir.
        services.Configure<ConnectionConfigModel>(configuration.GetSection("ConnectionStrings"));
        services.Configure<AppConfigModel>(configuration.GetSection("AppConfig"));
        services.Configure<RedisConfigModel>(configuration.GetSection("Redis"));
        services.Configure<HttpClientConfigModel>(configuration.GetSection("HttpClientConnections"));
        services.Configure<List<Users>>(configuration.GetSection("ApplicationSettings:Users"));
        services.Configure<ApplicationSettingModel>(configuration.GetSection("ApplicationSettings"));
        services.Configure<PrimeNodeCoreApiApplicationOptions>(configuration.GetSection("PrimeNodeCoreApiApplicationOptions"));
        services.Configure<TokenOptionsModel>(configuration.GetSection("ApplicationSettings:TokenOptions"));

        return services;
    }
    public static IServiceCollection ConfigureBusinessServices(this IServiceCollection services)
    {

        //INFO: Business Service tanımları MM.IT.Core.Services içerisinde yapılıp bu kısımda eklenir.
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddScoped<IMediaMarktService, MediaMarktService>();
        services.AddScoped<IMarketPlaceService, MarketPlaceService>();
        services.AddScoped<IMMCustomerInfoService, MMCustomerInfoService>();
        services.AddScoped<IMMGlobalAuthService, MMGlobalAuthService>();
        services.AddScoped<IESBService, ESBService>();
        services.AddScoped<IDigitalCardService, DigitalCardService>();
        services.AddScoped<IEpayClientService, EpayClientService>();
        services.AddScoped<ISMSSenderService, SMSSenderService>();
        services.AddScoped<IMMITService, MMITService>();
        services.AddScoped<IOptiyolService, OptiyolService>();
        services.AddTransient<ISterlingOrderClientService, SterlingOrderClientService>();


        //INFO: Constructor injection kullanımını basitleştirmek için tüm Business Service tanımlamalarını içerir. Kullanılmak istenen servis IServiceWrapper içirisinde tanımlanmalı.
        services.AddScoped<IServiceWrapper, ServiceWrapper>();

        return services;
    }
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var providerConfigs = configuration.GetSection("AppConfig:Provider").Get<ProviderConfigModel>();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = $"MMIT Api {Environments.Development}", Version = "v1" });
            options.DocInclusionPredicate((docName, description) => true);
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "JWT Authorization eader using the Bearer Scheme : (Example 'Bearer token')",
                Name = "Authorization",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            },
                        },
                        Array.Empty<string>()
                    }
                });


        });

        services.AddFluentValidationRulesToSwagger();

        return services;
    }
    public static IServiceCollection ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionConfigModel>();
        //var rabbitMQConfigs = configuration.GetSection("RabbitMQ").Get<RabbitMQConfigModel>();
        var redisConfigs = configuration.GetSection("Redis").Get<RedisConfigModel>();
        //var authenticationConfig = configuration.GetSection("Authentication").Get<AuthenticationConfigModel>();
        var appConfig = configuration.GetSection("AppConfig").Get<AppConfigModel>();



        return services;

    }
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection("AppConfig").Get<AppConfigModel>();

        return services;
    }
    public static IServiceCollection ConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }
    public static IServiceCollection ConfigureIntegrationAdapters(this IServiceCollection services)
    {
        services.AddScoped<IOptiyolIntegrationAdapter, OptiyolIntegrationAdapter>();
        //INFO: IntegrationAdapter tanımları MM.IT.Core.Adapters.IntegrationAdapter içerisinde yapılıp bu kısımda eklenir.
        return services;
    }
    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<ICrossNetworkClientService, CrossNetworkClientService>();
        services.AddHttpClient<ISterlingCosV1OrderClientService, SterlingCosV1OrderClientService>();
        services.AddHttpClient<IAyenSoftClientService, AyenSoftClientService>();

        return services;
    }
}
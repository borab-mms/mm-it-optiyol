using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using MM.IT.Common.Constants;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Options;
using MM.IT.Core.Api.Attributes;
using MM.IT.Core.Services.Base.Interfaces;
using MM.IT.Data.Entities.MMIT;
using MM.IT.Data.Providers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MM.IT.Core.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDefaultApiApplicationBuilder(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment environment, string defaultCorsPolicy = CorsConstants.OnlyMM)
    {
        PrimeNodeCoreApiApplicationOptions applicationOptions = app.ApplicationServices.GetRequiredService<IOptions<PrimeNodeCoreApiApplicationOptions>>().Value;
        app.UsePathBase(applicationOptions.DeploymentOptions.BasePath);
        app.UseHttpsRedirection();
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
        }

        app.UseSwaggerWithOptions(configuration);

        app.UseRouting();

        app.UseLocalization(configuration);

#if (DEBUG)
        defaultCorsPolicy = CorsConstants.AllowAll;
#endif

        app.UseCors(defaultCorsPolicy);

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

        });
        if (applicationOptions.ActionGeneratorEnabled)
            app.GenerateActions();
        return app;
    }
    public static IApplicationBuilder UseSwaggerWithOptions(this IApplicationBuilder app, IConfiguration configuration)
    {
        var providerConfigs = configuration.GetSection("AppConfig:Provider").Get<ProviderConfigModel>();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", providerConfigs.Name);
            c.RoutePrefix = "api/swagger";
            c.DisplayRequestDuration();
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        });
        return app;
    }
    public static IApplicationBuilder UseLocalization(this IApplicationBuilder app, IConfiguration configuration)
    {
        //var localizationConfigs = configuration.GetSection("AppConfig:Localization").Get<LocalizationConfigModel>();
        //var supportedCultures = localizationConfigs.SupportedCultures.Select(p => new CultureInfo(p)).ToList();
        //var options = new RequestLocalizationOptions()
        //{
        //    DefaultRequestCulture = new RequestCulture(localizationConfigs.DefaultCulture, localizationConfigs.DefaultCulture),
        //    SupportedCultures = supportedCultures,
        //    SupportedUICultures = supportedCultures,
        //    RequestCultureProviders = new List<IRequestCultureProvider>() {
        //            new AcceptLanguageHeaderRequestCultureProvider(),
        //        }
        //};

        //app.UseRequestLocalization(options);

        //app.UseMiddleware<LanguageHandlerMiddleware>();

        return app;
    }
    private static IApplicationBuilder GenerateActions(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        Assembly entryAssembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Entry assembly is null.");
        var applicationOptions = serviceProvider.GetRequiredService<IOptions<PrimeNodeCoreApiApplicationOptions>>().Value;
        var dataProvider = serviceProvider.GetRequiredService<EFCoreMMITSqlProvider>();

        string assemblyName = entryAssembly.GetName().Name ?? "UnknownAssembly";
        string basePath = string.IsNullOrEmpty(applicationOptions.DeploymentOptions.BasePath)
            ? "/" + applicationOptions.ProjectName.ToLowerInvariant()
            : applicationOptions.DeploymentOptions.BasePath;

        var allActionNamespaces = new List<string>();
        var existingApis = dataProvider.ApiLists
            .Where(p => p.ProjectId == applicationOptions.ProjectId && p.AssemblyName == assemblyName)
            .ToList();

        try
        {
            dataProvider.Database.BeginTransaction();

            var controllerTypes = entryAssembly.GetTypes()
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var controller in controllerTypes)
            {
                var controllerRoute = controller.GetCustomAttribute<RouteAttribute>()?.Template ?? "";

                var methods = controller.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(m => m.IsPublic && m.GetCustomAttribute<PrimeNodeActionAttribute>() != null && !m.IsDefined(typeof(NonActionAttribute)));

                foreach (var method in methods)
                {
                    string actionNamespace = $"{controller.Namespace}.{controller.Name}.{method.Name}";
                    allActionNamespaces.Add(actionNamespace);

                    var httpAttributes = method.GetCustomAttributes<HttpMethodAttribute>().ToList();
                    var methodRoute = httpAttributes.FirstOrDefault()?.Template ?? method.GetCustomAttribute<RouteAttribute>()?.Template ?? "";

                    string version = ResolveApiVersion(method, controller, applicationOptions.Version.ToString());

                    string combinedRoute = CombineRoutes(controllerRoute, methodRoute);
                    string finalRoute = ReplaceRouteTokens(combinedRoute, new Dictionary<string, string>
                {
                    { "version", version },
                    { "controller", controller.Name.Replace("Controller", "") },
                    { "action", method.Name }
                });

                    string fullApiUrl = (applicationOptions.DeploymentOptions.Host + basePath + "/" + finalRoute).ToLowerInvariant().TrimEnd('/');

                    var apiEntity = existingApis.SingleOrDefault(p => p.NamespacePath == actionNamespace) ?? new ApiListEntity
                    {
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "PrimeNode",
                        ProjectId = applicationOptions.ProjectId
                    };

                    apiEntity.AssemblyName = assemblyName;
                    apiEntity.NamespacePath = actionNamespace;
                    apiEntity.ApiName = $"{controller.Name.Replace("Controller", "")}.{method.Name}";
                    apiEntity.ApiLink = fullApiUrl;
                    apiEntity.IsActive = true;
                    apiEntity.UpdatedDate = DateTime.UtcNow;
                    apiEntity.UpdatedBy = "PrimeNode";

                    if (apiEntity.Id == 0)
                    {
                        dataProvider.ApiLists.Add(apiEntity);
                        dataProvider.SaveChanges();

                        //add demo user
                        var apiAndChannelEntity = new ApiAndChannelEntity()
                        {
                            CreatedDate = DateTime.UtcNow,
                            CreatedBy = "PrimeNode"
                        };
                        apiAndChannelEntity.ChannelId = 1;
                        apiAndChannelEntity.ApiId = apiEntity.Id;
                        apiAndChannelEntity.IsActive = true;

                        dataProvider.ApiAndChannels.Add(apiAndChannelEntity);
                    }
                    else
                        dataProvider.ApiLists.Update(apiEntity);
                }
            }

            // Deaktivasyon
            var toDeactivate = existingApis
                .Where(p => !allActionNamespaces.Contains(p.NamespacePath))
                .ToList();

            foreach (var entity in toDeactivate)
            {
                entity.IsActive = false;
                entity.UpdatedDate = DateTime.UtcNow;
                entity.UpdatedBy = "PrimeNode";
                dataProvider.ApiLists.Update(entity);
            }
            dataProvider.SaveChanges();
            dataProvider.Database.CommitTransaction();
        }
        catch
        {
            dataProvider.Database.RollbackTransaction();
            throw;
        }

        return app;

        // ✨ Yardımcı Fonksiyonlar:

        static string ResolveApiVersion(MethodInfo method, Type controller, string defaultVersion)
        {
            var methodApiVersion = method.GetCustomAttribute<ApiVersionAttribute>();
            if (methodApiVersion?.Versions?.Any() == true)
                return methodApiVersion.Versions.OrderByDescending(v => v.MajorVersion).ThenByDescending(v => v.MinorVersion)
                    .Select(v => $"{v.MajorVersion}.{v.MinorVersion}").First();

            var controllerApiVersion = controller.GetCustomAttribute<ApiVersionAttribute>();
            if (controllerApiVersion?.Versions?.Any() == true)
                return controllerApiVersion.Versions.OrderByDescending(v => v.MajorVersion).ThenByDescending(v => v.MinorVersion)
                    .Select(v => $"{v.MajorVersion}.{v.MinorVersion}").First();

            return defaultVersion;
        }

        static string CombineRoutes(string controllerRoute, string actionRoute)
        {
            var controllerPart = controllerRoute?.Trim('/') ?? "";
            var actionPart = actionRoute?.Trim('/') ?? "";

            if (string.IsNullOrWhiteSpace(controllerPart))
                return actionPart;
            if (string.IsNullOrWhiteSpace(actionPart))
                return controllerPart;

            return $"{controllerPart}/{actionPart}";
        }

        static string ReplaceRouteTokens(string route, IDictionary<string, string> tokens)
        {
            if (string.IsNullOrEmpty(route)) return string.Empty;

            return new Regex(@"{(\w+)(:[^}]*)?}")
                .Replace(route, match =>
                {
                    var key = match.Groups[1].Value;
                    return tokens.TryGetValue(key, out var value) ? value : match.Value;
                })
                .Replace("//", "/")
                .Trim('/');
        }
    }

}
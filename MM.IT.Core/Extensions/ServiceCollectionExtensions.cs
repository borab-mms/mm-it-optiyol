using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MM.IT.Common.Models.Configs;
using MM.IT.Core.Adapters.RedisAdaptor.Interfaces;
using MM.IT.Core.Adapters.RedisAdaptor;
using MM.IT.Core.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MM.IT.Data.Providers;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.Repositories;
using MM.IT.Data.UnitOfWork.Interfaces;
using MM.IT.Data.UnitOfWork;
using MM.IT.Common.Helpers.EmailHelper;
using MM.IT.Common.Helpers.SmsHelper.Interfaces;
using MM.IT.Common.Helpers.SmsHelper;
using Microsoft.AspNetCore.Hosting;
using StackExchange.Redis;
using MM.IT.Core.Adapters.CacheAdapter.Interfaces;
using MM.IT.Core.Adapters.CacheAdapter;
using MM.IT.Core.Adapters.MapperAdapter.Interfaces;
using MM.IT.Core.Adapters.MapperAdapter;
using MM.IT.Common.Helpers.HttpClientHelper.Interfaces;
using MM.IT.Common.Helpers.HttpClientHelper;
using MM.IT.Core.Services.EKOLStock.Interfaces;
using MM.IT.Core.Services.EKOLStock;
using MM.IT.Core.Services.MarketPlace.Interfaces;
using MM.IT.Core.Services.MarketPlace;
using Microsoft.EntityFrameworkCore;
using MM.IT.Data;

namespace MM.IT.Core.Extensions;

/// <summary>
/// IServiceCollection öğesinden türetilen nesnelerin extension metodlarını içerir.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseBaseServiceCollection(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.ConfigureBaseMiddlewares();
        services.ConfigureDatabaseProviders();
        services.ConfigureSession();
        services.ConfigureRedisDistributed(configuration);
        services.ConfigureHelpers();
        services.ConfigureMapper();
        services.ConfigureDistributedCache(configuration, environment);
        services.ConfigureHttpClients(configuration);

        return services;
    }

    public static IServiceCollection ConfigureDistributedCache(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var redisConfigs = configuration.GetSection("Redis").Get<RedisConfigModel>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = ConfigurationOptions.Parse($"{redisConfigs.Host}:{redisConfigs.Port},password={redisConfigs.Password}");
            options.ConfigurationOptions.AbortOnConnectFail = false;
            options.ConfigurationOptions.SyncTimeout = 10000;
            options.InstanceName = $"{redisConfigs.Name}{environment.EnvironmentName}.";
        });

        services.AddSingleton<IRedisDatabaseProvider, RedisDatabaseProvider>();
        services.AddSingleton<IDistributedCacheAdapter, RedisDistributedCacheAdapter>();

        return services;
    }
    public static IServiceCollection ConfigureMapper(this IServiceCollection services)
    {
        //INFO: Mapper Profile tanımları MM.Platform.Core.Adapters.MapperAdapter.AutoMapperProfile içerisinde yapılır.
        services.AddSingleton<IMapperAdapter, AutoMapperAdapter>();

        return services;
    }
    public static IServiceCollection ConfigureBaseMiddlewares(this IServiceCollection services)
    {
        //INFO: Middleware tanımları MM.IT.Core.Middlewares içerisinde yapılıp bu kısımda eklenir.
        services.AddScoped<GlobalExceptionHandlerMiddleware>();
        services.AddScoped<EnvironmentHandlerMiddleware>();

        return services;
    }
    public static IServiceCollection ConfigureDatabaseProviders(this IServiceCollection services)
    {

        //INFO: EntityFramework dışında bir provider kullanıldığı takdirde: services.AddScoped<IDataProvider, CurrentProvider>(); *CurrentProvider:Eklenmek istenen provider.
        services.AddDbContext<EFCoreMMOnlineSqlProvider>();
        services.AddDbContext<EFCoreMediaMarktITSqlProvider>();
        services.AddDbContext<EFCoreFomSqlProvider>();
        services.AddDbContext<EFCoreMMLogisticsSqlProvider>();
        services.AddDbContext<EFCoreMasterDataSqlProvider>();
        services.AddDbContext<EFCoreMMITSqlProvider>();
        services.AddDbContext<EFCoreMMOfflineSqlProvider>();
        services.AddDbContext<EFCoreOBHomeDeliverySqlProvider>();
        services.AddDbContext<EfCoreMMDFSSqlProvider>();
        services.AddDbContext<EFCoreSterlingSqlProvider>();

        services.AddScoped(typeof(IGenericRepository<,>), typeof(EFCoreSqlGenericRepository<,>));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(EFCoreSqlUnitOfWork<>));

        //INFO: Constructor injection kullanımını basitleştirmek için tüm entity repository tanımlamalarını içerir. Kullanılmak istenen repository IRepositoryWrapper içirisinde tanımlanmalı.
        services.AddScoped<IMMOnlineRepositoryWrapper, MMOnlineRepositoryWrapper>();
        services.AddScoped<IMediaMarktITRepositoryWrapper, MediaMarktITRepositoryWrapper>();
        services.AddScoped<IFOMRepositoryWrapper, FOMRepositoryWrapper>();
        services.AddScoped<IMMLogisticsRepositoryWrapper, MMLogisticsRepositoryWrapper>();
        services.AddScoped<IMasterDataRepositoryWrapper, MasterDataRepositoryWrapper>();
        services.AddScoped<IMMITRepositoryWrapper, MMITRepositoryWrapper>();
        services.AddScoped<IMMOfflineRepositoryWrapper, MMOfflineRepositoryWrapper>();
        services.AddScoped<IOBHomeDeliveryRepositoryWrapper, OBHomeDeliveryRepositoryWrapper>();
        services.AddScoped<IMMDFSRepositoryWrapper, MMDFSRepositoryWrapper>();
        services.AddScoped<ISterlingRepositoryWrapper, SterlingRepositoryWrapper>();


        return services;

    }
    public static IServiceCollection ConfigureSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.Cookie.IsEssential = true;
            options.IdleTimeout = TimeSpan.FromMinutes(60);
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return services;
    }
    public static IServiceCollection ConfigureRedisDistributed(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRedisDistributedAdapter, RedisDistributedAdapter>();
        services.AddSingleton<IRedisAdapter, RedisAdapter>();

        return services;
    }
    public static IServiceCollection ConfigureHelpers(this IServiceCollection services)
    {
        //INFO: Helper tanımları PmCore.Common.Helpers içerisinde yapılıp bu kısımda eklenir.
        services.AddTransient<ISmsHelper, SmsHelper>();
        services.AddTransient<IEmailHelper, EmailHelper>();

        return services;
    }
    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IEKOLDesiClientService, EKOLDesiClientService>();
        services.AddHttpClient<IESBClientService, ESBClientService>();

        return services;
    }
}
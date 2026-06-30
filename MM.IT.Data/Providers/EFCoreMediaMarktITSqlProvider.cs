using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.MediaMarktIT;
using MM.IT.Data.Entities.MEX;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;

public class EFCoreMediaMarktITSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreMediaMarktITSqlProvider(
        DbContextOptions<EFCoreMediaMarktITSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
        ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.MediaMarktITConnection, options =>
        {
            options.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
        });
        optionsBuilder.UseLoggerFactory(_loggerFactory);

        optionsBuilder.UseLazyLoadingProxies()
            .ConfigureWarnings(w => w.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SMSAutomaticContentEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<SMSHeadEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<SMSSuccessfullEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<SMSErrorEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<SMSChannelEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<SMSSendingLOGEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<SMSOrderContentMatchEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<MMITStoreStockEntity>().HasKey(x => new { x.SAP_CODE, x.Article, x.WarehouseID });
        modelBuilder.Entity<EmailContentEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<MMITT800StoreStockEntity>().HasNoKey();
        modelBuilder.Entity<MMITT601StoreStockEntity>().HasNoKey();
        modelBuilder.Entity<AyenSoftLogEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXOrderHeadEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXOrderItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXOrderPaymentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MMProductsEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<OIDeliveryMethodEntity>().HasKey(x => new { x.ID });
        modelBuilder.Entity<MMOTPaymentTypeEntity>().HasKey(x => new { x.Id });

        base.OnModelCreating(modelBuilder);
    }

    #region MMIT

    public DbSet<SMSHeadEntity> SMSHead { get; set; }
    public DbSet<SMSHeadNewEntity> SMSHeadNew { get; set; }
    public DbSet<SMSSuccessfullEntity> SMSSuccessfull { get; set; }
    public DbSet<SMSErrorEntity> SMSErrors { get; set; }
    public DbSet<SMSChannelEntity> SMSChannel { get; set; }
    public DbSet<SMSSendingLOGEntity> SMSSendingLOGs { get; set; }
    public DbSet<SMSOrderContentMatchEntity> SMSOrderContentMatch { get; set; }
    public DbSet<MMITStoreStockEntity> StoreStocks { get; set; }
    public DbSet<EmailContentEntity> EmailContent { get; set; }
    public DbSet<MMITT800StoreStockEntity> T800StoreStocks { get; set; }
    public DbSet<MMITT601StoreStockEntity> T601StoreStocks { get; set; }
    public DbSet<AyenSoftLogEntity> AyenSoftLogs { get; set; }
    public DbSet<MEXOrderHeadEntity> MEXOrderHeads { get; set; }
    public DbSet<MEXOrderItemEntity> MEXOrderItems { get; set; }
    public DbSet<MEXOrderPaymentEntity> MEXOrderPayments { get; set; }
    public DbSet<MMProductsEntity> MMProducts { get; set; }
    public DbSet<OIDeliveryMethodEntity> OIDeliveryMethods { get; set; }
    public DbSet<MMOTPaymentTypeEntity> MMOTPaymentTypes { get; set; }

    #endregion
}


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.MMIT;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;
public class EFCoreMMITSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreMMITSqlProvider(
        DbContextOptions<EFCoreMMITSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
        ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.MMITConnection, options =>
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
        modelBuilder.Entity<VCRInvoiceHeadEntity>().HasKey(x => new { x.InvoiceId });
        modelBuilder.Entity<VCRCustomersEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRLogEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRPaymentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRProductEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRSalesDocEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRTotalDiscountEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRTotalVatEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VCRExcludeEntity>().HasKey(x => new { x.Id });

        modelBuilder.Entity<ApiChannelEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ApiProjectEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ApiAndChannelEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ApiListEntity>().HasKey(x => new { x.Id });

        base.OnModelCreating(modelBuilder);
    }

    #region VCR
    public DbSet<VCRInvoiceHeadEntity> VCRInvoiceHead { get; set; }
    public DbSet<VCRCustomersEntity> VCRCustomers { get; set; }
    public DbSet<VCRLogEntity> VCRLogs { get; set; }
    public DbSet<VCRPaymentEntity> VCRPayments { get; set; }
    public DbSet<VCRProductEntity> VCRProducts { get; set; }
    public DbSet<VCRSalesDocEntity> VCRSalesDocs { get; set; }
    public DbSet<VCRTotalDiscountEntity> VCRTotalDiscounts { get; set; }
    public DbSet<VCRTotalVatEntity> VCRTotalVats { get; set; }
    public DbSet<VCRExcludeEntity> VCRExcludes { get; set; }

    #endregion   
    
    #region Api
    public DbSet<ApiListEntity> ApiLists { get; set; }
    public DbSet<ApiAndChannelEntity> ApiAndChannels { get; set; }

    #endregion
}

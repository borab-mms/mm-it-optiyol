using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;

public class EFCoreFomSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreFomSqlProvider(
        DbContextOptions<EFCoreFomSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
        ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.OBFomConnection, options =>
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
        modelBuilder.Entity<FomOrderHeadEntity>().HasKey(p => p.customer_order_number);
        modelBuilder.Entity<OrderItemFulFillmentEntity>().HasKey(p => p.customer_order_number);
        modelBuilder.Entity<EKOLOrderHeadEntity>().HasKey(p => p.OrderNo);
        modelBuilder.Entity<FomOrderItemEntity>().HasKey(p => new { p.customer_order_number, p.item_id });
        modelBuilder.Entity<FomArvatoHeadEntity>().HasKey(p => p.orderNumber);

        base.OnModelCreating(modelBuilder);
    }

    #region FOM
    public DbSet<FomOrderHeadEntity> FomOrderHeads { get; set; }
    public DbSet<OrderItemFulFillmentEntity> OrderItemFulFillments { get; set; }
    public DbSet<EKOLOrderHeadEntity> EKOLOrderHeads { get; set; }
    public DbSet<FomOrderItemEntity> FomOrderItems { get; set; }
    public DbSet<FomArvatoHeadEntity> FomArvatoHead { get; set; }

    #endregion
}

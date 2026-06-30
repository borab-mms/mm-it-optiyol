using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.MMLogistics;
using MM.IT.Data.Providers.Interfaces;
using MM.Optiyol.Api.Data.Entities.External.MMLogistics.Optiyol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;

public class EFCoreMMLogisticsSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreMMLogisticsSqlProvider(
        DbContextOptions<EFCoreMMLogisticsSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
    ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.MMLogisticsConnection, options =>
        {
            options.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
        });
        optionsBuilder.UseLoggerFactory(_loggerFactory);

        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.ConfigureWarnings(w => w.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EKOLStockEntity>().HasKey(a=>a.Id);

        base.OnModelCreating(modelBuilder);
    }

    #region EKOLStock
    public DbSet<EKOLStockEntity> EKOLStocks { get; set; }
    #endregion

    #region Optiyol

    public DbSet<OptiyolNotificationEntity> OptiyolNotifications { get; set; }
    public DbSet<OptiyolOrderArrivedEntity> OptiyolOrderArriveds { get; set; }
    public DbSet<OptiyolOrderCanceledEntity> OptiyolOrderCanceleds { get; set; }
    public DbSet<OptiyolOrderCanceledWithItemListEntity> OptiyolOrderCanceledWithItemLists { get; set; }
    public DbSet<OptiyolOrderCanceledWithItemsEntity> OptiyolOrderCanceledWithItems { get; set; }
    public DbSet<OptiyolOrderCompletedEntity> OptiyolOrderCompleteds { get; set; }
    public DbSet<OptiyolOrderCompletedWithItemEntity> OptiyolOrderCompletedWithItem { get; set; }
    public DbSet<OptiyolOrderCompletedWithItemLotNumbersEntity> OptiyolOrderCompletedWithItemLotNumbers { get; set; }
    public DbSet<OptiyolOrderCompletedWithItemsEntity> OptiyolOrderCompletedWithItems { get; set; }
    public DbSet<OptiyolOrderReturnedEntity> OptiyolOrderReturneds { get; set; }
    public DbSet<OptiyolOrderUndoCanceledEntity> OptiyolOrderUndoCanceleds { get; set; }
    public DbSet<OptiyolRouteFinishedEntity> OptiyolRouteFinisheds { get; set; }
    public DbSet<OptiyolRouteLoadListCompletedEntity> OptiyolRouteLoadListCompleteds { get; set; }
    public DbSet<OptiyolRouteLoadListCompletedItemsEntity> OptiyolRouteLoadListCompletedItems { get; set; }
    public DbSet<OptiyolRoutePlannedEntity> OptiyolRoutePlanneds { get; set; }
    public DbSet<OptiyolRouterStartedEntity> OptiyolRouterStarteds { get; set; }
    public DbSet<OptiyolRouterStartedOrdersEntity> OptiyolRouterStartedOrders { get; set; }
    public DbSet<OptiyolRawDataEntity> OptiyolRawDatas { get; set; }
    public DbSet<OptiyolOrderPickupVirtualCompletedEntity> OptiyolOrderPickupVirtualCompleteds { get; set; }
    public DbSet<OptiyolStatusEntity> OptiyolStatuses { get; set; }
    public DbSet<OptiyolOrderStatusEntity> OptiyolOrderStatuses { get; set; }
    public DbSet<OptiyolBarcodeHeadEntity> OptiyolBarcodeHeads { get; set; }
    public DbSet<OptiyolBarcodeYearIdLogsEntity> OptiyolBarcodeYearIdLogs { get; set; }


    #endregion

}

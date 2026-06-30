using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.MMOffline;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;
public class EFCoreMMOfflineSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreMMOfflineSqlProvider(
        DbContextOptions<EFCoreMMOfflineSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
        ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.MMOfflineConnection, options =>
        {
            options.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
        });
        optionsBuilder.UseLoggerFactory(_loggerFactory);

        optionsBuilder.UseLazyLoadingProxies()
            .ConfigureWarnings(w =>
                w.Ignore(CoreEventId.DetachedLazyLoadingWarning)
                 .Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WWSSFSPickupReportEntity>().HasKey(x => new { x.SAP_CODE, x.WWS_DOC_NO, x.ART_NO, x.ONLINE_SALES_DOC_NO });

        base.OnModelCreating(modelBuilder);
    }

    #region WWS

    public DbSet<WWSSFSPickupReportEntity> WWSSFSPickupReportEntities { get; set; }

    #endregion
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Entities.OBHomeDelivery;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers
{
    public class EFCoreOBHomeDeliverySqlProvider : DbContext, IDataProvider
    {
        private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
        private readonly ILoggerFactory _loggerFactory;

        public EFCoreOBHomeDeliverySqlProvider(
            DbContextOptions<EFCoreOBHomeDeliverySqlProvider> options,
            IOptions<ConnectionConfigModel> connectionConfigs,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            _connectionConfigs = connectionConfigs;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionConfigs.Value.OBHomeDeliveryConnection, options =>
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
            modelBuilder.Entity<HDSalesDocumentHeadEntity>().HasKey(p => new { p.StoreId, p.SalesDocument });
            modelBuilder.Entity<KarturOrderLinkEntity>().HasKey(p => p.ReferenceId);
            modelBuilder.Entity<SFSBorusanHeadEntity>().HasKey(p => p.OrderId);
            modelBuilder.Entity<SFSBorusanStatusEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<YurticiHeadEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<YurticiLogEntity>().HasKey(p => new { p.Id, p.changeDate });
            modelBuilder.Entity<HDBarcodeLocationEntity>().HasNoKey();
            modelBuilder.Entity<HDBarcodeEKOLEntity>().HasNoKey();
			modelBuilder.Entity<HDSalesDocumentHeadCurrentEntity>().HasKey(p => new { p.StoreId, p.SalesDocument });
			modelBuilder.Entity<HDSalesDocumentItemCurrentEntity>().HasKey(p => new { p.StoreId, p.SalesDocument,p.ItemId });
			modelBuilder.Entity<HDBarcodeHeadEntity>().HasKey(p => new { p.Barcode});
			modelBuilder.Entity<HDBarcodeLogEntity>().HasKey(p => new { p.LogId});



			modelBuilder.Entity<SFSBorusanStatusEntity>()
            .HasOne<SFSBorusanHeadEntity>(p => p.SFSBorusanHead)
            .WithMany(p => p.SFSBorusanStatus)
            .HasForeignKey(p => p.OrderId);

            base.OnModelCreating(modelBuilder);
        }

        #region OBHomeDelivery
        public DbSet<HDSalesDocumentHeadEntity> HDSalesDocumentHead { get; set; }
        public DbSet<KarturOrderLinkEntity> KarturOrderLinks { get; set; }
        public DbSet<SFSBorusanHeadEntity> SFSBorusanHead { get; set; }
        public DbSet<SFSBorusanStatusEntity> SFSBorusanStatus { get; set; }
        public DbSet<YurticiHeadEntity> YurticiHead { get; set; }
        public DbSet<YurticiLogEntity> YurticiLogs { get; set; }
        public DbSet<HDBarcodeLocationEntity> HDBarcodeLocations { get; set; }
        public DbSet<HDBarcodeEKOLEntity> HDBarcodeEKOLs { get; set; }
        public DbSet<HDSalesDocumentHeadCurrentEntity> HDSalesDocumentHeadCurrents { get; set; }
        public DbSet<HDSalesDocumentItemCurrentEntity> HDSalesDocumentItemCurrents { get; set; }
        public DbSet<HDBarcodeHeadEntity> HDBarcodeHead { get; set; }
        public DbSet<HDBarcodeLogEntity> HDBarcodeLog { get; set; }

        #endregion
    }
}

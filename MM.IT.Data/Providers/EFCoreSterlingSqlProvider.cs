using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.FOM;
using MM.IT.Data.Entities.MMONLINE;
using MM.IT.Data.Entities.MMONLINE.Sterling;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers
{

    public class EFCoreSterlingSqlProvider : DbContext, IDataProvider
    {
        private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
        private readonly ILoggerFactory _loggerFactory;

        public EFCoreSterlingSqlProvider(
            DbContextOptions<EFCoreSterlingSqlProvider> options,
            IOptions<ConnectionConfigModel> connectionConfigs,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            _connectionConfigs = connectionConfigs;
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionConfigs.Value.SterlingConnection, options =>
            {
                options.CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds);
            });
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            optionsBuilder.UseLazyLoadingProxies()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SterlingGlobalLastUpdatedDataEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderHeadEntity>().HasKey(p => new { p.GuidId });
            modelBuilder.Entity<SterlingOrderAddressEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingMasterDataStatusListEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingRawDataEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingMasterDataSellerEntity>().HasNoKey();
            modelBuilder.Entity<SterlingOrderItemEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderItemHoldsEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderItemPriceEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderItemsPriceAdjustmentEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderItemsShippingCostsDetailEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderItemsStateQuantityEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderFulfillmentEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderFulfillmentItemEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderFulfillmentOrderShipmentEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderNoteEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderPaymentEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderPaymentPartEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderRequestedFulfillmentEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderRequestedFulfillmentItemEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderReturnChargeEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderReturnEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderReturnItemEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderTotalDetailAmountEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderTotalDetailEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderTotalDetailVatEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderTotalEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderFulfillmentOrderShipmentHandlingUnitEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderFulfillmentOrderShipmentHandlingUnitItemEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingMasterDataSmsContentMatchEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingMasterDataPgBasedSmsMatchEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingProcessSendToFSPEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingProcessShipmentDataEntity>().HasKey(p => p.OrderFulfillmentId);
            modelBuilder.Entity<SterlingMissingDataOrderEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderAddressComponentEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingCOSv3TransitionEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<SterlingOrderComponentAttributeEntity>().HasKey(p => p.Id);

            modelBuilder.Entity<SterlingOrderItemEntity>()
            .HasOne<SterlingOrderHeadEntity>(p => p.OrderHead)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(p => p.OrderHeadGuidId);

            //modelBuilder.Entity<SterlingOrderItemsStateQuantityEntity>()
            //.HasOne<SterlingOrderHeadEntity>(p => p.OrderHead)
            //.WithMany(p => p.SterlingOrderItemStateQuantities)
            //.HasForeignKey(p => p.OrderHeadGuidId);

            base.OnModelCreating(modelBuilder);
        }

        #region Sterling
        public DbSet<SterlingGlobalLastUpdatedDataEntity> SterlingGlobalLastUpdatedData { get; set; }
        public DbSet<SterlingOrderHeadEntity> SterlingOrderHead { get; set; }
        public DbSet<SterlingOrderAddressEntity> SterlingOrderAddress { get; set; }
        public DbSet<SterlingMasterDataStatusListEntity> SterlingMasterDataStatusList { get; set; }
        public DbSet<SterlingRawDataEntity> SterlingRawDatas { get; set; }
        public DbSet<SterlingMasterDataSellerEntity> SterlingMasterDataSellers { get; set; }
        public DbSet<SterlingOrderItemEntity> SterlingOrderItems { get; set; }
        public DbSet<SterlingOrderItemHoldsEntity> SterlingOrderItemHolds { get; set; }
        public DbSet<SterlingOrderItemPriceEntity> SterlingOrderItemPrices { get; set; }
        public DbSet<SterlingOrderItemsPriceAdjustmentEntity> SterlingOrderItemsPriceAdjustments { get; set; }
        public DbSet<SterlingOrderItemsShippingCostsDetailEntity> SterlingOrderItemsShippingCostsDetails { get; set; }
        public DbSet<SterlingOrderItemsStateQuantityEntity> SterlingOrderItemsStateQuantities { get; set; }
        public DbSet<SterlingOrderFulfillmentEntity> SterlingOrderFulfillments { get; set; }
        public DbSet<SterlingOrderFulfillmentItemEntity> SterlingOrderFulfillmentItems { get; set; }
        public DbSet<SterlingOrderFulfillmentOrderShipmentEntity> SterlingOrderFulfillmentOrderShipments { get; set; }
        public DbSet<SterlingOrderNoteEntity> SterlingOrderNotes { get; set; }
        public DbSet<SterlingOrderPaymentEntity> SterlingOrderPayments { get; set; }
        public DbSet<SterlingOrderPaymentPartEntity> SterlingOrderPaymentParts { get; set; }
        public DbSet<SterlingOrderRequestedFulfillmentEntity> SterlingOrderRequestedFulfillments { get; set; }
        public DbSet<SterlingOrderRequestedFulfillmentItemEntity> SterlingOrderRequestedFulfillmentItems { get; set; }
        public DbSet<SterlingOrderReturnChargeEntity> SterlingOrderReturnCharges { get; set; }
        public DbSet<SterlingOrderReturnEntity> SterlingOrderReturns { get; set; }
        public DbSet<SterlingOrderReturnItemEntity> SterlingOrderReturnItems { get; set; }
        public DbSet<SterlingOrderTotalDetailAmountEntity> SterlingOrderTotalDetailAmounts { get; set; }
        public DbSet<SterlingOrderTotalDetailEntity> SterlingOrderTotalDetails { get; set; }
        public DbSet<SterlingOrderTotalDetailVatEntity> SterlingOrderTotalDetailVats { get; set; }
        public DbSet<SterlingOrderTotalEntity> SterlingOrderTotals { get; set; }
        public DbSet<SterlingOrderFulfillmentOrderShipmentHandlingUnitEntity> SterlingOrderFulfillmentOrderShipmentHandlingUnits { get; set; }
        public DbSet<SterlingOrderFulfillmentOrderShipmentHandlingUnitItemEntity> SterlingOrderFulfillmentOrderShipmentHandlingUnitItems { get; set; }
        public DbSet<SterlingMasterDataSmsContentMatchEntity> SterlingMasterDataSmsContentMatch { get; set; }
        public DbSet<SterlingMasterDataPgBasedSmsMatchEntity> SterlingMasterDataPgBasedSmsMatchEntity { get; set; }
        public DbSet<SterlingProcessSendToFSPEntity> SterlingProcessSendToFSPs { get; set; }
        public DbSet<SterlingProcessShipmentDataEntity> SterlingProcessShipmentDatas { get; set; }
        public DbSet<SterlingMissingDataOrderEntity> SterlingMissingDataOrders { get; set; }
        public DbSet<SterlingOrderAddressComponentEntity> SterlingOrderAddressComponents { get; set; }
        public DbSet<SterlingCOSv3TransitionEntity> SterlingCOSv3Transitions { get; set; }
        public DbSet<SterlingOrderComponentAttributeEntity> SterlingOrderComponentAttributes { get; set; }

        #endregion
    }
}

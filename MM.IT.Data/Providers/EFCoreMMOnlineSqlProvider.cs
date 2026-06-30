using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Data.Entities.MEX;
using MM.IT.Data.Entities.MMONLINE;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;

// dotnet ef migrations add Migrations --context EFCoreMMOnlineSqlProvider
public class EFCoreMMOnlineSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreMMOnlineSqlProvider(
        DbContextOptions<EFCoreMMOnlineSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
        ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.MMOnlineConnection, options =>
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
        modelBuilder.Entity<MEXRawDataEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXFlowProblemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXPaymentHeaderEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXPaymentItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXPaymentPaymentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXPaymentPaymentCardEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXPaymentPaymentPosEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<MEXPaymentPaymentPosEntity>().HasKey(x => new { x.Id });

        #region MarketPlaces

        modelBuilder.Entity<ReturnDemandEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ReturnDemandStatuEntity>().HasNoKey();
        modelBuilder.Entity<ReturnRejectionReasonEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<SaleChannelEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<SaleChannelEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<SalesChannelOrderStatuEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<OrderHeadEntity>().HasKey(x => new { x.OrderHeadId });
        modelBuilder.Entity<SalesChannelReturnStatuEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<OrderItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<CustomerEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<T800ExcludeListEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<VatKeyEntity>().HasNoKey();
        modelBuilder.Entity<ESBEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<ESBLogEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<OrderUpdateHistoryEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<RawCancellationDataEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<BackorderTrackingEntity>().HasKey(x => new { x.Id });

        #endregion

        #region DigitalCard

        modelBuilder.Entity<DCHeadEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<DCItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<DCTransactionResultEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<DCErrorEntity>().HasKey(x => new { x.Id });
        #endregion

        #region FOM

        modelBuilder.Entity<FOMRawDataEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderHeadEntity>().HasKey(x => new { x.CustomerOrderNumber });
        modelBuilder.Entity<FOMOrderItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderCustomersEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderAddressEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMLCAndCargoMatchEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderPaymentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderDiscountEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderChargesEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemRelationsEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderReturnItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemReturnStatusEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemPriceEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemWarrantyEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemDiscountEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemChargeEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemFulFillmentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemFulFillmentDeliveryEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderItemFulFillmentDeliveryPackageEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderShipmentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderShipmentLineEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderHeadEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderCustomersEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderAddressEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderPaymentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderDiscountEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderReturnItemEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemReturnStatusEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemPriceEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemWarrantyEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemDiscountEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemChargeEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemFulFillmentDeliveryEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemFulFillmentDeliveryPackageEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderShipmentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderShipmentLineEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<PGSMSEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMArchiveCheckEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMTimeStampKeyEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMDeliveryMethodEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemFulFillmentEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderChargesEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMHistoryOrderItemRelationsEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMT800SendLogEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMSellerEntity>().HasNoKey();
        modelBuilder.Entity<FOMPaymentTypeEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderShipmentStatusEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMOrderShipmentTypesEntity>().HasNoKey();
        modelBuilder.Entity<FOMSenderMpShippingStatusLogEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMStatusListEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMVCRExcludeEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMGlobalLastUpdatedDataEntity>().HasKey(x => new { x.Id });
        modelBuilder.Entity<FOMMissingDataOrderEntity>().HasNoKey();

        #endregion

        #region Cancellation

        modelBuilder.Entity<CancellationProcessReasonsForCancellationEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<CancellationProcessCancellationRequestEntity>().HasKey(x => x.Id);

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    #region MMOnline

    public DbSet<MEXRawDataEntity> MEXRawDatas { get; set; }
    public DbSet<MEXFlowProblemEntity> MEXFlowProblems { get; set; }
    public DbSet<MEXPaymentHeaderEntity> MEXPaymentHeaders { get; set; }
    public DbSet<MEXPaymentItemEntity> MEXPaymentItems { get; set; }
    public DbSet<MEXPaymentPaymentEntity> MEXPaymentPayments { get; set; }
    public DbSet<MEXPaymentPaymentCardEntity> MEXPaymentPaymentCards { get; set; }
    public DbSet<MEXPaymentPaymentPosEntity> MEXPaymentPaymentPoss { get; set; }

    #region MarketPlaces
    public DbSet<ReturnDemandEntity> ReturnDemands { get; set; }
    public DbSet<ReturnDemandStatuEntity> ReturnDemandStatus { get; set; }
    public DbSet<ReturnRejectionReasonEntity> ReturnRejections { get; set; }
    public DbSet<SaleChannelEntity> SaleChannels { get; set; }
    public DbSet<SalesChannelOrderStatuEntity> SalesChannelOrderStatu { get; set; }
    public DbSet<OrderHeadEntity> OrderHead { get; set; }
    public DbSet<SalesChannelReturnStatuEntity> SalesChannelReturnStatu { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<T800ExcludeListEntity> T800ExcludeList { get; set; }
    public DbSet<VatKeyEntity> VatKeys { get; set; }
    public DbSet<ESBEntity> ESB { get; set; }
    public DbSet<ESBLogEntity> ESBLog { get; set; }
    public DbSet<OrderUpdateHistoryEntity> OrderUpdateHistories { get; set; }
    public DbSet<RawCancellationDataEntity> RawCancellationDatas { get; set; }

    #endregion

    #region DigtalCard
    public DbSet<DCHeadEntity> DCHead { get; set; }
    public DbSet<DCItemEntity> DCItems { get; set; }
    public DbSet<DCTransactionResultEntity> DCTransactionResults { get; set; }
    public DbSet<DCErrorEntity> DCErrors { get; set; }

    #endregion

    public DbSet<FOMRawDataEntity> FOMRawData { get; set; }
    public DbSet<FOMOrderHeadEntity> FOMOrderHead { get; set; }
    public DbSet<MM.IT.Data.Entities.MMONLINE.FOMOrderItemEntity> FOMOrderItem { get; set; }
    public DbSet<FOMOrderCustomersEntity> FOMOrderCustomers { get; set; }
    public DbSet<FOMOrderAddressEntity> FOMOrderAddress { get; set; }
    public DbSet<FOMLCAndCargoMatchEntity> FOMLCAndCargoMatch { get; set; }
    public DbSet<PGSMSEntity> PGSMS { get; set; }
    public DbSet<FOMTimeStampKeyEntity> FOMTimeStampKey { get; set; }
    public DbSet<FOMT800SendLogEntity> FOMT800SendLogs { get; set; }
    public DbSet<FOMSellerEntity> FOMSellers { get; set; }
    public DbSet<FOMPaymentTypeEntity> FOMPaymentTypes { get; set; }
    public DbSet<FOMOrderShipmentStatusEntity> FOMOrderShipmentStatus { get; set; }
    public DbSet<FOMOrderShipmentTypesEntity> FOMOrderShipmentTypes { get; set; }
    public DbSet<FOMSenderMpShippingStatusLogEntity> FOMSenderMpShippingStatusLog { get; set; }
    public DbSet<FOMStatusListEntity> FOMStatusList { get; set; }
    public DbSet<FOMGlobalLastUpdatedDataEntity> FOMGlobalLastUpdatedDatas { get; set; }
    public DbSet<FOMVCRExcludeEntity> FOMVCRExcludes { get; set; }
    public DbSet<FOMMissingDataOrderEntity> FOMMissingDataOrders { get; set; }

    #endregion
    #region Cancellation
    public DbSet<CancellationProcessReasonsForCancellationEntity> CancellationProcessReasonsForCancellations { get; set; }
    public DbSet<CancellationProcessCancellationRequestEntity> CancellationProcessCancellationRequests { get; set; }
    #endregion
}
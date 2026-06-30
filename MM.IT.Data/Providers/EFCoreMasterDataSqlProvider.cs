using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MM.IT.Common.Models.Configs;
using MM.IT.Common.Models.MEX;
using MM.IT.Data.Entities.MasterData;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;
public class EFCoreMasterDataSqlProvider : DbContext, IDataProvider
{
    private readonly IOptions<ConnectionConfigModel> _connectionConfigs;
    private readonly ILoggerFactory _loggerFactory;

    public EFCoreMasterDataSqlProvider(
        DbContextOptions<EFCoreMasterDataSqlProvider> options,
        IOptions<ConnectionConfigModel> connectionConfigs,
        ILoggerFactory loggerFactory
        ) : base(options)
    {
        _connectionConfigs = connectionConfigs;
        _loggerFactory = loggerFactory;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionConfigs.Value.MasterDataConnection, options =>
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

        modelBuilder.Entity<DesiEntity>().HasKey(a=>a.Id);
        modelBuilder.Entity<MasterDataSTRStoreEntity>().HasKey(a=>a.Id);
        modelBuilder.Entity<MasterDataZCNeighborhoodEntity>().HasKey(a=>a.Id);

        //modelBuilder.Entity<MasterDataARTArticleEntity>()
        //            .HasOne(e => e.ARTBrand)
        //            .WithMany(e => e.ARTArticles)
        //            .HasForeignKey(e => e.BrandId)
        //            .IsRequired();
        //modelBuilder.Entity<MasterDataARTBrandEntity>()
        //            .HasMany(e => e.ARTArticles)
        //            .WithOne(e => e.ARTBrand)
        //            .HasForeignKey(e => e.BrandId)
        //            .IsRequired();

        modelBuilder.Entity<MasterDataARTArticleEntity>().HasNoKey();
        modelBuilder.Entity<MasterDataARTBrandEntity>().HasNoKey();
        modelBuilder.Entity<MasterDataARTDepartmentEntity>().HasNoKey();
        modelBuilder.Entity<MasterDataMMProductEntity>().HasKey(a=>a.ID);
        modelBuilder.Entity<MasterDataARTProductGroupEntity>().HasNoKey();
        modelBuilder.Entity<MasterDataProductEntity>().HasKey(a=>a.Id);

        base.OnModelCreating(modelBuilder);
    }

    #region Master Data

    public DbSet<DesiEntity> Desis { get; set; }
    public DbSet<MasterDataSTRStoreEntity> STRStores { get; set; }
    public DbSet<MasterDataARTArticleEntity> ARTArticles { get; set; }
    public DbSet<MasterDataARTBrandEntity> ARTBrands { get; set; }
    public DbSet<MasterDataARTDepartmentEntity> ARTDepartments { get; set; }
    public DbSet<MasterDataZCCityEntity> Cities { get; set; }
    public DbSet<MasterDataZCDistrictEntity> Districts { get; set; }
    public DbSet<MasterDataZCNeighborhoodEntity> Neighborhoods { get; set; }
    public DbSet<MasterDataMMProductEntity> MMProducts { get; set; }
    public DbSet<MasterDataARTProductGroupEntity> MMProductGroup { get; set; }
    #endregion
}
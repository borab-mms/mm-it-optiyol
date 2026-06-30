using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MM.IT.Common.Constants;
using MM.IT.Common.Extensions;
using MM.IT.Common.Models.Common;
using MM.IT.Data.Entities.Interfaces;
using MM.IT.Data.Providers.Interfaces;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Data.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.UnitOfWork;

/// <summary>
/// IUnitOfWork şartlarını barındıran EF Core Unit Of Work Nesnesi
/// </summary>
public class EFCoreSqlUnitOfWork<TProvider> : IUnitOfWork<TProvider>
    where TProvider : DbContext, IDataProvider
{

    private readonly IServiceProvider _serviceProvider;
    private readonly TProvider _context;
    private readonly ILogger<EFCoreSqlUnitOfWork<TProvider>> _logger;

    public EFCoreSqlUnitOfWork(IServiceProvider serviceProvider,
        TProvider context,
        ILogger<EFCoreSqlUnitOfWork<TProvider>> logger)
    {
        _serviceProvider = serviceProvider;
        _context = context;
        _logger = logger;
    }

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
    {
        await _context.Database.BeginTransactionAsync(isolationLevel);
        _logger.LogInformation($"Transaction started with id: {_context.Database.CurrentTransaction.TransactionId}");
    }

    public void BeginTransaction(IsolationLevel isolationLevel)
    {
        _context.Database.BeginTransaction(isolationLevel);
        _logger.LogInformation($"Transaction started with id: {_context.Database.CurrentTransaction.TransactionId}");
    }

    public IGenericRepository<TProvider, TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
    {
        return _serviceProvider.GetService(typeof(IGenericRepository<TProvider, TEntity>)) as IGenericRepository<TProvider, TEntity>;
    }

    public async Task<int> SaveChangesAsync()
    {
        var currentUserId = GetHttpContext().GetCurrentUser()?.Id ?? PrimaryKeyConstants.UserAnonymous.ToGuid().Value;
        BeforeSaveChangesListener(currentUserId);
        var auditEntries = BeforeSaveChangesAuditTraceListener();
        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            AfterSaveChangesAuditTraceListener(auditEntries);
        }
        return result;
    }

    public int SaveChanges()
    {
        var currentUserId = GetHttpContext().GetCurrentUser()?.Id ?? PrimaryKeyConstants.UserAnonymous.ToGuid().Value;
        BeforeSaveChangesListener(currentUserId);
        var auditEntries = BeforeSaveChangesAuditTraceListener();
        var result = _context.SaveChanges();
        if (result > 0)
        {
            AfterSaveChangesAuditTraceListener(auditEntries);
        }
        return result;
    }

    public async Task<int> SaveChangesAsync(Guid userId)
    {
        BeforeSaveChangesListener(userId);
        var auditEntries = BeforeSaveChangesAuditTraceListener();
        var result = await _context.SaveChangesAsync();
        if (result > 0)
        {
            AfterSaveChangesAuditTraceListener(auditEntries);
        }
        return result;
    }

    public int SaveChanges(Guid userId)
    {
        BeforeSaveChangesListener(userId);
        var auditEntries = BeforeSaveChangesAuditTraceListener();
        var result = _context.SaveChanges();
        if (result > 0)
        {
            AfterSaveChangesAuditTraceListener(auditEntries);
        }
        return result;
    }

    public async Task CommitAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogInformation($"Transaction {_context.Database.CurrentTransaction.TransactionId} committed.");
            await _context.Database.CurrentTransaction.CommitAsync();
        }
    }

    public void Commit()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogInformation($"Transaction {_context.Database.CurrentTransaction.TransactionId} committed.");
            _context.Database.CurrentTransaction.Commit();
        }
    }

    public async Task RollbackAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogWarning($"Transaction {_context.Database.CurrentTransaction.TransactionId} rolled back.");
            await _context.Database.CurrentTransaction.RollbackAsync();

        }

    }

    public void Rollback()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            _logger.LogWarning($"Transaction {_context.Database.CurrentTransaction.TransactionId} rolled back.");
            _context.Database.CurrentTransaction.Rollback();
        }

    }

    public IQueryable<TQuery> GetQuery<TQuery>(string command, params object[] parameters) where TQuery : class
    {
        return _context.Set<TQuery>().FromSqlRaw(command, parameters).AsQueryable().AsNoTracking();
    }

    public IQueryable<TQuery> GetQuery<TQuery>(FormattableString command) where TQuery : class
    {
        return _context.Set<TQuery>().FromSqlInterpolated(command).AsQueryable().AsNoTracking();
    }

    public TProvider GetProvider()
    {
        return _context;
    }

    #region Private Functions

    /// <summary>
    /// IUnitOfWork SaveChanges ve SaveChangesAsync öncesinde yapılması gereken ortak değişiklikleri yapar.
    /// </summary>
    /// <param name="userId">Object: İşlemi yapmış User Primary key bilgisi</param>
    private void BeforeSaveChangesListener(Guid userId)
    {

        //INFO: IAuditableEntity'den türeyen Entity'lerin Ekleme, Güncelleme ve Silme sırasında yapılması gereken ortak düzenlemeleri yapar.
        foreach (var entry in _context.ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.Now;
                    entry.Entity.UpdatedBy = userId;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = userId;
                    break;
            }

        }

        //INFO: IEntity'den türeyen Entity'lerin Ekleme ve Silme sırasında yapılması gereken ortak düzenlemeleri yapar. Soft Silme İşlemi Gerçekleştirir.

        foreach (var entry in _context.ChangeTracker.Entries<IDeactivableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.Entity.IsActive = false;
                    break;
                case EntityState.Added:
                    entry.Entity.IsActive = true;
                    break;
            }
        }

        foreach (var entry in _context.ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.Entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Added:
                    entry.Entity.IsDeleted = false;
                    break;
            }
        }

        var currentTenant = GetHttpContext().GetCurrentTenant();

        //INFO: IWithTenantEntity'dan türeyen Entity'lerin oluşurken Aktif Tenant:PrimaryKey atamasının yapılmasını sağlar.
        foreach (var entity in _context.ChangeTracker.Entries<IWithTenantEntity>().Where(x => x.State == EntityState.Added).ToList())
        {
            entity.Entity.TenantId = currentTenant?.Id ?? PrimaryKeyConstants.TenantDefault;
        }

        if (currentTenant != null)
        {
            foreach (var entity in _context.ChangeTracker.Entries<IWithNullableTenantEntity>().Where(x => x.State == EntityState.Added).ToList())
            {
                entity.Entity.TenantId = currentTenant.Id;
            }
        }
    }

    /// <summary>
    /// ITraceableEntity'den türeyen Entity'lerin SaveChanges ve SaveChangesAsync öncesindeki bilgileri AuditEntry listesi olarak döner.
    /// </summary>
    /// <returns>IList: ITraceableEntity'den türeyen değişikliğe uğrayacak Entity'lerin listesi</returns>
    private IList<AuditEntry> BeforeSaveChangesAuditTraceListener()
    {
        var auditEntries = new List<AuditEntry>();

        foreach (var entry in _context.ChangeTracker.Entries<ITraceableEntity>().Where(p => p.State != EntityState.Detached && p.State != EntityState.Unchanged))
        {
            var auditEntry = new AuditEntry()
            {
                TableName = entry.Metadata.DisplayName(),
                State = entry.State.ToString(),
                TransactionId = _context.Database.CurrentTransaction.TransactionId
            };

            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                var propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = property.OriginalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        break;
                    case EntityState.Added:
                        auditEntry.NewValues[propertyName] = property.CurrentValue;
                        break;
                    default:
                        break;
                }
            }

            auditEntries.Add(auditEntry);
        }

        return auditEntries;
    }

    /// <summary>
    /// ITraceableEntity'den türeyen Entity'lerin SaveChanges ve SaveChangesAsync sonrasındaki hallerini önceki hallerini alarak log servisine iletir..
    /// </summary>
    /// <param name="auditEntries">IList: Entity'lerin SaveChanges ve SaveChangesAsync öncesindeki bilgileri</param>
    private void AfterSaveChangesAuditTraceListener(IList<AuditEntry> auditEntries)
    {
        if (auditEntries == null || auditEntries.Count == 0)
            return;

        var auditData = new List<AuditModel>();

        foreach (var auditEntry in auditEntries)
        {
            foreach (var prop in auditEntry.TemporaryProperties)
            {
                if (prop.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                }
                else
                {
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                }
            }

            auditData.Add(auditEntry.ToAuditModel());
        }

        _logger.LogInformation("Audit Trace Data:{@AuditData}", auditData);

    }

    /// <summary>
    /// Container'dan IHttpContextAccessor ile HttpContext nesnesine ulaşır ver döner.
    /// </summary>
    /// <returns>HttpContext: HttpContext nesnesi</returns>
    private HttpContext GetHttpContext()
    {
        return (_serviceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor).HttpContext;
    }

    #endregion
}
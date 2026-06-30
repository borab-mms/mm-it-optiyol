using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MM.IT.Data.Entities.Interfaces;
using MM.IT.Data.Providers.Interfaces;
using MM.IT.Data.Repositories.Interfaces;
using MM.IT.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories;

/// <summary>
/// IGenericRepository şartlarını barındıran EF Core Generic Repository Nesnesi
/// </summary>
/// <typeparam name="TEntity">TEntity: IEntity interface'inden türemiş Generic Entity nesnesi</typeparam>
public class EFCoreSqlGenericRepository<TProvider, TEntity> : IGenericRepository<TProvider, TEntity>
    where TEntity : class, IEntity
    where TProvider : DbContext, IDataProvider
{
    private readonly TProvider _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public EFCoreSqlGenericRepository(TProvider context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public TEntity GetById(object id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Create(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public void BulkInsert(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        _context.BulkInsert<TEntity>(entities.ToList(), _options);
    }

    public async Task BulkInsertAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        await _context.BulkInsertAsync<TEntity>(entities.ToList(), _options);
    }

    public void BulkUpdate(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        _context.BulkUpdate<TEntity>(entities.ToList(), _options);
    }

    public async Task BulkUpdateAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        await _context.BulkUpdateAsync<TEntity>(entities.ToList(), _options);
    }

    public void BulkMerge(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        _context.BulkInsertOrUpdate<TEntity>(entities.ToList(), _options);
    }

    public async Task BulkMergeAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        await _context.BulkInsertOrUpdateAsync<TEntity>(entities.ToList(), _options);
    }

    public void BulkSync(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        _context.BulkInsertOrUpdateOrDelete<TEntity>(entities.ToList(), _options);
    }

    public async Task BulkSyncAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        await _context.BulkInsertOrUpdateOrDeleteAsync<TEntity>(entities.ToList(), _options);
    }

    public void BulkDelete(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        _context.BulkDelete<TEntity>(entities.ToList(), _options);
    }

    public async Task BulkDeleteAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options)
    {
        var _options = new BulkConfig() { UseTempDB = true };
        options?.Invoke(_options);
        await _context.BulkDeleteAsync<TEntity>(entities.ToList(), _options);
    }

    public void Truncate()
    {
        _context.Truncate<TEntity>();
    }

    public async Task TruncateAsync()
    {
        await _context.TruncateAsync<TEntity>();
    }

    public IQueryable<TEntity> GetQuery(bool tenantControl = true)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var currentTenant = httpContext.GetCurrentTenant();

        if (tenantControl && typeof(IWithTenantEntity).IsAssignableFrom(typeof(TEntity)))
        {
            return _context.Set<TEntity>()
                .Cast<TEntity>().AsQueryable();
        }

        if (tenantControl && typeof(IWithNullableTenantEntity).IsAssignableFrom(typeof(TEntity)))
        {
            return _context.Set<TEntity>()
                .Cast<IWithNullableTenantEntity>().Where(p => (currentTenant == null ? false : p.TenantId == currentTenant.Id) || p.TenantId == null)
                .Cast<TEntity>().AsQueryable();
        }

        return _context.Set<TEntity>().AsQueryable();
    }
}
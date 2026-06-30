using EFCore.BulkExtensions;
using MM.IT.Data.Entities.Interfaces;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Repositories.Interfaces;

/// <summary>
/// Generic Repository Interface Tanımı
/// </summary>
/// <typeparam name="TEntity">TEntity: IEntity interface'inden türemiş Generic Entity nesnesi</typeparam>
public interface IGenericRepository<TProvider, TEntity>
    where TEntity : class, IEntity
    where TProvider : IDataProvider
{
    /// <summary>
    /// id bilgisi alarak Entity nesnesini döner.
    /// </summary>
    /// <param name="id">id: Entity.Id -> Primary key</param>
    /// <returns>IEntity: IEntity interface'inden türemiş Generic Entity nesnesi</returns>
    TEntity GetById(object id);

    /// <summary>
    /// Async: id bilgisi alarak Entity nesnesini döner.
    /// </summary>
    /// <param name="id">id: Entity.Id -> Primary key</param>
    /// <returns>IEntity: IEntity interface'inden türemiş Generic Entity nesnesi</returns>
    Task<TEntity> GetByIdAsync(object id);

    /// <summary>
    /// Entity nesnesini alarak DB'de kaydını oluşturur.
    /// </summary>
    /// <param name="entity">IEntity: IEntity interface'inden türemiş Generic Entity nesnesi</param>
    void Create(TEntity entity);

    /// <summary>
    /// Entity listesini alarak DB'de kayıtları oluşturur.
    /// </summary>
    /// <param name="entities">IEntity: IEntity interface'inden türemiş Generic Entity Listesi nesnesi</param>
    void Create(IEnumerable<TEntity> entities);

    /// <summary>
    /// Entity nesnesini alarak DB'deki kaydını günceller.
    /// </summary>
    /// <param name="entity">IEntity: IEntity interface'inden türemiş Generic Entity nesnesi</param>
    void Update(TEntity entity);

    /// <summary>
    /// Entity nesnesini alarak DB'deki listesini günceller.
    /// </summary>
    /// <param name="entities">IEntity: IEntity interface'inden türemiş Generic Entity  Listesi nesnesi</param>
    void Update(IEnumerable<TEntity> entities);

    /// <summary>
    /// Entity nesnesini alarak DB'deki kaydını siler.
    /// </summary>
    /// <param name="entity">IEntity: IEntity interface'inden türemiş Generic Entity nesnesi</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Entity listesi nesnesini alarak DB'deki kayıtları siler.
    /// </summary>
    /// <param name="entities">IEntity: IEntity interface'inden türemiş Generic Entity Listesi nesnesi</param>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    /// Entity Truncate Eder.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public void Truncate();

    /// <summary>
    /// Async: Entity Truncate Eder.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public Task TruncateAsync();

    /// <summary>
    /// Entity Listesi Alarak DB'de Bulk Insert şeklinde kayıtlarını oluşturur.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public void BulkInsert(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Async: Entity Listesi Alarak DB'de Bulk Insert şeklinde kayıtlarını oluşturur.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public Task BulkInsertAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Entity Listesi Alarak DB'de Bulk Update şeklinde kayıtlarını düzenler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public void BulkUpdate(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Async: Entity Listesi Alarak DB'de Bulk Update şeklinde kayıtlarını düzenler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public Task BulkUpdateAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Entity Listesi Alarak DB'de BulkInsertOrUpdate şeklinde kayıtlarını düzenler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public void BulkMerge(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Async: Entity Listesi Alarak DB'de BulkInsertOrUpdate şeklinde kayıtlarını düzenler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public Task BulkMergeAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Entity Listesi Alarak DB'de BulkInsertOrUpdateOrDelete şeklinde kayıtlarını düzenler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public void BulkSync(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Async: Entity Listesi Alarak DB'de BulkInsertOrUpdateOrDelete şeklinde kayıtlarını düzenler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public Task BulkSyncAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Entity Listesi Alarak DB'de Bulk Delete şeklinde kayıtlarını siler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public void BulkDelete(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Async: Entity Listesi Alarak DB'de Bulk Delete şeklinde kayıtlarını siler.
    /// IMPORTANT: SaveChanges gibi IAuditableEntity veya IAuditableEntity datalarını otomatik olarak düzenlemez. Manuel düzenlenmeli.
    /// </summary>
    /// <typeparam name="TEntity">Entity Tipi</typeparam>
    /// <param name="entities">Entity Listesi</param>
    /// <param name="options">Bulk Config</param>
    public Task BulkDeleteAsync(IEnumerable<TEntity> entities, Action<BulkConfig> options = null);

    /// <summary>
    /// Generic Entity nesnesinin Query bilgisini oluşturur ve döner.
    /// </summary>
    /// <returns>IQueryable: IEntity interface'inden türemiş Generic Entity Query nesnesi</returns>
    IQueryable<TEntity> GetQuery(bool tenantControl = true);
}

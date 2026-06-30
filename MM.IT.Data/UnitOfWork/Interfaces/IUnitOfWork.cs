using MM.IT.Data.Entities.Interfaces;
using MM.IT.Data.Providers.Interfaces;
using MM.IT.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.UnitOfWork.Interfaces;

/// <summary>
/// Unit of work interface tanımı
/// </summary>
public interface IUnitOfWork<TProvider> where TProvider : IDataProvider
{
    /// <summary>
    /// Async: IsolationLevel bilgisini alarak transaction başlatır.
    /// </summary>
    /// <param name="isolationLevel">IsolationLevel</param>
    /// <returns>Task: Async İşlem Bilgisi</returns>
    public Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// IsolationLevel bilgisini alarak transaction başlatır.
    /// </summary>
    /// <param name="isolationLevel">IsolationLevel</param>
    public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// Generic Entity bilgisini alarak Container'da yer alan IGenericRepository'den türemiş nesneyi döner.
    /// </summary>
    /// <typeparam name="TEntity">TEntity: IEntity interface'inden türemiş Generic Entity nesnesi</typeparam>
    /// <returns>IGenericRepository: IGenericRepository interface'inden türemiş Generic Repository nesnesi</returns>
    public IGenericRepository<TProvider, TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

    /// <summary>
    /// Generic Query nesnesinin Query bilgisini oluşturur ve döner.
    /// FromSqlRaw Kullanır.
    /// Sadece performans sorunlarında özel sorgular için kullanılmalıdır.
    /// </summary>
    /// <returns>IQueryable: Generic Query nesnesi</returns>
    public IQueryable<TQuery> GetQuery<TQuery>(string command, params object[] parameters) where TQuery : class;

    /// <summary>
    /// Generic Query nesnesinin Query bilgisini oluşturur ve döner.
    /// FromSqlInterpolated kullanır.
    /// Sadece performans sorunlarında özel sorgular için kullanılmalıdır.
    /// </summary>
    /// <returns>IQueryable: Generic Query nesnesi</returns>
    public IQueryable<TQuery> GetQuery<TQuery>(FormattableString command) where TQuery : class;

    /// <summary>
    /// Provider - Context Bilgisini Döner.
    /// </summary>
    /// <returns></returns>
    public TProvider GetProvider();

    /// <summary>
    /// Async: IDbProvider'dan türemiş nesnenin yapılan değişiklikleri kaydetmesini sağlar.
    /// </summary>
    /// <returns>Async işlem bilgisi -> int: Etkilenen kayıt sayı bilgisi</returns>
    public Task<int> SaveChangesAsync();

    /// <summary>
    /// IDbProvider'dan türemiş nesnenin yapılan değişiklikleri kaydetmesini sağlar.
    /// </summary>
    /// <returns>int: Etkilenen kayıt sayı bilgisi</returns>
    public int SaveChanges();

    /// <summary>
    /// Async: IDbProvider'dan türemiş nesnenin yapılan değişiklikleri kaydetmesini sağlar.
    /// </summary>
    /// <param name="userId">Kullanıcı Bilgisi : UserEntity.Id</param>
    /// <returns>Async işlem bilgisi -> int: Etkilenen kayıt sayı bilgisi</returns>
    public Task<int> SaveChangesAsync(Guid userId);

    /// <summary>
    /// IDbProvider'dan türemiş nesnenin yapılan değişiklikleri kaydetmesini sağlar.
    /// </summary>
    /// <param name="userId">Kullanıcı Bilgisi : UserEntity.Id</param>
    /// <returns>int: Etkilenen kayıt sayı bilgisi</returns>
    public int SaveChanges(Guid userId);

    /// <summary>
    /// Async: Transaction'nın commitlenmesini(onaylama) sağlar.
    /// </summary>
    /// <returns>Async işlem bilgisi</returns>
    public Task CommitAsync();

    /// <summary>
    /// Transaction'nın commitlenmesini(onaylama) sağlar.
    /// </summary>
    public void Commit();

    /// <summary>
    /// Async: Transaction'nın rollback(geri alma) yapılmasını sağlar.
    /// </summary>
    /// <returns>Async işlem bilgisi</returns>
    public Task RollbackAsync();

    /// <summary>
    /// Transaction'nın rollback(geri alma) yapılmasını sağlar.
    /// </summary>
    public void Rollback();
}

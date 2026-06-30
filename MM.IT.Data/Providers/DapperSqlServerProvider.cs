using Dapper;
using MM.IT.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.IT.Data.Providers;

/// <summary>
/// Dapper Sql Server Database Provider Nesnesi
/// </summary>
public class DapperSqlServerProvider : IDataProvider, IDisposable
{
    private IDbConnection _dbConnection;
    private IDbTransaction _dbTransaction;

    /// <summary>
    /// Constructor -> ConnectionString Bilgisini Alarak Connection Oluşturur, Transaction Başlatır.
    /// </summary>
    /// <param name="connectionString">ConnectionString</param>
    public DapperSqlServerProvider(string connectionString)
    {
        //_dbConnection = new SqlConnection(connectionString);
        _dbConnection.Open();
    }

    /// <summary>
    /// Transaction Başlatır.
    /// </summary>
    /// <param name="isolationLevel">Isolation Level</param>
    public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        _dbTransaction = _dbConnection.BeginTransaction(isolationLevel);
    }


    /// <summary>
    /// SQL Command ve parametrelere uyan ilk kaydı döner. Bulamazsa null döner.
    /// </summary>
    /// <typeparam name="TSelectModel">Sorgu Modeli</typeparam>
    /// <param name="command">Sql Sorgusu</param>
    /// <param name="param">Parametre Nesnesi</param>
    /// <param name="commandType">Command Tipi</param>
    /// <returns>Sorgu Modeli</returns>
    public TSelectModel GetFirstOrDefaultWithCommand<TSelectModel>(string command, object param = null, CommandType commandType = CommandType.Text, int? commandTimeout = 0)
    {
        return _dbConnection.QueryFirstOrDefault<TSelectModel>(command, param, commandType: commandType, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Async: SQL Command ve parametrelere uyan ilk kaydı döner. Bulamazsa null döner.
    /// </summary>
    /// <typeparam name="TSelectModel">Sorgu Modeli</typeparam>
    /// <param name="command">Sql Sorgusu</param>
    /// <param name="param">Parametre Nesnesi</param>
    /// <param name="commandType">Command Tipi</param>
    /// <returns>Sorgu Modeli</returns>
    public async Task<TSelectModel> GetFirstOrDefaultWithCommandAsync<TSelectModel>(string command, object param = null, CommandType commandType = CommandType.Text, int? commandTimeout = 0)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<TSelectModel>(command, param, commandType: commandType, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// SQL Command ve parametrelere uyan listeyi döner.
    /// </summary>
    /// <typeparam name="TSelectModel">Sorgu Modeli</typeparam>
    /// <param name="command">Sql Sorgusu</param>
    /// <param name="param">Parametre Nesnesi</param>
    /// <param name="commandType">Command Tipi</param>
    /// <returns>Sorgu Modeli Listesi</returns>
    public IEnumerable<TSelectModel> GetListWithCommand<TSelectModel>(string command, object param = null, CommandType commandType = CommandType.Text, int? commandTimeout = 0)
    {
        return _dbConnection.Query<TSelectModel>(command, param, commandType: commandType, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Async: SQL Command ve parametrelere uyan listeyi döner.
    /// </summary>
    /// <typeparam name="TSelectModel">Sorgu Modeli</typeparam>
    /// <param name="command">Sql Sorgusu</param>
    /// <param name="param">Parametre Nesnesi</param>
    /// <param name="commandType">Command Tipi</param>
    /// <returns>Sorgu Modeli Listesi</returns>
    public async Task<IEnumerable<TSelectModel>> GetListWithCommandAsync<TSelectModel>(string command, object param = null, CommandType commandType = CommandType.Text, int? commandTimeout = 0)
    {
        return await _dbConnection.QueryAsync<TSelectModel>(command, param, commandType: commandType, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// SQL Command ve parametreler bilgisini alarak yapılan değişiklikleri kaydetmesini sağlar.
    /// </summary>
    /// <param name="command">Sql Sorgusu</param>
    /// <param name="param">Parametre Nesnesi</param>
    /// <param name="commandType">Command Tipi</param>
    /// <returns>Int: Değişiklik yapılan kayıt sayısı</returns>
    public int ExcequteCommand(string command, object param = null, CommandType commandType = CommandType.Text, int? commandTimeout = 0)
    {
        if (_dbTransaction == null)
        {
            this.BeginTransaction();
        }

        return _dbConnection.Execute(command, param, commandType: commandType, transaction: _dbTransaction, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Async: SQL Command ve parametreler bilgisini alarak yapılan değişiklikleri kaydetmesini sağlar.
    /// </summary>
    /// <param name="command">Sql Sorgusu</param>
    /// <param name="param">Parametre Nesnesi</param>
    /// <param name="commandType">Command Tipi</param>
    /// <returns>Int: Değişiklik yapılan kayıt sayısı</returns>
    public async Task<int> ExcequteCommandAsync(string command, object param = null, CommandType commandType = CommandType.Text, int? commandTimeout = 0)
    {
        if (_dbTransaction == null)
        {
            this.BeginTransaction();
        }

        return await _dbConnection.ExecuteAsync(command, param, commandType: commandType, transaction: _dbTransaction, commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Database'de yapılan değişikliklerin onaylanmasını sağlar.
    /// </summary>
    public void Commit()
    {
        _dbTransaction.Commit();
    }

    /// <summary>
    /// Database'de yapılan değişikliklerin geri almasını sağlar.
    /// </summary>
    public void Rollback()
    {
        _dbTransaction.Rollback();
    }

    /// <summary>
    /// Connection Bilgisini 
    /// </summary>
    public void Dispose()
    {
        if (_dbTransaction != null)
        {
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
        }

        if (_dbConnection != null)
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
        }
    }
}
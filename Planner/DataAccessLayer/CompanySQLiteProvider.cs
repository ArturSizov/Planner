using Microsoft.Extensions.Logging;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.DataAccessLayer.DAO;
using SQLite;

namespace Planner.DataAccessLayer
{
    /// <summary>
    /// Working with the database Company
    /// </summary>
    public class CompanySQLiteProvider : IDataProvider<CompanyDAO>
    {
        private const SQLiteOpenFlags _flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// Logger Company SQLite provider
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Connection string 
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Database
        /// </summary>
        private SQLiteAsyncConnection? _database;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Logger Company SQLite provider</param>
        /// <param name="options">Options connection</param>
        public CompanySQLiteProvider(ILogger<CompanySQLiteProvider> logger, DbConnectionOptions options)
        {
            _logger = logger;
            _connectionString = options.ConnectionString;
            Task.Run(InitAsync);
        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(CompanyDAO item)
        {
            if (_database is null)
                return Task.FromResult(0);

            try
            {
                return _database.InsertAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in CreateAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            if (_database is null)
                return Task.FromResult(0);

            try
            {
                return _database.DeleteAllAsync<CompanyDAO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAllAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(CompanyDAO item)
        {
            if (_database is null)
                return Task.FromResult(0);

            try
            {
                return _database.DeleteAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<List<CompanyDAO>> ReadAllAsync()
        {
            if (_database is null)
                return Task.FromResult(new List<CompanyDAO>());

            try
            {
                return _database.Table<CompanyDAO>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAllAsync()");
                return Task.FromResult(new List<CompanyDAO>());
            }
        }

        /// <inheritdoc/>
        public Task<CompanyDAO?> ReadAsync(int id)
        {
            if (_database is null)
                return Task.FromResult<CompanyDAO?>(null);

            try
            {
                return _database.Table<CompanyDAO?>().Where(x => x != null && x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAsync()");
                return Task.FromResult<CompanyDAO?>(null);
            }
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(CompanyDAO item)
        {
            if (_database is null)
                return Task.FromResult(0);

            try
            {
                return _database.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateAsync()");
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <returns></returns>
        private async Task InitAsync()
        {
            try
            {
                _database ??= new SQLiteAsyncConnection(_connectionString, _flags);
                var exists = await IsTableExists(nameof(CompanyDAO));

                if (!exists)
                    _ = await _database.CreateTableAsync<CompanyDAO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InitAsync()");
            }
        }

        /// <summary>
        /// Checks if table already exists
        /// </summary>
        /// <param name="tableName">Table name to check</param>
        /// <returns><see langword="true"/> if exists; otherwise <see langword="false"/></returns>
        private async Task<bool> IsTableExists(string tableName)
        {
            try
            {
                _database ??= new SQLiteAsyncConnection(_connectionString, _flags);
                var info = await _database.GetTableInfoAsync(tableName);
                return info != null && info.Count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in IsTableExists()");
                return false;
            }
        }
    }
}

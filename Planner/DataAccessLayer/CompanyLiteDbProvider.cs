using LiteDB;
using Microsoft.Extensions.Logging;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.DataAccessLayer.DAO;

namespace Planner.DataAccessLayer
{
    /// <summary>
    /// Working with the database Company
    /// </summary>
    public class CompanyLiteDbProvider : IDataProvider<CompanyDAO>
    {
        private const string companiesDataCollectionName = "companies";

        /// <summary>
        /// Logger Company LiteDatabase provider
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Connection string 
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Database
        /// </summary>
        private LiteDatabase? _database;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Logger Company SQLite provider</param>
        /// <param name="options">Options connection</param>
        public CompanyLiteDbProvider(ILogger<CompanyLiteDbProvider> logger, DbConnectionOptions options)
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
                var collection = _database.GetCollection<CompanyDAO>(companiesDataCollectionName);
                collection.Insert(item);
                collection.EnsureIndex(x => x.Name);

                return Task.FromResult(1);
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
                var collection = _database.GetCollection<CompanyDAO>(companiesDataCollectionName).DeleteAll();

                return Task.FromResult(1);
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
                _database.GetCollection<CompanyDAO>(companiesDataCollectionName).Delete(item.Id);

                return Task.FromResult(1);
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
                var collection = _database.GetCollection<CompanyDAO>(companiesDataCollectionName);

                return Task.FromResult(collection.Query().ToList());
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
                var collection = _database.GetCollection<CompanyDAO?>(companiesDataCollectionName);

                return Task.FromResult(collection.Query().Where(x => x != null && x.Id == id).FirstOrDefault());
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
                var collection = _database.GetCollection<CompanyDAO>(companiesDataCollectionName);
                var a = collection.Update(item);

                return Task.FromResult(1);
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
        private void InitAsync()
        {
            try
            {
                _database ??= new LiteDatabase(_connectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InitAsync()");
            }
        }
    }
}

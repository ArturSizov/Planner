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
        /// Lite collection
        /// </summary>
        private ILiteCollection<CompanyDAO?> ?_collection;

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
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.Insert(item);

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
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.DeleteAll();

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
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.Delete(item.Id);

                return Task.FromResult(1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DeleteAsync()");
                return Task.FromResult(0);
            }
        }

        /// <inheritdoc/>
        public Task<List<CompanyDAO?>> ReadAllAsync()
        {
            if (_collection is null)
                return Task.FromResult(new List<CompanyDAO?>());

            try
            {
                return Task.FromResult(_collection.FindAll().ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ReadAllAsync()");
                return Task.FromResult(new List<CompanyDAO?>());
            }
        }

        /// <inheritdoc/>
        public Task<CompanyDAO?> ReadAsync(int id)
        {
            if (_collection is null)
                return Task.FromResult<CompanyDAO?>(null);

            try
            {
                return Task.FromResult(_collection.FindById(id));
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
            if (_collection is null)
                return Task.FromResult(0);

            try
            {
                _collection.Update(item);

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
                var database = new LiteDatabase(_connectionString);
                _collection = database.GetCollection<CompanyDAO?>(companiesDataCollectionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InitAsync()");
            }
        }
    }
}

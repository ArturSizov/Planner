using Microsoft.Extensions.Logging;
using Planner.Abstractions;
using Planner.Models;

namespace Planner.Managers
{
    /// <summary>
    /// Companies manager
    /// </summary>
    public class CompaniesManager : ICompaniesManager
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        public CompaniesManager(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(Company item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(Company item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<List<Company>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Company?> ReadAsync(string item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(Company item)
        {
            throw new NotImplementedException();
        }
    }
}

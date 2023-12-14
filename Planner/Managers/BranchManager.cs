using Planner.Abstractions;
using Planner.Models;

namespace Planner.Managers
{
    /// <summary>
    /// Branch manager
    /// </summary>
    public class BranchManager : IBranchManager
    {
        /// <inheritdoc/>
        public Task<int> CreateAsync(Branch item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(Branch item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<List<Branch>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<Branch?> ReadAsync(string item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(Branch item)
        {
            throw new NotImplementedException();
        }
    }
}

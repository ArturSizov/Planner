using Planner.Abstractions;
using Planner.Models;
using System.Collections.ObjectModel;

namespace Planner.Managers
{
    public class BranchManager : IDataManager<BranchModel>
    {
        /// <summary>
        /// Branch data provider
        /// </summary>
        private readonly IDataProvider<BranchModel> _dataProvider;

        /// <inheritdoc/>
        public ObservableCollection<BranchModel> Items { get; set; } = new();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataProvider">Branch data provider</param>
        public BranchManager(IDataProvider<BranchModel> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Task<int> CreateAsync(BranchModel item)
        {
            throw new NotImplementedException();
        }

        public Task<BranchModel?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(BranchModel item)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(BranchModel item)
        {
            throw new NotImplementedException();
        }

        public Task<List<BranchModel>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}

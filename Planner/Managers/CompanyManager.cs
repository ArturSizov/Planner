using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.DataAccessLayer.DAO;
using Planner.Models;
using System.Collections.ObjectModel;

namespace Planner.Managers
{
    /// <summary>
    /// Company manger
    /// </summary>
    public class CompanyManager : IDataManager<CompanyModel>
    {
        /// <summary>
        /// Company data provider
        /// </summary>
        private readonly IDataProvider<CompanyDAO> _dataProvider;

        /// <inheritdoc/>
        public ObservableCollection<CompanyModel> Items { get; set; } = new();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataProvider">Company data provider</param>
        public CompanyManager(IDataProvider<CompanyDAO> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(CompanyModel item)
        {
            int id;

            if (Items.Count <= 0)
                id = 1;
            else
                id = Items.Max(x => x.Id) + 1;

            item.Id = id;

            Items.Add(item);
            return _dataProvider.CreateAsync(item.ToDAO());
        }

        /// <inheritdoc/>
        public Task<int> DeleteAllAsync()
        {
            Items.Clear();
            return _dataProvider.DeleteAllAsync();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(CompanyModel item)
        {
            Items.Remove(item);
            return _dataProvider.DeleteAsync(item.ToDAO());
        }

        /// <inheritdoc/>
        public Task<List<CompanyModel?>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<CompanyModel?> ReadAsync(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(CompanyModel item)
        {
            Items.FirstOrDefault(x => x.Id == item.Id);

            return _dataProvider.UpdateAsync(item.ToDAO());
        }

        /// <inheritdoc/>
		public async Task ReadAllCompaniesAsync()
        {
            var items = await _dataProvider.ReadAllAsync();
            
            Items = new ObservableCollection<CompanyModel>(items.Select(x => x!.ToModel()));
        }
    }
}

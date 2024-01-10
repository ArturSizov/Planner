﻿using Planner.Abstractions;
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
            Task.Run(ReadAllCompaniesAsync);
        }

        /// <inheritdoc/>
        public Task<int> CreateAsync(CompanyModel item)
        {
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
            var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);

            if (foundItem == null)
                return Task.FromResult(0);

            Items.Remove(foundItem);
            return _dataProvider.DeleteAsync(item.ToDAO());
        }

        /// <inheritdoc/>
        public Task<List<CompanyModel>> ReadAllAsync() => Task.FromResult(Items.ToList());

        /// <inheritdoc/>
        public Task<CompanyModel?> ReadAsync(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        /// <inheritdoc/>
        public Task<int> UpdateAsync(CompanyModel item)
        {
            var foundItem = Items.FirstOrDefault(x => x.Id == item.Id);

            if (foundItem == null)
                return Task.FromResult(0);

            if(foundItem != null)
            {
                foundItem.Id = item.Id;
                foundItem.Name = item.Name;
                foundItem.Branches = item.Branches;
            }

            return _dataProvider.UpdateAsync(item.ToDAO());
        }

        /// <summary>
		/// Reads all data from db
		/// </summary>
		private async Task ReadAllCompaniesAsync()
        {
            var items = await _dataProvider.ReadAllAsync();
            Items = new ObservableCollection<CompanyModel>(items.Select(x => x.ToModel()));
        }
    }
}

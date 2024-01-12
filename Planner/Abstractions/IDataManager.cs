using System.Collections.ObjectModel;

namespace Planner.Abstractions
{
    public interface IDataManager<T> : IBasicCRUD<T>
    {
        /// <summary>
		/// Observable items
		/// </summary>
		ObservableCollection<T> Items { get; set; }

        /// <summary>
		/// Reads all data from db
		/// </summary>
        Task ReadAllCompaniesAsync();
    }
}

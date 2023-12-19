using System.Collections.ObjectModel;

namespace Planner.Abstractions
{
    public interface IDataManager<T> : IBasicCRUD<T>
    {
        /// <summary>
		/// Observable items
		/// </summary>
		ObservableCollection<T> Items { get; set; }
    }
}

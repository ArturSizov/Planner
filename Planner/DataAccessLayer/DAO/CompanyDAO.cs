using Planner.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace Planner.DataAccessLayer.DAO
{
    /// <summary>
    /// Company Data Access Object
    /// </summary>
    public class CompanyDAO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ObservableCollection<BranchModel> Branches { get; set; } = new();
    }
}

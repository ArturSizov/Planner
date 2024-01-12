using Planner.Models;
using System.Collections.ObjectModel;

namespace Planner.DataAccessLayer.DAO
{
    /// <summary>
    /// Company Data Access Object
    /// </summary>
    public class CompanyDAO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ObservableCollection<BranchModel> Branches { get; set; } = new();
    }
}

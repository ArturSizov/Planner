using System.Collections.ObjectModel;

namespace Planner.Models
{
    public class Company
    {
        public string Name { get; set; } = string.Empty;

        public ObservableCollection<Branch> Branches { get; set; } = new();
    }
}

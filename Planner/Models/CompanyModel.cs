using System.Collections.ObjectModel;

namespace Planner.Models
{
    /// <summary>
    /// Company model
    /// </summary>
    public class CompanyModel : BindableObject
    {
        private string _name = string.Empty;
        private int _id;
        private ObservableCollection<BranchModel> _branches = new();

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BranchModel> Branches
        {
            get => _branches;
            set
            {
                _branches = value;
                OnPropertyChanged();
            }
        }
    }
}

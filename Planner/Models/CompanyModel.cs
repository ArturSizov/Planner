using System.Collections.ObjectModel;

namespace Planner.Models
{
    /// <summary>
    /// Company model
    /// </summary>
    public class CompanyModel : BindableObject
    {
        private int _id;
        private string _name = string.Empty;
        private ObservableCollection<BranchModel> _branches = [];

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

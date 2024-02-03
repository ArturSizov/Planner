using System.Collections.ObjectModel;

namespace Planner.Models
{
    /// <summary>
    /// Branch model
    /// </summary>
    public class BranchModel : BindableObject
    {
        private string _name = string.Empty;

        private bool @default;

        private ObservableCollection<ServiceModel> _services = [];
        
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool Default
        {
            get => @default;
            set
            {
                @default = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ServiceModel> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }
    }
}

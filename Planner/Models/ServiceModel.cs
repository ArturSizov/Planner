namespace Planner.Models
{
    /// <summary>
    /// Service model
    /// </summary>
    public class ServiceModel : BindableObject
    {
        private string _name = string.Empty;
        private int _plan = 0;
        private int _fact = 0;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public int Plan
        {
            get => _plan;
            set
            {
                _plan = value;
                OnPropertyChanged();
            }
        }
        public int Fact
        {
            get => _fact;
            set
            {
                _fact = value;
                OnPropertyChanged();
            }
        }
    }
}

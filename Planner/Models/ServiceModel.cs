namespace Planner.Models
{
    /// <summary>
    /// Service model
    /// </summary>
    public class ServiceModel : BindableObject
    {
        private string _name = string.Empty;
        private double? _plan;
        private double? _fact;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public double? Plan
        {
            get => _plan;
            set
            {
                _plan = value;
                OnPropertyChanged();
            }
        }
        public double? Fact
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

namespace Planner.Models
{
    /// <summary>
    /// Service model
    /// </summary>
    public class ServiceModel : BindableObject
    {
        private string _name = string.Empty;
        private ushort? _plan;
        private ushort? _fact;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public ushort? Plan
        {
            get => _plan;
            set
            {
                _plan = value;
                OnPropertyChanged();
            }
        }
        public ushort? Fact
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

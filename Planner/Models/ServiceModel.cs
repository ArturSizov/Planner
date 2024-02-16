namespace Planner.Models
{
    /// <summary>
    /// Service model
    /// </summary>
    public class ServiceModel : BindableObject
    {
        private string _name = string.Empty;
        private ushort? _plan = 0;
        private ushort? _fact = 0;

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

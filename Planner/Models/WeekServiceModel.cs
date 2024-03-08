namespace Planner.Models
{
    /// <summary>
    /// Weekly service model
    /// </summary>
    public class WeekServiceModel : BindableObject
    {
        private ServiceModel _service = new();

        private string? _notes;

        public string? Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public ServiceModel Service
        {
            get => _service;
            set
            {
                _service = value;
                OnPropertyChanged();
            }
        }
    }
}

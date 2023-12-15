namespace Planner.Models
{
    /// <summary>
    /// Branch model
    /// </summary>
    public class BranchModel : BindableObject
    {
        private string _name = string.Empty;
        private int _id;

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
    }
}

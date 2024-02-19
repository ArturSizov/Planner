using Planner.Abstractions;

namespace Planner
{
    public partial class MainPage : ContentPage
    {
        private ICustomDialogService _customDialogService;

        public MainPage(ICustomDialogService customDialogService)
        {
            InitializeComponent();

            _customDialogService = customDialogService;
        }

        protected override bool OnBackButtonPressed()
        {
            var result = _customDialogService?.DialogReference;

            if(result?.Dialog != null)
            {
                if (_customDialogService == null)
                    return false;

                _customDialogService.IsOpened = true;

                result.Close();

                _customDialogService.DialogReference = null;

                return true;
            }

            return false;
        }
    }
}

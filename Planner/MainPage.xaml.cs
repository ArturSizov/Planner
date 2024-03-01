using Planner.Abstractions;

namespace Planner
{
    public partial class MainPage : ContentPage
    {
        /// <inheritdoc/>
        private ICustomDialogService _customDialogService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="customDialogService"></param>
        public MainPage(ICustomDialogService customDialogService)
        {
            InitializeComponent();

            _customDialogService = customDialogService;
        }

        /// <summary>
        /// Logica back button
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            bool result;

            //This logic doesn't work. I haven't found a solution yet

            //if (_customDialogService.IsOpened)
            //{
            //    _customDialogService.DrawerToggle();
            //    result = true;
            //}

            var dialogResult = _customDialogService?.DialogReference;

            if (dialogResult?.Dialog != null)
            {
                if (_customDialogService == null)
                    return result = false;

                _customDialogService.IsOpened = true;

                dialogResult.Close();

                _customDialogService.DialogReference = null;

                result = true;
            }
            else result = false;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (!result)
                {
                    if (await DisplayAlert("Внимание!", "Закрыть приложение?", "Да", "Нет"))
                        Application.Current?.Quit();
                }
            });

            return true;
        }
    }
}

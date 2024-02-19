using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Planner.Abstractions;

namespace Planner
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(ICustomDialogService customDialogService)
        {
            InitializeComponent();

            MainPage = new MainPage(customDialogService);

            //Solves the problem with displaying the virtual keyboard on Android

#if ANDROID
            Current?.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

#endif
        }

        /// <summary>
        /// For debugging in Windows
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 380;
            const int newHeight = 700;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}

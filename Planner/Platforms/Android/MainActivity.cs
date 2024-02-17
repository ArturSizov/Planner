using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Planner
{
    [Activity(Theme = "@style/SplashTheme",
          ScreenOrientation = ScreenOrientation.Sensor,
          MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            Window?.SetNavigationBarColor(Android.Graphics.Color.ParseColor("#512BD4"));

            Window?.SetStatusBarColor(Android.Graphics.Color.ParseColor("#512BD4"));

            base.OnCreate(savedInstanceState);
        }
    }
}

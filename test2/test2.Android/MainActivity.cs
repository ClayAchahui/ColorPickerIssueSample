using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace test2.Android;

[Activity(Label = "test2.Android", MainLauncher = true, Theme = "@style/MyTheme.NoActionBar", Icon = "@drawable/icon",
    LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
public class MainActivity : AvaloniaMainActivity<App>
{
}
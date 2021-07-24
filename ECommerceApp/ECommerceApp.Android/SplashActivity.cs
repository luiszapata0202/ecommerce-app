using Android.App;
using Android.Content;
using Android.OS;

namespace ECommerceApp.Droid
{
    [Activity(Theme = "@style/ECommerceAppTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            mainActivityIntent.AddFlags(ActivityFlags.NoAnimation);
            StartActivity(mainActivityIntent);
        }
    }
}
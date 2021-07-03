using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;
using ECommerceApp.PlatformSpecific;

[assembly: Dependency(typeof(ECommerceApp.Droid.PlatformSpecific.SetupTheme))]
namespace ECommerceApp.Droid.PlatformSpecific
{
    public class SetupTheme : ISetupTheme
    {
        public void SetStatusBarColor(Color color)
        {
            var androidColor = color.ToAndroid();

            CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
        }
    }
}
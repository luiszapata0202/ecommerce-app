using System;
using ECommerceApp.PlatformSpecific;
using Xamarin.Forms;

[assembly: Dependency(typeof(ECommerceApp.iOS.PlatformSpecific.SetupTheme))]
namespace ECommerceApp.iOS.PlatformSpecific
{
    public class SetupTheme : ISetupTheme
    {
        public void SetStatusBarColor(Color color)
        {
            //throw new NotImplementedException();
        }
    }
}
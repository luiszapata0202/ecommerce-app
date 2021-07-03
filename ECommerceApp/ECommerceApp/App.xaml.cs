using System;
using ECommerceApp.Pages;
using ECommerceApp.PlatformSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerceApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignUpPage());

            var setupTheme = DependencyService.Get<ISetupTheme>();
            setupTheme.SetStatusBarColor((Color)Current.Resources["StatusBar"]);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

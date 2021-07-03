using System;
using ECommerceApp.Pages;
using ECommerceApp.PlatformSpecific;
using ECommerceApp.ViewModels;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerceApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignUpPage());           
        }

        protected override void OnStart()
        {
            var setupTheme = DependencyService.Get<ISetupTheme>();
            setupTheme.SetStatusBarColor((Color)Current.Resources["StatusBar"]);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        }
    }
}

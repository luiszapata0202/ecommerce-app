using Acr.UserDialogs;
using ECommerceApp.Helpers;
using ECommerceApp.Interfaces;
using ECommerceApp.Pages;
using ECommerceApp.PlatformSpecific;
using ECommerceApp.Services;
using ECommerceApp.ViewModels;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ECommerceApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            var accessToken = Preferences.Get("accessToken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
                await NavigationService.NavigateAsync("NavigationPage/SignUpPage");
            }
            else
            {
                if (TokenValidator.CheckTokenValidity())
                {
                    await NavigationService.NavigateAsync("NavigationPage/HomePage");
                }
                else
                {
                    await NavigationService.NavigateAsync("NavigationPage/SignUpPage");
                }
            }
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UserDialogs.Instance.Toast("Internet is back!");
            }
            else
            {
                UserDialogs.Instance.Toast("We lost internet connection :c");
            }
        }

        protected override void OnStart()
        {
            //var setupTheme = DependencyService.Get<ISetupTheme>();
            //setupTheme.SetStatusBarColor((Color)Current.Resources["StatusBar"]);
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
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailPage, ProductDetailViewModel>();
            containerRegistry.RegisterForNavigation<ProductListPage, ProductListViewModel>();
            containerRegistry.RegisterForNavigation<ShoppingCartPage, ShoppingCartViewModel>();
            containerRegistry.RegisterForNavigation<PlaceOrderPage, PlaceOrderViewModel>();


            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IProductService, ProductService>();
            containerRegistry.Register<IShoppingCartService, ShoppingCartService>();
            containerRegistry.Register<IOrderService, OrderService>();
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace ECommerceApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private int _shoppingCartItemsCount;

        public int ShoppingCartItemsCount
        {
            get { return _shoppingCartItemsCount; }
            set { SetProperty(ref _shoppingCartItemsCount, value); }
        }

        #endregion

        #region Constructor
        public HomePageViewModel(INavigationService navigationService,
            IProductService productService,
            IShoppingCartService shoppingCartService)
            : base(navigationService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;

            PopularProducts = new ObservableCollection<Product>();
            Categories = new ObservableCollection<Category>();
            ShowMenuCommand = new DelegateCommand(ShowMenu);
            ShoppingCartCommand = new DelegateCommand(ShoppingCart);
            LogoutCommand = new DelegateCommand(async () => await Logout());
        }
        #endregion

        #region Public Properties
        public DelegateCommand ShowMenuCommand { get; set; }
        public DelegateCommand ShoppingCartCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public ObservableCollection<Product> PopularProducts { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        #endregion

        #region Methods
        public async override void Initialize(INavigationParameters parameters)
        {
            var categories = await _productService.GetCategories();
            foreach (var item in categories)
            {
                Categories.Add(item);
            }

            var popularProducts = await _productService.GetPopularProducts();
            foreach (var item in popularProducts)
            {
                PopularProducts.Add(item);
            }

            var cartItemsCount = await _shoppingCartService.GetCartItemsCount();
            ShoppingCartItemsCount = cartItemsCount;
        }

        private void ShowMenu()
        {
           
        }

        private void ShoppingCart()
        {
           
        }

        private async Task Logout()
        {
            var response = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to logout?", "Logout", "Yes", "Cancel");

            if (response)
            {
                Preferences.Set("accessToken", string.Empty);
                Preferences.Set("tokenExpirationTime", string.Empty);

                await NavigationService.NavigateAsync(new Uri("app:///NavigationPage/SignUpPage"));
            }
        }
        #endregion
    }
}

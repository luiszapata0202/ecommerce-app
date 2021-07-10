using System.Collections.ObjectModel;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Prism.Navigation;

namespace ECommerceApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
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
        }
        #endregion

        #region Public Properties
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
        }
        #endregion
    }
}

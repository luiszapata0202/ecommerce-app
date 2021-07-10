using ECommerceApp.Interfaces;
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
        }
        #endregion

        #region Methods
        public async override void Initialize(INavigationParameters parameters)
        {
            var categories = await _productService.GetCategories();
            var popularProducts = await _productService.GetPopularProducts();
            var cartItemsCount = await _shoppingCartService.GetCartItemsCount();
        }
        #endregion
    }
}

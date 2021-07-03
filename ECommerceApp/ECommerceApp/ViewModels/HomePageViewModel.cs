using ECommerceApp.Interfaces;
using Prism.Navigation;

namespace ECommerceApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IProductService _productService;
        #endregion

        #region Constructor
        public HomePageViewModel(INavigationService navigationService,
            IProductService productService)
            : base(navigationService)
        {
            _productService = productService;
        }
        #endregion

        #region Methods
        public async override void Initialize(INavigationParameters parameters)
        {
            var categories = await _productService.GetCategories();
            var popularProducts = await _productService.GetPopularProducts();
        }
        #endregion
    }
}

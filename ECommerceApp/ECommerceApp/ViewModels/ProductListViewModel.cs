using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Prism.Commands;
using Prism.Navigation;

namespace ECommerceApp.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IProductService _productService;
        private string _categoryName;



        #endregion

        #region Constructor
        public ProductListViewModel(INavigationService navigationService,
            IProductService productService)
            :base(navigationService)
        {
            _productService = productService;

            Products = new ObservableCollection<Product>();
            GoBackCommand = new DelegateCommand(async () => await GoBack());
        }
        #endregion

        #region Public Properties
        public DelegateCommand GoBackCommand { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public string CategoryName
        {
            get { return _categoryName; }
            set { SetProperty(ref _categoryName, value); }
        }
        #endregion

        #region Methods
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var category = parameters.GetValue<Category>("category");

            CategoryName = category.Name;

            UserDialogs.Instance.ShowLoading("Loading...");

            await LoadProducts(category.Id);

            UserDialogs.Instance.HideLoading();
        }

        public async Task LoadProducts(int categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async Task GoBack()
        {
            await NavigationService.GoBackAsync(useModalNavigation: true);
        }
        #endregion
    }
}

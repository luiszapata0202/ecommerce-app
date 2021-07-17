using System.Threading.Tasks;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Prism.Commands;
using Prism.Navigation;

namespace ECommerceApp.ViewModels
{
    public class ProductDetailViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        private Product _product;
        private string _productImageUrl;
        private string _productName;
        private decimal _price;
        private string _description;
        private int _quantity;
        private decimal _total;
        #endregion

        #region Constructor
        public ProductDetailViewModel(INavigationService navigationService,
            IProductService productService,
            IShoppingCartService shoppingCartService)
            :base(navigationService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;

            DecrementCommand = new DelegateCommand(Decrement);
            IncrementCommand = new DelegateCommand(Increment);
            AddToCartCommand = new DelegateCommand(async () => await AddToCart());
            GoBackCommand = new DelegateCommand(async () => await GoBack());
        }
        #endregion

        #region Public Properties
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand DecrementCommand { get; set; }
        public DelegateCommand IncrementCommand { get; set; }
        public DelegateCommand AddToCartCommand { get; set; }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public decimal Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        public string ProductImageUrl
        {
            get { return _productImageUrl; }
            set { SetProperty(ref _productImageUrl, value); }
        }

        public decimal Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        #endregion

        #region Methods
        public override void Initialize(INavigationParameters parameters)
        {
            Quantity = 1;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var productId = parameters.GetValue<int>("productId");

            await LoadProductInfo(productId);
        }

        private async Task LoadProductInfo(int productId)
        {
            _product = await _productService.GetProductInfo(productId);

            ProductName = _product.Name;
            ProductImageUrl = _product.FullImageUrl;
            Price = _product.Price;
            Description = _product.Detail;
            Total = _price;
        }

        private void Decrement()
        {
            if (_quantity - 1 == 0)
                return;

            Quantity--; // Quantity = Quantity - 1;
            Total = _quantity * _price;
        }

        private void Increment()
        {
            Quantity++; // Quantity = Quantity + 1;
            Total = _quantity * _price;
        }

        private async Task AddToCart()
        {
            var result = await _shoppingCartService.AddProductToCart(_product, _quantity);

            if (result)
            {
                await UserDialogs.Instance.AlertAsync("Your items has been added to the cart");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Something went wrong", "Error");
            }
        }

        private async Task GoBack()
        {
            await NavigationService.GoBackAsync(useModalNavigation: true);
        }
        #endregion
    }
}

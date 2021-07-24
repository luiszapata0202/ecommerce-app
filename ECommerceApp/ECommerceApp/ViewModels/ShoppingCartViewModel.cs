using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Prism.Commands;
using Prism.Navigation;

namespace ECommerceApp.ViewModels
{
    public class ShoppingCartViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IShoppingCartService _shoppingCartService;
        private double _total;
        private bool _canPlaceOrder;
        #endregion

        #region Constructor
        public ShoppingCartViewModel(INavigationService navigationService,
            IShoppingCartService shoppingCartService)
            : base(navigationService)
        {
            _shoppingCartService = shoppingCartService;

            Title = "Your Cart";

            ShoppingCartItems = new ObservableCollection<ShoppingCartItem>();
            ClearItemsCommand = new DelegateCommand(async () => await ClearItems());
            PlaceOrderCommand = new DelegateCommand(async () => await PlaceOrder())
                .ObservesCanExecute(() => CanPlaceOrder);
        }
        #endregion

        #region Public Properties
        public double Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        public bool CanPlaceOrder
        {
            get { return _canPlaceOrder; }
            set { SetProperty(ref _canPlaceOrder, value); }
        }

        public ObservableCollection<ShoppingCartItem> ShoppingCartItems { get; set; }        
        public DelegateCommand ClearItemsCommand { get; set; }
        public DelegateCommand PlaceOrderCommand { get; set; }
        #endregion

        #region Methods
        public override async void Initialize(INavigationParameters parameters)
        {
            UserDialogs.Instance.ShowLoading("Loading...");

            var shoppingCartItems = await _shoppingCartService.GetShoppingCartItems();
            foreach (var item in shoppingCartItems)
            {
                ShoppingCartItems.Add(item);
            }

            CanPlaceOrder = shoppingCartItems.Count > 0;

            Total = ShoppingCartItems.Sum(i => i.Price * i.Quantity);

            UserDialogs.Instance.HideLoading();
        }

        private async Task PlaceOrder()
        {
            var navParams = new NavigationParameters
            {
                { "orderTotal", _total }
            };

            await NavigationService.NavigateAsync("PlaceOrderPage", navParams, useModalNavigation: true);
        }

        private async Task ClearItems()
        {
            var response = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to clear your shopping cart?", "Clear Shopping Cart", "Yes");

            if (response)
            {
                UserDialogs.Instance.ShowLoading("Removing Items...");

                var result = await _shoppingCartService.ClearShoppingCartItems();

                UserDialogs.Instance.HideLoading();

                if (!result)
                {
                    await UserDialogs.Instance.AlertAsync("Something went wrong");
                }
                else
                {
                    ShoppingCartItems.Clear();
                    Total = 0;
                    CanPlaceOrder = false;
                }
            }
        }
        #endregion
    }
}

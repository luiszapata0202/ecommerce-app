using System.Threading.Tasks;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace ECommerceApp.ViewModels
{
    public class PlaceOrderViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IOrderService _orderService;
        private string _name;
        private string _phone;
        private string _address;
        private double _total;
        #endregion

        #region Constructor
        public PlaceOrderViewModel(INavigationService navigationService,
            IOrderService orderService)
            : base(navigationService)
        {
            _orderService = orderService;

            Title = "PLACE ORDER";

            PlaceOrderCommand = new DelegateCommand(async () => await PlaceOrder());
        }
        #endregion

        #region Public Properties
        public DelegateCommand PlaceOrderCommand { get; set; }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        #endregion

        #region Methods
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _total = parameters.GetValue<double>("orderTotal");            
        }

        private async Task PlaceOrder()
        {
            var order = new Order();
            order.FullName = Name;
            order.Phone = Phone;
            order.Address = Address;
            order.UserId = Preferences.Get("userId", 0);
            order.OrderTotal = _total;

            var result = await _orderService.PlaceOrder(order);

            if (result != 0)
            {
                await UserDialogs.Instance.AlertAsync($"Your Order Id is {result}");
                await NavigationService.NavigateAsync("HomePage");
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Something went wrong");
            }
        }
        #endregion
    }
}

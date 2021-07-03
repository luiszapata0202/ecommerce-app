using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using Prism.Navigation;
using Xamarin.Forms;

namespace ECommerceApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Private Attributes
        private readonly IUserService _userService;
        private string _email;
        private string _password;
        #endregion

        #region Constructor
        public LoginViewModel(INavigationService navigation,
            IUserService userService)
            :base(navigation)
        {
            _userService = userService;

            SubmitCommad = new Command(async () => await Login());
        }
        #endregion

        #region Public Properties
        public ICommand SubmitCommad { get; set; }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        #endregion

        #region Methods
        private async Task Login()
        {
            var result = await _userService.Login(Email, Password);

            if (!result)
            {
                await UserDialogs.Instance.AlertAsync("Something went wrong", "Error");
            }
            else
            {
                await NavigationService.NavigateAsync("HomePage");
            }
        }
        #endregion
    }
}

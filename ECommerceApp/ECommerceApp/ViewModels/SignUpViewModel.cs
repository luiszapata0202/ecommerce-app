using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ECommerceApp.Interfaces;
using ECommerceApp.Pages;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace ECommerceApp.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region Private Attributes
        private IUserService _userService;
        private string _fullName;
        private string _email;
        private string _password;
        private string _passwordConfirmation;
        #endregion

        #region Constructor
        public SignUpViewModel(INavigationService navigationService,
            IUserService userService)
            : base(navigationService)
        {
            _userService = userService;

            ContinueCommand = new DelegateCommand(async () => await Continue());
            LoginCommand = new DelegateCommand(async () => await Login());
        }
        #endregion

        #region Public Properties
        public DelegateCommand ContinueCommand { get; set; }

        public DelegateCommand LoginCommand { get; set; }

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                SetProperty(ref _password, value);
            }
        }

        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set
            {
                _passwordConfirmation = value;
                SetProperty(ref _passwordConfirmation, value);
            }
        }
        #endregion

        #region Methods
        private async Task Continue()
        {
            if (Password != PasswordConfirmation)
            {
                Password = string.Empty;
                PasswordConfirmation = string.Empty;

                await UserDialogs.Instance.AlertAsync("Please check your password", "Password mismatch");
            }
            else
            {
                UserDialogs.Instance.ShowLoading("Registration in progress...");

                var result = await _userService.Register(FullName, Email, Password);

                UserDialogs.Instance.HideLoading();

                if (!result)
                {
                    await UserDialogs.Instance.AlertAsync("Something went wrong", "Error");
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Your account has been created", "Hi");                    
                    await NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
                }
            }
        }

        private async Task Login()
        {
            await NavigationService.NavigateAsync("LoginPage", useModalNavigation: true);
        }
        #endregion
    }
}

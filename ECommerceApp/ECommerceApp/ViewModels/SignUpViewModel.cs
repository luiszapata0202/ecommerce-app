using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using ECommerceApp.Pages;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ECommerceApp.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        #region Private Attributes
        private INavigation _navigation;
        private string _fullName;
        private string _email;
        private string _password;
        private string _passwordConfirmation;
        #endregion

        #region Constructor
        public SignUpViewModel(INavigation navigation)
        {
            _navigation = navigation;

            ContinueCommand = new Command(async () => await Continue());
            LoginCommand = new Command(async () => await Login());
        }
        #endregion

        #region Public Properties
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ContinueCommand { get; set; }

        public ICommand LoginCommand { get; set; }

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
                NotifyPropertyChanged();
            }
        }

        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set
            {
                _passwordConfirmation = value;
                NotifyPropertyChanged();
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
                var register = new
                {
                    Name = FullName,
                    Email = Email,
                    Password = Password
                };

                var json = JsonConvert.SerializeObject(register);

                var httpClient = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://ecommercebackendapi.azurewebsites.net/api/Accounts/Register", content);

                if (!response.IsSuccessStatusCode)
                {
                    await UserDialogs.Instance.AlertAsync("Please try again later", "Error");
                }
                else
                {
                    await UserDialogs.Instance.AlertAsync("Your account has been created", "Hi");
                    await _navigation.PushModalAsync(new LoginPage());
                }
            }
        }

        private async Task Login()
        {
            await _navigation.PushAsync(new LoginPage());
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

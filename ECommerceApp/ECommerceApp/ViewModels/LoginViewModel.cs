using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace ECommerceApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Private Attributes        
        private string _email;
        private string _password;
        #endregion

        #region Constructor
        public LoginViewModel(INavigationService navigation)
            :base(navigation)
        {
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
        private Task Login()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

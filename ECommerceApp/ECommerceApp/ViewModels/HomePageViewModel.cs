using Prism.Navigation;

namespace ECommerceApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        #region Constructor
        public HomePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
        #endregion
    }
}

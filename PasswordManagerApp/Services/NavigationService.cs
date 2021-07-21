using PasswordManagerApp.Views;
using Xamarin.Forms;

namespace PasswordManagerApp.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateToHomePage()
        {
            Application.Current.MainPage = new HomePage();

        }

    }
}

using PasswordManagerApp.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasswordManagerApp.ViewModels
{
    public class LoginPageViewModel: IDisposable
    {
        public ICommand LoginCommand { get; set; }
        private string myMasterPassword;
        public string MasterPassword
        {
            get
            {
                return myMasterPassword;
            }
            set
            {
                myMasterPassword = value;
                (LoginCommand as Command.Command).RaiseCanExecuteChanged();
            }
        }
        private readonly INavigationService myNavigationService;

        /// <summary>
        /// ctor
        /// </summary>
        public LoginPageViewModel(INavigationService navigationService)
        {
            myNavigationService = navigationService;
            LoginCommand = new PasswordManagerApp.Command.Command(OnLogin, CanLogin);
        }

        private bool CanLogin(object parameter)
        {
            return !(string.IsNullOrWhiteSpace(MasterPassword));
        }

        private void OnLogin(object parameter)
        {
            myNavigationService.NavigateToHomePage();
            MessagingCenter.Send(this, "MasterPassword", MasterPassword);
        }

        public void Dispose()
        {
            LoginCommand = null;
            MasterPassword = null;
        }
    }
}

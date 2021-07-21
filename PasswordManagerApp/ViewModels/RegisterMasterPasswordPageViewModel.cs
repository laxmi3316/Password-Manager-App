using PasswordManagerApp.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasswordManagerApp.ViewModels
{
    public class RegisterMasterPasswordPageViewModel: IDisposable
    {
        public ICommand RegisterMasterPwdCommand { get; set; }
        private readonly INavigationService myNavigationService;
        private readonly IRepositoryService myRepositoryService;
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
                (RegisterMasterPwdCommand as Command.Command).RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="navigationService"></param>
        public RegisterMasterPasswordPageViewModel(INavigationService navigationService, IRepositoryService repositoryService)
        {
            myNavigationService = navigationService;
            myRepositoryService = repositoryService;
            RegisterMasterPwdCommand = new PasswordManagerApp.Command.Command(OnRegister, CanRegisterPwd);
            myRepositoryService.CreateUserDataTable();
        }


        private bool CanRegisterPwd(object parameter)
        {
            return !(string.IsNullOrWhiteSpace(MasterPassword));
        }

        private void OnRegister(object parameter)
        {
            myNavigationService.NavigateToHomePage();
            MessagingCenter.Send(this, "MasterPassword", MasterPassword);
            
        }

        public void Dispose()
        {
            RegisterMasterPwdCommand = null;
            MasterPassword = null;
        }
    }
}

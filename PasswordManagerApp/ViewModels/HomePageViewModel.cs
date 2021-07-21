using PasswordManagerApp.Models;
using PasswordManagerApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasswordManagerApp.ViewModels
{
    public class HomePageViewModel: INotifyPropertyChanged, IDisposable
    {
        public ICommand SaveCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<UserData> UserDataList { get; set; }
        public ObservableCollection<UserData> EncryptedUserDataList { get; set; }
        private string myMasterPassword;
        private readonly IRepositoryService myRepositoryService;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                if(userName.Contains(" "))
                {
                    (SaveCommand as Command.Command).RaiseCanExecuteChanged();
                }
                PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
                
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                (SaveCommand as Command.Command).RaiseCanExecuteChanged();
            }
        }
        private string website;
        public string Website
        {
            get { return website; }
            set
            {
                website = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Website"));
                (SaveCommand as Command.Command).RaiseCanExecuteChanged();
            }
        }

        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Url"));
                (SaveCommand as Command.Command).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// ctor
        /// </summary>
        public HomePageViewModel(IRepositoryService repositoryService)
        {
            myRepositoryService = repositoryService;
            UserDataList = new ObservableCollection<UserData>() { };
            SaveCommand = new Command.Command(OnSave, CanSave);
            MessagingCenter.Subscribe<RegisterMasterPasswordPageViewModel, string>(this, "MasterPassword",
                                        (regPwdViewModel, masterPwd) => OnMasterPwdReceived(masterPwd));
            MessagingCenter.Subscribe<LoginPageViewModel, string>(this, "MasterPassword",
                                       (regPwdViewModel, masterPwd) => OnMasterPwdReceived(masterPwd));
            
        }

        private void FetchDataFromDb()
        {
            var userDataFromDb = myRepositoryService.FetchSavedUserData();
            if (userDataFromDb.Count > 0)
            {
                var decryptedUserDataList = new List<UserData> { };
                foreach (var item in userDataFromDb)
                {
                    decryptedUserDataList.Add(new UserData()
                    {
                        UserName = item.UserName,
                        Password = (item.Password).Decrypt(myMasterPassword),
                        Website = item.Website,
                        Url = item.Url
                    });
                }
                
                foreach (var item in decryptedUserDataList)
                {
                    UserDataList.Add(new UserData()
                    {
                        UserName = item.UserName,
                        Password = item.Password,
                        Website = item.Website,
                        Url = item.Url
                    });
                }
            }
        }

        private void OnSave(object parameter)
        {
                // Update the list to update UI
                UserDataList.Add(new UserData()
                {
                    UserName = this.UserName,
                    Password = this.Password,
                    Website = this.Website,
                    Url = this.Url
                });

                // encrypt and store to DB
                var encryptedUserData = new UserData()
                {
                    UserName = this.UserName,
                    Password = (this.Password).Encrypt(myMasterPassword),
                    Website = this.Website,
                    Url = this.Url
                };

                myRepositoryService.SaveEncrypedUserDataToDb(encryptedUserData);

                //Clear the fields
                this.UserName = String.Empty;
                this.Password = String.Empty;
                this.Website = String.Empty;
                this.Url = String.Empty;
            
        }


        private void OnMasterPwdReceived(string masterPwd)
        {
            myMasterPassword = masterPwd;
            FetchDataFromDb();

        }

        private bool CanSave(object parameter)
        {
            return !(string.IsNullOrWhiteSpace(this.UserName) || string.IsNullOrWhiteSpace(this.Password)
                    || string.IsNullOrWhiteSpace(this.Website) || string.IsNullOrWhiteSpace(this.Url));
        }

     
        public void Dispose()
        {
            MessagingCenter.Unsubscribe<RegisterMasterPasswordPageViewModel, string>(this, "MasterPassword");
            MessagingCenter.Unsubscribe<LoginPageViewModel, string>(this, "MasterPassword");
            SaveCommand = null;
            myMasterPassword = null;
        }
    }
}

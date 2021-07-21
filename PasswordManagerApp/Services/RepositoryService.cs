using System;
using System.Collections.Generic;
using System.Text;
using PasswordManagerApp.Models;
using Xamarin.Forms;

namespace PasswordManagerApp.Services
{
    public class RepositoryService : IRepositoryService
    {
        public void CreateUserDataTable()
        {
            DependencyService.Get<ISQLite>().CreateTable();
        }

        public List<UserData> FetchSavedUserData()
        {
            return DependencyService.Get<ISQLite>().FetchSavedUserData();
        }

        public void SaveEncrypedUserDataToDb(UserData encryptedUserData)
        {
            DependencyService.Get<ISQLite>().Save(encryptedUserData);
        }
    }
}

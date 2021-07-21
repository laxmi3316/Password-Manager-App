using PasswordManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManagerApp.Services
{
    public interface IRepositoryService
    {
        void CreateUserDataTable();
        List<UserData> FetchSavedUserData();
        void SaveEncrypedUserDataToDb(UserData userData);
    }
}

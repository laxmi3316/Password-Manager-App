using PasswordManagerApp.Models;
using SQLite;
using System.Collections.Generic;

namespace PasswordManagerApp
{
    public interface ISQLite
    {
        SQLiteConnection GetConnectionWithDatabase();
        void CreateTable();
        void Save(UserData userData);
        List<UserData> FetchSavedUserData();
        void CloseDbConnection();
    }
}

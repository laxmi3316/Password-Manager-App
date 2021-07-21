using PasswordManagerApp.Droid;
using PasswordManagerApp.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteDroid))]
namespace PasswordManagerApp.Droid
{
    public class SQLiteDroid : ISQLite
    {
        SQLiteConnection sqliteConn;

        public void CreateTable()
        {
            sqliteConn.CreateTable<UserData>();
        }

        public List<UserData> FetchSavedUserData()
        {
            string sql = "SELECT * FROM UserData ";
            List<UserData> userDataList = sqliteConn.Query<UserData>(sql);
            return userDataList;
        }
            
        public SQLiteConnection GetConnectionWithDatabase()
        {
            var dbName = "pwdmgrdb.sqlite";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbPath, dbName);
            sqliteConn= new SQLiteConnection(path);
            return sqliteConn;

        }

        public void Save(UserData userData)
        {
            sqliteConn.Insert(userData);
            
        }

        public void CloseDbConnection()
        {
            sqliteConn.Close();
        }

    }
}
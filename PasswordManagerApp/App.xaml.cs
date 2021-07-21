using PasswordManagerApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PasswordManagerApp
{
    public partial class App : Application, IDisposable
    {
        public App()
        {
            InitializeComponent();
            AppContainer.RegisterDependencies();
            DependencyService.Get<ISQLite>().GetConnectionWithDatabase();

            if (Application.Current.Properties.ContainsKey("IsFirstUse"))
            {
                MainPage = new LoginPage();
            }
            else
            {
                Application.Current.Properties["IsFirstUse"] = false;
                MainPage = new RegisterMasterPasswordPage();
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void Dispose()
        {
            DependencyService.Get<ISQLite>().CloseDbConnection();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasswordManagerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterMasterPasswordPage : ContentPage
    {
        public RegisterMasterPasswordPage()
        {
            InitializeComponent();

        }

        private void RegisterPwdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Contains(" "))
            {
                (sender as Entry).Text = e.OldTextValue;
            }
        }
    }
}
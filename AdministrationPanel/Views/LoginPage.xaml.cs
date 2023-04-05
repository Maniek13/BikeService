using Microsoft.Win32;
using System;
using ToDoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToDoApp.Views
{
    internal sealed partial class LoginPage : Page
    {
        internal LoginViewModel ViewModel { get; set; }
        internal LoginPage()
        {
            try
            {
                this.InitializeComponent();
                ViewModel = new LoginViewModel();
            }
            catch(Exception ex)
            {
                errorField.Text = ex.Message;
                errorField.Visibility = Visibility.Visible;
            }
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                errorField.Text = "";
                errorField.Visibility = Visibility.Collapsed;

                ViewModel.frame = Frame;
                ViewModel.User.Login = loginInput.Text;
                ViewModel.User.Password = paswordInput.Password;

                if(rememberUser.IsChecked == true)
                {
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                    localSettings.Values["username"] = loginInput.Text;
                    localSettings.Values["password"] = paswordInput.Password;
                }

                ViewModel.Login();
            }
            catch (Exception ex)
            {
                errorField.Text = ex.Message;
                errorField.Visibility = Visibility.Visible;
            }
        }
    }
}

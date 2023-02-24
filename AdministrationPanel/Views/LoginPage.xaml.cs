using System;
using ToDoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    internal sealed partial class LoginPage : Page
    {
        internal LoginViewModel viewModel { get; set; }
        internal LoginPage()
        {
            this.InitializeComponent();
            viewModel= new LoginViewModel();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                errorField.Text = "";
                errorField.Visibility = Visibility.Collapsed;

                viewModel.frame = Frame;
                viewModel.User.Login = loginInput.Text;
                viewModel.User.Password = paswordInput.Text;

                viewModel.Login(loginInput.Text, paswordInput.Text);
            }
            catch (Exception ex)
            {
                errorField.Text = ex.Message;
                errorField.Visibility = Visibility.Visible;
            }
        }
    }
}

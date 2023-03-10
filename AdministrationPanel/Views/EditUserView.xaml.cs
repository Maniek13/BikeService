using System;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToDoApp.Views
{
    internal sealed partial class EditUserView : Page
    {
        internal UserViewModel viewModel { get; set; }
        internal EditUserView()
        {
            InitializeComponent();
            viewModel = new UserViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel.User = e.Parameter as User;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginInput.Text))
                {
                    viewModel.User.Login = loginInput.Text;
                }
                if (!string.IsNullOrEmpty(paswordInput.Text))
                {
                    viewModel.User.Password = paswordInput.Text;
                }
                viewModel.EditUser();

                this.Content = null;
            }
            catch(Exception ex)
            { 
                ErrorEdit.Text = ex.Message;
                ErrorEdit.Visibility = Visibility.Visible;
            }
           
        }
    }
}

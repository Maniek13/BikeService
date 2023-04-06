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
        internal UserViewModel ViewModel { get; set; }
        internal EditUserView()
        {
            InitializeComponent();
            DataContext = ViewModel = new UserViewModel();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.User = e.Parameter as User;
        }
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginInput.Text))
                {
                    ViewModel.User.Login = loginInput.Text;
                }
                if (!string.IsNullOrEmpty(paswordInput.Password))
                {
                    ViewModel.User.Password = paswordInput.Password;
                }

                ViewModel.EditUser();
                
                this.Content = null;
            }
            catch(Exception ex)
            { 
                ErrorEdit.Text = ex.Message;
                ErrorEdit.Visibility = Visibility.Visible;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
    }
}

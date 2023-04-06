using System;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ToDoApp.Views
{
    internal sealed partial class DeleteUserView : Page
    {
        internal UserViewModel ViewModel { get; set; }
        internal DeleteUserView()
        {
            InitializeComponent();
            DataContext = ViewModel = new UserViewModel();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.User = e.Parameter as User;
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.DeleteUser();
                this.Content = null;
            }
            catch(Exception ex)
            {
                ErrorDelete.Text = ex.Message;
                ErrorDelete.Visibility = Visibility.Visible;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) 
        { 
            this.Content = null;
        }
    }
}

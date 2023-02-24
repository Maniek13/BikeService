using System;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    internal sealed partial class ListOfUser : Page
    {
        internal ListOfUserViewModel viewModel { get; set; }
        internal ListOfUser()
        {
            InitializeComponent();

            try 
            {
                viewModel = new ListOfUserViewModel();
                viewModel.GetUsers();
            }
            catch(Exception ex)
            {
                ErrorList.Text = ex.Message;
                ErrorList.Visibility= Visibility.Visible;
            }
            
        }

        private void ShowAddView_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowContent(MainContent ,typeof(AddUserView));
        }

        private void ShowEditView_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowContent(MainContent, typeof(EditUserView), ((Button)sender).Tag as User);
        }
    }
}

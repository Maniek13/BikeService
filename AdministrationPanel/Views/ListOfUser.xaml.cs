using System;
using System.Diagnostics;
using System.Linq;
using ToDoApp.Models;
using ToDoApp.ModelViews;
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
            viewModel = new ListOfUserViewModel();
        }

        private void ShowAddView_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowContent(MainContent ,typeof(AddUserView));
        }

        private void ShowEditView_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowContent(MainContent, typeof(EditUserView), (User)((Button)sender).Tag);
        }
    }
}

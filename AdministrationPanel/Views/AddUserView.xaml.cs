﻿using System;
using ToDoApp.Models;
using ToDoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.Views
{
    internal sealed partial class AddUserView : Page
    {
        internal UserViewModel viewModel { get; set; }
        internal AddUserView()
        {
            InitializeComponent();
            viewModel = new UserViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User()
                {
                    Login = loginInput.Text,
                    Password = paswordInput.Text
                };

                viewModel.User = user;
                viewModel.AddUser();

                this.Content = null;
            }
            catch (Exception ex)
            {
                ErrorAdd.Text = ex.Message;
                ErrorAdd.Visibility = Visibility.Visible;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
    }
}

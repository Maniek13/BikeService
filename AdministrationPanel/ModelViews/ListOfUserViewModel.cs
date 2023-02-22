using System;
using System.Collections.ObjectModel;
using System.Globalization;
using ToDoApp.Controller;
using ToDoApp.Models;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ModelViews
{
    public class ListOfUserViewModel
    {
        private ObservableCollection<User> _users { get; set; }
        public ObservableCollection<User> Users { get { return _users; } }

        public ListOfUserViewModel()
        {
            GetUsers();
        }

        internal void GetUsers()
        {
            _users = UserController.Get();
        }

        internal void ShowContent(Frame frame, Type typeOf, User user = null)
        {
            frame.Navigate(typeOf, user);
        }
    }
}

using System;
using System.Collections.ObjectModel;
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

        public void GetUsers()
        {
            _users = UserController.Get();
        }

        public void ShowContent(Frame frame, Type typeOf)
        {
            frame.Navigate(typeOf);
        }
    }
}

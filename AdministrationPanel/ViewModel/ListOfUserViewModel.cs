using System;
using System.Collections.ObjectModel;
using ToDoApp.Controller;
using ToDoApp.Models;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModel
{
    public class ListOfUserViewModel
    {
        private ObservableCollection<User> _users { get; set; }
        public ObservableCollection<User> Users { get { return _users; } }


        public ListOfUserViewModel()
        {
            _users = UserController.SetList();
        }

        public void GetUsers()
        {
            try
            {
                _users = UserController.GetUsers();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }

        internal void ShowContent(Frame frame, Type typeOf, User user = null)
        {
            frame.Navigate(typeOf, user);
        }
    }
}

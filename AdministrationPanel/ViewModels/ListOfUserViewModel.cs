using System;
using System.Collections.ObjectModel;
using ToDoApp.Controller;
using ToDoApp.Models;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModels
{
    public class ListOfUserViewModel
    {
        #region private members
        private ObservableCollection<User> _users { get; set; }
        private UserController _controler;
        #endregion

        public ObservableCollection<User> Users { get { return _users; } }
        public ListOfUserViewModel()
        {
            _controler = new UserController();
            _users = _controler.SetList();
        }

        public void GetUsers()
        {
            try
            {
                _users = _controler.GetUsers();
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

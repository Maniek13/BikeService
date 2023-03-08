using System;
using System.Collections.ObjectModel;
using ToDoApp.BaseClasses;
using ToDoApp.Models;
using ToDoApp.Settings;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModels
{
    public class ListOfUserViewModel
    {
        #region private members
        private ObservableCollection<User> _users { get; set; }
        private UserControllerBase _controler;
        #endregion

        public ObservableCollection<User> Users { get { return _users; } }
        public ListOfUserViewModel()
        {
            _controler = ControllersSettings.userController;
            _users = _controler.SetList();
        }

        internal void GetUsers()
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

        internal string GetUserName()
        {
            try
            {
                return _controler.User.Login;
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

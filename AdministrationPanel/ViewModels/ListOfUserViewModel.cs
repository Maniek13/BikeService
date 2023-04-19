using System;
using System.Collections.ObjectModel;
using ToDoApp.BaseClasses;
using ToDoApp.Models;
using ToDoApp.Settings;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModels
{
    public sealed class ListOfUserViewModel
    {
        #region private members
        private ObservableCollection<User> _users { get; set; }
        private readonly UserControllerBase _userControler;
        private readonly AdminControllerBase _adminController;
        #endregion

        public ObservableCollection<User> Users { get { return _users; } }
        public ListOfUserViewModel()
        {
            _userControler = ControllersSettings.userController;
            _adminController = ControllersSettings.adminController;
            _users = _userControler.SetList();
        }

        internal void GetUsers()
        {
            try
            {
                _users = _userControler.GetUsers(_adminController.Admin);
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
                return _adminController.Admin.Login;
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

        internal void LogOut()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove("username");
            localSettings.Values.Remove("password");

            _adminController.Admin = new User();
        }
    }
}

using System;
using ToDoApp.BaseClasses;
using ToDoApp.Models;
using ToDoApp.Settings;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModels
{
    public class LoginViewModel
    {
        #region private members
        private User _user = new User();
        private AdminControllerBase _adminController;
        #endregion

        public Frame frame = new Frame();
        public LoginViewModel() 
        {
            _adminController = ControllersSettings.adminController;
        }
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        internal void Login()
        {
            try
            {
                _adminController.Admin = _adminController.Login(_user.Login, _user.Password);

                frame.Navigate(typeof(Views.ListOfUser));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

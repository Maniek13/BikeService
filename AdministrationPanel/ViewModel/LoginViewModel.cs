using System;
using ToDoApp.Controller;
using ToDoApp.Models;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModel
{
    public class LoginViewModel
    {
        #region private members
        private User _user = new User();
        private UserController _controller;
        #endregion

        public Frame frame = new Frame();
        public LoginViewModel() 
        { 
            _controller = new UserController();
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

        internal void Login(string login, string password)
        {
            try
            {
                _controller.Login(login, password);

                frame.Navigate(typeof(Views.ListOfUser));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

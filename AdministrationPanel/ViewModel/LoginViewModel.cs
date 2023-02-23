using System;
using ToDoApp.Controller;
using ToDoApp.Models;
using Windows.UI.Xaml.Controls;

namespace ToDoApp.ViewModel
{
    public class LoginViewModel
    {
        private User _user = new User();
        public Frame frame = new Frame();
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

        internal void Login(string login, string passweord)
        {
            try
            {
                UserController.Login(login, passweord);

                frame.Navigate(typeof(Views.ListOfUser));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

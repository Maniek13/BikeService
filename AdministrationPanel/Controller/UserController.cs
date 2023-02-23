using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.Controller
{
    internal class UserController
    {
        #region private members
        private static User _user = new User();
        private static ObservableCollection<User> _users = new ObservableCollection<User>() { };
        #endregion

        internal static User User { get { return _user; } }
        internal ObservableCollection<User> SetList()
        {
            return _users;
        }
        internal ObservableCollection<User> GetUsers()
        {
            try
            {
                _users.Clear();
                ServiceController service = new ServiceController();
                List<User> users = service.GetUsers(_user);

                for(int i = 0; i < users.Count; i++)
                {
                    _users.Add(users[i]);
                }

                return _users;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal void AddUser(User user)
        {
            user.Id= _users.Count+1;
            _users.Add(user);
        }
        internal void EditUser(User user)
        {
            _users.RemoveAt(user.Id-1);
            _users.Insert(user.Id-1, user);
        }
        internal void Login(string login, string password)
        {
            try
            {
                _user.Login = login;
                _user.Password = password;

                ServiceController service = new ServiceController();
                User user = service.LogIn(_user);

                if (user.Id == 0)
                    throw new Exception("Błędne dane");


                _user.Id = user.Id;
                _user.AppId = user.AppId;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

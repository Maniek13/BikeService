using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.BaseClasses;
using ToDoApp.Models;
using ToDoApp.Providers;
using ToDoApp.Settings;

namespace ToDoApp.Controller
{
    internal class UserController : UserControllerBase
    {
        private ServiceProviderBase service = ProvidersSettings.bikeWebServiceProvider;

        #region internal function
        internal override ObservableCollection<User> SetList()
        {
            return _users;
        }
        internal override ObservableCollection<User> GetUsers()
        {
            try
            {
                _users.Clear();
                List<User> users = service.GetUsers(_user);

                for(int i = 0; i < users.Count; i++)
                    _users.Add(users[i]);

                return _users;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override void AddUser(User user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
        }
        internal override void EditUser(User user)
        {
            _users.RemoveAt(user.Id - 1);
            _users.Insert(user.Id - 1, user);
        }
        internal override void Login(string login, string password)
        {
            try
            {
                _user.Login = login;
                _user.Password = password;

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
        #endregion
    }
}

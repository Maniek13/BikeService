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
        internal override ObservableCollection<User> GetUsers(User admin)
        {
            try
            {
                _users.Clear();
                List<User> users = service.GetUsers(admin);

                for(int i = 0; i < users.Count; i++)
                    _users.Add(users[i]);

                return _users;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override void AddUser(User adnmin, User user)
        {
            try
            {
                service.AddUser(adnmin, user);

                user.Id = _users.Count + 1;
                _users.Add(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override void EditUser(User adnmin, User user)
        {
            try
            {
                service.EditUser(adnmin, user);

                _users.RemoveAt(user.Id - 1);
                _users.Insert(user.Id - 1, user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}

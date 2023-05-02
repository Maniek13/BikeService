using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoApp.BaseClasses;
using ToDoApp.Models;
using ToDoApp.Settings;

namespace ToDoApp.Controller
{
    internal sealed class UserController : UserControllerBase
    {
        private readonly ServiceProviderBase service = ProvidersSettings.bikeWebServiceProvider;

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
                user = service.AddUser(adnmin, user);
                _users.Add(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override void EditUser(User adnmin, User user, User oldUser)
        {
            try
            {
                int id = _users.IndexOf(user);
            
                user = service.EditUser(adnmin, user, oldUser);

                _users.RemoveAt(id);
                _users.Insert(id, user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override void DeleteUser(User adnmin, User user)
        {
            try
            {
                int index = _users.IndexOf(user);
                int resId = service.DeleteUser(adnmin, user.Id);

                
                _users.RemoveAt(index);

                if (resId == 0)
                    throw new Exception($"Użytkownik o id: {user.Id} nie istnieje");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.Controller
{
    internal class UserController
    {
        private static User User = new User();

        private static ObservableCollection<User> _users = new ObservableCollection<User>(){};
        internal static ObservableCollection<User> SetList()
        {
            return _users;
        }
        internal static ObservableCollection<User> GetUsers()
        {
            try
            {
                _users.Clear();
                List<User> users = ServiceController.GetUsers(User);

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
        internal static void AddUser(User user)
        {
            user.Id= _users.Count+1;
            _users.Add(user);
        }
        internal static void EditUser(User user)
        {
            _users.RemoveAt(user.Id-1);
            _users.Insert(user.Id-1, user);
        }
        internal static void Login(string login, string password)
        {
            try
            {
                User.Login = login;
                User.Password = password;

                User user = ServiceController.LogIn(User);

                if (user.Id == 0)
                    throw new Exception("Błędne dane");


                User.Id = user.Id;
                User.AppId = user.AppId;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

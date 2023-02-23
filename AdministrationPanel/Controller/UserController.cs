using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Controller
{
    internal class UserController
    {
        private static ObservableCollection<User> _users = new ObservableCollection<User>()
        {
            new User()
            {
                Id = 1,
                Login = "pierwszy",
                Password= "password1"
            },
             new User()
            {
                Id = 2,
                Login = "drugi",
                Password= "password2"
            },  new User()
            {
                Id= 3,
                Login = "trzeci",
                Password= "password3"
            }
        };

        internal static ObservableCollection<User> Get()
        {
            return _users;
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
        internal static void Login(User user)
        {
            try
            {
                user = ServiceController.LogIn(user);
                if (user.Id == 0)
                    throw new Exception("Błędne dane");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

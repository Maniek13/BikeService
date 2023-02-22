using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.Controller
{
    internal class UserController
    {
        private static ObservableCollection<User> users = new ObservableCollection<User>()
        {
            new User()
            {
                Login = "pierwszy",
                Password= "password1"
            },
             new User()
            {
                Login = "drugi",
                Password= "password2"
            },  new User()
            {
                Login = "trzeci",
                Password= "password3"
            }
        };

        internal static ObservableCollection<User> Get()
        {
            return users;
        }
        internal static void AddUser(User user)
        {
            users.Add(user);
        }
    }
}

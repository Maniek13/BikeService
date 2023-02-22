using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.Controller
{
    public class UserController
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

        public static ObservableCollection<User> Get()
        {
            return users;
        }
        public static void AddUser(User user)
        {
            users.Add(user);
        }
    }
}

using ToDoApp.Controller;
using ToDoApp.HelperClasses;
using ToDoApp.Models;

namespace ToDoApp.ModelViews
{
    public class UserViewModel : PropertyChange
    {
        private User _user = new User();
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        internal void AddUser(User user)
        {
            UserController.AddUser(user);
        }
    }
}

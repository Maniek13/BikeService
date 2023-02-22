using ToDoApp.Controller;
using ToDoApp.HelperClasses;
using ToDoApp.Models;
using Windows.Security.Authentication.OnlineId;
using Windows.UI.Xaml.Navigation;

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

        internal void EditUser()
        {
            if (User.Id != 0)
            {
                UserController.EditUser(User);
            }
        }

        internal void AddUser()
        {
            if (User.Id == 0)
            {
                UserController.AddUser(User);
            }
        }
    }
}

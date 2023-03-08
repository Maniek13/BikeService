using ToDoApp.BaseClasses;
using ToDoApp.HelperClasses;
using ToDoApp.Models;
using ToDoApp.Settings;

namespace ToDoApp.ViewModels
{
    public class UserViewModel : PropertyChange
    {
        #region private members
        private User _user = new User();
        private UserControllerBase _controller;
        #endregion

        public UserViewModel() 
        { 
            _controller = ControllersSettings.userController;
        }
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
            if (_user.Id != 0)
            {
                _controller.EditUser(_user);
            }
        }

        internal void AddUser()
        {
            if (_user.Id == 0)
            {
                _controller.AddUser(_user);
            }
        }
    }
}

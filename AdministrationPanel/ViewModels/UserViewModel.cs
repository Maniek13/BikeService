using ToDoApp.BaseClasses;
using ToDoApp.Controller;
using ToDoApp.HelperClasses;
using ToDoApp.Models;

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
            _controller = new UserController();
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
            if (User.Id != 0)
            {
                _controller.EditUser(User);
            }
        }

        internal void AddUser()
        {
            if (User.Id == 0)
            {
                _controller.AddUser(User);
            }
        }
    }
}

using System;
using ToDoApp.BaseClasses;
using ToDoApp.HelperClasses;
using ToDoApp.Models;
using ToDoApp.Settings;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace ToDoApp.ViewModels
{
    public sealed class UserViewModel : PropertyChange
    {
        #region private members
        private User _user = new User();
        private readonly UserControllerBase _userController;
        private readonly AdminControllerBase _adminController;
        #endregion

        public UserViewModel() 
        { 
            _userController = ControllersSettings.userController;
            _adminController = ControllersSettings.adminController;
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
                OnPropertyChanged();
            }
        }

        internal void EditUser()
        {
            try
            {
                if (_user.Id != 0)
                {
                    _userController.EditUser(_adminController.Admin, _user);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        internal void AddUser()
        {
            try
            {
                if (_user.Id == 0)
                {
                    _userController.AddUser(_adminController.Admin, _user);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        internal void DeleteUser()
        {
            try
            {
                if (_user.Id != 0)
                {
                    _userController.DeleteUser(_adminController.Admin, _user);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

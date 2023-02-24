using System.Collections.ObjectModel;
using ToDoApp.Models;

namespace ToDoApp.BaseClasses
{
    internal abstract class UserControllerBase
    {
        #region protected members
        protected static User _user = new User();
        protected static ObservableCollection<User> _users = new ObservableCollection<User>();
        #endregion

        #region abstract functions
        internal abstract ObservableCollection<User> SetList();
        internal abstract ObservableCollection<User> GetUsers();
        internal abstract void AddUser(User user);
        internal abstract void EditUser(User user);
        internal abstract void Login(string login, string password);
        #endregion
    }
}

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

        #region virtual properties
        internal virtual User User { get { return _user; } }
        #endregion

        #region abstract functions
        internal abstract ObservableCollection<User> SetList();
        internal abstract ObservableCollection<User> GetUsers(User admin);
        internal abstract void AddUser(User admin, User userToAdd);
        internal abstract void EditUser(User adnmin, User userToEdit, User oldUser);
        internal abstract void DeleteUser(User adnmin, User user);
        #endregion
    }
}

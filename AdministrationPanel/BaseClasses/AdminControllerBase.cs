using ToDoApp.Controller;
using ToDoApp.Models;

namespace ToDoApp.BaseClasses
{
    internal abstract class AdminControllerBase
    {
        protected static User _admin = new User();
        internal virtual User Admin
        {
            get { return AdminController._admin; }
            set { AdminController._admin = value; }
        }

        internal abstract User Login(string login, string password);
    }
}

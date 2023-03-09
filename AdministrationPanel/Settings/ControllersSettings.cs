using ToDoApp.BaseClasses;
using ToDoApp.Controller;

namespace ToDoApp.Settings
{
    internal class ControllersSettings
    {
        internal static AdminControllerBase adminController = new AdminController();
        internal static UserControllerBase userController = new UserController();
    }
}

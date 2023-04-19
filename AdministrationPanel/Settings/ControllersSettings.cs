using ToDoApp.BaseClasses;
using ToDoApp.Controller;

namespace ToDoApp.Settings
{
    internal sealed class ControllersSettings
    {
        internal static AdminControllerBase adminController = new AdminController();
        internal static UserControllerBase userController = new UserController();
    }
}
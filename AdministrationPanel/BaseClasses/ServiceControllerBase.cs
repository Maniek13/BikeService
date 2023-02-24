using System.Collections.Generic;

namespace ToDoApp.BaseClasses
{
    internal abstract class ServiceControllerBase
    {
        #region internal abstract functions
        internal abstract List<Models.User> GetUsers(Models.User user);
        internal abstract Models.User LogIn(Models.User user);
        #endregion
    }
}

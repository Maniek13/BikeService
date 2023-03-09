using BikeWebService.Models;
using System.Collections.Generic;

namespace BikeWebService.AbstractClasses
{
    internal abstract class UsersControllerAbstractClass
    {
        internal abstract void CheckIsUser(User user);
        internal abstract void CheckIsAdministratorUser(User user);
        internal abstract void AddUser(User user);
        internal abstract void Register(User user, string appKey);
        internal abstract void EditUser(User user);
        internal abstract List<User> GetAllUsers(User user);
        internal abstract void DeleteUser(int id);
    }
}
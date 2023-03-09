using BikeWebService.DbControllers;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeWebService.AbstractClasses
{
    internal abstract class UserDbControllerAbstractClass : BaseDbController
    {
        internal abstract void CheckIsUser(User user);
        internal abstract void CheckIsAministratorUser(User user);
        internal abstract void AddUser(User user);
        internal abstract void AddUser(User user, string appKey);
        internal abstract int EditUser(User user);
        internal abstract List<User> GetAllUsers(User user);
        internal abstract void DeleteUser(int id);
    }
}
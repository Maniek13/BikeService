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
        internal abstract int EditUser(string login, string password, int id);
        internal abstract List<User> GetAllUser(User user);
    }
}
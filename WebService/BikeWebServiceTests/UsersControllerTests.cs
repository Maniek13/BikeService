using BikeWebService.AbstractClasses.DbControllers;
using BikeWebService.Controllers;
using BikeWebService.DbControllers;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.PeerToPeer.Collaboration;
using System.Web.Configuration;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace BikeWebServiceTests
{
    #pragma warning disable CS0219
    #pragma warning disable IDE0059
    [TestClass]
    public class UsersControllerTests
    {
        //UsersController usersController = new UsersController(new UserDbController());
        [TestMethod]
        public void ValidateNoUser()
        {
            User user = null;

            //Assert.AreEqual("Brak przekazanego objektu", UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void ValidateNoPassword()
        {
            User user = new User()
            {
                Login = "test",
                Password = null
            };

            //Assert.AreEqual("Pole hasło nie może być puste", UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void ValidateNoLogin()
        {
            User user = new User()
            {
                Login = null,
                Password = "test"
            };

            //Assert.AreEqual("Pole login nie może być puste", UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void ValidateUser()
        {
            User user = new User()
            {
                Login = "test",
                Password = "test"
            };

            //Assert.AreEqual("OK", UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void CheckIsUser()
        {
            User user = new User()
            {
                Login = "test",
                Password = "12345"
            };

           // user = UsersController.CheckIsUser(user);

            Assert.AreEqual(1
                , user.Id);
        }
    }  
}

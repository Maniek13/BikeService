using BikeWebService.Controllers;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeWebServiceTests
{
    [TestClass]
    public class UsersControllerTests
    {
        [TestMethod]
        public void ValidateNoUser()
        {
            User user = null;

            Assert.AreEqual("Brak przekazanego objektu"
                , UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void ValidateNoPassword()
        {
            User user = new User()
            {
                Login = "test",
                Password = null
            };

            Assert.AreEqual("Pole hasło nie może być puste"
                , UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void ValidateNoLogin()
        {
            User user = new User()
            {
                Login = null,
                Password = "test"
            };

            Assert.AreEqual("Pole login nie może być puste"
                , UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void ValidateUser()
        {
            User user = new User()
            {
                Login = "test",
                Password = "test"
            };

            Assert.AreEqual("OK"
                , UsersController.ValidateUser(user));
        }

        [TestMethod]
        public void CheckIsUser()
        {
            User user = new User()
            {
                Login = "test",
                Password = "12345"
            };

            user = UsersController.CheckIsUser(user);

            Assert.AreEqual(1
                , user.Id);
        }
    }  
}

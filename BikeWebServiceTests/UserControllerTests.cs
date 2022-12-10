using BikeWebService.Controllers;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeWebServiceTests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void ValidateNoUser()
        {
            User user = null;
            HttpContextResponse HttpContextResponse = UserController.ValidateUser(user);

            Assert.AreEqual(400
                , HttpContextResponse.StatusCode);
            Assert.AreEqual("Brak przekazanego objektu"
                , HttpContextResponse.StatusDescription);
        }

        [TestMethod]
        public void ValidateNoPassword()
        {
            User user = new User()
            {
                Login = "test",
                Password = null
            };
            HttpContextResponse HttpContextResponse = UserController.ValidateUser(user);

            Assert.AreEqual(400
                , HttpContextResponse.StatusCode);
            Assert.AreEqual("Pole hasło nie może być puste"
                , HttpContextResponse.StatusDescription);
        }

        [TestMethod]
        public void ValidateNoLogin()
        {
            User user = new User()
            {
                Login = null,
                Password = "test"
            };
            HttpContextResponse HttpContextResponse = UserController.ValidateUser(user);

            Assert.AreEqual(400
                , HttpContextResponse.StatusCode);
            Assert.AreEqual("Pole login nie może być puste"
                , HttpContextResponse.StatusDescription);
        }

        [TestMethod]
        public void CheckIsUser()
        {
            User user = new User()
            {
                Login = "a",
                Password = "b"
            };

            user = UserController.CheckIsUser(user);

            Assert.AreEqual(1
                , user.Id);
        }
    }  
}

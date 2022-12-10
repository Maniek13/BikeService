using BikeWebService.Controllers;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeWebServiceTests
{
    [TestClass]
    public class DbControllerTests
    {
        [TestMethod]
        public void CheckIsUser()
        {
            User user = new User()
            {
                Login = "a",
                Password = "3e23e8160039594a33894f6564e1b1348bbd7a0088d42c4acb73eeaed59c009d"
            };

            DbController db = new DbController();
            Assert.AreNotEqual(db.CheckIsUser(user), 0);
        }

        [TestMethod]
        public void CheckIsUserWrongData()
        {
            User user = new User()
            {
                Login = "a",
                Password = "a"
            };

            DbController db = new DbController();
            Assert.AreEqual(db.CheckIsUser(user), 0);
        }
    }
}

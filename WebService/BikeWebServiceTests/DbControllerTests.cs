using BikeWebService.Controllers;
using BikeWebService.Interfaces;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                Login = "test",
                Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5"
            };

            BaseDbController db = new BaseDbController();
            Assert.AreNotEqual(0, db.CheckIsUser(user));
        }

        [TestMethod]
        public void CheckIsUserWrongData()
        {
            User user = new User()
            {
                Login = "a",
                Password = "a"
            };

            BaseDbController db = new BaseDbController();
            Assert.AreEqual(0, db.CheckIsUser(user));
        }
        [TestMethod]
        public void GetTask()
        {
            BaseDbController db = new BaseDbController();
            Order order = db.GetTask("11BS112345");


            Assert.IsInstanceOfType(order,
                typeof(Order)
                );
            Assert.AreEqual(1, order.taskID
               );
            Assert.AreEqual(1, order.appID
               );
            Assert.AreEqual("a", order.header
               );
            Assert.AreEqual("b", order.description
               );
            Assert.AreEqual(1, order.state
               );
            Assert.AreEqual("11BS112345", order.taskIDKey
               );
        }


        [TestMethod]
        public void GetTasks()
        {
            User user = new User()
            {
                Login = "test",
                Password = "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5"
            };

            BaseDbController db = new BaseDbController();
            List<Order> result = db.GetTasks(user);

            Assert.IsInstanceOfType(result, typeof(List<Order>));
            Assert.AreEqual(2, result.Count);
        }
    }
}

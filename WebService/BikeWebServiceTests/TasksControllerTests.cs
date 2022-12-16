using BikeWebService.Controllers;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeWebServiceTests
{
    [TestClass]
    public class TasksControllerTests
    {
        [TestMethod]
        public void FindTask()
        {
            string taskIDKey = "11BS112345";

            Order order = TasksController.FindTask(taskIDKey);

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

    }  
}

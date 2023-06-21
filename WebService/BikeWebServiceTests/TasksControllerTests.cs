using BikeWebService.AbstractClasses.DbControllers;
using BikeWebService.Controllers;
using BikeWebService.DbControllers;
using BikeWebService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BikeWebServiceTests
{
    [TestClass]
    public class TasksControllerTests
    {
        readonly TasksController tasksController = new TasksController(new TaskDbController());

        [TestMethod]
        public void FindTask()
        {
            string taskIDKey = "11BS112345";
            
            Order order = tasksController.FindTask(taskIDKey);

            Assert.IsInstanceOfType(order,
                typeof(Order)
                );
            Assert.AreEqual(1, order.TaskId
               );
            Assert.AreEqual(1, order.AppId
               );
            Assert.AreEqual("a", order.Header
               );
            Assert.AreEqual("b", order.Description
               );
            Assert.AreEqual(1, order.State
               );
            Assert.AreEqual("11BS112345", order.TaskIdKey
               );
        }
        [TestMethod]
        public void GetTasks()
        {
            User user = new User()
            {
                Login = "test",
                Password = "12345"
            };

            List<Order> result = tasksController.GetTasks(user);

            Assert.IsInstanceOfType(result, typeof(List<Order>));
            Assert.AreEqual(2, result.Count);
        }
    }  
}

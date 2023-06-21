using BikeWebService.Models;
using System.Collections.Generic;

namespace BikeWebService.AbstractClasses.Controllers
{
    internal abstract class TasksControllerAbstractClass
    {
        internal abstract Order FindTask(string taskIDKey);

        internal abstract List<Order> GetTasks(User user);

        internal abstract bool IsSame(Order oldOrder);

        internal abstract void AddTask(int appId, Order order);

        internal abstract void EditTask(Order order);

        internal abstract void DeleteTask(int id);
    }
}
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeWebService.AbstractClasses
{
    internal abstract class TaskDbControllerAbstractClass : BaseDbController
    {
        internal abstract Order GetTask(string taskKey);
        internal abstract List<Order> GetTasks(User user);
        internal abstract void AddOrder(Order order);
        internal abstract int EditOrder(Order order);
        internal abstract int DeleteOrder(int id);
        internal abstract bool IsSameOrder(Order order);
        internal abstract int IsOrder(int taskId);
    }
}

using BikeWebService.Interfaces;
using System;

namespace BikeWebService.Models
{
    public class Order : IOrder
    {
        public int taskID { get; set; } = 0;
        public int appID { get; set; } = 0;
        public string header { get; set; } = "";
        public string description { get; set; } = "";
        public int state { get; set; } = 0;
        public string taskIDKey { get; set; } = "";

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order p = (Order)obj;
                return (taskID == p.taskID);
            }
        }
    }
}
using BikeWebService.Interfaces;
using System;

namespace BikeWebService.Models
{
    public class Order : IOrder
    {
        public int TaskId { get; set; } = 0;
        public int AppId { get; set; } = 0;
        public string Header { get; set; } = "";
        public string Description { get; set; } = "";
        public int State { get; set; } = 0;
        public string TaskIdKey { get; set; } = "";

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Order p = (Order)obj;
                return (TaskId == p.TaskId);
            }
        }
    }
}
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
        public DateTime InitDate { get; set; } = new DateTime();

        public override bool Equals(object obj)
        {
            Order other = obj as Order;
            if(!this.TaskId.Equals(other.TaskId))
            {
                return false;
            }
            if (!this.AppId.Equals(other.AppId))
            {
                return false;
            }
            if (!this.Header.Equals(other.Header))
            {
                return false;
            }
            if (!this.Description.Equals(other.Description))
            {
                return false;
            }
            if (!this.State.Equals(other.State))
            {
                return false;
            }
            if (!this.TaskIdKey.Equals(other.TaskIdKey))
            {
                return false;
            }
            if (!this.InitDate.Equals(other.InitDate))
            {
                return false;
            }

            return true;
        }
    }
}
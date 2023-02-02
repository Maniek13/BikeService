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
        public DateTime InitDate { get; set; }
    }
}
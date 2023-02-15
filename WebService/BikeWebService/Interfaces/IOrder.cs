using System;

namespace BikeWebService.Interfaces
{
    public interface IOrder
    {
        int TaskId { get; set; }
        int AppId { get; set; }
        string Header { get; set; } 
        string Description { get; set; }
        int State { get; set; }
        string TaskIdKey { get; set; }

        DateTime? InitDate { get; set; }
    }
}

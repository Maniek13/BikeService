namespace BikeWebService.Interfaces
{
    public interface IOrder
    {
        int taskID { get; set; }
        int appID { get; set; }
        string header { get; set; } 
        string description { get; set; }
        int state { get; set; }
        string taskIDKey { get; set; }
    }
}

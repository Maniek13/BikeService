using BikeWebService.Interfaces;

namespace BikeWebService.Models
{
    public class Task : ITask
    {
        public int taskID { get; set; } = 0;
        public int appID { get; set; } = 0;
        public string header { get; set; } = "";
        public string description { get; set; } = "";
        public int state { get; set; } = 0;
        public string taskIDKey { get; set; } = "";
    }
}
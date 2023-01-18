using BikeWebService.Interfaces;

namespace BikeWebService.Models
{
    public class User : IUser
    {
        public int Id { get; set; } = 0;
        public int AppId { get; set; } = 0;
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
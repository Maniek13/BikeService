namespace BikeWebService.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}

namespace ToDoApp.Models
{
    public sealed class User
    {
        public int Id { get; set; } = 0;
        public string Login { get; set; } 
        public string Password { get; set; }
        public int AppId { get; set; }
    }
}

using BikeWebService.Interfaces;

namespace BikeWebService.Models
{
    public class User : IUser
    {
        public int Id { get; set; } = 0;
        public int AppId { get; set; } = 0;
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        public override bool Equals(object obj)
        {
            User other = obj as User;

            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            if (!this.Id.Equals(other.Id))
            {
                return false;
            }
            if (!this.AppId.Equals(other.AppId))
            {
                return false;
            }
            if (!this.Login.Equals(other.Login))
            {
                return false;
            }
            if (!this.Password.Equals(other.Password))
            {
                return false;
            }

            return true;
        }
    }
}
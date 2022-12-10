using BikeWebService.Classes;
using BikeWebService.Models;
using System;
namespace BikeWebService.Controllers
{
    public class UserController
    {
        static public string ValidateUser(User user)
        {

            if (Object.Equals(user, null))
            {
                return "Brak przekazanego objektu";
            }
            else if (String.IsNullOrEmpty(user.Login))
            {
                return "Pole login nie może być puste";
            }
            else if (String.IsNullOrEmpty(user.Password) )
            {
                return "Pole hasło nie może być puste";
            }
            else
            {
                return "OK";
            }
        }

        static public User CheckIsUser(User user)
        {
            try
            {
                user.Password = Crypto.EncryptSha256(user.Password);

                DbController dbController = new DbController();
                user.Id = dbController.CheckIsUser(user);
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

    }
}
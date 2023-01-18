using BikeWebService.Classes;
using BikeWebService.Models;
using System;

namespace BikeWebService.Controllers
{
    public class UsersController
    {
        static public string ValidateUser(User user)
        {
            if (Object.Equals(user, null))
            {
                throw new Exception("Brak przekazanego objektu");
            }
            else if (String.IsNullOrEmpty(user.Login))
            {
                throw new Exception("Pole login nie może być puste");
            }
            else if (String.IsNullOrEmpty(user.Password) )
            {
                throw new Exception("Pole hasło nie może być puste");
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
                user = dbController.CheckIsUser(user);

                if (user.Id.Equals(0))
                {
                    throw new Exception("Niepoprawne dane logowania");
                }

                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

    }
}
using BikeWebService.Classes;
using BikeWebService.Models;
using System;
using System.Web.Http.Results;
using System.Web.UI.WebControls;

namespace BikeWebService.Controllers
{
    public class UsersController
    {
        static public void ValidateUser(User user)
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

        static public User AddUser(User user)
        {
            try
            {
                user.Password = Crypto.EncryptSha256(user.Password);

                DbController dbController = new DbController();
                user = dbController.AddUser(user);

                if (user.Id.Equals(0))
                {
                    throw new Exception("Niepoprawne dane");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static public User EditUser(User user)
        {

            try
            {
                ValidateUser(user);
                user.Password = Crypto.EncryptSha256(user.Password);

                DbController dbController = new DbController();
               
                if(dbController.EditUser(user.Login, user.Password, user.Id) == 0)
                {
                    throw new Exception("Błąd edycji");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
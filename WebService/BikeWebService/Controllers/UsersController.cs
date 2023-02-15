using BikeWebService.Classes;
using BikeWebService.DbControllers;
using BikeWebService.Models;
using System;

namespace BikeWebService.Controllers
{
    public class UsersController
    {
        #region public functions

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


        static public void CheckIsUser(User user)
        {
            try
            {
                user.Password = Crypto.EncryptSha256(user.Password);

                UserDbController dbController = new UserDbController();
                dbController.CheckIsUser(user);

                if (user.Id.Equals(0))
                {
                    throw new Exception("Niepoprawne dane logowania");
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }


        static public void AddUser(User user)
        {
            try
            {
                user.Password = Crypto.EncryptSha256(user.Password);

                UserDbController dbController = new UserDbController();
                dbController.AddUser(user);

                if (user.Id.Equals(0))
                {
                    throw new Exception("Niepoprawne dane");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        static public void EditUser(User user)
        {

            try
            {
                ValidateUser(user);
                user.Password = Crypto.EncryptSha256(user.Password);

                UserDbController dbController = new UserDbController();
               
                if(dbController.EditUser(user.Login, user.Password, user.Id) == 0)
                {
                    throw new Exception("Błąd edycji");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
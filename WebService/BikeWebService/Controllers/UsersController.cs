using BikeWebService.Classes;
using BikeWebService.DbControllers;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal class UsersController
    {
        #region private functions
        static private void validateUser(User user)
        {
            if (Object.Equals(user, null))
            {
                throw new Exception("Brak przekazanego objektu");
            }
            else if (String.IsNullOrEmpty(user.Login))
            {
                throw new Exception("Pole login nie może być puste");
            }
            else if (String.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Pole hasło nie może być puste");
            }

            user.Password = Crypto.EncryptSha256(user.Password);
        }

        #endregion

        #region internal functions
        static internal void CheckIsUser(User user)
        {
            try
            {
                validateUser(user);                

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
        static internal void CheckIsAdministratorUser(User user)
        {
            try
            {
                validateUser(user);
                UserDbController dbController = new UserDbController();

                dbController.CheckIsAministratorUser(user);

                if (user.Id.Equals(0))
                {
                    throw new Exception("Niepoprawne dane logowania");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static internal void AddUser(User user)
        {
            try
            {
                validateUser(user);

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

        static internal void EditUser(User user)
        {

            try
            {
                validateUser(user);

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

        static internal List<User> GetAllUsers(User user)
        {
            try
            {
                validateUser(user);
                CheckIsAdministratorUser(user);

                UserDbController dbController = new UserDbController();
                return dbController.GetAllUser(user);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
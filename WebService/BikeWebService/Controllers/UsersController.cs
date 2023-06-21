using BikeWebService.AbstractClasses.Controllers;
using BikeWebService.AbstractClasses.DbControllers;
using BikeWebService.Classes;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal sealed class UsersController : UsersControllerAbstractClass
    {

        private readonly UserDbControllerAbstractClass _userDbController;
        internal UsersController(UserDbControllerAbstractClass service)
        {
            _userDbController = service;
        }

        #region private functions
        private void ValidateUser(User user)
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
        internal override void CheckIsUser(User user)
        {
            try
            {
                ValidateUser(user);
                _userDbController.CheckIsUser(user);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane logowania");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        internal override void CheckIsAdministratorUser(User user)
        {
            try
            {
                ValidateUser(user);
                _userDbController.CheckIsAministratorUser(user);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane logowania");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override void AddUser(User user)
        {
            try
            {
                ValidateUser(user);
                _userDbController.AddUser(user);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override void Register(User user, string appKey)
        {
            try
            {
                ValidateUser(user);

                _userDbController.AddUser(user, appKey);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override void EditUser(User user)
        {

            try
            {
                int id = 0;
                ValidateUser(user);

                id = _userDbController.EditUser(user);

                if ( id == 0)
                    throw new Exception("Błąd edycji");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override List<User> GetAllUsers(User user)
        {
            try
            {
                return _userDbController.GetAllUsers(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override void DeleteUser(int id)
        {
            try
            {
                _userDbController.DeleteUser(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override bool IsSame(User userOld)
        {
            try
            {
                ValidateUser(userOld);
                return _userDbController.IsSameUser(userOld);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
    }
}
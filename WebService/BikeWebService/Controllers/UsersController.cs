using BikeWebService.AbstractClasses;
using BikeWebService.Classes;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal class UsersController
    {
        private readonly object lockUser = new Lazy<object>(() =>
        {
            return new object();
        });

        private readonly UserDbControllerAbstractClass _userDbController;
        public UsersController(UserDbControllerAbstractClass service)
        {
            _userDbController = service;
        }

        #region private functions
        private void validateUser(User user)
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
        internal void CheckIsUser(User user)
        {
            try
            {
                validateUser(user);
                _userDbController.CheckIsUser(user);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane logowania");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        internal void CheckIsAdministratorUser(User user)
        {
            try
            {
                validateUser(user);
                _userDbController.CheckIsAministratorUser(user);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane logowania");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void AddUser(User user)
        {
            try
            {
                validateUser(user);
                _userDbController.AddUser(user);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void Register(User user, string appKey)
        {
            try
            {
                validateUser(user);
                _userDbController.AddUser(user, appKey);

                if (user.Id.Equals(0))
                    throw new Exception("Niepoprawne dane");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void EditUser(User user)
        {

            try
            {
                int id = 0;
                validateUser(user);

                lock (lockUser)
                {
                    id = _userDbController.EditUser(user);
                }

                if ( id == 0)
                    throw new Exception("Błąd edycji");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal List<User> GetAllUsers(User user)
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

        internal void DeleteUser(int id)
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
        #endregion
    }
}
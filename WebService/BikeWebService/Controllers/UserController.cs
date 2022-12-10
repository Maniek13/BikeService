using BikeWebService.Classes;
using BikeWebService.Models;
using System;
using System.Web;
using System.Web.Http;

namespace BikeWebService.Controllers
{
    public class UserController
    {
        static public HttpContextResponse ValidateUser(User user)
        {
            HttpContextResponse resp = new HttpContextResponse();

            if (Object.Equals(user, null))
            {
                resp.StatusCode = 400;
                resp.StatusDescription = "Brak przekazanego objektu";
            }
            else if (String.IsNullOrEmpty(user.Login))
            {
                resp.StatusCode = 400;
                resp.StatusDescription = "Pole login nie może być puste";
            }
            else if (String.IsNullOrEmpty(user.Password) )
            {
                resp.StatusCode = 400;
                resp.StatusDescription = "Pole hasło nie może być puste";
            }

            return resp;
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
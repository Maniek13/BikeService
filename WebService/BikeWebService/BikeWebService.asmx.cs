using BikeWebService.Controllers;
using BikeWebService.Models;
using System;
using System.Web.Http;
using System.Web.Services;

namespace BikeWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BikeWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public ResponseModel<User> LogIn(User user)
        {
            string message = UserController.ValidateUser(user);
            try
            {
                if (!message.Equals("OK"))
                {
                    throw new Exception(message);
                }
           
                ResponseModel<User> response = new ResponseModel<User>();

                user = UserController.CheckIsUser(user);

                if(user.Id.Equals(0)) 
                {
                    throw new Exception("Niepoprawne dane logowania");
                }

                response.message = "OK";
                response.resultCode = 1;
                response.Data = user;

                return response;

            }
            catch (Exception ex)
            {
                return new ResponseModel<User>()
                {
                    message = ex.Message,
                    resultCode = -1,
                    Data = null
                };
            }
        }
    }
}

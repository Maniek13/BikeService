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
            HttpContextResponse context = UserController.ValidateUser(user);

            try
            {
                if (!context.StatusCode.Equals(200))
                {
                    Context.Response.StatusCode = context.StatusCode;
                    Context.Response.StatusDescription = context.StatusDescription;
                    throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
                }
           
                ResponseModel<User> response = new ResponseModel<User>();

                user = UserController.CheckIsUser(user);

                if(user.Id.Equals(0)) 
                {
                    context.StatusDescription = "Niepoprawne dane logowania";
                    Context.Response.StatusDescription = "Niepoprawne dane logowania";
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
                    message = context.StatusDescription,
                    resultCode = -1,
                    Data = null
                };
            }
        }
    }
}

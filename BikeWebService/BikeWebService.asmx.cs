using BikeWebService.Models;
using System;
using System.Security.Authentication;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace BikeWebService
{
    /// <summary>
    /// Summary description for BikeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BikeWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public ResponseModel<User> LogIn(User user)
        {

            try
            {
                ResponseModel<User> response = new ResponseModel<User>();


                if (user == null)
                {
                    Context.Response.StatusCode = 204;
                    Context.Response.Status = "No content";
                    Context.Response.StatusDescription = "Brak przekazanego objektu";
                    throw new HttpResponseException(System.Net.HttpStatusCode.NoContent);
                }

                if (String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Login))
                {
                    Context.Response.StatusCode = 400;
                    Context.Response.Status = "Bad request";
                    Context.Response.StatusDescription = "Hasło i login nie może być puste";
                    throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
                }

                user.Id = 1;

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

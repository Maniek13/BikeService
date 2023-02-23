using System;
namespace ToDoApp.Controller
{
    internal class ServiceController
    {
        internal static Models.User LogIn(Models.User user)
        {
            try
            {
                BikeWebService.User serviceUser = ConvertToServiceUser(user);


                using (BikeWebService.BikeWebServiceSoapClient client = new BikeWebService.BikeWebServiceSoapClient(new BikeWebService.BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.LogInAsync(serviceUser);
                    service.Wait();
                    var res = service.Result;

                    if (res.Body.LogInResult.resultCode != 1)
                        throw new Exception(res.Body.LogInResult.message);

                    return new Models.User()
                    {
                        Id = res.Body.LogInResult.Data.Id,
                        Login = res.Body.LogInResult.Data.Login,
                        Password = res.Body.LogInResult.Data.Password,
                        AppId = res.Body.LogInResult.Data.AppId
                    };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static BikeWebService.User ConvertToServiceUser(Models.User user)
        {
            return new BikeWebService.User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                AppId = user.AppId
            };
        }
    }
}


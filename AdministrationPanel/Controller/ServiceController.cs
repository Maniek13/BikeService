using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Controller
{
    internal class ServiceController
    {
        internal List<Models.User> GetUsers(Models.User user)
        {
            try
            {
                BikeWebService.User serviceUser = ConvertToServiceUser(user);

                using (BikeWebService.BikeWebServiceSoapClient client = new BikeWebService.BikeWebServiceSoapClient(new BikeWebService.BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.GetUsersAsync(serviceUser);
                    service.Wait();
                    var res = service.Result;

                    if (res.Body.GetUsersResult.resultCode != 1)
                        throw new Exception(res.Body.GetUsersResult.message);
                    
                    var serviceUsers = res.Body.GetUsersResult.Data;

                    List<Models.User> users = new List<Models.User>();

                    for(int i = 0; i< serviceUsers.Count; i++)
                    {
                        BikeWebService.User tempUser = serviceUsers.ElementAt(i);
                        users.Add(ConvertToUser(tempUser));
                    }

                    return users;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        internal Models.User LogIn(Models.User user)
        {
            try
            {
                BikeWebService.User serviceUser = ConvertToServiceUser(user);

                using (BikeWebService.BikeWebServiceSoapClient client = new BikeWebService.BikeWebServiceSoapClient(new BikeWebService.BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.LogInAsAdministratorAsync(serviceUser);
                    service.Wait();
                    var res = service.Result;

                    if (res.Body.LogInAsAdministratorResult.resultCode != 1)
                        throw new Exception(res.Body.LogInAsAdministratorResult.message);

                    return new Models.User
                    {
                        Id = res.Body.LogInAsAdministratorResult.Data.Id,
                        Login = res.Body.LogInAsAdministratorResult.Data.Login,
                        Password = res.Body.LogInAsAdministratorResult.Data.Password,
                        AppId = res.Body.LogInAsAdministratorResult.Data.AppId
                    };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #region private function
        private BikeWebService.User ConvertToServiceUser(Models.User user)
        {
            return new BikeWebService.User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                AppId = user.AppId
            };
        }

        private Models.User ConvertToUser(BikeWebService.User user)
        {
            return new Models.User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                AppId = user.AppId
            };
        }
        #endregion
    }
}


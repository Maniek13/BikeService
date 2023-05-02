using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.BaseClasses;
using ToDoApp.BikeWebService;

namespace ToDoApp.Providers
{
    internal sealed class BikeWebServiceProvider : ServiceProviderBase
    {
        #region internal function
        internal override List<Models.User> GetUsers(Models.User user)
        {
            try
            {
                User serviceUser = ConvertToServiceUser(user);

                using (BikeWebServiceSoapClient client = new BikeWebServiceSoapClient(new BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.GetUsersAsync(serviceUser);
                    service.Wait();
                    var res = service.Result.Body.GetUsersResult;

                    if (res.resultCode != 1)
                        throw new Exception(res.message);
                    
                    var serviceUsers = res.Data;

                    List<Models.User> users = new List<Models.User>();

                    for(int i = 0; i< serviceUsers.Count; i++)
                    {
                        User tempUser = serviceUsers.ElementAt(i);
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
        internal override Models.User LogIn(Models.User user)
        {
            try
            {
                User serviceUser = ConvertToServiceUser(user);

                using (BikeWebServiceSoapClient client = new BikeWebServiceSoapClient(new BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.LogInAsAdministratorAsync(serviceUser);
                    service.Wait();
                    var res = service.Result.Body.LogInAsAdministratorResult;

                    if (res.resultCode != 1)
                        throw new Exception(res.message);

                    return new Models.User
                    {
                        Id = res.Data.Id,
                        Login = res.Data.Login,
                        Password = res.Data.Password,
                        AppId = res.Data.AppId
                    };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override Models.User EditUser(Models.User admin, Models.User userToEdit)
        {
            try
            {
                User adminEdit= ConvertToServiceUser(admin);
                User userEdit = ConvertToServiceUser(userToEdit);

                using (BikeWebServiceSoapClient client = new BikeWebServiceSoapClient(new BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.EditUserAsync(adminEdit, userEdit, null);
                    service.Wait();
                    var res = service.Result.Body.EditUserResult;

                    if (res.resultCode != 1)
                        throw new Exception(res.message);

                    return new Models.User
                    {
                        Id = res.Data.Id,
                        Login = res.Data.Login,
                        Password = "",
                        AppId = res.Data.AppId
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override Models.User AddUser(Models.User admin, Models.User userToAdd)
        {
            try
            {
                userToAdd.AppId = admin.AppId;
                User adminAdd = ConvertToServiceUser(admin);
                User userAdd = ConvertToServiceUser(userToAdd);

                using (BikeWebServiceSoapClient client = new BikeWebServiceSoapClient(new BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.AddUserAsync(adminAdd, userAdd);
                    service.Wait();
                    var res = service.Result.Body.AddUserResult;

                    if (res.resultCode != 1)
                        throw new Exception(res.message);

                    return new Models.User
                    {
                        Id = res.Data.Id,
                        Login = res.Data.Login,
                        Password = "",
                        AppId = res.Data.AppId
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        internal override int DeleteUser(Models.User admin, int id)
        {
            try
            {
                User adminAdd = ConvertToServiceUser(admin);

                using (BikeWebServiceSoapClient client = new BikeWebServiceSoapClient(new BikeWebServiceSoapClient.EndpointConfiguration()))
                {
                    var service = client.DeleteUserAsync(adminAdd, id);
                    service.Wait();
                    var res = service.Result.Body.DeleteUserResult;

                    if (res.resultCode != 1)
                        throw new Exception(res.message);

                    return res.Data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region private function
        private User ConvertToServiceUser(Models.User user)
        {
            return new User
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                AppId = user.AppId
            };
        }
        private Models.User ConvertToUser(User user)
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


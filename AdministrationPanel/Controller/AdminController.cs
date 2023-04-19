using System;
using ToDoApp.BaseClasses;
using ToDoApp.Models;
using ToDoApp.Settings;

namespace ToDoApp.Controller
{
    internal sealed class AdminController : AdminControllerBase
    {
        private readonly ServiceProviderBase service = ProvidersSettings.bikeWebServiceProvider;

        internal override User Login(string login, string password)
        {
            try
            {
                _admin.Login = login;
                _admin.Password = password;

                User user = service.LogIn(_admin);

                if (user.Id == 0)
                    throw new Exception("Błędne dane");

                _admin.Id = user.Id;
                _admin.AppId = user.AppId;

                return new User()
                {
                    Id = user.Id,
                    AppId = user.AppId,
                    Password = password,
                    Login = login
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}

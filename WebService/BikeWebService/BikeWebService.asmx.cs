using BikeWebService.Controllers;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Http.Results;
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
            try
            {
                UsersController.ValidateUser(user);

                ResponseModel<User> response = new ResponseModel<User>();

                response.message = "OK";
                response.resultCode = 1;
                response.Data = UsersController.CheckIsUser(user);

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

        [WebMethod]
        public ResponseModel<Order> GetTask(string taskIDKey)
        {
            try
            {
                Order task = TasksController.FindTask(taskIDKey);

                return new ResponseModel<Order>()
                {
                    message = "OK",
                    resultCode = 1,
                    Data = task
                };
            }
            catch (Exception ex) 
            {
                return new ResponseModel<Order>()
                {
                    message = ex.Message,
                    resultCode = -1,
                    Data = null
                };
            }
        }

        [WebMethod]
        public ResponseModel<List<Order>> GetTasks(User user)
        {
            try
            { 
                UsersController.ValidateUser(user);

                List<Order> result = TasksController.GetTasks(user);

                return new ResponseModel<List<Order>>()
                {
                    message = "OK",
                    resultCode = 1,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<Order>>()
                {
                    message = ex.Message,
                    resultCode = -1,
                    Data = null
                };
            }
        }

        [WebMethod]
        public ResponseModel<Order> AddOrder(User user, Order order)
        {
            try
            {
                UsersController.ValidateUser(user);
                user = UsersController.CheckIsUser(user);

                order = TasksController.AddTask(user, order);


                return new ResponseModel<Order>()
                {
                    message = "OK",
                    resultCode = 1,
                    Data = order
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<Order>()
                {
                    message = ex.Message,
                    resultCode = -1,
                    Data = null
                };
            }
        }
    } 
}

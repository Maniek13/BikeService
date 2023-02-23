using BikeWebService.Controllers;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace BikeWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BikeWebService : System.Web.Services.WebService
    {
        #region methods for user

        [WebMethod]
        public ResponseModel<User> LogIn(User user)
        {
            try
            {
                UsersController.CheckIsUser(user);

                ResponseModel<User> response = new ResponseModel<User>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = user
                };

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
        public ResponseModel<User> LogInAsAdministrator(User user)
        {
            try
            {
                UsersController.CheckIsAdministratorUser(user);

                ResponseModel<User> response = new ResponseModel<User>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = user
                };

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
        public ResponseModel<User> Register(User user)
        {
            try
            {
                UsersController.AddUser(user);

                ResponseModel<User> response = new ResponseModel<User>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = user
                };

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
        public ResponseModel<User> EditUser(User user, User newUser)
        {
            try
            {
                UsersController.CheckIsUser(user);
                UsersController.EditUser(newUser);

                ResponseModel<User> response = new ResponseModel<User>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = user
                };

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
        public ResponseModel<List<User>> GetUsers(User user) 
        {
            try
            {
                ResponseModel<List<User>> response = new ResponseModel<List<User>>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = UsersController.GetAllUsers(user)
                };

                return response;

            }
            catch (Exception ex)
            {
                return new ResponseModel<List<User>>()
                {
                    message = ex.Message,
                    resultCode = -1,
                    Data = null
                };
            }
        }
        #endregion

        #region methods for tasks

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
                UsersController.CheckIsUser(user);
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
                UsersController.CheckIsUser(user);

                if (order.AppId == 0)
                {
                    order.AppId = user.AppId;
                }

                TasksController.AddTask(order.AppId, order);

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

        [WebMethod]
        public ResponseModel<Order> EditOrder(User user, Order order, Order orderOld = null)
        {
            try
            {
                UsersController.CheckIsUser(user);

                if(orderOld != null )
                    if (!TasksController.IsSame(orderOld))
                        throw new Exception("Zamowienie zostało zmienione. Odświerz dane");

                TasksController.EditTask(order);

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
        [WebMethod]

        public ResponseModel<int> DeleteOrder(User user, int orderId)
        {
            try
            {
                UsersController.CheckIsUser(user);
                TasksController.DeleteTask(orderId);

                return new ResponseModel<int>()
                {
                    message = "OK",
                    resultCode = 1,
                    Data = orderId
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>()
                {
                    message = ex.Message,
                    resultCode = -1,
                    Data = -1
                };
            }
        }

        #endregion
    }
}

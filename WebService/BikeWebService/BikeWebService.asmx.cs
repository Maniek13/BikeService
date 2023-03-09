using BikeWebService.AbstractClasses;
using BikeWebService.Controllers;
using BikeWebService.DbControllers;
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
        private static readonly TasksController _tasksController;
        private static readonly UsersController _usersController;
        static BikeWebService()
        {
            _tasksController = new TasksController(new TaskDbController());
            _usersController = new UsersController(new UserDbController());
        }

        #region method for admin

        [WebMethod]
        public ResponseModel<User> LogInAsAdministrator(User user)
        {
            try
            {
                _usersController.CheckIsAdministratorUser(user);

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
        public ResponseModel<User> AddUser(User admin, User newUser)
        {
            try
            {
                _usersController.CheckIsAdministratorUser(admin);
                _usersController.AddUser(newUser);

                ResponseModel<User> response = new ResponseModel<User>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = newUser
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
                _usersController.CheckIsAdministratorUser(user);
                ResponseModel<List<User>> response = new ResponseModel<List<User>>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = _usersController.GetAllUsers(user)
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

        [WebMethod]
        public ResponseModel<int> DeleteUser(User user, int id)
        {
            try
            {
                _usersController.CheckIsAdministratorUser(user);
                _usersController.DeleteUser(id);

                return new ResponseModel<int>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = id
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

        #region methods for user

        [WebMethod]
        public ResponseModel<User> LogIn(User user)
        {
            try
            {
                _usersController.CheckIsUser(user);

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
        public ResponseModel<User> Register(User user, string appKey)
        {
            try
            {
                _usersController.Register(user, appKey);

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
                //check curent user is in database
                _usersController.CheckIsUser(user);

                //edit user
                _usersController.EditUser(newUser);

                ResponseModel<User> response = new ResponseModel<User>
                {
                    message = "OK",
                    resultCode = 1,
                    Data = newUser
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
        #endregion

        #region methods for tasks

        [WebMethod]
        public ResponseModel<Order> GetTask(string taskIDKey)
        {
            try
            {
                Order task = _tasksController.FindTask(taskIDKey);

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
                _usersController.CheckIsUser(user);
                List<Order> result = _tasksController.GetTasks(user);

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
                _usersController.CheckIsUser(user);

                if (order.AppId == 0)
                    order.AppId = user.AppId;

                _tasksController.AddTask(order.AppId, order);

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
                _usersController.CheckIsUser(user);

                if(orderOld != null && !_tasksController.IsSame(orderOld))
                    throw new Exception("Zamowienie zostało zmienione. Odświerz dane");

                _tasksController.EditTask(order);

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
                _usersController.CheckIsUser(user);
                _tasksController.DeleteTask(orderId);

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

using BikeWebService.Classes;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeWebService.Controllers
{
    public class TasksController
    {
        static public Order FindTask(string taskIDKey)
        {
            if (String.IsNullOrEmpty(taskIDKey))
            {
                throw new Exception("Brak identyfikatora zlecenia");
            }

            try
            {
                DbController dbController = new DbController();
                Order task = dbController.GetTask(taskIDKey);

                if (task == null)
                {
                    throw new Exception("Niepoprawny identyfikator zlecenia");
                }

                if (task.state == 0)
                {
                    throw new Exception("Zlecenie nie zostało dodane");
                }
                return task;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static public List<Order> GetTasks(User user)
        {
            try
            {
                user.Password = Crypto.EncryptSha256(user.Password);

                DbController dbController = new DbController();

                return dbController.GetTasks(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
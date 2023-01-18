using BikeWebService.Classes;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    public class TasksController
    {
        static private string ValidateTask(Order task)
        {
            if (Object.Equals(task, null))
            {
                throw new Exception("Brak przekazanego objektu");
            }
            else if (String.IsNullOrEmpty(task.description))
            {
                throw new Exception("Pole opis nie może być puste");
            }
            else if (String.IsNullOrEmpty(task.header))
            {
                throw new Exception("Pole tytuł nie może być puste");
            }
            else if (String.IsNullOrEmpty(task.state.ToString()))
            {
                throw new Exception("Pole status nie może być puste");
            }
            else if (String.IsNullOrEmpty(task.appID.ToString()))
            {
                throw new Exception("Prosze podać id aplikacji");
            }
            else
            {
                return "OK";
            }
        }

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

        static public Order AddTask(int appId, Order order)
        {
            
            try
            {
                order.appID = appId;
                order.state = 1;

                ValidateTask(order);

                DbController dbController = new DbController();
                order = dbController.AddOrder(order);

                if(order.taskID == 0)
                {
                    throw new Exception("Błąd zapisu zlecenia w bazie danych");
                }
                
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static public Order EditTask(Order order)
        {
            try
            {
                ValidateTask(order);

                DbController dbController = new DbController();

                if (dbController.EditOrder(order) == 0)
                {
                    throw new Exception("Błąd edycji zlecenia");
                }

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
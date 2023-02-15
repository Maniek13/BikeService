using BikeWebService.Classes;
using BikeWebService.DbControllers;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal class TasksController
    {
        #region private functions

        static private void validateTask(Order task)
        {
            if (Object.Equals(task, null))
            {
                throw new Exception("Brak przekazanego objektu");
            }
            else if (String.IsNullOrEmpty(task.Description))
            {
                throw new Exception("Pole opis nie może być puste");
            }
            else if (String.IsNullOrEmpty(task.Header))
            {
                throw new Exception("Pole tytuł nie może być puste");
            }
            else if (String.IsNullOrEmpty(task.State.ToString()))
            {
                throw new Exception("Pole status nie może być puste");
            }
            else if (String.IsNullOrEmpty(task.AppId.ToString()))
            {
                throw new Exception("Prosze podać id aplikacji");
            }
        }

        #endregion

        #region internal functions

        static internal Order FindTask(string taskIDKey)
        {
            if (String.IsNullOrEmpty(taskIDKey))
            {
                throw new Exception("Brak identyfikatora zlecenia");
            }

            try
            {
                TaskDbController dbController = new TaskDbController();
                Order task = dbController.GetTask(taskIDKey);

                if (task == null)
                {
                    throw new Exception("Niepoprawny identyfikator zlecenia");
                }

                if (task.State == 0)
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

        static internal List<Order> GetTasks(User user)
        {
            try
            {

                TaskDbController dbController = new TaskDbController();

                return dbController.GetTasks(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        static internal bool IsSame(Order oldOrder)
        {

            try
            {
                TaskDbController dbController = new TaskDbController();
                return dbController.IsSameOrder(oldOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        static internal void AddTask(int appId, Order order)
        {
            
            try
            {
                order.AppId = appId;

                if(order.State != 0)
                {
                    order.State = 1;
                }
                
                validateTask(order);

                TaskDbController dbController = new TaskDbController();
                dbController.AddOrder(order);

                if(order.TaskId == 0)
                {
                    throw new Exception("Błąd zapisu zlecenia w bazie danych");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static internal void EditTask(Order order)
        {
            try
            {
                validateTask(order);

                TaskDbController dbController = new TaskDbController();

                if (dbController.EditOrder(order) == 0)
                {
                    throw new Exception("Błąd edycji zlecenia");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static internal void DeleteTask(int id)
        {
            try
            {
                TaskDbController dbController = new TaskDbController();

                if (dbController.DeleteOrder(id) == 0)
                {
                    throw new Exception("Błąd usuwania zlecenia");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion 
    }
}
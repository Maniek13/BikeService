using BikeWebService.AbstractClasses;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal class TasksController : TasksControllerAbstractClass
    {
        readonly object lockTask  = new Lazy<object>(() =>
        {
            return  new object ();
        });

        private readonly TaskDbControllerAbstractClass _taskDbController;
        public TasksController(TaskDbControllerAbstractClass service)
        {
            _taskDbController = service;
        }

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

        internal override Order FindTask(string taskIDKey)
        {
            if (String.IsNullOrEmpty(taskIDKey))
                throw new Exception("Brak identyfikatora zlecenia");

            try
            {
                Order task = _taskDbController.GetTask(taskIDKey);

                if (task == null)
                    throw new Exception("Niepoprawny identyfikator zlecenia");

                if (task.State == 0)
                    throw new Exception("Zlecenie nie zostało dodane");

                return task;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override List<Order> GetTasks(User user)
        {
            try
            {
                return _taskDbController.GetTasks(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        internal override bool IsSame(Order oldOrder)
        {
            try
            {
                return _taskDbController.IsSameOrder(oldOrder);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        internal override void AddTask(int appId, Order order)
        {
            
            try
            {
                order.AppId = appId;

                if (order.State != 0)
                {
                    order.State = 1;
                }

                validateTask(order);

                _taskDbController.AddOrder(order);

                if (order.TaskId == 0)
                    throw new Exception("Błąd zapisu zlecenia w bazie danych");                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override void EditTask(Order order)
        {
            try
            {
                int id = 0;
                validateTask(order);

                lock (lockTask)
                {
                    id = _taskDbController.EditOrder(order);
                }

                if (id  == 0)
                    throw new Exception("Błąd edycji zlecenia");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override void DeleteTask(int id)
        {
            try
            {
                if (_taskDbController.DeleteOrder(id) == 0) 
                    throw new Exception("Błąd usuwania zlecenia");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion 
    }
}
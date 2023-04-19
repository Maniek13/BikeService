﻿using BikeWebService.AbstractClasses;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BikeWebService.DbControllers
{
    internal sealed class TaskDbController : TaskDbControllerAbstractClass
    {
        #region internal function
        internal override Order GetTask(string taskKey)
        {
            try
            {
                string query = @"
                    SELECT * FROM tasks 
                    WHERE taskIDKey = @taskKey;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskKey",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = taskKey
                        });

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Object[] values = new Object[reader.FieldCount];
                                reader.GetValues(values);

                                return ConvertToTask(values);
                            }

                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        internal override List<Order> GetTasks(User user)
        {
            List<Order> tasks = new List<Order>();

            try
            {
                string query = @"
                    SELECT t.taskID, t.appID, t.header, t.description, t.state, t.taskIDKey, t.initDate FROM tasks t
                    JOIN users u ON t.appID = u.appID
                    WHERE login = @login AND password = @password
                    ORDER BY t.state, t.initDate, t.header ASC";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@login",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = user.Login
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@password",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = user.Password
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Object[] values = new Object[reader.FieldCount];
                                reader.GetValues(values);
                                tasks.Add(ConvertToTask(values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return tasks;
        }

        internal override void AddOrder(Order order)
        {
            order.State = 1;

            int taskId = 0;
            DateTime initDate = new DateTime();
            string taskIdKey = "";
           
            try
            {
                string query = @"
                    DECLARE @table table([taskID] INT)

                    INSERT INTO tasks 
                        (appID, header, description, state)
                    OUTPUT Inserted.taskID INTO @table
                    VALUES
                        (@appId, @header, @description, @state)
                    
                    SELECT taskID FROM @table";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@appId",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = order.AppId
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@header",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = order.Header
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@description",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = order.Description
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@state",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = order.State
                        });


                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                int.TryParse(reader["taskID"].ToString(), out taskId);
                        }
                    }
                }

                order.TaskId = taskId;

                
                SetOrderKeyAndData(taskId, ref taskIdKey, ref initDate);
                order.TaskIdKey = taskIdKey;
                order.InitDate= initDate;
                
               

                //in triger on insert
                //order.taskIDKey = GenerateOrderKey(order.appID, taskId);
                //SetOrderKey(order.taskIDKey, taskId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override int EditOrder(Order order)
        {
            try
            {
                int result = 0;
                string query = @"
                    UPDATE tasks 
                    SET header = @header, description = @description, state = @state
                    OUTPUT INSERTED.taskID
                    WHERE taskID = @taskID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@header",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = order.Header
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@description",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = order.Description
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@state",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = order.State
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskID",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = order.TaskId
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                int.TryParse(reader["taskID"].ToString(), out result);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override int DeleteOrder(int id)
        {
            try
            {
                int result = 0;

                if (IsOrder(id) == 0)
                    return result;

                string query = @"
                    DELETE FROM tasks
                    WHERE taskID = @taskID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskID",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = id
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader()){}

                        result = id;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override bool IsSameOrder(Order order)
        {
            
            try
            {
                string query = @"
                SELECT * FROM tasks 
                WHERE taskID = @taskId;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskId",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = order.TaskId
                        });

                        connection.Open();


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Object[] values = new Object[reader.FieldCount];
                                reader.GetValues(values);
                                return order.Equals(ConvertToTask(values));
                            }

                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }

        internal override int IsOrder(int taskId)
        {
            try
            {
                int id = 0;
                string query = @"
                    SELECT taskID FROM tasks 
                    WHERE taskID = @taskId;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskId",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = taskId
                        });

                        connection.Open();


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                int.TryParse(reader["taskID"].ToString(), out id);
                        }
                    }
                }

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region private functions

        private void SetOrderKeyAndData(int id, ref string taskIdKey, ref DateTime initDate)
        {
            try
            {
                string query = @"
                    SELECT taskIDKey, initDate FROM tasks 
                    WHERE taskID = @taskID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskID",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = id
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                taskIdKey = reader["taskIDKey"].ToString();
                                DateTime.TryParse(reader["initDate"].ToString(), out initDate);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Obsolete("Is created in the insert triger, please don't use", true)]
        private void SetOrderKey(string orderKey, int id)
        {
            try
            {
                string query = @"
                UPDATE tasks SET
                    taskIDKey = @taskIDKey
                WHERE 
                    taskID = @taskID";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskIDKey",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = orderKey
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@taskID",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = id
                        });

                        connection.Open(); command.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [Obsolete("Key is generate in database")]
        private string GenerateOrderKey(int appId, int taskId)
        {
            try
            {
                string appKey = GetAppKey(appId);

                if (String.IsNullOrEmpty(appKey))
                    return "";

                Random random = new Random();

                int number = random.Next(1, 1000000);

                string orderKey = $"{taskId}{appKey}{number}";
                return orderKey;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetAppKey(int appId)
        {
            string appKey = "";

            try
            {
                string query = @"
                    SELECT appkey FROM app 
                    WHERE appID = @appID";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@appID",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = appId
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                appKey = reader["appkey"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return appKey;
        }

        private Order ConvertToTask(Object[] obj)
        {
            if (obj == null || obj.Length == 0)
                return null;

            DateTime.TryParse(obj[6].ToString(), out DateTime date);
            return new Order()
            {
                TaskId = Convert.ToInt32(obj[0].ToString()),
                AppId = Convert.ToInt32(obj[1].ToString()),
                Header = obj[2].ToString(),
                Description = obj[3].ToString(),
                State = Convert.ToInt32(obj[4].ToString()),
                TaskIdKey = obj[5].ToString(),
                InitDate = date
            };
        }

        #endregion
    }
}
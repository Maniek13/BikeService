using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BikeWebService.DbControllers
{
    public class TaskDbController : BaseDbController
    {
        public Order GetTask(string taskKey)
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
        public List<Order> GetTasks(User user)
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
        public Order AddOrder(Order order)
        {

            try
            {
                string query = @"
                    DECLARE @table table([taskID] INT, [taskIDKey] NVARCHAR(MAX), [initDate] DATETIME)

                    INSERT INTO tasks 
                        (appID, header, description, state)
                    OUTPUT Inserted.taskID, Inserted.taskIDKey, Inserted.initDate INTO @table
                    VALUES
                        (@appId, @header, @description, 1)
                    
                    SELECT taskID, taskIDKey, initDate FROM @table";
                int taskId = 0;
                DateTime initDate = new DateTime();

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

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int.TryParse(reader["taskID"].ToString(), out taskId);
                                DateTime.TryParse(reader["initDate"].ToString(), out initDate);
                            }
                        }
                    }
                }

                order.TaskId = taskId;
                order.TaskIdKey = GetOrderKey(taskId);
                order.InitDate= initDate;

                //in triger on insert
                //order.taskIDKey = GenerateOrderKey(order.appID, taskId);
                //SetOrderKey(order.taskIDKey, taskId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return order;
        }

        public int EditOrder(Order order)
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
                            {
                                int.TryParse(reader["taskID"].ToString(), out result);
                            }
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
        public int DeleteOrder(int id)
        {
            try
            {
                int result = 0;

                if (IsOrder(id) == 0)
                {
                    return result;
                }

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
        public int IsOrder(int taskId)
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
                            {
                                int.TryParse(reader["taskID"].ToString(), out id);
                            }
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
        private string GetOrderKey(int id)
        {

            string taskIdKey = "";

            try
            {
                string query = @"
                    SELECT taskIDKey FROM tasks 
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return taskIdKey;
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
                {
                    return "";
                }

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
                            {
                                appKey = reader["appkey"].ToString();
                            }
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
            {
                return null;
            }

            DateTime date = new DateTime();
            DateTime.TryParse(obj[6].ToString(), out date);
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
    }
}
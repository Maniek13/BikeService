using BikeWebService.Classes;
using BikeWebService.Interfaces;
using BikeWebService.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
namespace BikeWebService.Controllers
{
    public class DbController
    {
        private readonly string _connectionString;

        public DbController() 
        {
            _connectionString = Settings.GetConnectionString();
        }
        public User CheckIsUser(User user)
        {
            string query = @"  
                SELECT userID, appID FROM users 
                WHERE login = @login AND password = @password;";
            int id = 0;
            int appId = 0;

            try
            {
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
                            if (reader.Read())
                            {
                                int.TryParse(reader["userID"].ToString(), out id);
                                int.TryParse(reader["appID"].ToString(), out appId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            user.Id = id;
            user.AppId= appId;
            return user;
        }
        public User AddUser(User user)
        {
            try
            {
                string query = @"
                    INSERT INTO users 
                        (login, password, appID)
                    OUTPUT Inserted.userID
                    VALUES
                        (@login, @password, @appID)";
                int userId = 0;

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
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@appID",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = user.AppId
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int.TryParse(reader["userID"].ToString(), out userId);
                            }
                        }
                    }
                }

                user.Id = userId;

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int EditUser(string login, string password, int id)
        {
            try
            {
                string query = @"
                    UPDATE users 
                    SET
                        login = @login, 
                        password = @password
                    WHERE
                         userID = @userId";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@login",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = login
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@password",
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Value = password
                        });
                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@userId",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = id
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
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
            catch(Exception ex)
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
                    SELECT t.taskID, t.appID, t.header, t.description, t.state, t.taskIDKey FROM tasks t
                    JOIN users u ON t.appID = u.appID
                    WHERE login = @login AND password = @password";

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
                    DECLARE @table table([taskID] INT, [taskIDKey] NVARCHAR(MAX))

                    INSERT INTO tasks 
                        (appID, header, description, state)
                    OUTPUT Inserted.taskID, Inserted.taskIDKey INTO @table
                    VALUES
                        (@appId, @header, @description, 1)
                    
                    SELECT taskID, taskIDKey FROM @table";
                int taskId = 0;

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
                            }
                        }
                    }
                }

                order.TaskId = taskId;
                order.TaskIdKey = GetOrderKey(taskId);

                //order.taskIDKey = GenerateOrderKey(order.appID, taskId);
                //SetOrderKey(order.taskIDKey, taskId);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return order;
        }
        public int EditOrder(Order order)
        {
            try
            {
                string query = @"
                    UPDATE tasks 
                    SET header = @header, description = @description, state = @state
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
                        command.ExecuteReader();
                    }
                }

                return 1;
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
       
        }
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

                string orderKey =  $"{taskId}{appKey}{number}";
                return orderKey;
            }
            catch(Exception ex )
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

            return new  Order()
            {
                TaskId = Convert.ToInt32(obj[0].ToString()),
                AppId = Convert.ToInt32(obj[1].ToString()),
                Header = obj[2].ToString(),
                Description = obj[3].ToString(),
                State = Convert.ToInt32(obj[4].ToString()),
                TaskIdKey = obj[5].ToString()
            };
        }
    }
}
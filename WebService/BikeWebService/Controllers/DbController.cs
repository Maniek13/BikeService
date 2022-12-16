using BikeWebService.Classes;
using BikeWebService.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Xml;
namespace BikeWebService.Controllers
{
    public class DbController
    {
        private string _connectionString;

        public DbController() 
        {
            
            _connectionString = Settings.GetConnectionString();
        }

        public int CheckIsUser(User user)
        {

            string query = @"  
                SELECT userID FROM users 
                WHERE login = @login AND password = @password;";
            int id = 0;

            try
            {
                string pathToXml = AppDomain.CurrentDomain.BaseDirectory;
                XmlDocument doc = new XmlDocument();
                pathToXml = Path.Combine(pathToXml, "Settings.xml");
                doc.Load(pathToXml);
                XmlNodeList nList = doc.SelectNodes("/Settings/ConnectionOptions/ConnectionString");


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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return id;
        }

        public Task GetTask(string taskKey)
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

        public List<Task> GetTasks(User user)
        {
            List<Task> tasks = new List<Task>();

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

        private Task ConvertToTask(Object[] obj)
        {
            
            if (obj == null || obj.Length == 0) 
            {
                return null;
            }

            return new Task()
            {
                taskID = Convert.ToInt32(obj[0].ToString()),
                appID = Convert.ToInt32(obj[1].ToString()),
                header = obj[2].ToString(),
                description = obj[3].ToString(),
                state = Convert.ToInt32(obj[4].ToString()),
                taskIDKey = obj[5].ToString()
            };

        }
    }
}
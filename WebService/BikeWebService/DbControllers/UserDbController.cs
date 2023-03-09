﻿using BikeWebService.AbstractClasses;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BikeWebService.DbControllers
{
    internal class UserDbController : UserDbControllerAbstractClass
    {
        #region internal functions

        internal override void CheckIsUser(User user)
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
            user.AppId = appId;
        }

        internal override void CheckIsAministratorUser(User user)
        {
            string query = @"  
                SELECT u.userID, u.appID FROM administrators as a
                JOIN users as u ON u.userID = a.userID
                WHERE u.login = @login AND u.password = @password;";
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
            user.AppId = appId;
        }

        internal override void AddUser(User user)
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
                                int.TryParse(reader["userID"].ToString(), out userId);
                        }
                    }
                }

                user.Id = userId;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal override int EditUser(User user)
        {
            try
            {
                int result = 0;
                string query = @"
                    UPDATE users 
                    SET
                        login = @login, 
                        password = @password
                    OUTPUT INSERTED.userID
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
                            ParameterName = "@userId",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value = user.Id
                        });

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                int.TryParse(reader["userID"].ToString(), out result);
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

        internal override List<User> GetAllUser(User user)
        {
            string query = @"SELECT * FROM users";

            List<User> users = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Object[] values = new Object[reader.FieldCount];
                                reader.GetValues(values);
                                users.Add(convertToTask(values));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return users;
            #endregion
        }

        #region private functions
        private User convertToTask(Object[] obj)
        {
            if (obj == null || obj.Length == 0)
                return null;

            return new User()
            {
                Id = Convert.ToInt32(obj[0].ToString()),
                Login = obj[1].ToString(),
                Password = "",
                AppId = Convert.ToInt32(obj[3].ToString())
            };
        }
        #endregion
    }
}
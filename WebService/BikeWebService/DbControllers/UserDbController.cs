using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BikeWebService.DbControllers
{
    internal class UserDbController : BaseDbController
    {
        #region internal functions

        internal void CheckIsUser(User user)
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

        internal void AddUser(User user)
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
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal int EditUser(string login, string password, int id)
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
                            if (reader.Read())
                            {
                                int.TryParse(reader["userID"].ToString(), out result);
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

        #endregion
    }
}
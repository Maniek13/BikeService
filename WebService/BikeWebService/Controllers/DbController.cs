using BikeWebService.Classes;
using BikeWebService.Models;
using Microsoft.Data.SqlClient;
using System;
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
    }
}
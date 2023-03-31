using BikeWebService.AbstractClasses;
using BikeWebService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BikeWebService.DbControllers
{
    internal class CompaniesDbController : CompaniesDbControllerAbstractClass
    {
        internal override List<Company> GetCompanies()
        {
            try
            {
                List<Company> companies = new List<Company>();

                string query = @"
                    SELECT * FROM app;";

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
                                companies.Add(convertToCompany(values));
                            }

                            return companies;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Company convertToCompany(Object[] data)
        {
            if (data == null || data.Length == 0)
                return null;

            return new Company()
            {
                appID = Convert.ToInt32(data[0].ToString()),
                name = data[1].ToString(),
                appKey = ""
            };
        }
    }
}
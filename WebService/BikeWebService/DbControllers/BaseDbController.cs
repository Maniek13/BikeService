using BikeWebService.Classes;
using Microsoft.Data.SqlClient;
using System;

namespace BikeWebService.DbControllers
{
    public abstract class BaseDbController
    {
        internal readonly string _connectionString;

        public BaseDbController() 
        {
            _connectionString = Settings.GetConnectionString();
        }
    }
}
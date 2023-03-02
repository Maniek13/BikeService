using BikeWebService.Classes;
using Microsoft.Data.SqlClient;
using System;

namespace BikeWebService.AbstractClasses
{
    internal abstract class BaseDbController
    {
        internal readonly string _connectionString;
        internal BaseDbController() 
        {
            _connectionString = Settings.GetConnectionString();
        }
    }
}
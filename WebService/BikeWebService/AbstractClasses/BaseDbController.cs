using BikeWebService.Classes;

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
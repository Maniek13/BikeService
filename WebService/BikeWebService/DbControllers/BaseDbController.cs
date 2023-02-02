using BikeWebService.Classes;

namespace BikeWebService.DbControllers
{
    public class BaseDbController
    {
        internal readonly string _connectionString;

        public BaseDbController() 
        {
            _connectionString = Settings.GetConnectionString();
        }
    }
}
using BikeWebService.Classes;

namespace BikeWebService.AbstractClasses.DbControllers
{
    internal abstract class BaseDbController
    {
        protected readonly string _connectionString;
        protected BaseDbController() 
        {
            _connectionString = Settings.GetConnectionString();
        }
    }
}
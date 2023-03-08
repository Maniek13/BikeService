using ToDoApp.BaseClasses;
using ToDoApp.Providers;

namespace ToDoApp.Settings
{
    internal class ProvidersSettings
    {
        internal static ServiceProviderBase bikeWebServiceProvider = new BikeWebServiceProvider();
    }
}

using ToDoApp.BaseClasses;
using ToDoApp.Providers;

namespace ToDoApp.Settings
{
    internal sealed class ProvidersSettings
    {
        internal static ServiceProviderBase bikeWebServiceProvider = new BikeWebServiceProvider();
    }
}
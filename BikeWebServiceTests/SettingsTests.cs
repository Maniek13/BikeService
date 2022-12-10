using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeWebServiceTests
{
    [TestClass]
    public class SettingsTests
    {

        [TestMethod]
        public void GetConectionString()
        {
            Assert.AreEqual("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"G:\\Programowanie\\Serwis rowerowy\\BikeWebServiceTests\\TestDataBase.mdf\"; trusted_connection=true;encrypt=false", BikeWebService.Classes.Settings.GetConnectionString());
        }
    }
}

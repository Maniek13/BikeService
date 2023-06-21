using System;
using System.IO;
using System.Xml;

namespace BikeWebService.Classes
{
    internal sealed class Settings
    {

        internal static string GetConnectionString()
        {
            XmlDocument doc = new XmlDocument();
            string pathToXml = AppDomain.CurrentDomain.BaseDirectory;
            pathToXml = Path.Combine(pathToXml, "Settings.xml");
            doc.Load(pathToXml);
            XmlNode connectionString = doc.SelectSingleNode("/Settings/ConnectionOptions/ConnectionString");
            return connectionString.InnerText;
        }
    }
}
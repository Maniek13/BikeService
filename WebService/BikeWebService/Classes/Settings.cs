using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace BikeWebService.Classes
{
    public sealed class Settings
    {
        public static string GetConnectionString()
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BikeWebService;

namespace ToDoApp.StaticModels
{
    internal class Admin
    {
        internal static int Id { get; set; } = 0;
        internal static string Login { get; set; }
        internal static string Password { get; set; }
        internal static int AppId { get; set; }
    }
}

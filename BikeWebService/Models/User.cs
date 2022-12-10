using BikeWebService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeWebService.Models
{
    public class User : IUser
    {
        public int Id { get; set; } = 0;
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeWebService.Models
{
    public class HttpContextResponse
    {
        public int StatusCode { get; set; } = 200;
        public string StatusDescription { get; set; } = "OK";
    }
}
using BikeWebService.Interfaces;

namespace BikeWebService.Models
{
    public sealed class Company : ICompany
    {
        public int appID { get; set; }
        public string name { get; set; }
        public string appKey { get; set; }
    }
}
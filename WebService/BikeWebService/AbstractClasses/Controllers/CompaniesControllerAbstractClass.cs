using BikeWebService.Models;
using System.Collections.Generic;

namespace BikeWebService.AbstractClasses.Controllers
{
    internal abstract class CompaniesControllerAbstractClass
    {
        internal abstract List<Company> GetCompanies();
    }
}
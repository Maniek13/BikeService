using BikeWebService.Models;
using System.Collections.Generic;

namespace BikeWebService.AbstractClasses
{
    internal abstract class CompaniesDbControllerAbstractClass : BaseDbController
    {
        internal abstract List<Company> GetCompanies();
    }
}
using BikeWebService.AbstractClasses;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal sealed class CompaniesController : CompaniesControllerAbstractClass
    {
        private readonly CompaniesDbControllerAbstractClass _companiesController;
        public CompaniesController(CompaniesDbControllerAbstractClass service)
        {
            _companiesController = service;
        }
        internal override List<Company> GetCompanies()
        {
            try
            {
                return _companiesController.GetCompanies();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
using BikeWebService.AbstractClasses.Controllers;
using BikeWebService.AbstractClasses.DbControllers;
using BikeWebService.Models;
using System;
using System.Collections.Generic;

namespace BikeWebService.Controllers
{
    internal sealed class CompaniesController : CompaniesControllerAbstractClass
    {
        private readonly CompaniesDbControllerAbstractClass _companiesController;
        internal CompaniesController(CompaniesDbControllerAbstractClass service)
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
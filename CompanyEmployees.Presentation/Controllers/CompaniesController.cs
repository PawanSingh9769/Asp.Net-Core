﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController ]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _services;
        public CompaniesController(IServiceManager services) //injecting IServiceManager Interface
        {
            _services = services;
        }

        [HttpGet]                                          //decorating GetCompanies action with HTTP Get attribute
        public IActionResult GetCompanies()               // IActionResult which return not only the result but also status code
        {
            try
            {
                //we use injected service to call the service method that gets the data from repository clas
                var companies = _services.CompanyService.GetAllCompanies(trackChanges: false); 
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
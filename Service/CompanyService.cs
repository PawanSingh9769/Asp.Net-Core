﻿using Contracts;
using Entites.Models;
using Service.Contracts;
using Shared.DataTransferObject_DTO_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService // inheriting the IService class 
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger) //injecting IRepository and Ilogger interface
        {
            _repository = repository; // to access the repository method from each user (companyrepo or Employee repo)
            _logger = logger; // to access logging method 
        }

       /* We are using our repository manager to call the GetAllCompanies
         method from the CompanyRepository class and return all the companies
         from the database.*/
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges);

                var companiesDTO = companies.Select(c => new CompanyDto(c.Id,c.Name ??" ",string.Join(' ',c.Address , c.Country))).ToList();
                return companiesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the{nameof(GetAllCompanies)} service method {ex}");
                throw;
            }
        }
    }
}

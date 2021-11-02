using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace MISA.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBaseRepository<Department> _departmentRepository;
        private readonly IBaseRepository<Position> _positionRepository;
        //private readonly ServiceResult _serviceResult;

        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository) : base(baseRepository)
        {
            
            _employeeRepository = employeeRepository;
        }

        public ServiceResult GetEmployeesFilter(int pageNumber, int pageSize, string employeeFilter, string departmentId, string positionId)
        {
            ServiceResult serviceResult = new ServiceResult();
            if (pageNumber <= 0 || pageSize <= 0)
            {
                var response = new
                {

                    devMsg = $"pageNumber: {pageNumber}, pageSize: {pageSize}" ,
                    userMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + $"pageNumber: {pageNumber}, pageSize: {pageSize}",
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()

                };
                serviceResult.StatusCode = 400;
                serviceResult.Data = response;
            }
            var pageOffset = (pageNumber-1) * pageSize;
            
            var filterObject = _employeeRepository.GetEmployeesFilter(pageOffset, pageSize, employeeFilter, departmentId, positionId);
            if (filterObject == null)
            { 
                serviceResult.StatusCode = 204;
                serviceResult.Data = 1;
                return serviceResult;
            }
            serviceResult.StatusCode = 200;
            serviceResult.Data = filterObject;
            return serviceResult;
            throw new NotImplementedException();
        }
        
    }
}

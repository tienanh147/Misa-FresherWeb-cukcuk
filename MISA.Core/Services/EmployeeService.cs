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
        private readonly ServiceResult _serviceResult;

        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository) : base(baseRepository)
        {
            _serviceResult = new ServiceResult();
            _employeeRepository = employeeRepository;
        }

        public ServiceResult GetEmployeesFilter(int pageNumber, int pageSize, string employeeFilter, string departmentId, string positionId)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                var response = new
                {

                    devMsg = $"pageNumber: {pageNumber}, pageSize: {pageSize}" ,
                    userMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + $"pageNumber: {pageNumber}, pageSize: {pageSize}",
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()

                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
            }
            var pageOffset = (pageNumber-1) * pageSize;
            
            var filterObject = _employeeRepository.GetEmployeesFilter(pageOffset, pageSize, employeeFilter, departmentId, positionId);
            if (filterObject == null)
            {
                _serviceResult.StatusCode = 204;
                _serviceResult.Data = 1;
                return _serviceResult;
            }
            _serviceResult.StatusCode = 200;
            _serviceResult.Data = filterObject;
            return _serviceResult;
            throw new NotImplementedException();
        }

        //public ServiceResult Add(Employee employee)
        //{
        //    //Xử lý nghiệp vụ

        //    var properties = employee.GetType().GetProperties();
        //    var responseBadRequest = new
        //    {
        //        devMsg = Resources.Resources.MISABadRequestMsg,
        //        userMsg = Resources.Resources.MISABadRequestMsg,
        //        errorCode = "MISA_001",
        //        traceId = Guid.NewGuid().ToString()
        //    };
        //    //Kiểm tra thông tin nhân viên đã hợp lệ hay chưa?
        //    foreach (var prop in properties)
        //    {
        //        // Tên thuộc tính
        //        var propName = prop.Name;
        //        var propType = prop.PropertyType.Name;
        //        var propValue = prop.GetValue(employee);

        //        //1. Kiểm tra các trường bắt buộc
        //        switch (propName)
        //        {
        //            case "EmployeeCode":
        //            case "FullName":
        //            case "IdentityNumber":
        //            case "Email":
        //            case "PhoneNumber":
        //                {
        //                    if (propValue == null || propValue == "")
        //                    {
        //                        _serviceResult.IsValid = false;
        //                        _serviceResult.Data = responseBadRequest;
        //                        return _serviceResult;
        //                    }
        //                    break;
        //                }

        //            default:
        //                continue;
        //        }
        //        // 2. Validate định dạng dữ liệu theo Regex
        //        switch (propName)
        //        {
        //            case "Email":
        //                {
        //                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //                    Match match = regex.Match((string)propValue);
        //                    if (!match.Success)
        //                    {
        //                        _serviceResult.IsValid = false;
        //                        _serviceResult.Data = responseBadRequest;
        //                        return _serviceResult;
        //                    }
        //                    break;
        //                }
        //            default:
        //                continue;
        //        }

        //    }

        //    // 3. Tương tác kết nối với Database thông qua interface repos mà Infrastructure triển khai
        //    try
        //    {
        //        var rowEffect = _employeeRepository.Add(employee);
        //        if (rowEffect < 1)
        //        {
        //            throw new Exception();
        //        }
        //        else
        //        {
        //            var response = new
        //            {
        //                devMsg = rowEffect,
        //                userMsg = Resources.Resources.MISAInsertMsg
        //            };
        //            _serviceResult.Data = response;
        //            return _serviceResult;
        //        }
        //    }
        //    catch(SqlException e)
        //    {
        //        if (e.Number == 2601)
        //        {
        //            var response = new
        //            {
        //                userMsg = Resources.Resources.MISADuplicatekKeyMsg,
        //                devMsg = e.Message,
        //                errorCode = "MISA_004",
        //                traceId = Guid.NewGuid().ToString()
        //            };
        //            _serviceResult.IsValid = false;
        //            _serviceResult.Data = response;
        //            return _serviceResult;
        //        }
        //        else throw e;
        //    }
        //    throw new NotImplementedException();
        //}

        /*public ServiceResult Update(Employee employee, Guid employeeId)
        {

            //Xử lý nghiệp vụ

            //Tương tác kết nối với Database thông qua interface repos mà Infrastructure triển khai
            _serviceResult.Data = _employeeRepository.Update(employee, employeeId);
            return _serviceResult;
            throw new NotImplementedException();
        }*/
    }
}

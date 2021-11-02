using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using MISA.Core.Services;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntitysController<Employee>
    {
        #region Fields
        private readonly IEmployeeService _employeeService;

        #endregion

        #region Constructors

        public EmployeesController(IEmployeeService employeeService, IBaseService<Employee> baseService) : base(baseService)
        {
            _employeeService = employeeService;
        }

        #endregion

        [HttpGet("employeeSearch")]
        public IActionResult GetEmployeesByCode([FromQuery] string employeeCode)
        {
            try
            {
                var serviceResult = _baseService.GetByColumn<string>(employeeCode, "EmployeeCode");
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet("employeeFilter")]
        public IActionResult GetEmployeesFilter(int pageNumber, int pageSize, string employeeFilter, string departmentId, string positionId)
        {
            try
            {
                var serviceResult = _employeeService.GetEmployeesFilter(pageNumber, pageSize, employeeFilter, departmentId, positionId);
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

       
        //#region API
        ///// <summary>
        ///// Thêm thông tin khách hàng mới vào db
        ///// </summary>
        ///// <param name="employee">Object thông tin khách hàng muốn thêm vào db</param>
        ///// <returns></returns>
        ///// CreatedBy: TTAnh(12/08/2021)
        //[HttpPost]
        //public IActionResult Add(Employee employee)
        //{
        //    try
        //    {
        //        var serviceResult = _employeeService.Add(employee);
        //        //4. Trả về cho client
        //        if(serviceResult.StatusCode==201)
        //        {
        //            return StatusCode(201, serviceResult.Data);
        //        }
        //        else if(serviceResult.StatusCode==400)
        //        {
        //            return BadRequest(serviceResult.Data);
        //        }
        //        else
        //        {
        //            return StatusCode(200, serviceResult.Data);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        var response = new
        //        {
        //            devMsg = e.Message,
        //            userMsg = Properties.Resources.MISAErrorMessage,
        //            errorCode = "MISA_003",
        //            traceId = Guid.NewGuid().ToString()
        //        };
        //        return StatusCode(500, response);
        //    }

        //}

        ///// <summary>
        ///// Lấy danh sách tất cả nhân viên
        ///// </summary>
        ///// <returns></returns>
        ///// Createdby: TTAnh(17/08/2021)
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    try {
        //        var serviceResult = _employeeService.GetAll();
        //        if (serviceResult.StatusCode==204)
        //        {
        //            var response = new
        //            {
        //                devMsg = MISA.Core.Resources.Resources.MISANoContentMsg,
        //                userMsg = MISA.Core.Resources.Resources.MISANoContentMsg,
        //            };
        //            return StatusCode(204, response);
        //        }
        //        return StatusCode(200, serviceResult.Data);
        //    }
        //    catch(Exception e)
        //    {
        //        var response = new
        //        {
        //            devMsg = e.Message,
        //            userMsg = Properties.Resources.MISAErrorMessage,
        //            errorCode = "MISA_003",
        //            traceId = Guid.NewGuid().ToString()
        //        };
        //        return StatusCode(500, response);
        //    }

        //}

        //[HttpGet("{employeeId}")]
        //public IActionResult GetById(Guid employeeId)
        //{
        //    try
        //    {
        //        var serviceResult = _employeeService.GetById(employeeId);
        //        return StatusCode(serviceResult.StatusCode, serviceResult.Data);
        //    }catch(Exception e)
        //    {
        //        var response = new
        //        {
        //            devMsg = e.Message,
        //            userMsg = Properties.Resources.MISAErrorMessage,
        //            errorCode = "MISA_003",
        //            traceId = Guid.NewGuid().ToString()
        //        };
        //        return StatusCode(500, response);
        //    }
        //}
        //#endregion
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : BaseEntitysController<Customer>
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly Dictionary<string, string> _mappingColumnFileExcel;
        #endregion

        #region Constructors

        public CustomersController(ICustomerService customerService, IBaseService<Customer> baseService) : base(baseService)
        {
            _customerService = customerService;
            _mappingColumnFileExcel = new Dictionary<string, string>();
            _mappingColumnFileExcel.Add("Mã khách hàng (*)", "CustomerCode");
            _mappingColumnFileExcel.Add("Tên khách hàng (*)", "FullName");
            _mappingColumnFileExcel.Add("Mã thẻ thành viên", "MemberCardCode");
            _mappingColumnFileExcel.Add("Nhóm khách hàng", "CustomerGroupName");
            _mappingColumnFileExcel.Add("Số điện thoại", "PhoneNumber");
            _mappingColumnFileExcel.Add("Ngày sinh", "DateOfBirth");
            _mappingColumnFileExcel.Add("Tên công ty", "CompanyName");
            _mappingColumnFileExcel.Add("Mã số thuế","CompanyTaxCode");
            _mappingColumnFileExcel.Add("Email", "Email");
            _mappingColumnFileExcel.Add("Địa chỉ", "Address");
            _mappingColumnFileExcel.Add("Ghi chú", "Description");

        }

        #endregion

        [HttpPost("Import")]
        public IActionResult ImportExcel(IFormFile formFile)
        {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    return BadRequest("File is empty");
                }
                string[] allowFileTypes = { ".xlsx" };
                var fileName = Path.GetExtension(formFile.FileName);
                int count = 0;
                foreach (string type in allowFileTypes)
                {
                    if (fileName.Equals(type, StringComparison.OrdinalIgnoreCase)) break;
                    else count++;
                }
                if (count == allowFileTypes.Length) return BadRequest("Not supported file");
                var serviceResult = _customerService.ImportExcel(formFile, _mappingColumnFileExcel);

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
    }
}
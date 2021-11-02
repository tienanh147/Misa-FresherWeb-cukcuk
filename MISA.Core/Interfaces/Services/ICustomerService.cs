using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    public interface ICustomerService: IBaseService<Customer>
    {
        /// <summary>
        /// Nhập khẩu file excel    
        /// </summary>
        /// <param name="formFile">file người dùng tải lên</param>
        /// <param name="mappingColumnFileExcel">Map tên cột của file excel thành tên của property của entity Customer</param>
        /// <returns>Kết quả nghiệp vụ</returns>
        ServiceResult ImportExcel(IFormFile formFile, Dictionary<string, string> mappingColumnFileExcel);
    }
}

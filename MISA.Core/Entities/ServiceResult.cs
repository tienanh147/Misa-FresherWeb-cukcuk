using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        /// <summary>
        /// Dữ liệu truyền vào có valid hay không.
        /// --true: valid.
        /// --false: invalid
        /// </summary>
        public bool IsValid { get; set; } = true;
        /// <summary>
        /// Data trả về cho client
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Mã trả về
        /// </summary>
        public int StatusCode { get; set; }

        public ServiceResult(object data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }
        public ServiceResult()
        {
                
        }
    }
}

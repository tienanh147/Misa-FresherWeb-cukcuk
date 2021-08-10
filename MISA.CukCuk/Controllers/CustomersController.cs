using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //GET, POST, PUT, DELETE
        [HttpGet]
        public IActionResult GetCustomers()
        {
            //truy cập data base
            //1.Khai bảo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //3. Lấy dữ liệu
            var sqlCommand = "SELECT * FROM Customer";
            var customers = dbConnection.Query<object>(sqlCommand);

            //4. Trả về dữ liệu
            var response = StatusCode(200, customers);
            return response;

        }

        /*//router
        [HttpGet("{name}/{age}")]
        public string GetCustomer(string name, int? age)
        {
            return $"your name: {name}, your age: {age}";
        }*/


        //query tring
        /*[HttpGet]
        public string GetCustomer2(string name, int? age)
        {
            return $"your name: {name}, your age: {age}";
        }*/

        //body request
        [HttpPost]
        public IActionResult PostCustomers([FromBody]List<Customer> customers, [FromHeader] string token)
        {
            return Ok(token);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetById(Guid customerId)
        {
            //truy cập data base
            //1.Khai bảo thông tin database
            var connectionString = "Host = 47.241.69.179;" +
                "Database = MISA.CukCuk_Demo_NVMANH;" +
                "User Id = dev;" +
                "Password = 12345678;";
            //2. Khởi tạo đối tượng kết nối database
            IDbConnection dbConnection = new MySqlConnection(connectionString);
            //3. Lấy dữ liệu
            var sqlCommand = $"SELECT * FROM Customer WHERE CustomerId= @CustomerIdParam";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerIdParam", customerId);
            var customers = dbConnection.Query<object>(sqlCommand);
            //Query, Excute
            //4. Trả về dữ liệu 
            var response = StatusCode(200, customers);
            return response;

        }
    }
}

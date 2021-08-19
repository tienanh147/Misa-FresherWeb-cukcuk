using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : BaseEntitysController<Customer>
    {
        #region Fields
        private readonly ICustomerService _customerService;

        #endregion

        #region Constructors

        public CustomersController(ICustomerService customerService, IBaseService<Customer> baseService) : base(baseService)
        {
            _customerService = customerService;
        }

        #endregion
    }
}
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Services
{
    public class CustomerService: BaseService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ServiceResult _serviceResult;
        public CustomerService(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository) :base(baseRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}

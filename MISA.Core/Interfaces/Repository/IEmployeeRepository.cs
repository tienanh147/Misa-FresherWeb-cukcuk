using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {

        /// <summary>
        /// Lấy danh sách nhân viên phân trang và lọc
        /// </summary>
        /// <param name="pageOffset">số thứ tự của hàng bắt đầu lấy dữ liệu</param>
        /// <param name="pageSize">kích thước của 1 trang</param>
        /// <param name="employeeFilter">chuối filter</param>
        /// <param name="departmentId">filter theo phòng ban</param>
        /// <param name="positionId">filter theo vị trí</param>
        /// <returns></returns>
        Object GetEmployeesFilter(int pageOffset, int pageSize, string employeeFilter, string departmentId, string positionId);

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        IEnumerable<Employee> GetEmployeePaging(int limit, int offset);

        /// <summary>
        /// Lấy danh sách nhân viên theo phòng ban
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        IEnumerable<Employee> GetEmployeeByDepartmentId(Guid departmentId);


        /// <summary>
        /// Lấy danh sách nhân viên theo vị trí
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        IEnumerable<Employee> GetEmployeeByPositionId(Guid positionId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Model
{
    public class Employee : Person
    {
        #region Property
        
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime JoinDate { get; set; }
        public int MartialStatus { get; set; }
        public int EducationalBackground { get; set; }
        public Guid QualificationId { get; set; }
        public string QualificationName { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public int WorkStatus { get; set; }
        public string PersonalTaxCode { get; set; }
        public double Salary { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        #endregion

        #region Contructor
        public Employee():base()
        {
            EmployeeId = Guid.NewGuid();

        }
        #endregion
    }
}

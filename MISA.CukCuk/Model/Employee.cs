using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Model
{
    public class Employee : Person
    {
        #region Property
        
        public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        
        public DateTime joinDate { get; set; }
        public int martialStatus { get; set; }
        public int educationalBackground { get; set; }
        public string qualificationId { get; set; }
        public string departmentId { get; set; }
        public string positionId { get; set; }
        public int workStatus { get; set; }
        public string personalTaxCode { get; set; }
        public int salary { get; set; }
        public string positionCode { get; set; }
        public string positionName { get; set; }
        public string departmentCode { get; set; }
        public string departmentName { get; set; }
        public string qualificationName { get; set; }
        #endregion
    }
}

using MISA.Core.Enum;
using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Employee : Person
    {
        #region Property
        [MISAPrimaryKey][MISANotUpdate]
        public Guid EmployeeId { get; set; }

        [MISARequired]
        [MISAValidate("Unique")]
        public string EmployeeCode { get; set; }

        
        public DateTime? JoinDate { get; set; }
        public int? MartialStatus { get; set; }
        public int? EducationalBackground { get; set; }
        public Guid? QualificationId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public int? WorkStatus { get; set; }

        [MISANotMap]
        public string WorkStatusName
        {
            get
            {
                switch (WorkStatus)
                {
                    case 1:
                        return "Đang làm việc";
                    case 2:
                        return "Xin thôi việc";
                    case 3:
                        return "Xin nghỉ phép";
                    case 4:
                        return "Bị sa thải";
                    default:
                        return null;
                }
                //switch (WorkStatus)
                //{
                //    case WorkStatus.Working:
                //        return "Đang làm việc";
                //    case WorkStatus.QuitJob:
                //        return "Xin thôi việc";
                //    case WorkStatus.OffJob:
                //        return "Xin nghỉ phép";
                //    case WorkStatus.Fire:
                //        return "Bị sa thải";
                //    default:
                //        return null;
                //}
            }
        }
        public string PersonalTaxCode { get; set; }
        public double? Salary { get; set; }

        [MISARequired]
        [MISAValidate("Unique")]
        public string IdentityNumber { get; set; }

        
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }

        [MISANotMap]
        public string QualificationName { get; set; }
        [MISANotMap]
        public string PositionCode { get; set; }
        [MISANotMap]
        public string PositionName { get; set; }
        [MISANotMap]
        public string DepartmentCode { get; set; }
        [MISANotMap]
        public string DepartmentName { get; set; }
        #endregion

        #region Contructor
        public Employee() : base()
        {


        }
        #endregion
    }
}

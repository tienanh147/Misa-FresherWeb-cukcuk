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
        [MISAPrimaryKey]
        [MISANotUpdate]
        public Guid EmployeeId { get; set; }

        [MISARequired("Mã nhân viên")]
        [MISAUnique("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        public DateTime? JoinDate { get; set; }
        public int? MartialStatus { get; set; }
        public int? EducationalBackground { get; set; }
        public Guid? QualificationId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public WorkStatus? WorkStatus { get; set; }

        [MISANotMap]
        public string WorkStatusName
        {
            get
            {
                switch (WorkStatus)
                {
                    //case 1:
                    //    return "Đang làm việc";
                    //case 2:
                    //    return "Xin nghỉ việc";
                    //case 3:
                    //    return "Xin nghỉ phép";
                    //case 4:
                    //    return "Bị sa thải";
                    //default:
                    //    return null;
                    case MISA.Core.Enum.WorkStatus.Working:
                        return "Đang làm việc";
                    case MISA.Core.Enum.WorkStatus.QuitJob:
                        return "Xin nghỉ việc";
                    case MISA.Core.Enum.WorkStatus.OffJob:
                        return "Xin nghỉ phép";
                    case MISA.Core.Enum.WorkStatus.Fire:
                        return "Bị sa thải";
                    default:
                        return null;
                }
            }
        }
        public string PersonalTaxCode { get; set; }
        public double? Salary { get; set; }

        [MISARequired("Số CMTND/ Căn cước")]
        [MISAUnique("Số CMTND/ Căn cước")]
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

        [MISARequired("Email")]
        [MISAValidate("Email", "Email")]
        public string Email { get; set; }

        [MISARequired("Số điện thoại")]
        [MISAValidate("PhoneNumber", "Số điện thoại")]
        public string PhoneNumber { get; set; }

        #endregion

        #region Contructor
        public Employee() : base() { }
        #endregion
    }
}

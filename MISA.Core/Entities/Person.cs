using MISA.Core.Enum;
using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Person : BaseEntity
    {
        #region Contructor
        protected Person()
        {
        }
        #endregion
        #region Property
        public string FirstName { get; set; }

        public string LastName { get; set; }
        [MISARequired]
        public string FullName { get; set; }

        public int? Gender { get; set; }

        [MISANotMap]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 1:
                        return "Nam";
                    case 0:
                        return "Nữ";
                    case 2:
                        return "Khác";
                    default:
                        return null;
                }
            }
        }

        public string Address { get; set; }

    
        public DateTime? DateOfBirth { get; set; }
        [MISARequired]
        [MISAValidate("Email")]
        public string Email { get; set; }
        [MISARequired]
        [MISAValidate("PhoneNumber")]
        public string PhoneNumber { get; set; }


        #endregion
    }
}

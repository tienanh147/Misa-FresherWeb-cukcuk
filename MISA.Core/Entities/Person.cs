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
        [MISARequired("Tên đầy đủ")]
        public string FullName { get; set; }

        public Gender? Gender { get; set; }

        [MISANotMap]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    //case 1:
                    //    return "Nam";
                    //case 0:
                    //    return "Nữ";
                    //case 2:
                    //    return "Khác";
                    //default:
                    //    return null;
                    case MISA.Core.Enum.Gender.Male:
                        return "Nam";
                    case MISA.Core.Enum.Gender.Female:
                        return "Nữ";
                    case MISA.Core.Enum.Gender.Other:
                        return "Khác";
                    default:
                        return null;
                }
            }
        }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        #endregion
    }
}

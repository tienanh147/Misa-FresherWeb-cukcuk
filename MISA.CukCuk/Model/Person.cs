using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Model
{
    public class Person : BaseEntity
    {
        #region Contructor
        protected Person()
        {
            CreatedDate = DateTime.Now;
            ModifiedBy = "Tien Anh";
            Description = "Test Post Api";
        }
        #endregion
        #region Property
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int? Gender { get; set; }

        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string? IdentityPlace { get; set; }

        #endregion
    }
}

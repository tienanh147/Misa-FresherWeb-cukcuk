using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Customer : Person
    {
        #region Contructor
        public Customer() : base()
        {
        }
        #endregion

        #region Property
        [MISAPrimaryKey][MISANotUpdate]
        public Guid CustomerId { get; set; }

        [MISARequired("Mã khách hàng")]
        [MISAUnique("Mã Khách Hàng")]
        public string CustomerCode { get; set; }

        public string CompanyName { get; set; }

        public string CompanyTaxCode { get; set; }

        public Guid? CustomerGroupId { get; set; }

        [MISANotMap]
        public string CustomerGroupName { get; set; }

        public double? DebitAmount { get; set; }

        public bool? IsStopFollow { get; set; }

        public string MemberCardCode { get; set; }

        [MISARequired("Email")]
        [MISAValidate("Email","Email")]
        public string Email { get; set; }


        [MISARequired("Số điện thoại")]
        [MISAUnique("Số điện thoại")]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        #endregion


    }
}

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
        Customer() : base()
        {
            CustomerId = Guid.NewGuid();
        }
        #endregion

        #region Property
        [MISAPrimaryKey]
        public Guid CustomerId { get; set; }

        [MISAValidate("Unique")]
        public string CustomerCode { get; set; }

        #endregion


    }
}

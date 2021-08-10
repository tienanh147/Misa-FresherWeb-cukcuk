using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Model
{
    public class Customer : Person
    {
        #region Contructor
        Customer():base()
        {
            CustomerId = Guid.NewGuid();
        }
        #endregion

        #region Property
        public Guid CustomerId { get; set; }

        public string CustomerCode { get; set; }

        #endregion


    }
}

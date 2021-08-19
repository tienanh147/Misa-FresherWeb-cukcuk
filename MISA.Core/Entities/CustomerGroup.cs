using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class CustomerGroup : BaseEntity
    {
        public Guid CustomerGoupId { get; set; }

        public string GroupName { get; set; }

    }
}

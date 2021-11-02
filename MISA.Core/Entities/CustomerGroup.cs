using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class CustomerGroup : BaseEntity
    {
        public Guid CustomerGroupId { get; set; }

        [MISACheckExits]
        public string CustomerGroupName { get; set; }

        public string Description { get; set; }

    }
}

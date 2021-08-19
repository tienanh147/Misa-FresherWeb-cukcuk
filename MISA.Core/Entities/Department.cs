using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Department : BaseEntity
    {
        [MISAPrimaryKey]
        public Guid DepartmentId { get; set; }
        [MISAValidate("Unique")]
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}

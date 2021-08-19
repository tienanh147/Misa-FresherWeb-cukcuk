using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Position
    {
        [MISAPrimaryKey]
        public Guid PositionId { get; set; }

        [MISAValidate("Unique")]
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}

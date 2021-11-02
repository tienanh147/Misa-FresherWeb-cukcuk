using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Enum
{
    /// <summary>
    /// Giới tính
    /// </summary>
    /// CreatedBy: TTAnh(17/08/2021)
    public enum Gender: int
    {
        Male=1,
        Female=0,
        Other=2
    }

    /// <summary>
    /// Trạng thái công việc
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// Đang làm việc
        /// </summary>
        Working=1,
        /// <summary>
        /// Thôi việc
        /// </summary>  
        QuitJob=2,
        /// <summary>
        /// Nghỉ phép
        /// </summary>
        OffJob=3,
        /// <summary>
        /// Đuổi việc
        /// </summary>
        Fire=4
   
    }

    public enum PropertyName
    {
        Unique = 1,

    }
}

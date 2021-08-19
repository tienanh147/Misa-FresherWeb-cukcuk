using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Enum
{
    /// <summary>
    /// Giới tính
    /// </summary>
    /// CreatedBy: TTAnh(17/08/2021)
    public enum Gender
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
        Fire=4,

        None=0
   
    }

    public enum ValidateType
    {
        Unique = 1,

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.MISAAttribute
{
    /// <summary>
    /// Attribute của những property có trong class Entity nhưng ko có cột tương ứng trong bảng database 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISANotMap : Attribute
    {

    }
    /// <summary>
    /// Attribute của những property của class đóng vai trò là PrimaryKey của table tương ứng database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAPrimaryKey : Attribute
    {

    }
    /// <summary>
    /// Attribute của những property bắt buộc phải có
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public string PropertyName { get; }
        public MISARequired(string propertyName)
        {
            PropertyName = propertyName;
        }

    }
    /// <summary>
    /// Attribute của những property cần được validate
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAValidate : Attribute
    {
        public string ValidateType { get; }
        public string PropertyName { get; }
        public MISAValidate(string validateType)
        {
            ValidateType = validateType;
        }
        public MISAValidate(string validateType, string propertyName)
        {
            ValidateType = validateType;
            PropertyName = propertyName;
        }
    }

    /// <summary>
    /// Attribute của những property không được phép trùng nhau trong database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAUnique : Attribute
    {
        public string PropertyName { get; }
        public MISAUnique(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    /// <summary>
    ///  Attribute của những property ko được update
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MISANotUpdate : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class MISACheckExits : Attribute { }



}

using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MISA.Core.MISAAttribute;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Core.Services
{

    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        protected readonly IBaseRepository<MISAEntity> _baseRepository;
        //private readonly ServiceResult serviceResult;
        private readonly string _entityName;
        #region Contructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _entityName = typeof(MISAEntity).Name;
            _baseRepository = baseRepository;
        }
        #endregion

        #region ServiceResult
        public ServiceResult GetNewCode()
        {
            ServiceResult serviceResult = new ServiceResult();
            string newCode = _baseRepository.GetNewCode();
            if (newCode == null || newCode == "")
            {
                serviceResult.StatusCode = 204;
                return serviceResult;
            }
            serviceResult.StatusCode = 200;
            serviceResult.Data = newCode;
            return serviceResult;
            throw new NotImplementedException();
        }
        public ServiceResult Add(MISAEntity entity)
        {

            //xử lý nghiệp vụ
            var properties = entity.GetType().GetProperties().OfType<PropertyInfo>().ToList();
            properties.Find(prop => prop.Name == "CreatedDate").SetValue(entity, DateTime.Now);
            var primaryKey = GetMappingProperties(typeof(MISAEntity), typeof(MISAPrimaryKey));
            primaryKey[0].SetValue(entity, Guid.NewGuid(), null);
            // Validate các trường dữ liệu bắt buộc
            var validateServiceResult = ValidateAll(entity);
            if (validateServiceResult.IsValid == false) return validateServiceResult;

            ServiceResult serviceResult = new ServiceResult();
            //Thực thi tương tác database
            try
            {
                var rowEffect = _baseRepository.Add(entity);
                if (rowEffect >= 1)
                {
                    serviceResult.StatusCode = 201;
                    serviceResult.Data = rowEffect;
                    return serviceResult;
                }
                else
                {
                    var response = new
                    {
                        devMsg = Resources.Resources.MISASqlErrorMsg,
                        userMsg = Resources.Resources.MISASqlErrorMsg,
                        errorCode = "MISA_001",
                        traceId = Guid.NewGuid().ToString()
                    };
                    serviceResult.StatusCode = 500;
                    serviceResult.Data = response;
                    return serviceResult;
                }
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.Core.Resources.Resources.MISASqlErrorMsg,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.StatusCode = 500;
                serviceResult.Data = response;
                return serviceResult;
            }

            throw new NotImplementedException();
        }

        public ServiceResult DeleteById(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();

            var rowAffect = _baseRepository.DeleteById(entityId);
            if (rowAffect < 1)
            {
                var response = new
                {
                    devMsg = entityId,
                    userMsg = MISA.Core.Resources.Resources.MISADeleteFailMsg + ": " + _entityName + "Id: " + entityId,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.Data = response;
                serviceResult.StatusCode = 500;
                return serviceResult;
            }
            serviceResult.Data = rowAffect;
            serviceResult.StatusCode = 200;
            return serviceResult;


            throw new NotImplementedException();
        }

        public ServiceResult GetAll()
        {
            ServiceResult serviceResult = new ServiceResult();
            var entities = _baseRepository.GetAll();
            if (entities == null)
            {
                serviceResult.StatusCode = 204;
                return serviceResult;
            }
            else
            {
                serviceResult.StatusCode = 200;
                serviceResult.Data = entities;
                return serviceResult;
            }

            throw new NotImplementedException();
        }

        public ServiceResult GetByColumn<ColumnType>(ColumnType columnValue, string columnName)
        {
            ServiceResult serviceResult = new ServiceResult();
            var entities = _baseRepository.GetByColumn<ColumnType>(columnValue, columnName);
            if (entities == null)
            {
                serviceResult.StatusCode = 204;
                return serviceResult;
            }
            serviceResult.Data = entities;
            serviceResult.StatusCode = 200;
            return serviceResult;
            throw new NotImplementedException();
        }

        public ServiceResult GetById(Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            var entity = _baseRepository.GetById(entityId);

            if (entity == null)
            {
                serviceResult.StatusCode = 204;
                return serviceResult;
            }
            serviceResult.Data = entity;
            serviceResult.StatusCode = 200;
            return serviceResult;


            throw new NotImplementedException();
        }

        public ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            ServiceResult serviceResult = new ServiceResult();
            //Check xem trong data base có entityId hay không
            var entityOld = _baseRepository.GetById(entityId);
            if (entityOld == null)
            {
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISASqlNull + $" Id: {entityId}",
                    userMsg = MISA.Core.Resources.Resources.MISASqlNull,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.StatusCode = 400;
                serviceResult.Data = response;
                serviceResult.IsValid = false;
                return serviceResult;
            }
            //xử lý nghiệp vụ
            var tableName = typeof(MISAEntity).Name;
            var properties = entity.GetType().GetProperties().OfType<PropertyInfo>().ToList();
            properties.Find(prop => prop.Name == "ModifiedDate").SetValue(entity, DateTime.Now);
            var primaryKeys = GetMappingProperties(typeof(MISAEntity), typeof(MISAPrimaryKey));
            properties.Find(prop => prop.Name == $"{primaryKeys[0].Name}").SetValue(entity, entityId);

            // validate dữ liệu
            var validateServiceResult = ValidateAll(entity);
            if (validateServiceResult.IsValid == false) return validateServiceResult;

            //Thực thi tương tác database
            try
            {
                var rowEffect = _baseRepository.Update(entity, entityId);
                if (rowEffect >= 1)
                {
                    serviceResult.StatusCode = 200;
                    serviceResult.Data = rowEffect;
                    return serviceResult;
                }
                else
                {
                    var response = new
                    {
                        devMsg = Resources.Resources.MISASqlErrorMsg,
                        userMsg = Resources.Resources.MISASqlErrorMsg,
                        errorCode = "MISA_001",
                        traceId = Guid.NewGuid().ToString()
                    };
                    serviceResult.StatusCode = 500;
                    serviceResult.Data = response;
                    return serviceResult;
                }
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.Core.Resources.Resources.MISASqlErrorMsg,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.StatusCode = 500;
                serviceResult.Data = response;
                return serviceResult;
            }

            throw new NotImplementedException();
        }
        #endregion

        public ServiceResult DeleteSeries(List<Guid> entitiesId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult ImportList(List<MISAEntity> entities)
        {
            throw new NotImplementedException();
        }

        #region Utilities
        /// <summary>
        /// Lấy ra những property của object entity với attribute khớp với attribute truyền vào
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        /// <returns>Danh sách các property</returns>
        protected List<PropertyInfo> GetMappingProperties<Entity>(Entity entity, Type attribute)
        {
            List<PropertyInfo> returnProperties = new List<PropertyInfo>();
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propAttributeNotMap = property.GetCustomAttribute(attribute, true);


                if (propAttributeNotMap != null) { returnProperties.Add(property); }
            }
            return returnProperties;
        }

        protected List<PropertyInfo> GetMappingProperties(Type entityType, Type attribute)
        {
            List<PropertyInfo> returnProperties = new List<PropertyInfo>();
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                var propAttributeNotMap = property.GetCustomAttribute(attribute, true);


                if (propAttributeNotMap != null) { returnProperties.Add(property); }
            }
            return returnProperties;
        }

        /// <summary>
        /// Hàm Validate dữ liệu bắt buộc
        /// </summary>
        /// <param name="entity">dữ liệu gửi lên từ client dưới dạng object</param>
        /// <returns>Danh sách các property bắt buộc bị thiếu trong dữ liệu client gửi lên</returns>
        /// CreatedBy: TTAnh(17/08/2021)
        protected List<PropertyInfo> ValidateRequired(MISAEntity entity)
        {
            List<PropertyInfo> lackProperties = new List<PropertyInfo>();
            var requiredProperties = GetMappingProperties(typeof(MISAEntity), typeof(MISARequired));

            foreach (var prop in requiredProperties)
            {
                var propValue = prop.GetValue(entity);
                if (propValue == null || (string)propValue == "")
                {
                    lackProperties.Add(prop);
                }

            }
            return lackProperties;
        }

        /// <summary>
        /// Hàm validate những trường không được trùng lặp với database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Danh sách các trường bị trùng lặp với database</returns>
        protected List<PropertyInfo> ValidateUnique(MISAEntity entity)
        {
            List<PropertyInfo> invalidProperty = new List<PropertyInfo>();
            var validateProperties = GetMappingProperties(typeof(MISAEntity), typeof(MISAUnique));
            var primaryKeys = GetMappingProperties(typeof(MISAEntity), typeof(MISAPrimaryKey));
            foreach (var prop in validateProperties)
            {
                var propType = prop.PropertyType;
                var propName = prop.Name;
                object propValue = prop.GetValue(entity);


                if (propType.Name == "String" || propType.Name == "Guid")
                {
                    var entities = _baseRepository.GetByColumn((string)propValue, propName);
                    if (entities == null)
                    {
                        continue;
                    }
                    else if (entities.Count() >= 2) invalidProperty.Add(prop);
                    else if (entities.Count() == 1)
                    {
                        foreach (var key in primaryKeys)
                        {
                            if (!key.GetValue(entity).Equals(key.GetValue(entities.ToList()[0])))
                            {
                                invalidProperty.Add(prop);
                                break;
                            }

                        }
                    }

                }
                else if (propType.Name == "Int")
                {
                    var entities = _baseRepository.GetByColumn((int)propValue, propName);
                    if (entities != null) invalidProperty.Add(prop);
                }

            }
            return invalidProperty;

        }

        /// <summary>
        /// Hàm validate những trường dữ liệu theo chuẩn định dạng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Danh sách các trường bị sai định dạng</returns>
        protected List<PropertyInfo> ValidateFormat(MISAEntity entity)
        {
            List<PropertyInfo> invalidProperty = new List<PropertyInfo>();
            var validateProperties = GetMappingProperties(typeof(MISAEntity), typeof(MISAValidate));
            foreach (var prop in validateProperties)
            {
                var propType = prop.PropertyType;
                var propName = prop.Name;
                object propValue = prop.GetValue(entity);
                string validateType = (prop.GetCustomAttribute(typeof(MISAValidate), true) as MISAValidate).ValidateType;
                switch (validateType)
                {
                    case "Email":
                        {
                            //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                            //Match match = regex.Match((string)propValue);

                            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                            if (propValue == null) continue;
                            bool isValid = Regex.IsMatch((string)propValue, regex, RegexOptions.IgnoreCase);
                            if (!isValid)
                            {
                                invalidProperty.Add(prop);
                            }
                            break;
                        }
                    //case "PhoneNumber":
                    //    {
                    //        Regex regex = new Regex(@"^[0-9]*$");
                    //        Match match = regex.Match((string)propValue);
                    //        if (!match.Success)
                    //        {
                    //            invalidProperty.Add(prop);
                    //        }
                    //        break;
                    //    }
                    default:
                        continue;
                }
            }
            return invalidProperty;
        }

        /// <summary>
        /// hàm validate tổng hợp các hàm validate trên 
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>
        protected ServiceResult ValidateAll(MISAEntity entity)
        {
            ServiceResult serviceResult = new ServiceResult();
            serviceResult.IsValid = true;
            var lackProperties = ValidateRequired(entity);

            if (lackProperties.Count > 0)
            {
                string lackPropertiesString = String.Join(", ", from prop in lackProperties select ((MISARequired)prop.GetCustomAttribute(typeof(MISARequired), true)).PropertyName);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.StatusCode = 400;
                serviceResult.Data = response;
                serviceResult.IsValid = false;
                return serviceResult;
            };

            // Validate các trường dữ liệu không được phép trùng
            var duplicatedProperty = ValidateUnique(entity);
            if (duplicatedProperty.Count > 0)
            {
                string notUniquePropertiesString = String.Join(", ", from prop in duplicatedProperty select ((MISAUnique)prop.GetCustomAttribute(typeof(MISAUnique), true)).PropertyName);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISADuplicateProperty + ": " + notUniquePropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISADuplicateProperty + ": " + notUniquePropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.StatusCode = 400;
                serviceResult.Data = response;
                serviceResult.IsValid = false;
                return serviceResult;
            }

            // validate các trường dữ liệu chuẩn format
            var wrongFormatProperties = ValidateFormat(entity);
            if (wrongFormatProperties.Count > 0)
            {
                string wrongFormatPropertiesString = String.Join(", ", from prop in wrongFormatProperties select ((MISAValidate)prop.GetCustomAttribute(typeof(MISAValidate), true)).PropertyName);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                serviceResult.StatusCode = 400;
                serviceResult.Data = response;
                serviceResult.IsValid = false;
                return serviceResult;
            }


            return serviceResult;
        }

        public ServiceResult ImportExcelFile(IFormFile formFile, Dictionary<string, string> mappingColumnFileExcel)
        {
            ServiceResult serviceResult = new ServiceResult();
            var list = new List<MISAEntity>();
            var properties = typeof(MISAEntity).GetProperties().OfType<PropertyInfo>().ToList();
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, System.Threading.CancellationToken.None);


                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;

                    var columnNames = new List<string>();
                    for (int i = 1; i <= colCount; i++)
                        columnNames.Add(worksheet.Cells[1, i].Value.ToString().Trim());

                    int col = 1;
                    //Type guidNull = System.Nullable`1[System.Guid];
                    for (int row = 2; row <= rowCount; row++)
                    {

                        MISAEntity instance = (MISAEntity)Activator.CreateInstance(typeof(MISAEntity)); ;

                        foreach (string colName in columnNames)
                        {
                            var property = properties.Find(prop => prop.Name == mappingColumnFileExcel[colName]);
                            Type propType = property.PropertyType;
                            var cellString = worksheet.Cells[row, col].Value.ToString().Trim();
                            col++;
                            //cellString = cellString == null ? null : cellString.ToString().Trim();
                            if (property == null) continue;
                            object value;
                            if (propType.FullName.Contains("System.Nullable`1[[System.Guid") || propType.Name == "Guid")
                            {
                                value = Guid.Parse(cellString);
                            }
                            else
                            {
                                value = ChangeType(cellString, propType);
                            }
                            

                            property.SetValue(instance, value);
                            
                        }

                    }
                }
            }
            serviceResult.StatusCode = 200;
            serviceResult.Data = "oke";
            return serviceResult;
            throw new NotImplementedException();
        }
        #endregion
        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }
    }
}

using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MISA.Core.Services
{

    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        private readonly IBaseRepository<MISAEntity> _baseRepository;
        private readonly ServiceResult _serviceResult;
        private readonly string _entityName;
        #region Contructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _entityName = typeof(MISAEntity).Name;
            _serviceResult = new ServiceResult();
            _baseRepository = baseRepository;
        }
        #endregion

        #region ServiceResult
        public ServiceResult GetNewCode()
        {
            string newCode = _baseRepository.GetNewCode();
            if(newCode == null || newCode == "")
            {
                _serviceResult.StatusCode = 204;
                return _serviceResult;
            }
            _serviceResult.StatusCode = 200;
            _serviceResult.Data = newCode;
            return _serviceResult;
            throw new NotImplementedException();
        }
        public ServiceResult Add(MISAEntity entity)
        {
            //validate dữ liệu và xử lý nghiệp vụ
            var properties = entity.GetType().GetProperties().OfType<PropertyInfo>().ToList();
            properties.Find(prop => prop.Name == "CreatedDate").SetValue(entity, DateTime.Now);
            var primaryKey = GetMappingProperties<MISAEntity>(entity, typeof(MISAPrimaryKey));
            primaryKey[0].SetValue(entity, Guid.NewGuid(), null);
            // Validate các trường dữ liệu bắt buộc
            var lackProperties = validateRequired(entity);
            if (lackProperties.Count > 0)
            {
                string lackPropertiesString = String.Join(", ", from prop in lackProperties select prop.Name);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            };

            // Validate các trường dữ liệu không được phép trùng
            var duplicatedProperty = validateUnique(entity);
            if (duplicatedProperty.Count > 0)
            {
                string notUniquePropertiesString = String.Join(", ", from prop in duplicatedProperty select prop.Name);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISADuplicateProperty + ": " + notUniquePropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISADuplicateProperty + ": " + notUniquePropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            }

            // validate các trường dữ liệu chuẩn format
            var wrongFormatProperties = validateFormat(entity);
            if (wrongFormatProperties.Count > 0)
            {
                string wrongFormatPropertiesString = String.Join(", ", from prop in wrongFormatProperties select prop.Name);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            }
            //Thực thi tương tác database
            try
            {
                var rowEffect = _baseRepository.Add(entity);
                if (rowEffect >= 1)
                {
                    _serviceResult.StatusCode = 201;
                    _serviceResult.Data = rowEffect;
                    return _serviceResult;
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
                    _serviceResult.StatusCode = 500;
                    _serviceResult.Data = response;
                    return _serviceResult;
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
                _serviceResult.StatusCode = 500;
                _serviceResult.Data = response;
                return _serviceResult;
            }

            throw new NotImplementedException();
        }

        public ServiceResult DeleteById(Guid entityId)
        {

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
                _serviceResult.Data = response;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
            _serviceResult.Data = rowAffect;
            _serviceResult.StatusCode = 200;
            return _serviceResult;


            throw new NotImplementedException();
        }

        public ServiceResult GetAll()
        {
            var entities = _baseRepository.GetAll();
            if (entities == null)
            {
                _serviceResult.StatusCode = 204;
                return _serviceResult;
            }
            else
            {
                _serviceResult.StatusCode = 200;
                _serviceResult.Data = entities;
                return _serviceResult;
            }

            throw new NotImplementedException();
        }

        public ServiceResult GetByColumn<ColumnType>(ColumnType columnValue, string columnName)
        {
            var entities = _baseRepository.GetByColumn<ColumnType>(columnValue, columnName);
            if (entities == null)
            {
                _serviceResult.StatusCode = 204;
                return _serviceResult;
            }
            _serviceResult.Data = entities;
            _serviceResult.StatusCode = 200;
            return _serviceResult;
            throw new NotImplementedException();
        }

        public ServiceResult GetById(Guid entityId)
        {

            var entity = _baseRepository.GetById(entityId);

            if (entity == null)
            {
                _serviceResult.StatusCode = 204;
                return _serviceResult;
            }
            _serviceResult.Data = entity;
            _serviceResult.StatusCode = 200;
            return _serviceResult;


            throw new NotImplementedException();
        }

        public ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            //Check xem trong data base có entityId hay không
            var entityOld = _baseRepository.GetById(entityId);
            if (entityOld == null)
            {
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISASqlNull+ $" Id: {entityId}",
                    userMsg = MISA.Core.Resources.Resources.MISASqlNull,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            }
            //validate dữ liệu và xử lý nghiệp vụ
            var tableName = typeof(MISAEntity).Name;
            var properties = entity.GetType().GetProperties().OfType<PropertyInfo>().ToList();
            properties.Find(prop => prop.Name == "ModifiedDate").SetValue(entity, DateTime.Now);
            var primaryKeys = GetMappingProperties<MISAEntity>(entity, typeof(MISAPrimaryKey));
            properties.Find(prop => prop.Name == $"{tableName}Id").SetValue(entity, entityId);
            // Validate các trường dữ liệu bắt buộc
            var lackProperties = validateRequired(entity);
            if (lackProperties.Count > 0)
            {
                string lackPropertiesString = String.Join(", ", from prop in lackProperties select prop.Name);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            };

            // Validate các trường dữ liệu không được phép trùng
            var duplicatedProperty = validateUnique(entity);
            if (duplicatedProperty.Count > 0)
            {
                string notUniquePropertiesString = String.Join(", ", from prop in duplicatedProperty select prop.Name);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISADuplicateProperty + ": " + notUniquePropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISADuplicateProperty + ": " + notUniquePropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            }

            // validate các trường dữ liệu chuẩn format
            var wrongFormatProperties = validateFormat(entity);
            if (wrongFormatProperties.Count > 0)
            {
                string wrongFormatPropertiesString = String.Join(", ", from prop in wrongFormatProperties select prop.Name);
                var response = new
                {
                    devMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString,
                    userMsg = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString,
                    errorCode = "MISA_001",
                    traceId = Guid.NewGuid().ToString()
                };
                _serviceResult.StatusCode = 400;
                _serviceResult.Data = response;
                _serviceResult.IsValid = false;
                return _serviceResult;
            }
            //Thực thi tương tác database
            try
            {
                var rowEffect = _baseRepository.Update(entity, entityId);
                if (rowEffect >= 1)
                {
                    _serviceResult.StatusCode = 200;
                    _serviceResult.Data = rowEffect;
                    return _serviceResult;
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
                    _serviceResult.StatusCode = 500;
                    _serviceResult.Data = response;
                    return _serviceResult;
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
                _serviceResult.StatusCode = 500;
                _serviceResult.Data = response;
                return _serviceResult;
            }

            throw new NotImplementedException();
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Lấy ra những property của object entity với attribute khớp với attribute truyền vào
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="attribute"></param>
        /// <returns>Danh sách các property</returns>
        private List<PropertyInfo> GetMappingProperties<Entity>(Entity entity, Type attribute)
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


        /// <summary>
        /// Hàm Validate dữ liệu bắt buộc
        /// </summary>
        /// <param name="entity">dữ liệu gửi lên từ client dưới dạng object</param>
        /// <returns>Danh sách các property bắt buộc bị thiếu trong dữ liệu client gửi lên</returns>
        /// CreatedBy: TTAnh(17/08/2021)
        private List<PropertyInfo> validateRequired(MISAEntity entity)
        {
            List<PropertyInfo> lackProperties = new List<PropertyInfo>();
            var requiredProperties = GetMappingProperties<MISAEntity>(entity, typeof(MISARequired));

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
        private List<PropertyInfo> validateUnique(MISAEntity entity)
        {
            List<PropertyInfo> invalidProperty = new List<PropertyInfo>();
            var validateProperties = GetMappingProperties(entity, typeof(MISAValidate));
            var primaryKeys = GetMappingProperties(entity, typeof(MISAPrimaryKey));
            foreach (var prop in validateProperties)
            {
                var propType = prop.PropertyType;
                var propName = prop.Name;
                object propValue = prop.GetValue(entity);
                string validateType = ((MISAValidate)prop.GetCustomAttribute(typeof(MISAValidate), true)).ValidateType;
                if (validateType == "Unique")
                {
                    if (propType.Name == "String" || propType.Name == "Guid")
                    {
                        var entities = _baseRepository.GetByColumn((string)propValue, propName);
                        if(entities == null)
                        {
                            continue;
                        }
                        else if (entities.Count() >= 2) invalidProperty.Add(prop);
                        else if(entities.Count() == 1)
                        {
                            foreach(var key in primaryKeys)
                            {
                                if(!key.GetValue(entity).Equals(key.GetValue(entities.ToList()[0])))
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
            }
            return invalidProperty;

        }

        /// <summary>
        /// Hàm validate những trường dữ liệu theo chuẩn định dạng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Danh sách các trường bị sai định dạng</returns>
        private List<PropertyInfo> validateFormat(MISAEntity entity)
        {
            List<PropertyInfo> invalidProperty = new List<PropertyInfo>();
            var validateProperties = GetMappingProperties(entity, typeof(MISAValidate));
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
                            bool isValid = Regex.IsMatch((string)propValue, regex, RegexOptions.IgnoreCase);
                            if (!isValid)
                            {
                                invalidProperty.Add(prop);
                            }
                            break;
                        }
                    case "PhoneNumber":
                        {
                            Regex regex = new Regex(@"^[0-9]*$");
                            Match match = regex.Match((string)propValue);
                            if (!match.Success)
                            {
                                invalidProperty.Add(prop);
                            }
                            break;
                        }
                    default:
                        continue;
                }
            }
            return invalidProperty;
        }

       
        #endregion
    }
}

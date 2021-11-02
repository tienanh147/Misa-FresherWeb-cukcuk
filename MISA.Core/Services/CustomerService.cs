using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Services;
using MISA.Core.MISAAttribute;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MISA.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IBaseService<CustomerGroup> _customerGroupService;

        //private readonly ServiceResult _serviceResult;
        public CustomerService(IBaseService<CustomerGroup> customerGroupService, IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository) : base(baseRepository)
        {
            _customerRepository = customerRepository;
            _customerGroupService = customerGroupService;
        }

        public ServiceResult ImportExcel(IFormFile formFile, Dictionary<string, string> mappingColumnFileExcel)
        {
            ServiceResult serviceResult = new ServiceResult();

            var listCustomer = new List<Customer>();
            var properties = typeof(Customer).GetProperties().OfType<PropertyInfo>().ToList();

            #region đọc file
            using (var stream = new MemoryStream())
            {
                formFile.CopyToAsync(stream, System.Threading.CancellationToken.None);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;
                    var columnNames = new List<string>();
                    for (int col = 1; col <= colCount; col++)
                        columnNames.Add(worksheet.Cells[1, col].Value.ToString().Trim());
                    for (int row = 2; row <= rowCount; row++)
                    {
                        Customer customer = new Customer();
                        int col = 1;
                        foreach (string colName in columnNames)
                        {
                            var property = properties.Find(prop => prop.Name == mappingColumnFileExcel[colName]);
                            var cellValue = worksheet.Cells[row, col].Value;
                            col++;

                            #region check cellValue == null
                            if (cellValue != null)
                            {
                                string cellString = cellValue.ToString().Trim();
                                if (cellString == "null") cellString = null;
                                #region format DateTime
                                if (property.PropertyType.FullName.Contains("DateTime"))
                                {
                                    if (!cellString.Contains("/"))
                                    {
                                        cellString = cellString.Insert(0, "01/01/");
                                    }
                                }
                                #endregion

                                cellValue = cellString;
                            }
                            else
                            {
                                cellValue = null;
                            }
                            #endregion
                            object value;
                            if (property == null) continue;
                            Type propType = property.PropertyType;
                            value = ChangeType(cellValue, propType);
                            property.SetValue(customer, value);
                        }
                        listCustomer.Add(customer);
                    }
                }
            }
            #endregion

            #region validate danh sách vừa lấy ở trên
            var uniqueProperties = GetMappingProperties(typeof(Customer), typeof(MISAUnique));
            var listGetColumnDB = new List<string>();
            //danh sách các giá trị của từng cột trong file excel
            var setColumnValuesInFile = new Dictionary<PropertyInfo, HashSet<object>>();
            foreach (var prop in uniqueProperties)
            {
                var columnValues = new HashSet<object>();
                setColumnValuesInFile.Add(prop, columnValues);
                listGetColumnDB.Add(prop.Name);
            }

            //List<Customer> customerDataBase = (List<Customer>)_baseRepository.GetAll();

            List<Customer> customerDataBase = (List<Customer>)_baseRepository.GetAllLimitColumns(listGetColumnDB, true);

            var listResponse = new List<dynamic>();
            var listSuccess = new List<Customer>();
            int successImportNumber = 0;
            int failImportNumber = 0;

            #region duyệt danh sách các bản ghi để validate
            foreach (var customer in listCustomer)
            {
                dynamic customerWrap = new ExpandoObject();

                customerWrap.ImportStatus = new List<string>();

                #region validate tồn tại nhóm khách hàng
                var exitsCustomerGroup = ValidateExistCustomerGroup(customer.CustomerGroupName);
                if (!exitsCustomerGroup)
                {
                    string responseString = MISA.Core.Resources.Resources.MISANotExitsCustomerGroup;
                    customerWrap.ImportStatus.Add(responseString);
                }
                #endregion

                #region validate trùng lặp
                foreach (var property in uniqueProperties)
                {
                    var propValue = property.GetValue(customer);
                    //lấy ra PropertyName của Attribute MISAUnique của prop
                    var returnPropertyName = ((MISAUnique)property.GetCustomAttribute(typeof(MISAUnique), true)).PropertyName;

                    #region validate đã tồn tại trên tệp nhập khẩu
                    if (setColumnValuesInFile[property].Contains(propValue))
                    {
                        string responseString = returnPropertyName + " " + propValue + " đã tồn tại trên tệp nhập khẩu";
                        customerWrap.ImportStatus.Add(responseString);
                    }
                    else
                    {
                        setColumnValuesInFile[property].Add(propValue);
                    }
                    #endregion

                    #region validate đã tồn tại trong database
                    var propType = property.PropertyType;
                    var propName = property.Name;
                    bool exitsInDb = false;
                    // ép kiểu dữ liệu phù hợp để validate
                    if (propType.Name == "String" || propType.Name == "Guid")
                    {
                        exitsInDb = ValidataExistColumnValueDB(customerDataBase, property, propValue);
                    }
                    else if (propType.Name == "Int")
                    {
                        exitsInDb = ValidataExistColumnValueDB(customerDataBase, property, propValue);
                    }

                    // nếu đã tồn tại trả về câu trả lời 
                    if (exitsInDb == true)
                    {
                        string responseString = returnPropertyName + " " + propValue + " đã tồn tại";
                        customerWrap.ImportStatus.Add(responseString);
                    }
                    #endregion
                }
                #endregion

                #region validate các trường bắt buộc
                var lackProperties = ValidateRequired(customer);
                if (lackProperties.Count > 0)
                {
                    string lackPropertiesString = String.Join(", ", from prop in lackProperties select ((MISARequired)prop.GetCustomAttribute(typeof(MISARequired), true)).PropertyName);
                    string responseString = MISA.Core.Resources.Resources.MISALackOfData + ": " + lackPropertiesString;
                    customerWrap.ImportStatus.Add(responseString);
                };
                #endregion

                #region validate format
                var wrongFormatProperties = ValidateFormat(customer);
                if (wrongFormatProperties.Count > 0)
                {
                    string wrongFormatPropertiesString = String.Join(", ", from prop in wrongFormatProperties select ((MISAValidate)prop.GetCustomAttribute(typeof(MISAValidate), true)).PropertyName);
                    string responseString = MISA.Core.Resources.Resources.MISABadRequestMsg + ": " + wrongFormatPropertiesString;
                    customerWrap.ImportStatus.Add(responseString);
                }
                #endregion


                #region check trạng thái hợp lệ
                if (customerWrap.ImportStatus.Count == 0)
                {
                    listSuccess.Add(customer);
                    customerWrap.ImportStatus.Add("Hợp lệ");
                    customer.CustomerId = Guid.NewGuid();
                    successImportNumber++;
                }
                else { failImportNumber++; }
                #endregion

                customerWrap.Data = customer;
                listResponse.Add(customerWrap);

            }
            #endregion

            #endregion 
            var response = new
            {
                SuccessRecord = successImportNumber,
                FailRecord = failImportNumber,
                Data = listResponse
            };

            serviceResult.StatusCode = 200;
            serviceResult.Data = response;
            return serviceResult;
            throw new NotImplementedException();
        }

        /// <summary>
        /// validate Nhóm khách hàng có tồn tại trong database không
        /// </summary>
        /// <param name="groupName">Tên nhóm khách hàng</param>
        /// <returns></returns>
        private bool ValidateExistCustomerGroup(string groupName)
        {
            List<CustomerGroup> customerGroups;
            try
            {
                var cusomerGroupsServiceResult = _customerGroupService.GetAll();
                if (cusomerGroupsServiceResult.StatusCode == 200)
                {
                    customerGroups = (List<CustomerGroup>)cusomerGroupsServiceResult.Data;
                }
                else
                {
                    customerGroups = default;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            foreach (var customerGroup in customerGroups)
            {
                if (customerGroup.CustomerGroupName == groupName) return true;
            }
            return false;
        }


        private bool ValidataExistColumnValueDB(List<Customer> sourceList, PropertyInfo property, object propertyValue)
        {
            if (propertyValue == null) return false;
            foreach (var item in sourceList)
            {
                if (property.GetValue(item).Equals(propertyValue))
                {
                    return true;
                }
            }
            return false;
        }


        private dynamic MergeObject<Object1, Object2>(Object1 entity1, Object1 entity2)
        {
            dynamic mergedObject = new ExpandoObject();
            var properties1 = typeof(Object1).GetProperties();
            var properties2 = typeof(Object2).GetProperties();
            return null;
        }

    }
}

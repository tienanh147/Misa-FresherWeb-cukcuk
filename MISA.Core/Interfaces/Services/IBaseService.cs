using Microsoft.AspNetCore.Http;
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<Entity>
    {
        ServiceResult GetById(Guid entityId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResult GetNewCode();

        /// <summary>
        /// Thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>k
        /// CreatedBy: TTAnh(13/08/2021)
        ServiceResult Add(Entity entity);

        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>
        /// CreatedBy: TTAnh(13/08/2021)
        ServiceResult GetAll();

        /// <summary>
        /// Lấy thông tin của đối tượng theo cột
        /// </summary>
        /// <typeparam name="ColumnType">Kiểu dữ liệu</typeparam>
        /// <param name="columnValue">Giá trị của cột</param>
        /// <param name="columnName">Tên cột</param>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>
        /// CreatedBy: TTAnh(18/08/2021)
        ServiceResult GetByColumn<ColumnType>(ColumnType columnValue, string columnName);

        /// <summary>
        /// Cập nhật thông tin đối tượng
        /// </summary>
        /// <param name="entity">Thông tin entity</param>
        /// <param name="entityId">Id của entity cần cập nhật</param>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>
        /// CreatedBy: TTAnh(13/08/2021)
        ServiceResult Update(Entity entity, Guid entityId);

        /// <summary>
        /// Xóa dữ liệu của đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>
        /// CreatedBy: TTAnh(18/08/2021)
        ServiceResult DeleteById(Guid entityId);

        /// <summary>
        /// Chức năng xóa nhiều
        /// </summary>
        /// <param name="entitiesId">Mảng Id của đối tượng</param>
        /// <returns>kết quả xử lý qua nghiệp vụ</returns>
        ServiceResult DeleteSeries(List<Guid> entitiesId);

        ServiceResult ImportExcelFile(IFormFile formFile, Dictionary<string, string> mappingColumnFileExcel);
    }
}

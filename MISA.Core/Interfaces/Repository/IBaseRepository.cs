using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Repository
{
    public interface IBaseRepository<Entity>
    {
        /// <summary>
        /// Lấy dữ liệu đối tượng từ database
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        IEnumerable<Entity> GetAll();

        /// <summary>
        /// Lấy dữ liệu đối tượng theo Id
        /// </summary>
        /// <param name="entity">Id của đối tượng</param>
        /// <returns>Thông tin của đối tượng</returns>
        Entity GetById(Guid entityId);

        /// <summary>
        /// Lấy dữ liệu đối tượng theo cột
        /// </summary>
        /// <typeparam name="ColumnType"></typeparam>
        /// <param name="columnValue">giá trị của cột</param>
        /// <param name="columnName">tên cột</param>
        /// <returns></returns>
        /// CreatedBy: TTAnh(18/08/2021)
        IEnumerable<Entity> GetByColumn<ColumnType>(ColumnType columnValue, string columnName);

        /// <summary>
        /// Cung cấp 1 mã mới là mã lớn hơn tất cả còn lại trong database
        /// </summary>
        /// <returns>mã mới</returns>
        string GetNewCode();

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <returns>Số hàng bị tác động ở database</returns>
        /// CreatedBy: TTAnh(13/08/2021)
        int Add(Entity entity);

        /// <summary>
        /// Cập nhật thông tin đối tượng
        /// </summary>
        /// <param name="entity">Thông tin đối tượng</param>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Số hàng bị tác động ở database</returns>
        /// CreatedBy: TTAnh(13/08/2021)
        int Update(Entity entity, Guid entityId);


        /// <summary>
        /// Xóa đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Số hàng bị tác động ở database</returns>
        /// CreatedBy: TTAnh(13/08/2021)
        int DeleteById(Guid entityId);
    }
}

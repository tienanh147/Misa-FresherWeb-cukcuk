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
        /// Lấy dữ liệu các đối tượng có giá trị cột khớp với giá trị truyền vào
        /// </summary>
        /// <typeparam name="ColumnType"></typeparam>
        /// <param name="columnValue">giá trị của cột</param>
        /// <param name="columnName">tên cột</param>
        /// <returns>danh sách đối tượng có giá trị cột trùng với giá trị truyền vào</returns>
        /// CreatedBy: TTAnh(18/08/2021)
        IEnumerable<Entity> GetByColumn<ColumnType>(ColumnType columnValue, string columnName);
        /// <summary>
        /// Lấy dữ liệu tất cả các đối tượng có giới hạn các cột
        /// </summary>
        /// <param name="columnNames">các cột cần lấy</param>
        /// <param name="distinctMode">có trả về kết quả không trùng nhau hay không</param>
        /// <returns>các cột cần lấy của tất cả đối tượng</returns>
        public IEnumerable<Entity> GetAllLimitColumns(List<string> columnNames, bool distinctMode = false);
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

        /// <summary>
        /// Xóa nhiều đối tượng theo Id
        /// </summary>
        /// <param name="entitiesId">Danh sách Id cần xóa</param>
        /// <returns>Số bản ghi xóa thành công</returns>
        int DeleteSeries(List<Guid> entitiesId);

        /// <summary>
        /// Thêm nhiều đối tượng
        /// </summary>
        /// <param name="entities">Danh sách đối tượng</param>
        /// <returns>Số bản ghi thêm thành công</returns>
        /// CreatedBy: TTAnh(23/08/2021)
        int AddSeries(List<Entity> entities);
        
    }
}

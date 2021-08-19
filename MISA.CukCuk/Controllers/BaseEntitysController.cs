using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    public class BaseEntitysController<MISAEntity> : ControllerBase
    {
        private readonly string _entityName;
        #region Fields
        protected readonly IBaseService<MISAEntity> _baseService;
        #endregion

        #region Constructors

        public BaseEntitysController(IBaseService<MISAEntity> baseService)
        {
            _entityName = typeof(MISAEntity).Name;
            _baseService = baseService;
        }

        #endregion

        /// <summary>
        /// Lấy mã mới cho đối tượng
        /// </summary>
        /// <returns>mã mới</returns>
        /// CreatedBy: TTAnh(18/08/2021)
        [HttpGet("NewCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var serviceResult = _baseService.GetNewCode();
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch(Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Thêm thông tin khách hàng mới vào db
        /// </summary>
        /// <param name="entity">Object thông tin khách hàng muốn thêm vào db</param>
        /// <returns></returns>
        /// CreatedBy: TTAnh(12/08/2021)
        [HttpPost]
        public IActionResult Add(MISAEntity entity)
        {
            try
            {
                var serviceResult = _baseService.Add(entity);
                //4. Trả về cho client
                if (serviceResult.StatusCode == 201)
                {
                    return StatusCode(201, serviceResult.Data);
                }
                else if (serviceResult.StatusCode == 400)
                {
                    return BadRequest(serviceResult.Data);
                }
                else
                {
                    return StatusCode(200, serviceResult.Data);
                }
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }

        }

        [HttpPut("{entityId}")]
        public IActionResult Update(MISAEntity entity, Guid entityId)
        {
            try
            {
                var serviceResult = _baseService.Update(entity, entityId);
                //4. Trả về cho client
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }

        }

        /// <summary>
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        /// Createdby: TTAnh(17/08/2021)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var serviceResult = _baseService.GetAll();
                if (serviceResult.StatusCode == 204)
                {
                    var response = new
                    {
                        devMsg = MISA.Core.Resources.Resources.MISANoContentMsg,
                        userMsg = MISA.Core.Resources.Resources.MISANoContentMsg,
                    };
                    return StatusCode(204, response);
                }
                return StatusCode(200, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }

        }

        /// <summary>
        /// API lấy dữ liệu entity theo Id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet("{entityId}")]
        public IActionResult GetById(Guid entityId)
        {
            try
            {
                var serviceResult = _baseService.GetById(entityId);
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }
        [HttpDelete("{entityId}")]
        public IActionResult DeleteById(Guid entityId)
        {
            try
            {
                var serviceResult = _baseService.DeleteById(entityId);
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = Properties.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }
       
    }
}

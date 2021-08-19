using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BaseEntitysController<Position>
    {
        public PositionsController(IBaseService<Position> baseService):base(baseService)
        {

        }
    }
}

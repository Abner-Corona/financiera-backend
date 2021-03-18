using Financiera_backend.Interfaces;
using Financiera_backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Financiera_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _service;
        public RecordsController(IRecordService service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<RecordModel>> GetRecords()
        {
            try
            {
                return await _service.GetRecords();
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}

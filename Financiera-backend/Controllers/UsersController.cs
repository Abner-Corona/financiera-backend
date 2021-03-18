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
    public class UsersController : ControllerBase
    {

        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        // GET: api/<UsersController>
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            try
            {
                return await _service.GetUsers();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}

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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<GetAccountModel>> GetAccounts(uint id)
        {
            try
            {
                return await _service.GetAccounts(id);
            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpPost("[action]")]
        public async Task<bool> AddNewAccount([FromBody] AddAccountModel model)
        {
            try
            {
                return await _service.AddNewAccount(model);
            }
            catch (Exception)
            {

                return false;
            }
        }

        [HttpPut("[action]")]
        public async Task<bool> DepositAccount(OperationAccountModel model)
        {
            try
            {
                return await _service.DepositAccount(model);
            }
            catch (Exception)
            {

                return false;
            }
        }
        [HttpPut("[action]")]
        public async Task<bool>
        WithdrawalAccount(OperationAccountModel model)
        {
            try
            {
                return await _service.
        WithdrawalAccount(model);
            }
            catch (Exception )
            {

                return false;
            }
        }


    }
}

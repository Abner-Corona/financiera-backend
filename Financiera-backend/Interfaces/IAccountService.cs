using Financiera_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financiera_backend.Interfaces
{
    public interface IAccountService : IDisposable
    {
        public Task<IEnumerable<GetAccountModel>> GetAccounts(uint id);
        public Task<bool> AddNewAccount(AddAccountModel model);
        public Task<bool> DepositAccount(OperationAccountModel model);
        public Task<bool> WithdrawalAccount(OperationAccountModel model);
    }
}

using Dapper;
using Financiera_backend.Interfaces;
using Financiera_backend.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Financiera_backend.ServicesDapper
{
    public class AccountService : IAccountService
    {
        private readonly IDbConnection _conn;
        public AccountService(IDbConnection conn)
        {
            _conn = conn;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<IEnumerable<GetAccountModel>> GetAccounts(uint id)
        {
            using var db = _conn;
            return await _conn.QueryAsync<GetAccountModel>("getAccount", new
            {
                _idUser = id,
            }, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool>  AddNewAccount(AddAccountModel model)
        {
            using var db = _conn;
            return await _conn.QueryFirstOrDefaultAsync<bool>("addNewAccount", new
            {
                _name = model.Name,
                _idUser = model.IdUser,
                _amount = model.Amount
            }, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> DepositAccount(OperationAccountModel model)
        {
            using var db = _conn;
            return await _conn.QueryFirstOrDefaultAsync<bool>("depositAccount", new
            {
                _idAccount = model.IdAccount,
                _amount = model.Amount
            }, commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> WithdrawalAccount(OperationAccountModel model)
        {
            using var db = _conn;
            return await _conn.QueryFirstOrDefaultAsync<bool>("withdrawalAccount", new
            {
                _idAccount = model.IdAccount,
                _amount = model.Amount
            }, commandType: CommandType.StoredProcedure);
        }
    }
}

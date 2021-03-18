using Financiera_backend.Entity;
using Financiera_backend.Interfaces;
using Financiera_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financiera_backend.ServicesEntity
{
    public class AccountService : IAccountService
    {
        private readonly DBContexto _context;
        public AccountService(DBContexto context)
        {
            _context = context;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public async Task<IEnumerable<GetAccountModel>> GetAccounts(uint id)
        {
            return await _context.TabAccount.Where(w => w.Id == id).Select(s => new GetAccountModel
            {
                Amount = s.Amount,
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }
        public async Task<bool> DepositAccount(OperationAccountModel model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var account = await _context.TabAccount.Where(w => w.Id == model.IdAccount).SingleOrDefaultAsync();
                account.Amount += model.Amount;

                _context.TabAccount.Update(account);
                await _context.SaveChangesAsync();
                var record = new TabRecord()
                {
                    Amount = model.Amount,
                    Id = 0,
                    Operation = "deposit"
                   ,
                    Date = DateTime.Now,
                    IdAccount = account.Id
                };

                await _context.TabRecord.AddAsync(record);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

        }

        public async Task<bool> WithdrawalAccount(OperationAccountModel model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var account = await _context.TabAccount.Where(w => w.Id == model.IdAccount).SingleOrDefaultAsync();
                account.Amount -= model.Amount;
                if (account.Amount < 0)
                {
                    await transaction.RollbackAsync();
                    return false;
                }


                _context.TabAccount.Update(account);
                await _context.SaveChangesAsync();
                var record = new TabRecord()
                {
                    Amount = model.Amount,
                    Id = 0,
                    Operation = "withdrawal"
                   ,
                    Date = DateTime.Now,
                    IdAccount = account.Id
                };

                await _context.TabRecord.AddAsync(record);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> AddNewAccount(AddAccountModel model)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var account = new TabAccount()
                {
                    Amount = model.Amount,
                    Id = 0,
                    IdUser = model.IdUser,
                    Name = model.Name

                };
                await _context.TabAccount.AddAsync(account);
                await _context.SaveChangesAsync();
                var record = new TabRecord()
                {
                    Amount = model.Amount,
                    Id = 0,
                    Operation = "deposit"
                   ,
                    Date = DateTime.Now,
                    IdAccount = account.Id
                };

                await _context.TabRecord.AddAsync(record);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}

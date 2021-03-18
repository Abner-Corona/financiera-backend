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

    public class UserService : IUserService
    {
        private readonly DBContexto _context;
        public UserService(DBContexto context)
        {
            _context = context;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _context.TabUser.Select(s => new UserModel
            {
                Id = s.Id,
                Name = s.Name,
                IdUserType = s.IdUserType,
            }).ToListAsync();
        }
    }
}

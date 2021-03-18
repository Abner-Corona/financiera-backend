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
    public class UserService : IUserService
    {
        private readonly IDbConnection _conn;
        public UserService(IDbConnection conn)
        {
            _conn = conn;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            using var db = _conn;
            return await _conn.QueryAsync<UserModel>("getUsers", commandType: CommandType.StoredProcedure);
        }
    }
}

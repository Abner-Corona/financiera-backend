using Financiera_backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financiera_backend.Interfaces
{
   public interface IUserService : IDisposable
    {
        public Task<IEnumerable<UserModel>> GetUsers();

    }
}

using Financiera_backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Financiera_backend.Interfaces
{
    public interface IRecordService :IDisposable
    {
        public Task<IEnumerable<RecordModel>> GetRecords();
    }
}

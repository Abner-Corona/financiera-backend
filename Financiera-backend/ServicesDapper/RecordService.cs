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
    public class RecordService :
        IRecordService
    {
        private readonly IDbConnection _conn;
        public RecordService(IDbConnection conn)
        {
            _conn = conn;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public async Task<IEnumerable<RecordModel>> GetRecords()
        {
            using var db = _conn;
            return await _conn.QueryAsync<RecordModel>("getRecords", commandType: CommandType.StoredProcedure);
        }
    }
}

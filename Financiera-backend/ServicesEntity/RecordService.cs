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
    public class RecordService: IRecordService
    {
        private readonly DBContexto _context;
        public RecordService(DBContexto context)
        {
            _context = context;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<RecordModel>> GetRecords()
        {
            return await _context.TabRecord.Include(i => i.IdAccountNavigation).ThenInclude(ti => ti.IdUserNavigation).Select(s => new RecordModel
            {
                Account = s.IdAccountNavigation.Name,
                Amount=s.Amount,
                Operation=s.Operation,
                User=s.IdAccountNavigation.IdUserNavigation.Name,
                 Date = s.Date

            }).ToListAsync();
        }
    }
}

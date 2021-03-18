using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financiera_backend.Models
{
    public class RecordModel
    {
        public string User { get; set; }
        public string Account { get; set; }
        public decimal? Amount { get; set; }
        public string Operation { get; set; }
        public DateTime? Date { get; set; }
    }
}

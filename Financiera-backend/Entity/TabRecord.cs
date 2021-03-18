using System;
using System.Collections.Generic;

namespace Financiera_backend.Entity
{
    public partial class TabRecord
    {
        public uint Id { get; set; }
        public uint? IdAccount { get; set; }
        public string Operation { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }

        public virtual TabAccount IdAccountNavigation { get; set; }
    }
}

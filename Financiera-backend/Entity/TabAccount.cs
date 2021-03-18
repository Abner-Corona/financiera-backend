using System;
using System.Collections.Generic;

namespace Financiera_backend.Entity
{
    public partial class TabAccount
    {
        public TabAccount()
        {
            TabRecord = new HashSet<TabRecord>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public uint? IdUser { get; set; }

        public virtual TabUser IdUserNavigation { get; set; }
        public virtual ICollection<TabRecord> TabRecord { get; set; }
    }
}

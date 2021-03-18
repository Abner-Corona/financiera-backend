using System;
using System.Collections.Generic;

namespace Financiera_backend.Entity
{
    public partial class TabUser
    {
        public TabUser()
        {
            TabAccount = new HashSet<TabAccount>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public uint? IdUserType { get; set; }

        public virtual CatUserType IdUserTypeNavigation { get; set; }
        public virtual ICollection<TabAccount> TabAccount { get; set; }
    }
}

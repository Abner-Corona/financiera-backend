using System;
using System.Collections.Generic;

namespace Financiera_backend.Entity
{
    public partial class CatUserType
    {
        public CatUserType()
        {
            TabUser = new HashSet<TabUser>();
        }

        public uint Id { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }
        public string NameTranslation { get; set; }

        public virtual ICollection<TabUser> TabUser { get; set; }
    }
}

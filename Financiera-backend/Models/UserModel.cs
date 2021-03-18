using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financiera_backend.Models
{
    public class UserModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint? IdUserType { get; set; }
    }
}

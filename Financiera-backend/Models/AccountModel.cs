using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financiera_backend.Models
{
    public class GetAccountModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
    }
   
    public class AddAccountModel
    {
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public uint? IdUser { get; set; }
    }
    public class OperationAccountModel
    {


        public decimal? Amount { get; set; }

        public uint? IdAccount { get; set; }

    }
}

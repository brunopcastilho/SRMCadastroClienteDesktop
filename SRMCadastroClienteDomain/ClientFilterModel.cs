using System;
using System.Collections.Generic;
using System.Text;

namespace SRMCadastroClienteDomain
{
    public class ClientFilterModel
    {
        public String Name { get; set; }
        public String ProductName { get; set; }
        public String Segment { get; set; }
        public DateTime? CreationDateBegin { get; set; }
        public DateTime? CreationDateEnd { get; set; }
    }
}

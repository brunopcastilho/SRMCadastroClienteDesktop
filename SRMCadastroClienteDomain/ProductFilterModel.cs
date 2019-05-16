using System;
using System.Collections.Generic;
using System.Text;

namespace SRMCadastroClienteDomain
{
    public class ProductFilterModel
    {
        public String Name { get; set; }
        public DateTime? CreationDateBegin { get; set; }
        public DateTime? CreationDateEnd { get; set; }
    }
}

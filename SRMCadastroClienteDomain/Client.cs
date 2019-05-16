using System;
using System.Collections.Generic;

namespace SRMCadastroClienteDomain
{
    public class Client
    {
        public int Id { get;set; }
        public String Name { get; set; }
        public String Cnpj { get; set; }
        public String Segment { get; set; }
        public DateTime? BirthDate { get; set; }
        public IEnumerable<Product> ProductList { get; set; }
        public DateTime CreationDate { get; set; }
    }
}

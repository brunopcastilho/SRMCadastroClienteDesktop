using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using SRMCadastroClienteDomain;

namespace SRMCadastroClienteDB
{
    public class ProductAdapter : IAdapter<Product>
    {
        public IEnumerable<Product> getIEnumerable(SQLiteDataReader reader) {
            List<Product> lstProduct = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product();
                product.Id = (int)reader.GetInt64(0);
                product.Name = reader.GetString(1);
                product.CreationDate = reader.GetDateTime(2);
                lstProduct.Add(product);
            }

            return lstProduct;
        }
    }
}

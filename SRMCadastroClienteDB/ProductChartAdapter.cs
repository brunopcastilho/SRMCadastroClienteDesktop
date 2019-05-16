using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using SRMCadastroClienteDomain;

namespace SRMCadastroClienteDB
{
    public class ProductChartAdapter : IAdapter<ProductChart>
    {
        public IEnumerable<ProductChart> getIEnumerable(SQLiteDataReader reader) {
            List<ProductChart> lstProduct = new List<ProductChart>();
            while (reader.Read())
            {
                ProductChart product = new ProductChart();
                product.Id = (!reader.IsDBNull(0) ? (int)reader.GetInt64(0) : 0);
                product.Name = (!reader.IsDBNull(1) ? reader.GetString(1) : "");
                product.Quantity = (!reader.IsDBNull(2) ? reader.GetInt32(2) : 0);
                lstProduct.Add(product);
            }

            return lstProduct;
        }
    }
}

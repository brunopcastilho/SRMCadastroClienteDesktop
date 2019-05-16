using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using SRMCadastroClienteDomain;

namespace SRMCadastroClienteDB
{
    public class SegmentChartAdapter : IAdapter<SegmentChart>
    {
        public IEnumerable<SegmentChart> getIEnumerable(SQLiteDataReader reader) {
            List<SegmentChart> lstProduct = new List<SegmentChart>();
            while (reader.Read())
            {
                SegmentChart product = new SegmentChart();
                product.Segment = (!reader.IsDBNull(0) ? reader.GetString(0) : "");
                product.Quantity = (!reader.IsDBNull(1) ? reader.GetInt32(1) : 0);
                lstProduct.Add(product);
            }

            return lstProduct;
        }
    }
}

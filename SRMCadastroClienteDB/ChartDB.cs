using SRMCadastroClienteDomain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace SRMCadastroClienteDB
{
    public class ChartDB
    {
        public dynamic Queries { get; set; }

        public ChartDB()
        {
            Queries = SRMCadastroClientCommon.Util.ParseJson(@".\Resources\ChartScripts.json");
        }

        public IEnumerable<ProductChart> getProductChart()
        {
            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();

            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "CLIENT_BY_PRODUCT")
                {
                    return DALHelper<ProductChart>.GetDataReader(item.Value.ToString(), lstParameter, new ProductChartAdapter());
                }
            }

            return new List<ProductChart>();
        }

        public IEnumerable<SegmentChart> getSegmentChart()
        {
            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();

            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "CLIENT_BY_SEGMENT")
                {
                    return DALHelper<SegmentChart>.GetDataReader(item.Value.ToString(), lstParameter, new SegmentChartAdapter());
                }
            }

            return new List<SegmentChart>();
        }

    }
}

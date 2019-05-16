using System;
using System.Collections.Generic;
using System.Text;
using SRMCadastroClienteDomain;
using SRMCadastroClienteDB;

namespace SRMCadastroClienteBS
{
    public class ChartBS
    {

        public IEnumerable<ProductChart> getProductChart()
        {
            ChartDB chartDb = new ChartDB();
            return chartDb.getProductChart();
        }

        public IEnumerable<SegmentChart> getSegmentChart()
        {
            ChartDB chartDb = new ChartDB();
            return chartDb.getSegmentChart();
        }


    }
}

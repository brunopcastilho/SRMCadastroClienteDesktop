using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using SRMCadastroClienteBS;
using SRMCadastroClienteDomain;

namespace SRMCadastroClieneDesktop.Views.Chart
{
    /// <summary>
    /// Lógica interna para Chart.xaml
    /// </summary>
    /// 

    
    public partial class Chart : Window
    {

        public SeriesCollection seriesCollection;
        public List<String> title;
        public Chart()
        {
            InitializeComponent();
        }

        private void BtnProductClient_Click(object sender, RoutedEventArgs e)
        {

            ChartBS chartBs = new ChartBS();
            IEnumerable<ProductChart> lstChart = chartBs.getProductChart();




            LineSeries line = new LineSeries();
            ColumnSeries column = new ColumnSeries();

            title = new List<String>();
            ChartValues<int> columnValues = new ChartValues<int>();


            foreach (ProductChart product in lstChart)
            {

                title.Add(product.Name);
                columnValues.Add(product.Quantity);
                column.Values = columnValues;
           }
            

            seriesCollection = new SeriesCollection
            {
                column
            };

            chart.Series = seriesCollection;
            axisTitle.Labels = title;
            axisTitle.Title = "Produto";


        }

        private void BtnClientSegment_Click(object sender, RoutedEventArgs e)
        {
            ChartBS chartBs = new ChartBS();
            IEnumerable<SegmentChart> lstChart = chartBs.getSegmentChart();

            LineSeries line = new LineSeries();
            ColumnSeries column = new ColumnSeries();

            title = new List<String>();
            ChartValues<int> columnValues = new ChartValues<int>();


            foreach (SegmentChart product in lstChart)
            {

                title.Add(product.Segment);
                columnValues.Add(product.Quantity);
                column.Values = columnValues;
            }


            seriesCollection = new SeriesCollection
            {
                column
            };

            chart.Series = seriesCollection;
            axisTitle.Labels = title;
            axisTitle.Title = "Segmento";

        }
    }
 
}

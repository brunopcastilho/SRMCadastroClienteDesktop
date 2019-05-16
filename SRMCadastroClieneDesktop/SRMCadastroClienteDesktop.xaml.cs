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
using SRMCadastroClienteBS;

namespace SRMCadastroClieneDesktop
{
    /// <summary>
    /// Lógica interna para SRMCadastroClienteDesktop.xaml
    /// </summary>
    public partial class SRMCadastroClienteDesktop : Window
    {
        public SRMCadastroClienteDesktop()
        {
            InitializeComponent();
            CreateDatabaseBS createDatabaseBS = new CreateDatabaseBS();
            createDatabaseBS.createDataBase();
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            Views.Clients clientWindow = new Views.Clients();
            clientWindow.Show();
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            Views.Products productWindow = new Views.Products();
            productWindow.Show();
        }

        private void BtnGraficos_Click(object sender, RoutedEventArgs e)
        {
            Views.Chart.Chart chartWindow = new Views.Chart.Chart();
            chartWindow.Show();
        }
    }
}

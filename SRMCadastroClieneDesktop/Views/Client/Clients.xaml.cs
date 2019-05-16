using Microsoft.Win32;
using SRMCadastroClienteBS;
using SRMCadastroClienteDomain;
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

namespace SRMCadastroClieneDesktop.Views
{
    /// <summary>
    /// Lógica interna para Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        //BINDINGS
        IEnumerable<Client> clientList;
        public Clients()
        {
            InitializeComponent();
        }

        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            
            Client client = ((FrameworkElement)sender).DataContext as Client;
            AddClient addClientWindow = new AddClient(client);

            addClientWindow.Show();

        }



        private void BtnNewClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClientWindow = new AddClient(null);
            addClientWindow.Show();
        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    ClientBS productBs = new ClientBS();
                    ClientFilterModel model = new ClientFilterModel();
                    model.Name = txtName.Text;
                    model.Segment = txtSegment.Text;
                    model.CreationDateBegin = dtBegin.SelectedDate;
                    model.CreationDateEnd = dtEnd.SelectedDate;

                    clientList = productBs.getClientByFilter(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERRO: " + ex.Message);
                }
                grdClient.ItemsSource = clientList;

            }
        }

        private void BnExcelImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.ShowDialog();
            ExcelImportBS excelBs = new ExcelImportBS();
            List<Client> clientList = excelBs.importFromExcel(opf.FileName).ToList<Client>();

            MessageBox.Show("Processamento concluído. " + clientList.Count + " clientes inseridos/atualizados");
        }
    }
}

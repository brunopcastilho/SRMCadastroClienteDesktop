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
using SRMCadastroClienteDomain;
using SRMCadastroClienteBS;

namespace SRMCadastroClieneDesktop.Views
{
    /// <summary>
    /// Lógica interna para AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {

        IEnumerable<Product> productList;

        public AddClient(Client client)
        {
            InitializeComponent();
            if (client == null)
            {
                productList = new List<Product>();
            } else
            {
                loadClient(client);
                
            }

            grdProduct.ItemsSource = productList;
        }

        private void loadClient(Client client)
        {
            txtIdClient.Text = client.Id.ToString();
            txtClientName.Text = client.Name;
            txtClientCPF.Text = client.Cnpj;
            dtBirth.SelectedDate = client.BirthDate;
            txtSegment.Text = client.Segment;

            productList = client.ProductList;
            grdProduct.ItemsSource = productList;
            grdProduct.Items.Refresh();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductClient addProductClient = new AddProductClient(this);
            addProductClient.Show();
        }

        private void BtnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((FrameworkElement)sender).DataContext as Product;
            ((List<Product>)productList).Remove(product);

            grdProduct.ItemsSource = productList;
            grdProduct.Items.Refresh();
        }

        public void addNewProduct(Product product)
        {
            if (!productList.Contains(product))
            {
                ((List<Product>)productList).Add(product);
            }
            grdProduct.ItemsSource = productList;
            grdProduct.Items.Refresh();


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtIdClient.Text == "")
                {
                    Client client = new Client();
                    client.Name = txtClientName.Text;
                    client.Cnpj = txtClientCPF.Text;
                    client.BirthDate = dtBirth.SelectedDate;
                    client.Segment = txtSegment.Text;
                    client.CreationDate = System.DateTime.Now;


                    client.ProductList = productList;

                    ClientBS clientBs = new ClientBS();
                    client.Id =clientBs.AddClient(client);
                    txtIdClient.Text = client.Id.ToString();

                    

                    MessageBox.Show("Cliente " + client.Name + " ID " + client.Id + " Salvo com sucesso");

                }
                else
                {
                    Client client = new Client();
                    client.Id = int.Parse(txtIdClient.Text);
                    client.Name = txtClientName.Text;
                    client.Cnpj = txtClientCPF.Text;
                    client.Segment = txtSegment.Text;
                    client.BirthDate = dtBirth.SelectedDate;
                    client.CreationDate = System.DateTime.Now;

                    client.ProductList = productList;

                    ClientBS clientBs = new ClientBS();
                    client.Id = clientBs.EditClient(client);

                    MessageBox.Show("Cliente " + client.Name + " ID " + client.Id + " Alterado com sucesso");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }



        }
    }
}

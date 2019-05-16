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
    /// Lógica interna para AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtIdProduct.Text == "") { 
                    Product product = new Product();
                    product.Name = txtProduct.Text;
                    product.CreationDate = System.DateTime.Now;

                    ProductBS productBs = new ProductBS();
                    product.Id = productBs.AddProduct(product);
                    txtIdProduct.Text = product.Id.ToString();

                    MessageBox.Show("Produto " + product.Name + " ID " + product.Id + "Salvo com sucesso");

                    

                }
            else
                {
                    Product product = new Product();
                    product.Id = int.Parse(txtIdProduct.Text);
                    product.Name = txtProduct.Text;
                    product.CreationDate = System.DateTime.Now;

                    ProductBS productBs = new ProductBS();
                    product.Id = productBs.EditProduct(product);

                    MessageBox.Show("Produto " + product.Name + " ID " + product.Id + "Alterado com sucesso");
                }

            }
          
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }



        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

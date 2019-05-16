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
    /// Lógica interna para Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        //BINDINGS
        IEnumerable<Product> productList;


        public Products()
        {
            InitializeComponent();
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProductWindow = new AddProduct();
            addProductWindow.Show();
        }

        private void BtnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((FrameworkElement)sender).DataContext as Product;
            AddProduct addProductWindow = new AddProduct();
            addProductWindow.txtIdProduct.Text = product.Id.ToString();
            addProductWindow.txtProduct.Text = product.Name;

            addProductWindow.Show();

            

        }

        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductBS productBs = new ProductBS();
                ProductFilterModel model = new ProductFilterModel();
                model.Name = txtProductName.Text;
                model.CreationDateBegin = dtBegin.SelectedDate;
                model.CreationDateEnd = dtEnd.SelectedDate;

                productList = productBs.getProductByFilter(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.Message);
            }
            grdProduct.ItemsSource = productList;
            
        }
    }
}

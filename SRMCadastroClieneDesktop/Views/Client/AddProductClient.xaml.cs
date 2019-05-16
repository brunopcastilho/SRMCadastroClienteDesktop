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
    /// Lógica interna para AddProductClient.xaml
    /// </summary>
    public partial class AddProductClient : Window
    {
        IEnumerable<Product> productList;
        AddClient parent;


        public AddProductClient(AddClient parent)
        {
            InitializeComponent();

            ProductBS productBs = new ProductBS();

            productList = productBs.getAllProduct();
            cboProduct.ItemsSource = productList;
            this.parent = parent;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            parent.addNewProduct((Product)cboProduct.SelectedItem);
            this.Close();
        }
    }
}

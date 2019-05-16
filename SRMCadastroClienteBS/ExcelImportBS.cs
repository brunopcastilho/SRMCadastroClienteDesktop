using System;
using System.Collections.Generic;
using System.Text;
using SRMCadastroClienteDomain;
using SRMCadastroClienteDB;
using Excel = Microsoft.Office.Interop.Excel;

namespace SRMCadastroClienteBS
{
    public class ExcelImportBS
    {
        public IEnumerable<Client> importClientFromExcel(String excelPath)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook work = app.Workbooks.Open(excelPath);
            Excel.Worksheet sheet = (Excel.Worksheet)work.Sheets[1];

            List<Client> clientList = new List<Client>();
            List<Product> productList = new List<Product>();

            Client client = new Client();

            for (int i = 1; i <= 1000; i++)
            {
                String type = "";
                if (((Excel.Range)(sheet.UsedRange.Cells[i, 1])).Value2 != null)
                {
                    type = ((Excel.Range)(sheet.UsedRange.Cells[i, 1])).Value2.ToString();
                }
                if (type == "CLIENTE")
                {
                    if (i > 1)
                    {
                        clientList.Add(client);
                        client = new Client();
                    }
                    client.Name = ((Excel.Range)(sheet.UsedRange.Cells[i, 2])).Value2.ToString();
                    client.Cnpj = ((Excel.Range)(sheet.UsedRange.Cells[i, 3])).Value2.ToString();
                    String bd = ((Excel.Range)(sheet.UsedRange.Cells[i, 4])).Value2.ToString();
                    double d = double.Parse(bd);
                    client.BirthDate = DateTime.FromOADate(d);

                    client.Segment = ((Excel.Range)(sheet.UsedRange.Cells[i, 5])).Value2.ToString();
                    client.CreationDate = System.DateTime.Now;
                    client.ProductList = new List<Product>();

                }
                else if (type == "PRODUTO")
                {
                    Product product = new Product();
                    product.Name = ((Excel.Range)(sheet.UsedRange.Cells[i, 2])).Value2.ToString();
                    product.CreationDate = System.DateTime.Now;
                    ((List<Product>)client.ProductList).Add(product);
                }
                else if (type == "")
                {
                    clientList.Add(client);
                    break;
                }
            }


            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //close and release
            work.Close();

            //quit and release
            app.Quit();

            return clientList;
        }

        public IEnumerable<Client> importFromExcel(String excelPath)
        {
            IEnumerable<Client> clientList = importClientFromExcel(excelPath);
            clientList = updateProductFromExcel(clientList);
            clientList = updateClientFromExcel(clientList);

            return clientList;

        }

        public IEnumerable<Client> updateClientFromExcel(IEnumerable<Client> clientList)
        {

            ClientBS clientBS = new ClientBS();
            foreach (Client client in clientList)
            {
                Client clientdb = clientBS.getClientByName(client.Name);
                if (clientdb != null)
                {
                    clientBS.EditClient(client);
                }
                else
                {
                    clientBS.AddClient(client);
                }
            }

            return clientList;
        }


        private IEnumerable<Client> updateProductFromExcel(IEnumerable<Client> clientList)
        {
            ProductBS productBs = new ProductBS();

            foreach (Client client in clientList)
            {
                List<Product> productList = new List<Product>();
                foreach (Product product in client.ProductList)
                {
                    Product productdb = productBs.getProductByName(product.Name);
                    if (productdb != null)
                    {
                        productList.Add(productdb);
                    }
                    else
                    {
                        int productId = productBs.AddProduct(product);
                        product.Id = productId;
                        productList.Add(product);
                    }
                }
                client.ProductList = productList;
            }

            return clientList;

        }


    }
}

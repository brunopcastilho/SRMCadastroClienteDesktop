using System;
using System.Collections.Generic;
using System.Text;
using SRMCadastroClienteDomain;
using SRMCadastroClienteDB;

namespace SRMCadastroClienteBS
{
    public class ProductBS
    {
        public int AddProduct(Product product)
        {
            ProductDB productDB = new ProductDB();
            return productDB.AddProduct(product);
        }

        public int EditProduct(Product product)
        {
            ProductDB productDB = new ProductDB();
            return productDB.EditProduct(product);
        }

        public IEnumerable<Product> getProductByFilter(ProductFilterModel model)
        {
            ProductDB productDB = new ProductDB();
            return productDB.getProductByFilter(model);

        }

        public IEnumerable<Product> getAllProduct()
        {
            ProductDB productDB = new ProductDB();
            return productDB.getAllProduct();
        }

        public Product getProductByName(String name)
        {
            ProductDB productDB = new ProductDB();
            return productDB.getProductByName(name);
        }

    }
}

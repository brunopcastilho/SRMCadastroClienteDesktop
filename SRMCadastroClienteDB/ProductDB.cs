using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using SRMCadastroClienteDomain;

namespace SRMCadastroClienteDB
{
    public class ProductDB
    {
 
        public dynamic Queries { get; set; }

        public ProductDB()
        {
            Queries = SRMCadastroClientCommon.Util.ParseJson(@".\Resources\ProductScripts.json");
        }

        public int AddProduct(Product product)
        {

            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", product.Name));
            lstParameter.Add(new SQLiteParameter("@CREATIONDATE", product.CreationDate));
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "CREATE")
                {
                    return (int)DALHelper<Object>.ExecuteScalar(item.Value.ToString(), lstParameter);
                }
            }

            return 0;

        }

        public IEnumerable<Product> getProductByFilter(ProductFilterModel model)
        {

            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", "%" + model.Name + "%"));
            if (model.CreationDateBegin == null)
            {
                lstParameter.Add(new SQLiteParameter("@DATEBEGIN", System.DateTime.MinValue));
            } else
            {
                lstParameter.Add(new SQLiteParameter("@DATEBEGIN", model.CreationDateBegin));
            }
            if (model.CreationDateEnd == null)
            {
                lstParameter.Add(new SQLiteParameter("@DATEEND", System.DateTime.MaxValue));
            }
            else { 
                lstParameter.Add(new SQLiteParameter("@DATEEND", model.CreationDateEnd));
            }

            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "SELECT_BY_FILTER")
                {
                    return DALHelper<Product>.GetDataReader(item.Value.ToString(), lstParameter, new ProductAdapter());
                }
            }
            return new List<Product>();

        }

        public Product getProductByName(string name)
        {
            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", name ));
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "GET_BY_NAME")
                {
                    List<Product> tst = DALHelper<Product>.GetDataReader(item.Value.ToString(), lstParameter, new ProductAdapter());
                    if (tst.Count > 0){ 
                        return tst[0];
                    }
                }
            }
            return null;

        }

        public IEnumerable<Product> getAllProduct()
        {
            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "GET_ALL")
                {
                    return DALHelper<Product>.GetDataReader(item.Value.ToString(), lstParameter, new ProductAdapter());
                }
            }
            return new List<Product>();

        }

        public int EditProduct (Product product)
        {

            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", product.Name));
            lstParameter.Add(new SQLiteParameter("@ID", product.Id));

            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "UPDATE")
                {
                    DALHelper<Object>.ExecuteScalar(item.Value.ToString(), lstParameter);
                    return product.Id;
                }
            }

            return 0;

        }
    }
}

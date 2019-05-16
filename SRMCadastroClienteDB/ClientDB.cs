using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using SRMCadastroClienteDomain;

namespace SRMCadastroClienteDB
{
    public class ClientDB
    {
        public dynamic Queries { get; set; }

        public ClientDB()
        {
            Queries = SRMCadastroClientCommon.Util.ParseJson(@".\Resources\ClientScripts.json");
        }

        public int AddClient(Client client)
        {

            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", client.Name));
            lstParameter.Add(new SQLiteParameter("@CNPJ", client.Cnpj));
            lstParameter.Add(new SQLiteParameter("@SEGMENT", client.Segment));
            lstParameter.Add(new SQLiteParameter("@BIRTHDATE", client.BirthDate));
            lstParameter.Add(new SQLiteParameter("@CREATIONDATE", client.CreationDate));
            int idClient = 0;
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "CREATE")
                {
                    idClient = (int)DALHelper<Object>.ExecuteScalar(item.Value.ToString(), lstParameter);
                    client.Id = idClient;
                }
            }

            insertClientProduct(client);


            return idClient;

        }

        private void insertClientProduct(Client client)
        {

            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "DELETE_CLIENT_PRODUCT")
                {
                    lstParameter = new List<SQLiteParameter>();
                    lstParameter.Add(new SQLiteParameter("@ID_CLIENT", client.Id));
                    DALHelper<Object>.ExecuteNonQuery(item.Value.ToString(), lstParameter);
                }
            }


            foreach (Product product in client.ProductList)
            {

                lstParameter = new List<SQLiteParameter>();
                lstParameter.Add(new SQLiteParameter("@ID_CLIENT", client.Id));
                lstParameter.Add(new SQLiteParameter("@ID_PRODUCT", product.Id));

                foreach (var item in Queries.Scripts)
                {

                    if (item.Name == "INSERT_CLIENT_PRODUCT")
                    {
                        DALHelper<Object>.ExecuteScalar(item.Value.ToString(), lstParameter);
                    }
                }
            }
        }

        public int EditClient(Client client)
        {
            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", client.Name));
            lstParameter.Add(new SQLiteParameter("@CNPJ", client.Cnpj));
            lstParameter.Add(new SQLiteParameter("@SEGMENT", client.Segment));
            lstParameter.Add(new SQLiteParameter("@BIRTHDATE", client.BirthDate));
            lstParameter.Add(new SQLiteParameter("@ID", client.Id));

            
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "UPDATE")
                {
                    DALHelper<Object>.ExecuteScalar(item.Value.ToString(), lstParameter);
            
                }
            }

            insertClientProduct(client);

            return client.Id;

        }

        public IEnumerable<Client> getClientByFilter(ClientFilterModel model)
        {

            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", "%" + model.Name + "%"));
            lstParameter.Add(new SQLiteParameter("@SEGMENT", "%" + model.Segment + "%"));
            if (model.CreationDateBegin == null)
            {
                lstParameter.Add(new SQLiteParameter("@DATEBEGIN", System.DateTime.MinValue));
            }
            else
            {
                lstParameter.Add(new SQLiteParameter("@DATEBEGIN", model.CreationDateBegin));
            }
            if (model.CreationDateEnd == null)
            {
                lstParameter.Add(new SQLiteParameter("@DATEEND", System.DateTime.MaxValue));
            }
            else
            {
                lstParameter.Add(new SQLiteParameter("@DATEEND", model.CreationDateEnd));
            }

            IEnumerable<Client> lstClient = new List<Client>();
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "SELECT_CLIENT_BY_FILTER")
                {
                    lstClient  =  DALHelper<Client>.GetDataReader(item.Value.ToString(), lstParameter, new ClientAdapter());
                }
            }

            foreach(Client client in lstClient) {
                List<SQLiteParameter> lstParameterClient = new List<SQLiteParameter>();
                lstParameter.Add(new SQLiteParameter("@ID", client.Id));
                foreach (var item in Queries.Scripts)
                {
                    if (item.Name == "SELECT_PRODUCT_BY_CLIENT")
                    {
                        client.ProductList = DALHelper<Product>.GetDataReader(item.Value.ToString(), lstParameter, new ProductAdapter());
                    }
                }
                
            }

            return lstClient;

        }

        public Client getClientByName(string name)
        {
            List<SQLiteParameter> lstParameter = new List<SQLiteParameter>();
            lstParameter.Add(new SQLiteParameter("@NAME", name));
            Client returnClient = null;
            foreach (var item in Queries.Scripts)
            {
                if (item.Name == "GET_BY_NAME")
                {
                    List<Client> tst = DALHelper<Client>.GetDataReader(item.Value.ToString(), lstParameter, new ClientAdapter());
                    if (tst.Count > 0)
                    {
                        returnClient = tst[0];
                    }
                }
            }

            if (returnClient != null)
            {
                List<SQLiteParameter> lstParameterClient = new List<SQLiteParameter>();
                lstParameter.Add(new SQLiteParameter("@ID", returnClient.Id));
                foreach (var item in Queries.Scripts)
                {
                    if (item.Name == "SELECT_PRODUCT_BY_CLIENT")
                    {
                        returnClient.ProductList = DALHelper<Product>.GetDataReader(item.Value.ToString(), lstParameter, new ProductAdapter());
                    }
                }
            }

            return returnClient;

        }


    }
}

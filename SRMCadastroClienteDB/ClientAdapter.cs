using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using SRMCadastroClienteDomain;

namespace SRMCadastroClienteDB
{
    public class ClientAdapter : IAdapter<Client>
    {
        public IEnumerable<Client> getIEnumerable(SQLiteDataReader reader) {
            List<Client> lstProduct = new List<Client>();
            while (reader.Read())
            {
                Client client = new Client();
                client.Id = (!reader.IsDBNull(0) ? (int)reader.GetInt64(0) : 0);
                client.Name = (!reader.IsDBNull(1) ?  reader.GetString(1) : "");                
                client.Cnpj = (!reader.IsDBNull(2) ? reader.GetString(2) : "");
                client.Segment = (!reader.IsDBNull(3) ? reader.GetString(3) : "");
                client.BirthDate = (!reader.IsDBNull(4) ? reader.GetDateTime(4) : DateTime.MinValue);
                client.CreationDate = (!reader.IsDBNull(5) ? reader.GetDateTime(5) : DateTime.MinValue);
                lstProduct.Add(client);
                
            }
            return lstProduct;
        }
    }
}




//SELECT ID, NAME, CNPJ, SEGMENT, BIRTHDATE, CREATIONDATE FROM CLIENT WHERE NAME like @NAME AND CREATIONDATE >= @DATEBEGIN AND CREATIONDATE <= @DATEEND
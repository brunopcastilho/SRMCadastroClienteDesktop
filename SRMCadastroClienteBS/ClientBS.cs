using System;
using System.Collections.Generic;
using System.Text;
using SRMCadastroClienteDomain;
using SRMCadastroClienteDB;

namespace SRMCadastroClienteBS
{
    public class ClientBS
    {
        public int AddClient(Client client)
        {
            ClientDB clientDb = new ClientDB();
            return clientDb.AddClient(client);
        }

        public int EditClient(Client client)
        {
            ClientDB clientDb = new ClientDB();
            return clientDb.EditClient(client);
        }

        public IEnumerable<Client> getClientByFilter(ClientFilterModel model)
        {
            ClientDB clientDb = new ClientDB();
            return clientDb.getClientByFilter(model);
        }

        public Client getClientByName(String name)
        {
            ClientDB clientDB = new ClientDB();
            return clientDB.getClientByName(name);
        }
    }
}

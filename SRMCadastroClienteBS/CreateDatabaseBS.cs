using SRMCadastroClienteDB;
using System;

namespace SRMCadastroClienteBS
{
    public class CreateDatabaseBS     
    {
        public void createDataBase()
        {
            CreateDatabaseDB db = new CreateDatabaseDB();
            db.buildDataBase();
        }
    }
}

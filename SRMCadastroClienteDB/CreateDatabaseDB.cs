using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json;
using SRMCadastroClientCommon;

namespace SRMCadastroClienteDB
{
    public class CreateDatabaseDB
    {
        private SQLiteConnection sqlConn { get; set; } 

        private  Boolean checkIfDataBaseExists()
        {
            if (File.Exists(Util.getAppSetting("DatabaseLocation")))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public Boolean buildDataBase()
        {
            try
            {
                if (checkIfDataBaseExists())
                {
                    return true;
                }
                else
                {

                    dynamic jsonDDL = SRMCadastroClientCommon.Util.ParseJson(@".\Resources\DataBaseDDL.json");
                    foreach (var item in jsonDDL.DDL)
                    {
                        DALHelper<Object>.ExecuteNonQuery(item.Value.ToString());
                    }
                }
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
            
            


        }
        

    }
}


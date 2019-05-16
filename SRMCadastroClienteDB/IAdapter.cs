using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace SRMCadastroClienteDB
{
    public interface IAdapter<T>
    {
        IEnumerable<T> getIEnumerable(SQLiteDataReader reader);
    }
}

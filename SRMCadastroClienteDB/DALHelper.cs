using SRMCadastroClientCommon;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SRMCadastroClienteDB
{
    public class DALHelper<T>
    {

        private static SQLiteConnection _conn { get; set; }
        public static SQLiteConnection conn
        {
            get
            {
                if (_conn == null)
                {
                    _conn = new SQLiteConnection("Data Source=" + dataBasePath);

                }
                return _conn;

            }
        }
        public static string dataBasePath
        {
            get { return Util.getAppSetting("DatabaseLocation"); }
        }

        public static void ExecuteNonQuery(String cmd, IEnumerable<SQLiteParameter> parameters)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = cmd;
                foreach (SQLiteParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static void ExecuteNonQuery(String cmd)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = cmd;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static IEnumerable<T> GetDataReader(String cmd, IEnumerable<SQLiteParameter> parameters, IAdapter<T> adapter)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = cmd;
                foreach (SQLiteParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
                return adapter.getIEnumerable(command.ExecuteReader());
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        public static Object ExecuteScalar(String cmd, IEnumerable<SQLiteParameter> parameters)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = cmd;
                foreach (SQLiteParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }



                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static Object ExecuteScalar(String cmd)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

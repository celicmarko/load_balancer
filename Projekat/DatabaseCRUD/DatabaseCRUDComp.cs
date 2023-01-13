using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace DatabaseCRUD
{
    public class DatabaseCRUDComp : IDatabase
    {
        public bool PostojiUBazi(int id)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                connection.Open();
                return PostojiUBazi(id, connection);
            }
        }
    }

    private bool PostojiUBazi(int id, IDbConnection connection)
        {
            string query = "select * from brojilo where id = :id_b";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id", id);
                return command.ExecuteScalar() != null;
            }
        }
}

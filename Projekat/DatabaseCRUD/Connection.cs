using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCRUD
{
    public class Connection
    {
        public static readonly string DATA_SOURCE = "//localhost:1521/xe";
        public static readonly string USER_ID = "***";
        public static readonly string PASSWORD = "***";

        private static IDbConnection instance = null;

        public static IDbConnection GetConnection()
        {
            if (instance == null || instance.State == System.Data.ConnectionState.Closed)
            {
                OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
                ocsb.DataSource = DATA_SOURCE;
                ocsb.UserID = USER_ID;
                ocsb.Password = PASSWORD;

                ocsb.Pooling = true;
                ocsb.MaxPoolSize = 10;
                ocsb.MinPoolSize = 1;

                instance = new OracleConnection(ocsb.ConnectionString);


            }
            return instance;
        }

        public void Dispose()
        {
            if (instance != null)
            {
                instance.Close();
                instance.Dispose();
            }
        }
    }
}

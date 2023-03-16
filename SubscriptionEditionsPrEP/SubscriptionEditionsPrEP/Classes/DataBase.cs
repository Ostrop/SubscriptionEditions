using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SubscriptionEditionsPrEP.Models;
using System.Data;

namespace SubscriptionEditionsPrEP.Classes
{
    class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DIKHNICHHONOR\SQLEXPRESS;Initial Catalog=SubscriptionEditions;Integrated Security=True");


        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
        }

        public void closedConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

        public IEnumerable<Recipients> GetCompanies()
        {
            using (var connection = sqlConnection)
            {
                connection.Open();
                return connection.Query<Recipients>("SELECT * FROM Companies");
            }
        }
    }
}

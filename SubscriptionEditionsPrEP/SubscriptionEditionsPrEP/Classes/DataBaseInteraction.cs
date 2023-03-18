using SubscriptionEditionsPrEP.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SubscriptionEditionsPrEP.Classes
{
    public static class DataBaseInteraction
    {
        public static Recipients CheckUser(string login, string password)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-HRSRDD3;Initial Catalog=EducPrac;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string str = $"SELECT (Convert(Nvarchar(max), DecryptByCert(Cert_ID('SertPassword'), password, N'12345'))) FROM Recipients;";
            SqlCommand command = new SqlCommand(str, sqlConnection);

            adapter.SelectCommand = command;
            adapter.Fill(table);

            using (EducPracEntities1 db = new EducPracEntities1())
            {
                Recipients recipient = null;
                var Users = db.Recipients.ToList();
                for (int i = 0; i < Users.Count(); i++)
                {
                    if (table.Rows[i].ItemArray[0].ToString() == password && Users[i].login == login)
                        recipient = Users[i];

                }
                return recipient;
            }
        }
    }
}

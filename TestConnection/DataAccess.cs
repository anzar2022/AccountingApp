using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConnection
{
    public class DataAccess
    {
        string strConenction = "Server=account-sql-server,1438;Database=AccountingApp;Persist Security Info=True;User ID=sa;Password=Sahil@2014;TrustServerCertificate=True";
        public void TestConnection() {

            try
            {
                using (SqlConnection connection = new SqlConnection(strConenction))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        Console.WriteLine("Connection satart");
                        connection.Open();
                        Console.WriteLine("Connection open");
                    }
                    else
                    {
                        Console.WriteLine("Connection close");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

    }
}

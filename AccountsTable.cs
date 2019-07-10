using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace InstinetTicketer
{
    public static class AccountsTable
    {
        private static MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        public static DataTable dT = new DataTable();


        public static void getTable()
        {
            conn_string.Server = ConfigurationManager.AppSettings["server"];
            conn_string.UserID = ConfigurationManager.AppSettings["userID"];
            conn_string.Password = ConfigurationManager.AppSettings["password"];
            conn_string.Database = ConfigurationManager.AppSettings["database"];

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                MySqlDataAdapter AssetAdapter = new MySqlDataAdapter("Select distinct `Account Number` as Account, `PWATCH Equivalent` as Pwatch from kinsman.Accounts order by `Account Number`", conn);
                AssetAdapter.Fill(dT);

            }
        }
    }
}

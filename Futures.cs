using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace InstinetTicketer
{
    public static class Futures
    {

        private static MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
        public static DataTable FuturesTable = new DataTable();

        public static void LoadFutures()
        {
            conn_string.Server = ConfigurationManager.AppSettings["server"];
            conn_string.UserID = ConfigurationManager.AppSettings["userID"];
            conn_string.Password = ConfigurationManager.AppSettings["password"];
            conn_string.Database = ConfigurationManager.AppSettings["database"];


            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {

                    comm.Connection = conn;
                    conn.Open();

                    using (MySqlDataAdapter DA = new MySqlDataAdapter("Select `Pwatch Ticker` as Pwatch, `Reuters Ticker` as Reuters from kinsman.securities where `Asset Class` like '%FUTURES%' order by securities.id desc limit 10 ", conn))
                    {
                        DA.Fill(FuturesTable);
                    }
                }
            }
        }
    }
}

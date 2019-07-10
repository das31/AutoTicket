using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace InstinetTicketer
{
    class BaseTrades
    {
        public string Symbol { get; set; }
        public string Side { get; set; }
        public int Shares { get; set; }
        public decimal Avg_Price { get; set; }
        public string ClientAcct { get; set; }
        

        public MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();

    }
}

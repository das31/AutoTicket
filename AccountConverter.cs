using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace InstinetTicketer
{
    public class AccountConverter
    {
        private MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();


        public string ClientAccount { get; set; }


        public AccountConverter()
        {

        }
    }
}

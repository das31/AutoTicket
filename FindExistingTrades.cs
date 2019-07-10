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

    class  FindExistingTrades : BaseTrades
    {
        private string sql;
        private int Index;

        public FindExistingTrades(string _symbol,string _side, int _shares, decimal _price, string _account, int _index)
        {
            Symbol = _symbol.GetUntilOrEmpty().getFutures();
            Side = _side.removeShort();
            Avg_Price = _price;
            ClientAcct = _account;
            Shares = _shares.isBuyOrSell(Side.removeShort());
            Index = _index;



            conn_string.Server = ConfigurationManager.AppSettings["server"];
            conn_string.UserID = ConfigurationManager.AppSettings["userID"];
            conn_string.Password = ConfigurationManager.AppSettings["password"];
            conn_string.Database = ConfigurationManager.AppSettings["database"];

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The checker value to determine what to do with the trade object</returns>
        public async Task<int> Find()
        {
            if (ClientAcct != "PRBCOAV")
            {
                sql = "and Portfolio like '%" + ClientAcct + "%'";
                    
            }
            else
            {
                sql = "";
            }
     
            /* Find the most recent FUTURE for NASDAQ and S&P500 and try to convert the Reuters Ticker */


            /* Find the sum of shares in the Kinsman based on the sum of shares in instinet, if they match find the number trades that were made for the ticker */

            var t = new Task<int>(FindExact);
            var t2 = new Task<int>(FindZeroPriceTrades);
            var t3 = new Task<int>(FindMinimalMatch);

            t.Start();
            t2.Start();
            t3.Start();


            var findExact = await t;
            var findZeroPrice = await t2;
            var findMinimal = await t3;


            if (findExact > 0)
            {
                 return findExact;
            }
            else if(findZeroPrice > 0)
            {

                return findZeroPrice;
            } 
            else
            {

                return findMinimal;
            }



        }

        public void PaintDGV()
        {

        }
        public int FindExact()
        {
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;

                    if (ClientAcct.Contains(",") || ClientAcct == "PRBCOAV")
                    {
                        comm.CommandText = "Select Count(*) from kinsman.blotter_view where Ticker LIKE '%" + Symbol + "%' and Action LIKE '%" + Side + "%' and Price = '" + Avg_Price + "'";
                        conn.Open();
                        var val = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();
                        if (val > 0)
                        {
                            return 2;
                        }
                        return -1;
                    }
                    else
                    {
                        comm.CommandText = "Select Count(*) from kinsman.blotter_view where Ticker LIKE '%" + Symbol + "%' and Action LIKE '%" + Side + "%' and Shares = '" + Shares + "' and Price = '" + Avg_Price + "'";
                        conn.Open();
                        var val = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();
                        if (val > 0)
                        {
                            return 2;
                        }
                        return -1;
                    }
                }
            }
        }


        public int FindZeroPriceTrades()
        {
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;

                    if (ClientAcct.Contains(",") || ClientAcct == "PRBCOAV")
                    {
                        comm.CommandText = "Select Count(*) from kinsman.blotter_view where Ticker LIKE '%" + Symbol + "%' and Action LIKE '%" + Side + "%' and Price = '0'";
                        conn.Open();
                        var val2 = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();

                        if (val2 > 0)
                        {
                            return 3;
                        }
                        return -1;
                    }
                    else
                    {
                        comm.CommandText = "Select Count(*) from kinsman.blotter_view where Ticker LIKE '%" + Symbol + "%' and Action LIKE '%" + Side + "%' and Price = '0' and Shares = '" + Shares + "'";
                        conn.Open();
                        var val2 = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();

                        if (val2 > 0)
                        {
                            return 3;
                        }
                        return -1;
                    }
                }
            }
        }

        public int FindMinimalMatch()
        {
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;

                    if (ClientAcct.Contains(",") || ClientAcct == "PRBCOAV")
                    {
                        comm.CommandText = "Select Count(*) from kinsman.blotter_view where Ticker LIKE '%" + Symbol + "%' and Action LIKE '%" + Side + "%'";
                        conn.Open();
                        var val3 = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();

                        if (val3 > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        comm.CommandText = "Select Count(*) from kinsman.blotter_view where Ticker LIKE '%" + Symbol + "%' and Action LIKE '%" + Side + "%' and Shares = '" + Shares + "'";
                        conn.Open();
                        var val3 = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();

                        if (val3 > 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }




        /// <summary>
        /// Grabs the list of portfolios for the particular trade
        /// </summary>
        /// <returns></returns>
        public List<string> FindPortfolios()
        {
            List<string> ListOfPortfolios = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {


                    comm.CommandText = "Select Portfolio from Kinsman.blotter_view where Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%'" + " and Action like '%" + Side.removeShort() + "%' and Shares = '" + Shares.isBuyOrSell(Side.removeShort()) + "';";

                    comm.Connection = conn;
                    conn.Open();
                    var reader = Convert.ToString(comm.ExecuteScalar());

                    var _portfolios = reader.Split(',');
                    foreach (string str in _portfolios)
                    {
                        ListOfPortfolios.Add(str);
                    }

                }
            }
            return ListOfPortfolios;
        }

        public decimal getCommission()
        {
            decimal commission;
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {


                    comm.CommandText = "Select Commission from Kinsman.blotter_view where Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%'" + " and Action like '%" + Side.removeShort() + "%' and Shares = '" + Shares.isBuyOrSell(Side.removeShort()) + "' limit 1;";

                    comm.Connection = conn;
                    conn.Open();
                    commission = Convert.ToDecimal(comm.ExecuteScalar());


                }
            }
            return commission;
        }
       
        public string getIssuer()
        {
            string Issuer;
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {


                    comm.CommandText = "Select Issuer from Kinsman.trades where trade_date = '"+ DateTime.Today.ToString("yyyy-MM-dd") + "' and Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%'" + " and Action like '%" + Side.removeShort() + "%' and Portfolio Like '%" + ClientAcct + "%' and Price = '" + Avg_Price + "' limit 1;";
                    comm.Connection = conn;
                    conn.Open();
                    Issuer = Convert.ToString(comm.ExecuteScalar());
                }
            }
            return Issuer;
        }

        public string GetSettlementDate()
        {
            DateTime settlementDate;
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {


                    comm.CommandText = "Select `Settlement Date` from Kinsman.trades where trade_date = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%' and Action like '%" + Side.removeShort() + "%' and Portfolio Like '%" + ClientAcct + "%' and Price = '" + Avg_Price + "' limit 1;";
                    comm.Connection = conn;
                    conn.Open();
                    settlementDate = Convert.ToDateTime(comm.ExecuteScalar());
                }
            }
            return settlementDate.ToString("yyyy-MM-dd");
        }




        public string GetPwatchTicker()
        {
            string ticker;

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {

                    comm.CommandText = "Select `Ticker` from kinsman.blotter_View where Shares = '" + Shares.isBuyOrSell(Side.removeShort()) + "' and Price = '" + Avg_Price + "' and Portfolio LIKE '%" + ClientAcct + "%' limit 1;";
                    comm.Connection = conn;
                    conn.Open();
                    ticker = Convert.ToString(comm.ExecuteScalar());
                }
            }
            return ticker;
        }

        public string GetCurrency()
        {
           
            if (GetPwatchTicker().Contains("US"))
            {
                return "USD";
            }
            else
            {
                return "CAD";
            }
                   
        }

        public string GetSecurityName()
        {
            string securityName;

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {

                    comm.CommandText = "Select `Security Name` from kinsman.Securities where `Pwatch Ticker` = '" + GetPwatchTicker() + "' limit 1;";
                    comm.Connection = conn;
                    conn.Open();
                    securityName = Convert.ToString(comm.ExecuteScalar());

                }
            }

            return securityName;
        }


        public int GetShares()
        {
            int __shares;

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {

                    comm.CommandText = "Select `Shares` from kinsman.Trades where `Ticker` LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%' and Trade_Date = '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and Portfolio LIKE '%" + ClientAcct + "%' and Price = '" +Avg_Price + "' and Action LIKE '%" + Side.removeShort() + "%' Limit 1;";
                    comm.Connection = conn;
                    conn.Open();
                    __shares = Convert.ToInt32(comm.ExecuteScalar());

                }
            }

            return __shares;
        }
    }
}

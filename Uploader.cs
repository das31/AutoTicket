using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InstinetTicketer
{
    [System.Runtime.InteropServices.Guid("9B5F181F-CE4E-45ED-8BAD-2FCDB0E42520")]
    class Uploader : IBaseTrades
    {
        public string Symbol { get; set; }
        public string Side { get; set; }
        public int Shares { get; set; }
        public decimal Avg_Price { get; set; }
        public string ClientAcct { get; set; }
        public decimal Commission { get; set; }

        private MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();


        public Uploader(string _symbol, string _side, decimal _price, int _quantity, string _account, decimal _comm)
        {

            Symbol = _symbol;
            Side = _side;
            Avg_Price = _price;
            ClientAcct = _account;
            Commission = _comm;
            Shares = _quantity;
            

            conn_string.Server = ConfigurationManager.AppSettings["server"];
            conn_string.UserID = ConfigurationManager.AppSettings["userID"];
            conn_string.Password = ConfigurationManager.AppSettings["password"];
            conn_string.Database = ConfigurationManager.AppSettings["database"];

        }


        public void Upload()
        {
            if (ClientAcct == "PRBCOAV" || ClientAcct == "" || ClientAcct == null)
            {
                throw new Exception("1");
            }
            var _name = Regex.Replace(Environment.UserName, "[']", "");
            using(MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    conn.Open();
                    if (ClientAcct.Contains(","))
                    {
                        foreach(string str in ClientAcct.Split(',').ToList())
                        {
                            try
                            {

                                comm.CommandText = " Create temporary Table kinsman.tmpTrades like kinsman.trades; Insert into Kinsman.tmpTrades(Ticker,Action, Shares,Price, Portfolio, Commission)Values('" + Symbol + "', '" + Side.requiresShort() + "' , '" + Shares.isBuyOrSell(Side.removeShort()) + "', '" + Avg_Price + "', '" + str.Trim() + "', '" + Commission/100 + "'); " +
                                    "Call Instinet_Processor;" + "Call Post_insert_defaults_1;" + " Insert into kinsman.trades(Issuer, Record_Date, Last_Action_Time, Trade_Date, `Settlement Date`, Ticker, Portfolio, `Portfolio Code`, `Old_Weight`, New_Weight, Shares, Action, Direction, Price, Commission, Concession, `Hard Commission Per Share`, `Commission Type`, `Trade Type`, `Trade Type Percentage`, `Issue Type`, `Accrued Interest`, `Security Fee`, `MISC Fee`, Tax, FX, Broker, Allocation_Notes, OPS_Allocations, Trader, Last_Action_By, Is_Complete, Is_Warehoused, Is_Deleted, Is_Block_Trade, Notes, `Blotter Notes`, `Ledger Notes`, `Status`, Source, `Split Id`, Communication, Record_TimeStamp, Batch, Concession_id, Skip_in_audit_trail, `Settlement Amount`, `Cash Flow`, `Full Process Date`, `Process ID`, `Bulk Process ID`, `Process Flag`, `Type Flag`,`Cancel ID`, `Event 1 Corrected`, `Security ID`, `Group`)" +
                                                     "Select '" + _name + "', Current_Timestamp(), Last_Action_Time, Trade_Date, `Settlement Date`, Ticker, Portfolio, `Portfolio Code`, `Old_Weight`, New_Weight, Shares, Action, Direction, Price, Commission, Concession, `Hard Commission Per Share`, `Commission Type`, `Trade Type`, `Trade Type Percentage`, `Issue Type`, `Accrued Interest`, `Security Fee`, `MISC Fee`, Tax, FX, Broker, Allocation_Notes, OPS_Allocations, '" + _name + "', '" + _name + "' , '1', Is_Warehoused, Is_Deleted, Is_Block_Trade, Notes, `Blotter Notes`, `Ledger Notes`, 'Complete', Source, `Split Id`, Communication, Record_TimeStamp, Batch, Concession_id, Skip_in_audit_trail, `Settlement Amount`, `Cash Flow`, `Full Process Date`, `Process ID`, `Bulk Process ID`, `Process Flag`, `Type Flag`,`Cancel ID`, `Event 1 Corrected`, `Security ID`, `Group` from kinsman.tmptrades;" +
                                                        "Drop Temporary Table kinsman.tmpTrades;";
                                comm.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                comm.CommandText = "Drop temporary Table kinsman.tmpTrades";
                                comm.ExecuteNonQuery();
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            comm.CommandText = " Create temporary Table kinsman.tmpTrades like kinsman.trades; Insert into Kinsman.tmpTrades(Ticker,Action, Shares,Price, Portfolio, Commission)Values('" + Symbol + "', '" + Side.requiresShort() + "' , '" + Shares.isBuyOrSell(Side.removeShort()) + "', '" + Avg_Price + "', '" + ClientAcct + "', '" + Commission/100 + "'); " +
                                "Call Instinet_Processor;" + "Call Post_insert_defaults_1;" + " Insert into kinsman.trades(Issuer, Record_Date, Last_Action_Time, Trade_Date, `Settlement Date`, Ticker, Portfolio, `Portfolio Code`, `Old_Weight`, New_Weight, Shares, Action, Direction, Price, Commission, Concession, `Hard Commission Per Share`, `Commission Type`, `Trade Type`, `Trade Type Percentage`, `Issue Type`, `Accrued Interest`, `Security Fee`, `MISC Fee`, Tax, FX, Broker, Allocation_Notes, OPS_Allocations, Trader, Last_Action_By, Is_Complete, Is_Warehoused, Is_Deleted, Is_Block_Trade, Notes, `Blotter Notes`, `Ledger Notes`, `Status`, Source, `Split Id`, Communication, Record_TimeStamp, Batch, Concession_id, Skip_in_audit_trail, `Settlement Amount`, `Cash Flow`, `Full Process Date`, `Process ID`, `Bulk Process ID`, `Process Flag`, `Type Flag`,`Cancel ID`, `Event 1 Corrected`, `Security ID`, `Group`)" +
                                                 "Select '" + _name + "', Current_Timestamp(), Last_Action_Time, Trade_Date, `Settlement Date`, Ticker, Portfolio, `Portfolio Code`, `Old_Weight`, New_Weight, Shares, Action, Direction, Price, Commission, Concession, `Hard Commission Per Share`, `Commission Type`, `Trade Type`, `Trade Type Percentage`, `Issue Type`, `Accrued Interest`, `Security Fee`, `MISC Fee`, Tax, FX, Broker, Allocation_Notes, OPS_Allocations, '" + _name + "', '" + _name + "' , '1', Is_Warehoused, Is_Deleted, Is_Block_Trade, Notes, `Blotter Notes`, `Ledger Notes`, 'Complete', Source, `Split Id`, Communication, Record_TimeStamp, Batch, Concession_id, Skip_in_audit_trail, `Settlement Amount`, `Cash Flow`, `Full Process Date`, `Process ID`, `Bulk Process ID`, `Process Flag`, `Type Flag`,`Cancel ID`, `Event 1 Corrected`, `Security ID`, `Group` from kinsman.tmptrades;" +
                                                    "Drop Temporary Table kinsman.tmpTrades;";
                            comm.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            comm.CommandText = "Drop temporary Table kinsman.tmpTrades";
                            comm.ExecuteNonQuery();
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
        }

        public void Update()
        {
            if (ClientAcct == "PRBCOAV" || ClientAcct == "" || ClientAcct == null)
            {
                throw new Exception("1");
            }
            var _name = Regex.Replace(Environment.UserName, "[']", "");

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    try
                    {
                        comm.Connection = conn;
                        conn.Open();
                        comm.CommandText = "Update kinsman.Trades set Kinsman.Trades.Commission = '" + Commission / 100 + "', Kinsman.Trades.Status = 'Complete', Kinsman.Trades.Is_Complete = '1', Kinsman.Trades.Trader = '" + _name + "', Kinsman.Trades.Broker = 'Instinet' where Trade_date = '" + DateTime.Today.ToString("yyyy-MM-dd")
                        + "' and Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%'" + " and Action like '%" + Side.removeShort() + "%' and Price = '" + Avg_Price + "'";
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }



        public void ForceUpdate()
        {
            if (ClientAcct == "PRBCOAV" || ClientAcct == "" || ClientAcct == null)
            {
                throw new Exception("1");
            }
            var _name = Regex.Replace(Environment.UserName, "[']", "");

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {

                    comm.Connection = conn;
                    conn.Open();

                    if (ClientAcct.Contains(","))
                    {
                        foreach (string str in ClientAcct.Split(',').ToList())
                        {

                            try
                            {
                                comm.CommandText = "Update kinsman.Trades set Kinsman.Trades.Commission = '" + Commission / 100 + "', Kinsman.Trades.Price = '" + Avg_Price + "', Kinsman.Trades.Status = 'Complete', Kinsman.Trades.Is_Complete = '1', Kinsman.Trades.Broker = 'Instinet', Kinsman.Trades.Trader = '"+ _name +"' where Trade_Date = '" + DateTime.Today.ToString("yyyy-MM-dd")
                                    + "' and Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%'" + " and Action like '%" + Side.removeShort() + "%' and Portfolio = '" + str.Trim() + "' and Is_Deleted <> '1'";
                                comm.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        try
                        {

                            comm.CommandText = "Update kinsman.Trades set Kinsman.Trades.Commission = '" + Commission / 100 + "', Kinsman.Trades.Price = '" + Avg_Price + "', Kinsman.Trades.Status ='Complete', Kinsman.Trades.Is_Complete = '1', Kinsman.Trades.Broker = 'Instinet', Kinsman.Trades.Trader ='" + _name + "'  where Trade_date = '" + DateTime.Today.ToString("yyyy-MM-dd")
                            + "' and Ticker LIKE '%" + Symbol.GetUntilOrEmpty().getFutures() + "%'" + " and Action like '%" + Side.removeShort() + "%' and Shares = '" + Shares.isBuyOrSell(Side.removeShort()) + "' and Is_Deleted <> '1'";
                            comm.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                    }
                }
            }
        }
    }
}

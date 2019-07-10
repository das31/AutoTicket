using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace InstinetTicketer
{
    public partial class Allocate : Form
    {


        NotifyDGVChange notifyDel;
        public int Quantity;
        public string Symbol;
        public decimal Avg_Price;
        public string Side;
        public static bool _checked;
        public MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();


        public Allocate(string symbol, string side, int quantity, decimal avg_Price, NotifyDGVChange notify)
        {
            InitializeComponent();

            conn_string.Server = ConfigurationManager.AppSettings["server"];
            conn_string.UserID = ConfigurationManager.AppSettings["userID"];
            conn_string.Password = ConfigurationManager.AppSettings["password"];
            conn_string.Database = ConfigurationManager.AppSettings["database"];
            MySqlConnection conn = new MySqlConnection(conn_string.ToString());



            DataTable currencyTable = new DataTable();
            MySqlDataAdapter currencyAdapter = new MySqlDataAdapter("SELECT DISTINCT kinsman.`group list`.`portfolio`FROM kinsman.`group list` inner join kinsman.accounts ON `group list`.Portfolio = accounts.`PWATCH Equivalent` where `group name`='NON-IMA'; ", conn);
            currencyAdapter.Fill(currencyTable);
            tbcClientAcct.DataSource = currencyTable;
            tbcClientAcct.DisplayMember = "portfolio";
            

            lblAllocate.Text = "Allocate accounts for " + symbol + Environment.NewLine + "Side: " + side + Environment.NewLine + "Price: " + avg_Price;
            label1.Text = side;
            label2.Text = avg_Price.ToString();
            if (label1.Text == "BUY") { label1.BackColor = Color.Cyan; } else { label1.BackColor = Color.Pink; }
            lblTotal.Text = quantity.ToString();
            DGVAllocator.Rows.Add();
            DGVAllocator["tbcQty", 0].Value = quantity;

            notifyDel = notify;

            this.Avg_Price = avg_Price;
            this.Symbol = symbol;
            this.Side = side;



        }

        int _alloc;

        private void DGVAllocator_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                DGVAllocator.Rows.AddCopies(e.RowIndex, 1);
            }
        }

        private void btnSaveAllo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGVAllocator.Rows)
            {
                if (DGVAllocator["tbcCommission", row.Index].Value == null && DGVAllocator["tbcQty", row.Index].Value != null)
                {
                    DGVAllocator["tbcCommission", row.Index].Value = 0;
                }
            }

            
            if (!(_alloc == 0))
            {
                label6.Text = "Error: You have allocated the wrong amount of trades";
                label6.ForeColor = Color.Red;
                return;
            }

            if (DGVAllocator["tbcQty", DGVAllocator.Rows.Count - 1].Value == null)
            {
                DGVAllocator.Rows.RemoveAt(DGVAllocator.Rows.Count - 1);
            }


            string[] command = new string[DGVAllocator.Rows.Count - 1];


            List<int> Quantity =  new List<int>();
            List<string> Account =  new List<string>();
            decimal Commission = Convert.ToDecimal(DGVAllocator["tbcCommission",0].Value);


            foreach(DataGridViewRow row in DGVAllocator.Rows)
            {
                Quantity.Add(Convert.ToInt32(row.Cells["tbcQty"].Value));
                Account.Add(row.Cells["tbcClientAcct"].Value.ToString());
                

            }

            notifyDel?.Invoke(Quantity,Account,Commission);
            this.Close();
        }

        private void DGVAllocator_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            List<int> x = new List<int>();
            int total;
            if (e.ColumnIndex == 1)
            {
                foreach (DataGridViewRow row in DGVAllocator.Rows)
                {
                    x.Add(Convert.ToInt32(row.Cells[1].Value));
                }

                lblAllocated.Text = "Allocated: " + Math.Abs(x.Sum()).ToString();
                if (lblTotal.Text == "" || lblTotal.Text == "Total") { return; } else { total = Convert.ToInt32(lblTotal.Text); }


                lblRemaining.Text = "Remaining: " + (total - Math.Abs(x.Sum())).ToString();
                _alloc = total - Math.Abs(x.Sum());

                if (!label1.Text.Contains("BUY"))
                {
                    DGVAllocator[e.ColumnIndex, e.RowIndex].Value = Math.Abs(Convert.ToInt32(DGVAllocator[e.ColumnIndex, e.RowIndex].Value)) * -1;
                }
            }
            else { return; }
        }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace InstinetTicketer
{
    public delegate void NotifyDGVChange(List<int> Quantity, List<string> ClientAcct, decimal Commission);
    
    public partial class Instinet_Uploader : Form
    {
        public NotifyDGVChange notifyDelegate;
        int RowNum;
        public  Instinet_Uploader()
        {
            InitializeComponent();

            //Initialize security table
            AccountsTable.getTable();


            //initialize delegates;
            notifyDelegate += new NotifyDGVChange(DGVChanged);

            //initialize Futures table
            Futures.LoadFutures();

            //Disable Upload Button
            
            btnUpload.Enabled = false;
            btnUpload.ForeColor = Color.Gray;


            //Initialize printer event handlers

        }


        #region methods

        /// <summary>
        /// Add Columns
        /// </summary>
        private void addColumns()
        {
            try
            {
                InstinetParser parser = new InstinetParser(Clipboard.GetText());
                var table = parser.GetTable();

                foreach (DataColumn col in table.Columns)
                {
                    DGVUpload.Columns.Add(col.ColumnName, col.ColumnName);
                }
                foreach(DataRow row in table.Rows)
                {
                    DGVUpload.Rows.Add(row.ItemArray);
                }


                DataGridViewCheckBoxColumn chkBoxCol = new DataGridViewCheckBoxColumn();
                chkBoxCol.HeaderText = "";
                chkBoxCol.Name = "chkBoxCol";
                DGVUpload.Columns.Add(chkBoxCol);
                DGVUpload.Columns["chkBoxCol"].DisplayIndex = 0;
                DGVUpload.Columns.Add("Commission", "Commission");
                DGVUpload.Columns.Add("Status", "Status");

                //Resize
                DGVUpload.Columns["chkBoxCol"].Width = 20;
            }
            catch
            {
                return;
            }

        }

        private void findAccounts()
        {
            var _accountTable = AccountsTable.dT.AsEnumerable();
            foreach (DataGridViewColumn column in DGVUpload.Columns)
            {
                foreach (DataGridViewRow row in DGVUpload.Rows)
                {
                    if (column.Name == "ClientAcct" && (row.Index != DGVUpload.Rows.Count))
                    {
                        try
                        {
                            var accountNum = (from myrow in _accountTable
                                              where myrow.Field<string>("Account") == DGVUpload[column.Index, row.Index].Value.ToString()
                                              select myrow.Field<string>("Account")).First<string>();
                            var pwatchEQ = (from myrow in _accountTable
                                            where myrow.Field<string>("Account") == DGVUpload[column.Index, row.Index].Value.ToString()
                                            select myrow.Field<string>("Pwatch")).First<string>();
                            if (DGVUpload[column.Index, row.Index].Value.ToString() == accountNum)
                            {
                              
                                DGVUpload[column.Index, row.Index].Value = pwatchEQ;
                            }
                        }
                        catch
                        {
                            DGVUpload[column.Index, row.Index].Style.BackColor = Color.MistyRose;
                        }
                    }
                }
            }
        }

        public CheckBox chek;
        private void addCheckBox()
        {

            CheckBox chk = new CheckBox();
            chk.Padding = new Padding(0);
            chk.Margin = new Padding(0);
            chk.Text = "";
            chk.Name = "chk";
            chk.BackColor = Color.Transparent;
            chk.Size = new System.Drawing.Size(15, 15);
            DGVUpload.Controls.Add(chk);
            chek = chk;
            chk.Location = new Point(DGVUpload.Columns["chkBoxCol"].HeaderCell.ContentBounds.Left + 27 / 2 + 13, DGVUpload.Columns["chkBoxCol"].HeaderCell.ContentBounds.Top + 4);
            chk.CheckedChanged += Checkbox_CheckedChanged;
        }
        private void modifyColumns()
        {

            DGVUpload.Columns["Symbol"].Width = 55;
            DGVUpload.Columns["Side"].Width = 60;
            DGVUpload.Columns["Quantity"].Width = 80;
            DGVUpload.Columns["Avg_Price"].Width = 60;
            DGVUpload.Columns["Commission"].Width = 70;
            DGVUpload.Columns["Status"].Width = 120;
            DGVUpload.Columns["Symbol"].HeaderText = "Ticker";
            DGVUpload.Columns["Side"].HeaderText = "Action";
            DGVUpload.Columns["Avg_Price"].HeaderText = "Price";
            DGVUpload.Columns["ClientAcct"].HeaderText = "Portfolios";


            DGVUpload.RowHeadersWidth = 20;
            DGVUpload.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            DGVUpload.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            DGVUpload.BorderStyle = BorderStyle.None;
            DGVUpload.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //DGVUpload.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DGVUpload.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            DGVUpload.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            DGVUpload.BackgroundColor = Color.FromArgb(64, 64, 64);
            DGVUpload.EnableHeadersVisualStyles = false;
            DGVUpload.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DGVUpload.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            DGVUpload.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach(DataGridViewRow row in DGVUpload.Rows)
            {
                if (row.Cells["Side"].Value.ToString().Contains("BUY"))
                {
                    row.Cells["Side"].Style.BackColor = Color.FromArgb(153,255,237);

                }
                else
                {
                    row.Cells["Side"].Style.BackColor = Color.Pink;
                }
            }
        }

        #endregion


        #region Async methods
        #endregion


        #region Sub Event Handlers

        /// <summary>
        /// The event handler below will send all necessary data from the datagridview to the allocator form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGVUpload_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == DGVUpload["ClientAcct", 0].ColumnIndex)
            //{
            //    int rowIndex = e.RowIndex;
            //    RowNum = e.RowIndex;

            //    GridPositionHolder.columnIndex = e.ColumnIndex;
            //    GridPositionHolder.rowIndex = e.RowIndex;

            //    int columnIndex = e.ColumnIndex;
            //    string tester = DGVUpload["Symbol", rowIndex].Value.ToString();
            //    Form alloc = new Allocate(DGVUpload["Symbol", rowIndex].Value.ToString(), DGVUpload["Side", rowIndex].Value.ToString(), Convert.ToInt32(DGVUpload["Quantity", rowIndex].Value), Convert.ToDecimal(DGVUpload["Avg_Price", rowIndex].Value), notifyDelegate);
            //    //Form Allocator = new Allocator();
            //    alloc.ShowDialog();
            //}
            //else
            //{
            //    return;
            //}
            return;
        }

        private void DGVUpload_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGVUpload["ClientAcct", 0].ColumnIndex)
            {
                int rowIndex = e.RowIndex;
                RowNum = e.RowIndex;

                GridPositionHolder.columnIndex = e.ColumnIndex;
                GridPositionHolder.rowIndex = e.RowIndex;

                int columnIndex = e.ColumnIndex;
                string tester = DGVUpload["Symbol", rowIndex].Value.ToString();





                DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)DGVUpload["ChkBoxCol", e.RowIndex];
                

                if (chk1.Value == null)
                {
                    GridPositionHolder.isChecked = false;
                }
                else
                {
                    GridPositionHolder.isChecked = true;
                }


                Form alloc = new Allocate(DGVUpload["Symbol", rowIndex].Value.ToString(), DGVUpload["Side", rowIndex].Value.ToString(), Convert.ToInt32(DGVUpload["Quantity", rowIndex].Value), Convert.ToDecimal(DGVUpload["Avg_Price", rowIndex].Value), notifyDelegate);
                alloc.StartPosition = FormStartPosition.CenterParent;
                //Form Allocator = new Allocator();
                alloc.ShowDialog(this);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// CheckBox Header Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)DGVUpload["ChkBoxCol", 0];
            Boolean ischecked;

            if (chk1.Value == null)
            {
                ischecked = false;
            }
            else
            {
                ischecked = true;
            }


            foreach (DataGridViewRow row in DGVUpload.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["ChkBoxCol"];

                //chk.Value = !(chk.Value == null ? false : (bool)chk.Value);

                if (ischecked == false)
                {
                    chk.Value = true;
                }
                else
                {
                    chk.Value = null;
                }

            }
        }
        /// <summary>
        /// this will retrieve the data from the allocator
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="commission"></param>
        /// <param name="side"></param>
        /// <param name="account"></param>
        /// <param name="accumulator"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>

        public void DGVChanged(List<int> quantity, List<string> clientacct, decimal _commission)
        {


            var QuantityChange = quantity;
            var AccountChanged = clientacct;
            var Commission = _commission;

            for(int x=0; x < QuantityChange.Count(); x++)
            {
                DGVUpload.Rows.Add();
                DGVUpload["Symbol", DGVUpload.Rows.Count - 1].Value = DGVUpload["Symbol", GridPositionHolder.rowIndex].Value.ToString();
                DGVUpload["Side", DGVUpload.Rows.Count - 1].Value = DGVUpload["Side", GridPositionHolder.rowIndex].Value.ToString();
                DGVUpload["Quantity", DGVUpload.Rows.Count - 1].Value = QuantityChange[x];
                DGVUpload["Avg_Price", DGVUpload.Rows.Count - 1].Value = DGVUpload["Avg_Price", GridPositionHolder.rowIndex].Value;
                DGVUpload["ClientAcct", DGVUpload.Rows.Count - 1].Value = clientacct[x];
                DGVUpload["Commission", DGVUpload.Rows.Count - 1].Value = Commission;
                DGVUpload["chkBoxCol", DGVUpload.Rows.Count - 1].Value = GridPositionHolder.isChecked;
            }
            DGVUpload.Rows.RemoveAt(GridPositionHolder.rowIndex);
        }

        /// <summary>
        /// Delegator from Allocator, this method will create new rows from the retrieved data and implement in the datagrid
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="price"></param>
        /// <param name="quantity"></param>
        /// <param name="commission"></param>
        /// <param name="side"></param>
        /// 
        int Commission;
        public void Instinet(string symbol, decimal price, int quantity, int commission, string side)
        {
            DGVUpload.Rows.Add();
            DGVUpload["Symbol", DGVUpload.Rows.Count - 1].Value = DGVUpload["Symbol", GridPositionHolder.rowIndex].Value.ToString();
            DGVUpload["Side", DGVUpload.Rows.Count - 1].Value = DGVUpload["Side",GridPositionHolder.rowIndex].Value.ToString();
            DGVUpload["Quantity", DGVUpload.Rows.Count - 1].Value = DGVUpload["Quantity", GridPositionHolder.rowIndex].Value;
            DGVUpload["Avg_Price", DGVUpload.Rows.Count - 1].Value = DGVUpload["Avg_Price", GridPositionHolder.rowIndex].Value;
            DGVUpload["ckhBoxCol", DGVUpload.Rows.Count - 1].Value = DGVUpload["chkBoxCol", GridPositionHolder.rowIndex].Value;
            this.Commission = commission;
        }

        #endregion

        //List<int[]> results = new List<int[]>();

        //List<Task<int[]>> T = new List<Task<int[]>>();

        #region Main Events
        private async void btnPaste_Click(object sender, EventArgs e)
        {
            DGVUpload.Rows.Clear();
            DGVUpload.Refresh();
            DGVUpload.Columns.Clear();
            // initialize datagridview columns

            addColumns();

            findAccounts();

            // List<Task<FindExistingTrades>> findTradeTask = new List<Task<FindExistingTrades>>();

            // Check if any similar trades exist

            //List<FindExistingTrades> JobList = new List<FindExistingTrades>();
            //foreach(DataGridViewRow row in DGVUpload.Rows)
            //{
            //    JobList.Add(new FindExistingTrades(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToInt32(row.Cells["Quantity"].Value), Convert.ToDecimal(row.Cells["Avg_Price"].Value), row.Cells["ClientAcct"].Value.ToString(), row.Index));
            //}


            //foreach(FindExistingTrades jobs in JobList)
            //{
            //    T.Add(new Task<int[]>(jobs.Find));
            //}


            //foreach(Task<int[]> tasks in T)
            //{

            //    tasks.Start();
            //    results.Add(tasks.Result);
            //}

            progressBar1.Maximum = DGVUpload.Rows.Count;
            progressBar1.Value = 0;
            label2.Text = "Matching...";
            foreach (DataGridViewRow row in DGVUpload.Rows)
            {
                FindExistingTrades findExisting = new FindExistingTrades(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToInt32(row.Cells["Quantity"].Value), Convert.ToDecimal(row.Cells["Avg_Price"].Value), row.Cells["ClientAcct"].Value.ToString(), row.Index);
                progressBar1.Increment(1);  
                var searchVal = await findExisting.Find();


                if (searchVal == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.Cells["Status"].Value = "Found";
                }
                else if (searchVal == 3)
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    row.Cells["Status"].Value = "Potential trade matched";
                }
                else if (searchVal == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    row.Cells["Status"].Value = "Found similar trade, follow prompts - Price did not match";
                }
                else
                {
                    row.Cells["Status"].Value = "New trade details";
                }


                //await findExisting.Find().ContinueWith(r =>
                //{
                //    var searchVal = r.Result;
                //    if (searchVal == 2)
                //    {
                //        row.DefaultCellStyle.BackColor = Color.LightBlue;
                //        row.Cells["Status"].Value = "Found";
                //    }
                //    else if (searchVal == 3)
                //    {
                //        row.DefaultCellStyle.BackColor = Color.LightGreen;
                //        row.Cells["Status"].Value = "Found incomplete Trade";
                //    }
                //    else if (searchVal == 1)
                //    {
                //        row.DefaultCellStyle.BackColor = Color.Yellow;
                //        row.Cells["Status"].Value = "Found similar trade, follow prompts";
                //    }
                //    else
                //    {
                //        row.Cells["Status"].Value = "No Trades found";
                //    }

                //          

                //}, TaskScheduler.FromCurrentSynchronizationContext());


            }



            foreach (DataGridViewRow row in DGVUpload.Rows)
            {
                FindExistingTrades finder = new FindExistingTrades(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToInt32(row.Cells["Quantity"].Value), Convert.ToDecimal(row.Cells["Avg_Price"].Value), row.Cells["ClientAcct"].Value.ToString(), row.Index);
                var portfolios = finder.FindPortfolios();

                if(portfolios.Count > 1)
                {
                    row.Cells["ClientAcct"].Value = String.Join(", ", portfolios.ToArray());
                }
                else if (portfolios.Count == 1)
                {
                    if(portfolios[0] != "")
                    {
                        row.Cells["ClientAcct"].Value = portfolios[0];
                    }
                }

                row.Cells["Commission"].Value = finder.getCommission() * 100;

            }
            try
            {
                modifyColumns();
                if(DGVUpload.Controls.Find("chk", true).Count() == 0)
                {
                    addCheckBox();
                }

            }
            catch
            {
                return;
            }

            label2.Text = "Done";
            btnUpload.Enabled = true;
            btnUpload.ForeColor = Color.White;
            //DGVUpload.Columns["chkBoxCol"].Width = 20;
            foreach(DataGridViewRow row in DGVUpload.Rows)
            {
                if(row.DefaultCellStyle.BackColor == Color.LightBlue)
                {
                    btnPrint.Enabled = true;
                    return;
                }
            }


        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="Logger"> The Logger will take in data the create an error string and store in the G drive anytime an upload fails and then continue </param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {

            




            try
            {
                foreach(DataGridViewRow row in DGVUpload.Rows)
                {
                    // IF THE CHECK BOX IS CHECKED AND THE BACKCOLOR NOT GREEN WHICH USUALLY MEANS THE DATA WAS NOT FOUND INSIDE OF KINSMAN WE ARE GOING TO UPLOAD/INSERT DATA IN THE SERVER HERE
                    if((Convert.ToBoolean(row.Cells["chkBoxCol"].Value)) == true && !(row.DefaultCellStyle.BackColor == Color.LightGreen || row.DefaultCellStyle.BackColor == Color.LightBlue || row.DefaultCellStyle.BackColor == Color.Yellow))
                    {
                        try
                        {
                            Uploader _uploader = new Uploader(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToDecimal(row.Cells["Avg_Price"].Value), Convert.ToInt32(row.Cells["Quantity"].Value), row.Cells["ClientAcct"].Value.ToString(), Convert.ToDecimal(row.Cells["Commission"].Value));
                            _uploader.Upload();
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            row.Cells["Status"].Value = "Success";
                            row.Cells["Status"].Style.BackColor = Color.LightGreen;

                        }
                        catch(Exception ex)
                        {
                            if(ex.Message == "1")
                            {
                                row.Cells["Status"].Value = "Error";
                                row.Cells["Status"].Style.BackColor = Color.Orange;
                                continue;
                            }
                            List<string> errors = new List<string>();

                            foreach(DataGridViewColumn col in DGVUpload.Columns)
                            {
                                if(col.Index != 6)
                                {
                                    errors.Add(DGVUpload[col.Index, row.Index].Value.ToString());
                                }

                            }

                            Logger newLogger = new Logger(ex.Message, errors);
                            newLogger.Log();

                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                            DialogResult dialogResult = MessageBox.Show("An Error was caught while uploading to the server, press yes to continue, no to abort", "Error", MessageBoxButtons.YesNo);

                            if(dialogResult== DialogResult.Yes)
                            {
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }
                    } // ELSE IF THE DATA EXISTS START HERE NOTE WE ARE ONLY UPDATING THE COMMISSION FIELD NO OTHER VALS WILL BE TOUCHED
                    else if((Convert.ToBoolean(row.Cells["chkBoxCol"].Value)) == true && row.DefaultCellStyle.BackColor == Color.LightGreen)
                    {
                        try
                        {
                            Uploader _uploader = new Uploader(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToDecimal(row.Cells["Avg_Price"].Value), Convert.ToInt32(row.Cells["Quantity"].Value), row.Cells["ClientAcct"].Value.ToString(), Convert.ToDecimal(row.Cells["Commission"].Value));
                            _uploader.ForceUpdate();
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            row.Cells["Status"].Value = "Success";
                            row.Cells["Status"].Style.BackColor = Color.LightGreen;
                        }
                        catch(Exception ex)
                        {
                            if (ex.Message == "1")
                            {
                                row.Cells["Status"].Value = "Error";
                                row.Cells["Status"].Style.BackColor = Color.Orange;
                                continue;
                            }
                            List<string> errors = new List<string>();

                            foreach (DataGridViewColumn col in DGVUpload.Columns)
                            {
                                if (col.Index != 6)
                                {
                                    errors.Add(DGVUpload[col.Index, row.Index].Value.ToString());
                                }

                            }

                            Logger newLogger = new Logger(ex.Message, errors);
                            newLogger.Log();

                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                            DialogResult dialogResult = MessageBox.Show("An Error was caught while uploading to the server, press yes to continue, no to abort", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.Yes)
                            {
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }// IF THE ROW COLOR IS YELLOW THEN SEND THE USER A WARNING THAT A SIMILAR TRADE WAS FOUND, OVERWRITE, CREATE NEW OR IGNORE/SKIP
                    else if((Convert.ToBoolean(row.Cells["chkBoxCol"].Value)) == true && row.DefaultCellStyle.BackColor == Color.Yellow)
                    {
                        MessageBoxResult.dialogResult = -1;
                        CustomMsgBox cMsgBox = new CustomMsgBox();
                        cMsgBox.show("Overwrite", "New Trade", "Ignore & Skip", "A similar trade was found inside Kinsman at Row: "  + (row.Index +1) + System.Environment.NewLine + System.Environment.NewLine + "Would you like to overwrite or create a new trade?");
                        cMsgBox.ShowDialog();


                        if(MessageBoxResult.dialogResult == 0)
                        {
                            try
                            {
                                Uploader __uploader = new Uploader(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToDecimal(row.Cells["Avg_Price"].Value), Convert.ToInt32(row.Cells["Quantity"].Value), row.Cells["ClientAcct"].Value.ToString(), Convert.ToDecimal(row.Cells["Commission"].Value));
                                __uploader.ForceUpdate();
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                                row.Cells["Status"].Value = "Success";
                                row.Cells["Status"].Style.BackColor = Color.LightGreen;
                            }
                            catch(Exception ex)
                            {
                                if (ex.Message == "1")
                                {
                                    row.Cells["Status"].Value = "Error";
                                    row.Cells["Status"].Style.BackColor = Color.Orange;
                                    continue;
                                }
                                List<string> errors = new List<string>();

                                foreach (DataGridViewColumn col in DGVUpload.Columns)
                                {
                                    if (col.Index != 6)
                                    {
                                        errors.Add(DGVUpload[col.Index, row.Index].Value.ToString());
                                    }

                                }

                                Logger newLogger = new Logger(ex.Message, errors);
                                newLogger.Log();

                                row.DefaultCellStyle.BackColor = Color.OrangeRed;
                                DialogResult dialogResult = MessageBox.Show("An Error was caught while uploading to the server, press yes to continue, no to abort", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    continue;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else if(MessageBoxResult.dialogResult == 1)
                        {
                            if (row.Cells["ClientAcct"].Value.ToString().Contains(","))
                            {
                                MessageBox.Show("Please Allocate your trades with Quantities first");
                                continue;
                            }
                            try
                            {
                                Uploader newTrade = new Uploader(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToDecimal(row.Cells["Avg_Price"].Value), Convert.ToInt32(row.Cells["Quantity"].Value), row.Cells["ClientAcct"].Value.ToString(), Convert.ToDecimal(row.Cells["Commission"].Value));
                                newTrade.Upload();
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                                row.Cells["Status"].Value = "Success";
                                row.Cells["Status"].Style.BackColor = Color.LightGreen;
                            }
                            catch(Exception ex)
                            {
                                if (ex.Message == "1")
                                {
                                    row.Cells["Status"].Value = "Error";
                                    row.Cells["Status"].Style.BackColor = Color.Orange;
                                    continue;
                                }
                                List<string> errors = new List<string>();

                                foreach (DataGridViewColumn col in DGVUpload.Columns)
                                {
                                    if (col.Index != 6)
                                    {
                                        errors.Add(DGVUpload[col.Index, row.Index].Value.ToString());
                                    }

                                }

                                Logger newLogger = new Logger(ex.Message, errors);
                                newLogger.Log();

                                row.DefaultCellStyle.BackColor = Color.OrangeRed;
                                DialogResult dialogResult = MessageBox.Show("An Error was caught while uploading to the server, press yes to continue, no to abort", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    continue;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if((Convert.ToBoolean(row.Cells["chkBoxCol"].Value)) == true && row.DefaultCellStyle.BackColor == Color.LightBlue)
                    {
                        try
                        {
                            Uploader _uploader = new Uploader(row.Cells["Symbol"].Value.ToString(), row.Cells["Side"].Value.ToString(), Convert.ToDecimal(row.Cells["Avg_Price"].Value), Convert.ToInt32(row.Cells["Quantity"].Value), row.Cells["ClientAcct"].Value.ToString(), Convert.ToDecimal(row.Cells["Commission"].Value));
                            _uploader.Update();
                            row.Cells["Status"].Value = "Success";
                            row.Cells["Status"].Style.BackColor = Color.LightGreen;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "1")
                            {
                                row.Cells["Status"].Value = "Error";
                                row.Cells["Status"].Style.BackColor = Color.Orange;
                                continue;
                            }
                            List<string> errors = new List<string>();

                            foreach (DataGridViewColumn col in DGVUpload.Columns)
                            {
                                if (col.Index != 6)
                                {
                                    errors.Add(DGVUpload[col.Index, row.Index].Value.ToString());
                                }

                            }

                            Logger newLogger = new Logger(ex.Message, errors);
                            newLogger.Log();

                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                            DialogResult dialogResult = MessageBox.Show("An Error was caught while uploading to the server, press yes to continue, no to abort", 
                                "bruuuuh moment", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.Yes)
                            {
                                continue;
                            }
                            else
                            {
                                return;
                            }
                        }

                    }

                    btnPrint.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + " More information on this error was recorded on the logger.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageBox.Show("Complete", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }




        /// <summary>
        /// seriously, printing in c# is so annoying.
        /// </summary>
        /// <param name="sender"> objectname = RIP </param>
        /// <param name="e"> event = When attempting to write printing code </param>
        private DataTable printerTable;
        private int printCounter;
        ToolStripButton closeButton = new ToolStripButton();
        private void btnPrint_Click(object sender, EventArgs e)
        {

            int checker = 0;
            if(checker == 0)
            {
                foreach(DataGridViewRow row in DGVUpload.Rows)
                {
                    if(Convert.ToBoolean(row.Cells["chkBoxCol"].Value) == true)
                    {
                        checker += 1;
                    }
                }

                if (checker == 0)
                {
                    MessageBox.Show("Please select the trades you want to upload.");
                    return;
                }
            }


            
            var printTable = new DataTable();
            foreach(DataGridViewColumn col in DGVUpload.Columns)
            {
                if(col.Name != "chkBoxCol")
                {
                    printTable.Columns.Add(col.Name);
                }

            }

            foreach(DataGridViewRow row in DGVUpload.Rows)
            {
                if(row.DefaultCellStyle.BackColor == Color.LightBlue && Convert.ToBoolean(row.Cells[5].Value) == true)
                {
                    printTable.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value, row.Cells[6].Value);
                }
            }

            printerTable = printTable;


            PrintPreviewDialog printprevDialog = new PrintPreviewDialog();
            printprevDialog.Document = printDocument1;

            ToolStrip ts = new ToolStrip();
            ts.Name = "wrongToolStrip";
            foreach (Control ctl in printprevDialog.Controls)
            {
                if (ctl.Name.Equals("toolStrip1"))
                {
                    ts = ctl as ToolStrip;
                    break;
                }
            }

            ToolStripButton printButton = new ToolStripButton();

            foreach (ToolStripItem tsi in ts.Items)
            {
                if (tsi.Name.Equals("printToolStripButton"))
                {
                    printButton = tsi as ToolStripButton;
                }
                else if (tsi.Name.Equals("closeToolStripButton"))
                {
                    closeButton = tsi as ToolStripButton;
                }
            }
            ts.Items.Remove(printButton);
            ToolStripButton b = new ToolStripButton();
            b.ImageIndex = printButton.ImageIndex;
            b.Visible = true;
            ts.Items.Insert(0, b);
            b.Click += new EventHandler(this._init_Printer);

            printprevDialog.WindowState = FormWindowState.Maximized;
            printDocument1.Print();
            //printprevDialog.ShowDialog();

        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            var counter = printerTable.Rows[printCounter]["ClientAcct"].ToString().Split(',').ToList();
            var newCounter = new List<string>();
            foreach(string str in counter)
            {
                newCounter.Add(str.Trim());
            }



            FindExistingTrades finder = new FindExistingTrades(printerTable.Rows[printCounter]["Symbol"].ToString(), printerTable.Rows[printCounter]["Side"].ToString(), Convert.ToInt32(printerTable.Rows[printCounter]["Quantity"]), Convert.ToDecimal(printerTable.Rows[printCounter]["Avg_Price"]), "", 1);

            float point = 257;
            var brush = Brushes.Black;
            var background = Color.FromArgb(180, 150, 40, 27);
            var _regFont = new Font("Courier New", 10, FontStyle.Regular);
            var _boldFont = new Font("Courier New", 16, FontStyle.Bold);

            //TRADE DATA
            var issuer = finder.getIssuer();
            var settlement = finder.GetSettlementDate();
            var symbol = finder.GetPwatchTicker();
            var currency = finder.GetCurrency();
            var commission = Convert.ToDecimal(printerTable.Rows[printCounter]["Commission"]);
            var titlePrice = Convert.ToDecimal(printerTable.Rows[printCounter]["Avg_Price"]);
            var securityName = finder.GetSecurityName();



            var image = new Bitmap(600, 30);
            e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(60, 237, 650, 20));

            e.Graphics.DrawRectangle(Pens.Black, 60, 237, 150, 20);
            e.Graphics.DrawRectangle(Pens.Black, 210, 237, 220, 20);
            e.Graphics.DrawRectangle(Pens.Black, 430, 237, 160, 20);
            e.Graphics.DrawRectangle(Pens.Black, 590, 237, 120, 20);
            e.Graphics.DrawString("Portfolio Manager", _regFont, Brushes.Black, 60, 240);
            e.Graphics.DrawString("Portfolio", _regFont, Brushes.Black, 220, 240);
            e.Graphics.DrawString("Action", _regFont, Brushes.Black, 440, 240);
            e.Graphics.DrawString("Shares", _regFont, Brushes.Black, 600, 240);



            e.Graphics.DrawString("[Trade Ticket " + DateTime.Today.ToString("MMMM dd, yyyy") + "] - [" + printerTable.Rows[printCounter]["Side"].ToString() + "] Shares of " + symbol + " @ System Price Of " + titlePrice.ToString("C4") + "]" + Environment.NewLine + Environment.NewLine + "Printed On: " + DateTime.Now + "  |  By: " + Environment.UserName + "  |  Settlement Date: " + settlement + Environment.NewLine + Environment.NewLine + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" + Environment.NewLine + "Security: " + securityName + " [" + symbol + "] " + Environment.NewLine + "System Broker: Instinet |  System Commission: " + (commission/100).ToString("C4") + " " + currency + " Per Share" + Environment.NewLine + Environment.NewLine + Environment.NewLine + " Manual Price: ____________" + "     Manual Broker: _____________________" + "     Manual Commission: ________", new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Point), Brushes.Black, 20, 50);





            if (printerTable.Rows[printCounter]["Side"].ToString().Contains("BUY"))
            {
                brush = Brushes.Green;
                background = Color.FromArgb(80, 0, 102, 0);
            }
            else { brush = Brushes.Red; background = Color.FromArgb(80, 255, 40, 40); }



            foreach (string str in newCounter)
            {
                FindExistingTrades finder2 = new FindExistingTrades(printerTable.Rows[printCounter]["Symbol"].ToString(), printerTable.Rows[printCounter]["Side"].ToString(), Convert.ToInt32(printerTable.Rows[printCounter]["Quantity"]), Convert.ToDecimal(printerTable.Rows[printCounter]["Avg_Price"]), str,1);
                var _shares = finder2.GetShares();
                e.Graphics.DrawRectangle(Pens.Black, 60, point, 150, 20);
                e.Graphics.DrawRectangle(Pens.Black, 210, point, 220, 20);
                e.Graphics.DrawRectangle(Pens.Black, 430, point, 160, 20);
                e.Graphics.DrawRectangle(Pens.Black, 590, point, 120, 20);
                e.Graphics.DrawString(issuer, new Font("Courier New", 10), Brushes.Black, 60, point + 2);
                e.Graphics.DrawString(str, new Font("Courier New", 10), brush, 220, point + 2);
                e.Graphics.DrawString(printerTable.Rows[printCounter]["Side"].ToString(), new Font("Courier New", 10), brush, 440, point + 2);
                e.Graphics.DrawString((_shares.ToString("N0")), new Font("Courier New", 10), brush, 600, point + 2);
                point = Convert.ToInt32(point + 20);
            }


            if (printCounter < printerTable.Rows.Count - 1)
            {
                e.HasMorePages = true;
                printCounter++;

            }
            else
            {
                printCounter = 0;
                printerTable.Dispose();
                e.HasMorePages = false;

            }

        }

        public void _init_Printer(object sender, EventArgs e)
        {
            printDocument1.Print();
            closeButton.PerformClick();

        }
        /// <summary>
        /// Seriously its annoying
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #endregion

        private void DGVUpload_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var dgw = (DataGridView)sender;
            dgw.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }



        #region printBlotter


        public Bitmap memoryImage;

        private void printBlotterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureScreen();

            printDocument2.Print();

        }



        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(memoryImage, 0, 0);
        }


        private void CaptureScreen()
        {
            Graphics myGraphics = DGVUpload.CreateGraphics();
            Size s = DGVUpload.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(DGVUpload.Location.X, DGVUpload.Location.Y, 0, 0, s);
        }
        #endregion

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DGVUpload.Rows.Clear();
            DGVUpload.Refresh();
            DGVUpload.Columns.Clear();
            DGVUpload.Controls.Remove(chek);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Windows.Forms;

namespace InstinetTicketer
{
    public class InstinetParser
    {
        /// <summary>
        /// Public Fields
        /// </summary>
        public string Text { get; set; }


        /// <summary>
        /// Private Fields
        /// </summary>

        private string[] ColArray;
        private DataTable d = new DataTable();



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_text"></param>
        public InstinetParser(string _text)
        {
            Text = _text;
        }



       

        /// <summary>
        /// Return a dataTable method
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {

            try
            {

                string s = Text;

                s = Regex.Replace(s, "[A-Za-z]{5}[ ][A-Za-z]{4}[\\r][\\n][\\r][\\n][A-Za-z]{2}[0-9]{2}[ ][-][-][ ][A-z]{2}[0-9]{2}[\\r][\\n][\\r][\\n]", "");

                string columnarray = s.Substring(0, s.IndexOf("\t\t\r\n\r\n"));
                columnarray = columnarray.Replace(" ", "_");

                string dataArray = s.Substring(s.IndexOf("\t\t\r\n\r\n"), s.LastIndexOf("\t\t\r\n") - s.IndexOf("\t\t\r\n"));

                if(dataArray == "")
                {
                    dataArray = s.Substring(s.IndexOf("\t\t\r\n\r\n"), s.LastIndexOf("\r\n") - s.IndexOf("\r\n"));
                }

                dataArray = Regex.Replace(dataArray, "[\\t][\\t][\\r][\\n][\\r][\\n]", "");

                ColArray = Regex.Split(columnarray, "\t\t");
                string[] DatArray = Regex.Split(dataArray, "\t\t");
                DatArray = DatArray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                string datstring = string.Join("+", DatArray);
                datstring = Regex.Replace(datstring, @"[+\\r\\]\B", "");
                datstring = datstring.Replace(",", "");
                ColArray[0] = "Symbol";

                foreach (string b in ColArray)
                {
                    // if (b == "Symbol"  || b == "Side" || b == "Quantity" || b == "Shares" || b == "Price")
                    d.Columns.Add(new DataColumn(b));
                }
                int j = ColArray.Count();
                int k = DatArray.Count();
                int i = k / j;

                using (var reader = new StringReader(datstring))
                {
                    TextFieldParser parser = new TextFieldParser(reader)
                    { HasFieldsEnclosedInQuotes = false, Delimiters = new string[] { "+" } };
                    while (!parser.EndOfData)
                    {
                        var drow = d.NewRow();
                        drow.ItemArray = parser.ReadFields();
                        d.Rows.Add(drow);
                    }
                }

                try
                {
                    d.Columns.Remove("Last");
                }
                catch
                {

                }



                try
                {
                    if(d.Columns.IndexOf("Underlyer") > -1)
                    {
                        d.Columns.Remove("ClientAcct");

                        d.Columns["Underlyer"].ColumnName = "ClientAcct";
                    }
                }
                catch
                {

                }



                return d;
            }
            catch
            {
                MessageBox.Show("Please make sure you copied from Instinet");
                return null;
            }


        }


    }
}

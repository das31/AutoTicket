using System;
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
    public partial class CustomMsgBox : Form
    {
        public CustomMsgBox()
        {
            InitializeComponent();
        }


        public void show(string pos0, string pos1, string pos2, string message)
        {
            button1.Text = pos0;
            button2.Text = pos1;
            button3.Text = pos2;
            label1.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxResult.dialogResult = 0;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxResult.dialogResult = 1;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBoxResult.dialogResult = 2;
            this.Close();
        }
    }
}

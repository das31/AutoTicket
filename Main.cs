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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnInstinet_Uploader_Click(object sender, EventArgs e)
        {
            Instinet_Uploader form = new Instinet_Uploader();
            form.Show();
        }
    }
}

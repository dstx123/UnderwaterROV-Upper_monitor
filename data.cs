using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnderwaterROV
{
    public partial class data : Form
    {
        public data()
        {
            InitializeComponent();
        }
        public fmMain form = null;
        public void GetForm(fmMain theform)
        {
            form = theform;
        }
        private void data_Load(object sender, EventArgs e)
        {
          
        }

        private void TmFresh_Tick(object sender, EventArgs e)
        {
            tbSendData.Text = fmMain.senddata + "\r\n";
            tbReceivedata.Text = fmMain.receivedata + "\r\n";

        }

       
    }
}

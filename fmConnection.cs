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
    public partial class fmConnection : Form
    {
        public fmConnection()
        {
            InitializeComponent();
        }
        public fmMain form = null;
        public void GetForm(fmMain theform)
        {
            form = theform;
        }
        NetPort netport1;
        NetPort netport2;
        public static string patht1 = System.Windows.Forms.Application.StartupPath;//定义存储目录
        public string txt = System.IO.Path.Combine(patht1, "");
        private void btnPortCheck2_Click(object sender, EventArgs e)
        {
            form.SerConnectPort1 = tbPort1 .Text;
            form.SerConnectIP1 = tbIPadress1 .Text;
            //form.SerConnectPort2 = tbPort2 .Text;
            //form.SerConnectIP2 = tbIPadress2 .Text;
            String[] allLine = new String[2];

            allLine[0] = tbIPadress1.Text;
            allLine[1] = tbPort1.Text;

            //allLine[2] = tbIPadress2.Text;
            //allLine[3] = tbPort2.Text;


            System.IO.File.WriteAllLines(txt + @"\Config.txt", allLine);
            MessageBox.Show("设置成功！", "提示框");
            this.Close();
        }

        private void fmConnection_Load(object sender, EventArgs e)
        {
            netport1 = NetPort.getNetPort();
            //netport2 = NetPort.getNetPort();

            String[] allLine = System.IO.File.ReadAllLines(txt + @"\Config.txt");

            tbIPadress1.Text = allLine[0];
            tbPort1.Text = allLine[1];

           // tbIPadress2.Text = allLine[2];
            //tbPort2.Text = allLine[3];
        }
    }
}

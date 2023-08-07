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
    public partial class SystemFrom : Form
    {
        private static SystemFrom systemForm;
        private static readonly object _Lock = new object();
        private static DataToROV myDataToRov = DataToROV.GetDataToROV();
        private FromRovData myRovData = FromRovData.GetFromRovData();
        /// <summary>
        /// 窗口单例设计模式 if双重锁方法，返回窗口对象，其他名字为对象的引用，只有一个实现
        /// </summary>
        /// <returns></returns>
        public static SystemFrom GetForm()
        {
            if (systemForm == null)
            {
                lock (_Lock)
                {
                    if (systemForm == null)
                    {
                        systemForm = new SystemFrom();
                    }
                }
            }

            return systemForm;
        }
        public SystemFrom()
        {
            InitializeComponent();

        }
        static string patht1 = System.Windows.Forms.Application.StartupPath;
        //窗体加载
        private void SystemFrom_Load(object sender, EventArgs e)
        {
            //读取上次调试好的PID参数
            if (System.IO.File.Exists((patht1 + @"\系统辨识参数.txt")))
            {
                String[] allLine = System.IO.File.ReadAllLines((patht1 + @"\系统辨识参数.txt"));
                depthPHtxt.Text = allLine[0];

                depthIHtxt.Text = allLine[1];

                tbDirectFrequent.Text = allLine[2];


                // direct
                tbDirectAmplitude.Text = allLine[3];

                tbPitchFrequent.Text = allLine[4];

                tbPitchAmplitude.Text = allLine[5];



                tbRollFrequent.Text = allLine[6];

                tbRollAmplitude.Text = allLine[7];


            }
            else
            {
                // MessageBox.Show("请在：" + patht1 + "目录下新建‘pid参数.txt’文件");
                System.IO.FileStream myFlie = new System.IO.FileStream((patht1 + @"\系统辨识参数.txt"), System.IO.FileMode.Create);
                myFlie.Close();
            }
            timer1.Enabled = true;
        }
        //定深按钮
        private void depthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (depthCheckBox.Checked)
            {
                DataToROV.SystemControlCmd |= 0x01;
               
            }
            else
            {
                DataToROV.SystemControlCmd &= (byte)(0b011111110);
            }
        }
        //定向按钮
        private void directCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (directCheckBox .Checked)
            {
                DataToROV.SystemControlCmd |= 0x02;

            }
            else
            {
                DataToROV.SystemControlCmd &= (byte)(0b011111110);
            }
        }
        //定俯仰按钮
        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pitchCheckBox .Checked)
            {
                DataToROV.SystemControlCmd |= 0x04;

            }
            else
            {
                DataToROV.SystemControlCmd &= (byte)(0b011111110);
            }
        }
        //定横滚按钮
        private void rollCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rollCheckBox .Checked)
            {
                DataToROV.SystemControlCmd |= 0x08;

            }
            else
            {
                DataToROV.SystemControlCmd &= (byte)(0b011111110);
            }
        }
        //定深参数设置按钮
        private void depthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (depthRadioButton.Checked)
            {
                DataToROV.SystemChangeCmd = 1;
                DataToROV.Frequent  = Convert.ToByte(depthPHtxt.Text);
              
                DataToROV.Amplitude  = Convert.ToByte(depthIHtxt.Text);
              
            }
        }
        //定向参数设置按钮
        private void directRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (directRadioButton.Checked)
            {
                DataToROV.SystemChangeCmd = 2;
                DataToROV.Frequent = Convert.ToByte(tbDirectFrequent .Text);

                DataToROV.Amplitude = Convert.ToByte(tbDirectAmplitude .Text);

            }
        }
        //定俯仰参数设置按钮
        private void pitchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (pitchRadioButton.Checked)
            {
                DataToROV.SystemChangeCmd = 4;
                DataToROV.Frequent = Convert.ToByte(tbPitchFrequent .Text);

                DataToROV.Amplitude = Convert.ToByte(tbPitchAmplitude .Text);

            }
        }
        //定横滚参数设置按钮
        private void rollRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rollRadioButton.Checked)
            {
                DataToROV.SystemChangeCmd = 8;
                DataToROV.Frequent = Convert.ToByte(tbRollFrequent .Text);

                DataToROV.Amplitude = Convert.ToByte(tbRollAmplitude .Text);

            }
        }
        //点击不设置按钮
        private void noSetPidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (noSetPidRadioButton.Checked)
            {
                DataToROV.SystemChangeCmd = 0;
            }
        }
        //不查询参数
        private void noReadPidRadioButto_CheckedChanged(object sender, EventArgs e)
        {
            if (noReadPidRadioButto.Checked)
            {
                DataToROV.SystemInquireCmd = 0;
            }
        }
        //查询定深参数
        private void readDepthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readDepthRadioButton.Checked)
            {
                DataToROV.SystemInquireCmd = 0x01;
            }
        }
        //查询定向参数
        private void readDirectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readDirectRadioButton.Checked)
            {
                DataToROV.SystemInquireCmd = 0x02;
            }
        }
        //查询定俯仰参数
        private void readPitchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readPitchRadioButton.Checked)
            {
                DataToROV.SystemInquireCmd = 0x04;
            }
        }
        //查询定横滚参数
        private void readRollRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readRollRadioButton.Checked)
            {
                DataToROV.SystemInquireCmd = 0x08;
            }
        }
        //定时器刷新界面
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((DataToROV.SystemInquireCmd == 0) && (myRovData.SystemFeedback == 0))
            {
                return;
            }
            else if ((DataToROV.SystemInquireCmd == 1) && (myRovData.SystemFeedback == 1))
            {
                tbCDepthFrequent .Text = myRovData.Rovfrequent .ToString() ;

                tbCDepthAmplitude .Text = myRovData.Rovamplitude .ToString();

               

            }
            else if ((DataToROV.SystemInquireCmd == 2) && (myRovData.SystemFeedback == 2))
            {
                tbCDirectFrequent .Text = myRovData.Rovfrequent .ToString() ;

                tbCDirectAmplitude .Text = myRovData.Rovamplitude .ToString() ;

               

            }
            else if ((DataToROV.SystemInquireCmd == 4) && (myRovData.SystemFeedback == 4))
            {
                tbCPitchFrequent .Text = myRovData.Rovfrequent .ToString() ;

               tbCPtichAmplitude .Text = myRovData.Rovamplitude .ToString() ;


             

            }
            else if ((DataToROV.SystemInquireCmd == 8) && (myRovData.SystemFeedback == 8))
            {
                tbCRollFrequent .Text = myRovData.Rovfrequent .ToString() ;
               tbCRollAmplitude .Text = myRovData.Rovamplitude .ToString();
             

            }
        }
        //窗口开启时启动定时器刷新界面
        private void SystemFrom_Activated(object sender, EventArgs e)
        {
            timer1.Start();
        }
        //窗体关闭
        private void SystemFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            String[] allLine = new String[8];
            // depth
            allLine[0] = depthPHtxt.Text.Trim();

            allLine[1] = depthIHtxt.Text.Trim();

            allLine[2] = tbDirectFrequent .Text.Trim();

            // direct
            allLine[3] = tbDirectAmplitude .Text.Trim();

            allLine[4] = tbPitchFrequent .Text.Trim();

            allLine[5] = tbPitchAmplitude .Text.Trim();

            // pitch
            allLine[6] = tbRollFrequent .Text.Trim();

            allLine[7] = tbRollAmplitude .Text.Trim();

           

            if (System.IO.File.Exists((patht1 + @"\系统辨识参数.txt")))
            {
                System.IO.File.WriteAllLines(patht1 + @"\系统辨识参数.txt", allLine);


            }
            else
            {
                MessageBox.Show("请在：" + patht1 + "目录下新建‘系统辨识参数.txt’文件");
            }
            timer1.Stop();

            e.Cancel = true;
            Hide();
        }
    }
}

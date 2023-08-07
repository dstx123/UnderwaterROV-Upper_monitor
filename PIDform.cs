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
    public partial class PIDform : Form
    {
        private static PIDform  pidForm;
        private static readonly object _Lock = new object();
        private static DataToROV myDataToRov = DataToROV.GetDataToROV();
        private FromRovData myRovData = FromRovData.GetFromRovData();
        /// <summary>
        /// 窗口单例设计模式 if双重锁方法，返回窗口对象，其他名字为对象的引用，只有一个实现
        /// </summary>
        /// <returns></returns>
        public static PIDform GetForm()
        {
            if (pidForm == null)
            {
                lock (_Lock)
                {
                    if (pidForm == null)
                    {
                        pidForm = new PIDform();
                    }
                }
            }

            return pidForm;
        }
        public PIDform()
        {
            InitializeComponent();
        }
        static string patht1 = System.Windows.Forms.Application.StartupPath;//文件存储读取路径

        //窗体加载
        private void PIDform_Load(object sender, EventArgs e)
        {
            //读取上次调试好的PID参数
            if (System.IO.File.Exists((patht1 + @"\pid参数.txt")))
            {
                String[] allLine = System.IO.File.ReadAllLines((patht1 + @"\pid参数.txt"));
                depthPHtxt.Text = allLine[0];
             
                depthIHtxt.Text = allLine[1];
               
                depthDHtxt.Text = allLine[2];
              

                // direct
                directPHtxt.Text = allLine[3];
               
                directIHtxt.Text = allLine[4];
              
                directDHtxt.Text = allLine[5];
            


                pitchPHtxt.Text = allLine[6];
              
                pitchIHtxt.Text = allLine[7];
              
                pitchDHtxt.Text = allLine[8];
                tbSetRollP .Text = allLine[9];

                tbSetRollI .Text = allLine[10];

                tbSetRollD .Text = allLine[11];



                setDepthTextBox.Text = allLine[12];
                setDirectTextBox.Text = allLine[13];
                setPitchTextBox.Text = allLine[14];
                setRollTextBox.Text = allLine[15];
            }
            else
            {
                // MessageBox.Show("请在：" + patht1 + "目录下新建‘pid参数.txt’文件");
                System.IO.FileStream myFlie = new System.IO.FileStream((patht1 + @"\pid参数.txt"), System.IO.FileMode.Create);
                myFlie.Close();
            }
            timer1.Enabled = true;

        }
        //窗体关闭
        private void PIDform_FormClosing(object sender, FormClosingEventArgs e)
        {
            //存储参数
            String[] allLine = new String[16];
            // depth
            allLine[0] = depthPHtxt.Text.Trim();
       
            allLine[1] = depthIHtxt.Text.Trim();
        
            allLine[2] = depthDHtxt.Text.Trim();
         
            // direct
            allLine[3] = directPHtxt.Text.Trim();
         
            allLine[4] = directIHtxt.Text.Trim();
     
            allLine[5] = directDHtxt.Text.Trim();
 
            // pitch
            allLine[6] = pitchPHtxt.Text.Trim();
         
            allLine[7] = pitchIHtxt.Text.Trim();
        
            allLine[8] = pitchDHtxt.Text.Trim();
            allLine[9] = tbSetRollP .Text.Trim();

            allLine[10] = tbSetRollI .Text.Trim();

            allLine[11] = tbSetRollD .Text.Trim();



            allLine[12] = setDepthTextBox.Text.Trim();
            allLine[13] = setDirectTextBox.Text.Trim();
            allLine[14] = setPitchTextBox.Text.Trim();
            allLine[15] = setRollTextBox.Text.Trim();

            if (System.IO.File.Exists((patht1 + @"\pid参数.txt")))
            {
                System.IO.File.WriteAllLines(patht1 + @"\pid参数.txt", allLine);


            }
            else
            {
                MessageBox.Show("请在：" + patht1 + "目录下新建‘pid参数.txt’文件");
            }
            timer1.Stop();

            e.Cancel = true;
            Hide();
        }
        //定深PID参数设置按钮
        private void depthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (depthRadioButton.Checked)
            {
                DataToROV.PidChangeCmd = 1;
               
                DataToROV.PIntNum = Convert.ToByte(float.Parse(depthPHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.PDecimalNum = (byte)((double.Parse(depthPHtxt.Text) - Convert.ToInt32(float.Parse(depthPHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.IIntNum = Convert.ToByte(float.Parse(depthIHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.IDecimalNum = (byte)((double.Parse(depthIHtxt.Text) - Convert.ToInt32(float.Parse(depthIHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.DIntNum = Convert.ToByte(float.Parse(depthDHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.DDecimalNum = (byte)((double.Parse(depthDHtxt.Text) - Convert.ToInt32(float.Parse(depthDHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
            }
        }
        //定向PID参数设置按钮
        private void directRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (directRadioButton.Checked)
            {
                DataToROV.PidChangeCmd = 2;
              
                DataToROV.PIntNum = Convert.ToByte(float.Parse(directPHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.PDecimalNum = (byte)((double.Parse(directPHtxt.Text) - Convert.ToInt32(float.Parse(directPHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.IIntNum = Convert.ToByte(float.Parse(directIHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.IDecimalNum = (byte)((double.Parse(directIHtxt.Text) - Convert.ToInt32(float.Parse(directIHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.DIntNum = Convert.ToByte(float.Parse(directDHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.DDecimalNum = (byte)((double.Parse(directDHtxt.Text) - Convert.ToInt32(float.Parse(directDHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
            }
        }
        //定俯仰PID参数设置按钮
        private void pitchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (pitchRadioButton.Checked)
            {
                DataToROV.PidChangeCmd = 4;

                DataToROV.PIntNum = Convert.ToByte(float.Parse(pitchPHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.PDecimalNum = (byte)((double.Parse(pitchPHtxt.Text) - Convert.ToInt32(float.Parse(pitchPHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.IIntNum = Convert.ToByte(float.Parse(pitchIHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.IDecimalNum = (byte)((double.Parse(pitchIHtxt.Text) - Convert.ToInt32(float.Parse(pitchIHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.DIntNum = Convert.ToByte(float.Parse(pitchDHtxt.Text, System.Globalization.NumberStyles.Float));
                DataToROV.DDecimalNum = (byte)((double.Parse(pitchDHtxt.Text) - Convert.ToInt32(float.Parse(pitchDHtxt.Text, System.Globalization.NumberStyles.Float))) * 100);
            }
        }
        //定横滚PID参数设置按钮
        private void rollRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rollRadioButton.Checked)
            {
                DataToROV.PidChangeCmd = 8;

                DataToROV.PIntNum = Convert.ToByte(float.Parse(tbSetRollP.Text, System.Globalization.NumberStyles.Float));
                DataToROV.PDecimalNum = (byte)((double.Parse(tbSetRollP .Text) - Convert.ToInt32(float.Parse(tbSetRollP .Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.IIntNum = Convert.ToByte(float.Parse(tbSetRollI.Text, System.Globalization.NumberStyles.Float));
                DataToROV.IDecimalNum = (byte)((double.Parse(tbSetRollI.Text) - Convert.ToInt32(float.Parse(tbSetRollI.Text, System.Globalization.NumberStyles.Float))) * 100);
                DataToROV.DIntNum = Convert.ToByte(float.Parse(tbSetRollD.Text, System.Globalization.NumberStyles.Float));
                DataToROV.DDecimalNum = (byte)((double.Parse(tbSetRollD.Text) - Convert.ToInt32(float.Parse(tbSetRollD.Text, System.Globalization.NumberStyles.Float))) * 100);
            }
        }
        //点击不设置按钮
        private void noSetPidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (noSetPidRadioButton.Checked)
            {
                DataToROV.PidChangeCmd = 0;
            }
        }
      
        //定深按钮
        private void depthCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (depthCheckBox.Checked)
            {
                DataToROV.AutoControlCmd |= 0x01;
                DataToROV.DepthSetingNum = Convert.ToInt16(setDepthTextBox.Text.Trim('.'));
            }
            else
            {
                DataToROV.AutoControlCmd &= (byte)(0b011111110);
            }
        }
        //定向按钮
        private void directCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (directCheckBox.Checked)
            {
                DataToROV.AutoControlCmd |= 0x02;
                DataToROV.DirectSetingNum = Convert.ToInt16(setDirectTextBox.Text.Trim('.'));
            }
            else
            {

                DataToROV.AutoControlCmd &= (byte)(0b011111101);

            }
        }
        //定俯仰
        private void pitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pitchCheckBox.Checked)
            {
                DataToROV.AutoControlCmd |= 0x04;
                DataToROV.PitchSetingNum = Convert.ToInt16(setPitchTextBox.Text.Trim('.'));
            }
            else
            {
                DataToROV.AutoControlCmd &= (byte)(0b011111011);

            }
        }
        //定横滚
        private void rollCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            if (rollCheckBox.Checked)
            {
                DataToROV.AutoControlCmd |= 0x08;
                DataToROV.RollSetingNum = Convert.ToInt16(setRollTextBox.Text.Trim('.'));
            }
            else
            {
                DataToROV.AutoControlCmd &= (byte)(0b011111011);

            }
        }
        //查询定深PID参数
        private void readDepthRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readDepthRadioButton.Checked)
            {
                DataToROV.PidInquireCmd = 0x01;
            }
        }
        //查询定向PID参数
        private void readDirectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readDirectRadioButton.Checked)
            {
                DataToROV.PidInquireCmd = 0x02;
            }
        }
        //查询定俯仰PID参数
        private void readPitchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readPitchRadioButton.Checked)
            {
                DataToROV.PidInquireCmd = 0x04;
            }
        }
        //不查询PID参数
        private void noReadPidRadioButto_CheckedChanged(object sender, EventArgs e)
        {

            if (noReadPidRadioButto.Checked)
            {
                DataToROV.PidInquireCmd = 0;
            }
        }
        //查询定横滚PID参数
        private void readRollRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (readRollRadioButton.Checked)
            {
                DataToROV.PidInquireCmd = 0x08;
            }
        }
        //界面刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((DataToROV.PidInquireCmd == 0) && (myRovData.PidFeedback == 0))
            {
                return;
            }
            else if ((DataToROV.PidInquireCmd == 1) && (myRovData.PidFeedback == 1))
            {
                fRovdepthPHtxt.Text = myRovData.PnumH.ToString()+"."+ myRovData.PnumL.ToString();

                fRovdepthIHtxt.Text = myRovData.InumH.ToString()+"."+ myRovData.InumL.ToString();

                fRovdepthDHtxt.Text = myRovData.DnumH.ToString()+"." + myRovData.DnumL.ToString();

            }
            else if ((DataToROV.PidInquireCmd == 2) && (myRovData.PidFeedback == 2))
            {
                fRovdirectPHtxt.Text = myRovData.PnumH.ToString()+"."+ myRovData.PnumL.ToString();
                
                fRovdirectIHtxt.Text = myRovData.InumH.ToString()+"."+ myRovData.InumL.ToString();
                
                fRovdirectDHtxt.Text = myRovData.DnumH.ToString()+"."+ myRovData.DnumL.ToString();
               
            }
            else if ((DataToROV.PidInquireCmd == 4) && (myRovData.PidFeedback == 4))
            {
                fRovpitchPHtxt.Text = myRovData.PnumH.ToString()+"."+ myRovData.PnumL.ToString();
               
                fRovpitchIHtxt.Text = myRovData.InumH.ToString()+"."+ myRovData.InumL.ToString();


                fRovpitchDHtxt.Text = myRovData.DnumH.ToString()+"."+ myRovData.DnumL.ToString();

            }
            else if ((DataToROV.PidInquireCmd == 8) && (myRovData.PidFeedback == 8))
            {
                fRovrollPtxt.Text = myRovData.PnumH.ToString()+"."+ myRovData.PnumL.ToString();
                fRovrollItxt .Text = myRovData.InumH.ToString() + "." + myRovData.InumL.ToString();
                fRovrollDtxt .Text = myRovData.DnumH.ToString() + "." + myRovData.DnumL.ToString();
                
            }
        }
        //窗口开启时启动定时器刷新界面
        private void PIDform_Activated(object sender, EventArgs e)
        {
            timer1.Start();
        }

      
    }
}

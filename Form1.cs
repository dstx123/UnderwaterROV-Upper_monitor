using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JoyKeys.DirectInputJoy;
using PreviewDemo;
using UnderIceRov.FunctionFolder;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace UnderwaterROV
{
    [Serializable]
    public partial class fmMain : System.Windows.Forms.Form
    {

        #region 全局变量

        //摄像头1
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private Int32 m_lRealHandle = -1;
        private string str;

        CHCNetSDK.REALDATACALLBACK RealData = null;
        CHCNetSDK.LOGINRESULTCALLBACK LoginCallBack = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg;
        public CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLogInfo;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo;

        //摄像头2
        private uint iLastErr1 = 0;
        private Int32 m_lUserID1 = -1;
        private bool m_bInitSDK1 = false;
        private bool m_bRecord1 = false;
        private Int32 m_lRealHandle1 = -1;
        private string str1;

        CHCNetSDK.REALDATACALLBACK RealData1 = null;
        CHCNetSDK.LOGINRESULTCALLBACK LoginCallBack1 = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg1;
        public CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLogInfo1;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo1;

        //摄像头3
        private uint iLastErr2 = 0;
        private Int32 m_lUserID2 = -1;
        private bool m_bInitSDK2 = false;
        private bool m_bRecord2 = false;
        private Int32 m_lRealHandle2 = -1;
        private string str2;

        CHCNetSDK.REALDATACALLBACK RealData2 = null;
        CHCNetSDK.LOGINRESULTCALLBACK LoginCallBack2 = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg2;
        public CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLogInfo2;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo2;

        //摄像头4
        private uint iLastErr3 = 0;
        private Int32 m_lUserID3 = -1;
        private bool m_bInitSDK3 = false;
        private bool m_bRecord3 = false;
        private Int32 m_lRealHandle3 = -1;
        private string str3;

        CHCNetSDK.REALDATACALLBACK RealData3 = null;
        CHCNetSDK.LOGINRESULTCALLBACK LoginCallBack3 = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg3;
        public CHCNetSDK.NET_DVR_USER_LOGIN_INFO struLogInfo3;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo3;

        private NetPort np;//网口声明
        public delegate void UpdateTextStatusCallback(string strLogStatus, IntPtr lpDeviceInfo);





        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        //rivate System.ComponentModel.Container components = null;
        //private Label labelLogin;

        //Sunisoft.IrisSkin.SkinEngine SkinEngine = new Sunisoft.IrisSkin.SkinEngine();
        // List<string> Skins;
        Joystick_V _joystick_V = null;//手柄

        private DataCheck check = new DataCheck();

        NetPort netport1;
        NetPort netport2;
        public static string patht1 = System.Windows.Forms.Application.StartupPath;//定义存储目录
        public string txt = System.IO.Path.Combine(patht1, "");

        public string SerConnectPort1 = ""; // 网口端口
        public string SerConnectIP1 = ""; // 网口地址

        #endregion

        #region 加载释放相关函数
        /// <summary>
        /// 窗体初始化函数
        /// </summary>
        public fmMain()
        {
            //获取网口
            netport1 = NetPort.getNetPort();
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized; //窗体最大化
            main_Load(null, null); //控件比例放大

            //摄像头相关代码
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
            }

        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
            }
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
            }
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
            if (m_lRealHandle1 >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle1);
            }
            if (m_lUserID1 >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID1);
            }
            if (m_bInitSDK1 == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);

        }
        ////窗体加载
        //private void FmMain_Load(object sender, EventArgs e)
        //{
        //    CreateFiled();//创建文件
        //    Formsize();//改变窗体大小
        //    this.WindowState = FormWindowState.Maximized;//点击最大化按钮
        //    String[] allLine = System.IO.File.ReadAllLines(txt + @"\Config.txt");//读取config文件
        //    //读取上次网口地址和网口号
        //    SerConnectIP1 = allLine[0];
        //    SerConnectPort1 = allLine[1];


        //}

        /// <summary>
        /// C盘创建文件夹保存数据
        /// </summary>
        private void CreateFiled()
        {
            if (!Directory.Exists(@"D:\ROV data"))
            {
                string activeDir = @"D:\ROV data"; //在C盘下创建文件夹，名称可以自己改
                string newPath = System.IO.Path.Combine(activeDir);
                System.IO.Directory.CreateDirectory(newPath);
            }
            if (!Directory.Exists(@"D:\DCIM\vedio"))
            {
                string activeDir = @"D:\DCIM\vedio"; //在C盘下创建文件夹，名称可以自己改
                string newPath = System.IO.Path.Combine(activeDir);
                System.IO.Directory.CreateDirectory(newPath);
            }
            if (!Directory.Exists(@"D:\DCIM\photo"))
            {
                string activeDir = @"D:\DCIM\photo"; //在C盘下创建文件夹，名称可以自己改
                string newPath = System.IO.Path.Combine(activeDir);
                System.IO.Directory.CreateDirectory(newPath);
            }
        }

        #endregion


        #region 手柄相关函数

        /// <summary>
        /// 手柄初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            JoystickInit();
            np = NetPort.getNetPort();
            if (_joystick_V.IsConnected && _joystick_V.IsCapture)
            {
                //crystalButton2.BackColor = Color.SpringGreen;
                lbshoubing.ForeColor = Color.Chartreuse;
            }
            
        }
        /// <summary>
        /// 手柄断开
        /// </summary>
        /// <param name="e"></param>

        protected override void OnClosed(EventArgs e)
        {
            JoystickDispose();
            base.OnClosed(e);
        }
        /// <summary>
        /// 释放手柄资源
        /// </summary>
        private void JoystickDispose()
        {
            _joystick_V?.ReleaseCapture();
            _joystick_V?.Dispose();
        }
        /// <summary>
        /// 手柄初始化
        /// </summary>
        private void JoystickInit()
        {
            _joystick_V = Joystick_V.ReturnJoystick(JoystickAPI.JOYSTICKID1);
            _joystick_V.Capture();
        }

        public static byte[] bufButton = new byte[10]; //按键键值
        public static int[] bufRocker = { 0x80 * 256, 0x80 * 256, 0x80 * 256, 0x80 * 256, 0x80 * 256 }; //两个摇杆+一个数字摇杆键值
        public static byte[] bufdirecet = new byte[4];
        /// <summary>
        /// 手柄数据解析
        /// </summary>
        private void JoystickButtinValueRefresh()
        {

            bufRocker[0] = 0x80 * 256;
            if (_joystick_V.X != 65535)//防止分母为0
            {
                bufRocker[0] = _joystick_V.X + 1;
            }
            else
            {

                bufRocker[0] = _joystick_V.X;


            }

            if (_joystick_V.Y != 65535)
            {
                bufRocker[1] = _joystick_V.Y + 1;
            }
            else
            {

                bufRocker[1] = _joystick_V.Y;

            }

            if (_joystick_V.Z != 65535)
            {
                bufRocker[2] = _joystick_V.Z + 1;
            }
            else
            {

                bufRocker[2] = _joystick_V.Z;

            }

            if (_joystick_V.R != 65535)
            {

                bufRocker[3] = _joystick_V.R + 1;
            }
            else
            {

                bufRocker[3] = _joystick_V.R;

            }
            if (_joystick_V.U != 65535)
            {

                bufRocker[4] = _joystick_V.U + 1;
            }
            else
            {

                bufRocker[4] = _joystick_V.U;

            }
            if (((_joystick_V.CurButtonsState & JoystickButtons.POV_UP) == JoystickButtons.POV_UP) || ((_joystick_V.CurButtonsState & JoystickButtons.LeftUp) == JoystickButtons.LeftUp) || ((_joystick_V.CurButtonsState & JoystickButtons.RightUp) == JoystickButtons.RightUp))
            {
                bufdirecet[0] = 0x01;
            }
            else
            {
                bufdirecet[0] = 0x00;
            }

            if (((_joystick_V.CurButtonsState & JoystickButtons.POV_Down) == JoystickButtons.POV_Down) || ((_joystick_V.CurButtonsState & JoystickButtons.RightDown) == JoystickButtons.RightDown) || ((_joystick_V.CurButtonsState & JoystickButtons.LeftDown) == JoystickButtons.LeftDown))
            {
                bufdirecet[1] = 0x01;
            }
            else
            {
                bufdirecet[1] = 0x00;
            }
            if (((_joystick_V.CurButtonsState & JoystickButtons.POV_Left) == JoystickButtons.POV_Left) || ((_joystick_V.CurButtonsState & JoystickButtons.LeftDown) == JoystickButtons.LeftDown) || ((_joystick_V.CurButtonsState & JoystickButtons.LeftUp) == JoystickButtons.LeftUp))
            {
                bufdirecet[2] = 0x01;
            }
            else
            {
                bufdirecet[2] = 0x00;
            }
            if (((_joystick_V.CurButtonsState & JoystickButtons.POV_Right) == JoystickButtons.POV_Right) || ((_joystick_V.CurButtonsState & JoystickButtons.RightDown) == JoystickButtons.RightDown) || ((_joystick_V.CurButtonsState & JoystickButtons.RightUp) == JoystickButtons.RightUp))
            {
                bufdirecet[3] = 0x01;
            }
            else
            {
                bufdirecet[3] = 0x00;
            }

            if ((_joystick_V.CurButtonsState & JoystickButtons.B1) == JoystickButtons.B1)
            {
                bufButton[0] = 0x01;
            }
            else
            {
                bufButton[0] = 0x00;
            }

            if ((_joystick_V.CurButtonsState & JoystickButtons.B2) == JoystickButtons.B2)
            {
                bufButton[1] = 0x01;
            }
            else
            {
                bufButton[1] = 0x00;
            }

            if ((_joystick_V.CurButtonsState & JoystickButtons.B3) == JoystickButtons.B3)
            {
                bufButton[2] = 0x01;
            }
            else
            {
                bufButton[2] = 0x00;
            }

            if ((_joystick_V.CurButtonsState & JoystickButtons.B4) == JoystickButtons.B4)
            {
                bufButton[3] = 0x01;
            }
            else
            {
                bufButton[3] = 0x00;
            }

            if ((_joystick_V.CurButtonsState & JoystickButtons.B5) == JoystickButtons.B5)
            {
                bufButton[4] = 0x01;
            }
            else
            {
                bufButton[4] = 0x00;
            }
            if ((_joystick_V.CurButtonsState & JoystickButtons.B6) == JoystickButtons.B6)
            {
                bufButton[5] = 0x01;
            }
            else
            {
                bufButton[5] = 0x00;
            }
            if ((_joystick_V.CurButtonsState & JoystickButtons.B7) == JoystickButtons.B7)
            {
                bufButton[6] = 0x01;
            }
            else
            {
                bufButton[6] = 0x00;
            }
            if ((_joystick_V.CurButtonsState & JoystickButtons.B8) == JoystickButtons.B8)
            {
                bufButton[7] = 0x01;
            }
            else
            {
                bufButton[7] = 0x00;
            }
            if ((_joystick_V.CurButtonsState & JoystickButtons.B9) == JoystickButtons.B9)
            {
                bufButton[8] = 0x01;
            }
            else
            {
                bufButton[8] = 0x00;
            }
            if ((_joystick_V.CurButtonsState & JoystickButtons.B10) == JoystickButtons.B10)
            {
                bufButton[9] = 0x01;
            }
            else
            {
                bufButton[9] = 0x00;
            }
        }

        #endregion

        #region  窗体最大化

        ////改变窗体大小
        //private void setControls(float newx, float newy, Control cons)
        //{
        //    try
        //    {
        //        foreach (Control con in cons.Controls)//遍历窗体中的控件，重新设置控件的值
        //        {
        //            string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//获取控件的Tag属性值，并分割后存储字符串数组
        //            float a = Convert.ToSingle(mytag[0]) * newx;//根据窗体缩放比例确定控件的值，宽度
        //            con.Width = (int)a;//宽度
        //            a = Convert.ToSingle(mytag[1]) * newy;//高度
        //            con.Height = (int)(a);
        //            a = Convert.ToSingle(mytag[2]) * newx;//左边距离
        //            con.Left = (int)(a);
        //            a = Convert.ToSingle(mytag[3]) * newy;//上边缘距离
        //            con.Top = (int)(a);
        //            float currentSize = Convert.ToSingle(mytag[4]) * newy;//字体大小
        //            con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
        //            if (con.Controls.Count > 0)
        //            {
        //                setControls(newx, newy, con);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        //public float X, Y;

        //private void fmMain_Resize_1(object sender, EventArgs e)
        //{
        //    float newx = (this.Width) / X; //窗体宽度缩放比例
        //    float newy = this.Height / Y;//窗体高度缩放比例
        //    setControls(newx, newy, this);//随窗体改变控件大小
        //                                  // this.Text = this.Width.ToString() + " " + this.Height.ToString();//窗体标题栏文本
        //}


        //private void setTag(Control cons)
        //{
        //    //遍历窗体中的控件
        //    foreach (Control con in cons.Controls)
        //    {
        //        con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
        //        if (con.Controls.Count > 0)
        //            setTag(con);
        //    }
        //}
        ///// <summary>
        ///// 改变窗体大小
        ///// </summary>
        //private void Formsize()
        //{
        //    //
        //    //this.Resize += new EventHandler(Form1_Resize);//窗体调整大小时引发事件
        //    X = this.Width;//获取窗体的宽度
        //    Y = this.Height;//获取窗体的高度
        //    setTag(this);//调用方法

        //}
        #endregion



        #region 数据交互

        /// <summary>
        ///  发送数据
        /// </summary>


        public byte[] bufsend = new byte[64];
        public static string senddata = "";
        public static StringBuilder sbb = new StringBuilder();
        public static string name1 = "senddata" + DateTime.Now.ToString("yyyyMMddHHmmss");
        public delegate int AddHandler1(StringBuilder sb);//异步委托存储字符串
        public void Send()
        {
            JoystickButtinValueRefresh();
            bufsend[0] = 0xeb;
            bufsend[1] = 0x90;
            bufsend[2] = 0X40;
            bufsend[3] = (byte)(bufRocker[3] / 256);
            bufsend[4] = (byte)(bufRocker[4] / 256);
            bufsend[5] = (byte)(bufRocker[1] / 256);
            bufsend[6] = (byte)(bufRocker[0] / 256);
            bufsend[7] = bufdirecet[0];
            bufsend[8] = bufdirecet[1];
            bufsend[9] = bufdirecet[2];
            bufsend[10] = bufdirecet[3];
            bufsend[11] = bufButton[5];
            bufsend[12] = bufButton[4];

            bufsend[14] = (byte)(bufRocker[2] / 256);


            bufsend[13] = (byte)(bufRocker[2] / 256);



            bufsend[15] = bufButton[2];
            bufsend[16] = bufButton[3];
            bufsend[17] = bufButton[0];
            bufsend[18] = bufButton[1];
            bufsend[19] = bufButton[7];
            bufsend[20] = bufButton[6];
            bufsend[21] = bufButton[9];
            bufsend[22] = bufButton[8];
            bufsend[23] = DataToROV.PidChangeCmd;
            bufsend[24] = DataToROV.PidInquireCmd;
            bufsend[25] = DataToROV.PIntNum;
            bufsend[26] = DataToROV.PDecimalNum;
            bufsend[27] = DataToROV.IIntNum;
            bufsend[28] = DataToROV.IDecimalNum;
            bufsend[29] = DataToROV.DIntNum;
            bufsend[30] = DataToROV.DDecimalNum;
            bufsend[31] = DataToROV.AutoControlCmd;
            bufsend[32] = (byte)(DataToROV.DepthSetingNum >> 8);
            bufsend[33] = (byte)DataToROV.DepthSetingNum;
            bufsend[34] = (byte)(DataToROV.DirectSetingNum >> 8);
            bufsend[35] = (byte)DataToROV.DirectSetingNum;
            bufsend[36] = (byte)(DataToROV.PitchSetingNum >> 8);
            bufsend[37] = (byte)DataToROV.PitchSetingNum;
            bufsend[38] = (byte)(DataToROV.RollSetingNum >> 8);
            bufsend[39] = (byte)DataToROV.RollSetingNum;
           // bufsend[40] = (byte)numericUpDown1.Value;
            bufsend[41] = DataToROV.SystemControlCmd;
            bufsend[42] = DataToROV.SystemInquireCmd;
            bufsend[43] = DataToROV.SystemChangeCmd;
            bufsend[44] = DataToROV.Frequent;
            bufsend[45] = DataToROV.Amplitude;
            check.RsCrc(bufsend, 60, out bufsend[61], out bufsend[60]);
            bufsend[62] = 0x0d;
            bufsend[63] = 0x0a;
            netport1.NetSendData(bufsend);
            senddata = "";
            for (int i = 0; i < 64; i++)
            {
                senddata += bufsend[i].ToString("X2");
            }
            //senddata += "\r\n";

            //tbSendData.Text =senddata ;
            //LogHelpers.WriteLog(senddata);
            senddata = DateTime.Now.ToString("yyyyMMddHHmmss:") + senddata + "\r\n";


            AddHandler1 handler = new AddHandler1(Filed.SaveFile1);

            //IAsyncResult: 异步操作接口(interface)
            //BeginInvoke: 委托(delegate)的一个异步方法的开始
            IAsyncResult result = handler.BeginInvoke(sbb, null, null);
            string rr = handler.EndInvoke(result).ToString();
        }

        /// <summary>
        /// 刷新界面
        /// </summary>

        private void TmSend_Tick(object sender, EventArgs e)
        {
            Send();
            receive();
            if(tflag ==1)
            {
                time++;
                
                if (time == 10)
                {
                    time = 0;
                    lbTishi.Text  = "";
                    tflag = 0;
                }
            }
        }
        public static StringBuilder sb = new StringBuilder();

        public delegate int AddHandler(StringBuilder sbb);
        public static string name = "receivedata" + DateTime.Now.ToString("yyyyMMddHHmmss");

        public static string receivedata = "";

       /// <summary>
       /// 接收数据解析
       /// </summary>
        public void receive()
        {
            receivedata = "";
            receivedata = DateTime.Now.ToString("yyyyMMddHHmmss:");//加时间戳

            for (int i = 0; i < 64; i++)
            {
                receivedata += NetPort.temp[i].ToString("X2");
            }
            receivedata += "\r\n";
          
            AddHandler handler = new AddHandler(Filed.SaveFile);
           
            progressBar3.Value = DataToROV.Dangwei;//显示档位
            ////IAsyncResult: 异步操作接口(interface)
            ////BeginInvoke: 委托(delegate)的一个异步方法的开始
            IAsyncResult result = handler.BeginInvoke(sb, null, null);//异步调用，用来存储文件
            //heading
 //           compointCt1.DirAngle = ((NetPort.temp[6]) * 256 + (NetPort.temp[7])) / 10.0f;
           
            unchecked
            {
                int x = NetPort.temp[6] << 8 | NetPort.temp[7];
                float output = (x & 0x7FFF) / 10f;
                if ((NetPort.temp[6] & 0x80) > 0)
                {
                    // robot.PitchSign  = false;
                    lbheading.Text = string.Format("{0:f1}", 0.0f - float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float));

                }
                else
                {
                    // robot.PitchSign  = true;
                    lbheading.Text = string.Format("{0:f1}", float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float));

                }
            }
            //pitch
            unchecked
            {
                int x = NetPort.temp[10] << 8 | NetPort.temp[11];
                float output = (x & 0x7FFF) / 10f;
                if ((NetPort.temp[10] & 0x80) > 0)
                {
                    // robot.PitchSign  = false;
                    pitchAndBank1.Pitch = 0.0f - float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float);

                }
                else
                {
                    // robot.PitchSign  = true;
                    pitchAndBank1.Pitch = float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float);

                }
            }
            lbPitch.Text = string.Format("{0:f1}", pitchAndBank1.Pitch);

            //row
            unchecked
            {
                int x = NetPort.temp[8] << 8 | NetPort.temp[9];
                float output = (x & 0x7FFF) / 10f;
                if ((NetPort.temp[8] & 0x80) > 0)
                {
                    // robot.PitchSign  = false;
                    pitchAndBank1.Bank = 0.0f - float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float);

                }
                else
                {
                    // robot.PitchSign  = true;
                    pitchAndBank1.Bank = float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float);

                }
            }
            lbRow.Text = string.Format("{0:f1}", pitchAndBank1.Bank);

            //异常显示
            string a1 = "";
            string a2 = "";
            string a3 = "";
            string a4 = "";
            string a5 = "";
            string a6 = "";
            string a7 = "";
            string a8 = "";
            string a9 = "";
            string a10 = "";
            string a11 = "";
            string a12 = "";

            if ((NetPort.temp[3] & 0x80) == 0x80)
            {

                a1 = "右前横推异常" + " ";

            }
            else
            {
                a1 = "";
            }
            if ((NetPort.temp[3] & 0x40) == 0x40)
            {

                a2 = "右后横推异常" + " ";
            }
            else
            {
                a2 = "";
            }
            if ((NetPort.temp[3] & 0x20) == 0x20)
            {

                a3 = "左后横推异常" + " ";
            }
            else
            {
                a3 = "";
            }
            if ((NetPort.temp[3] & 0x10) == 0x10)
            {

                a4 = "左前横推异常" + " ";
            }
            else
            {
                a4 = "";
            }
            if ((NetPort.temp[3] & 0x08) == 0x08)
            {

                a5 = "右前纵推异常" + " ";
            }
            else
            {
                a5 = "";
            }
            if ((NetPort.temp[3] & 0x04) == 0x04)
            {

                a6 = "右后纵推异常" + " ";
            }
            else
            {
                a6 = "";
            }
            if ((NetPort.temp[3] & 0x02) == 0x02)
            {

                a7 = "左后纵推异常" + " ";
            }
            else
            {
                a7 = "";
            }
            if ((NetPort.temp[3] & 0x01) == 0x01)
            {

                a8 = "左前纵推异常" + " ";
            }
            else
            {
                a8 = "";
            }
            if ((NetPort.temp[4] & 0x80) == 0x80)
            {

                a9 = "控制仓温度异常" + " ";
            }
            else
            {
                a9 = "";
            }
            if ((NetPort.temp[4] & 0x40) == 0x40)
            {

                a10 = "电源模块温度异常" + " ";
            }
            else
            {
                a10 = "";
            }
            if ((NetPort.temp[4] & 0x20) == 0x20)
            {

                a11 = "前端盖漏水" + " ";
            }
            else
            {
                a11 = "";
            }
            if ((NetPort.temp[4] & 0x10) == 0x10)
            {

                a12 = "后端盖漏水" + " ";
            }
            else
            {
                a12 = "";
            }
            scrollingText1.Text = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9 + a10 + a11 + a12;
            //推进器
            if ((NetPort.temp[4] & 0x01) == 0x01)
            {
                ledleft.Value = true;
            }
            else
            {
                ledleft.Value = false;
            }
            if ((NetPort.temp[4] & 0x02) == 0x02)
            {
                ledright.Value = true;
            }
            else
            {
                ledright.Value = false;
            }
            //自动定深
            if ((NetPort.temp[5] & 0x80) == 0x80)
            {
                lbAutoDeep.ForeColor = Color.Chartreuse;
            }
            else
            {
                lbAutoDeep.ForeColor = Color.White;
            }
            //自动定向
            if ((NetPort.temp[5] & 0x40) == 0x40)
            {
                lbAutoDirect.ForeColor = Color.Chartreuse;
            }
            else
            {
                lbAutoDirect.ForeColor = Color.White;
            }
            //自动定姿态
            if ((NetPort.temp[5] & 0x20) == 0x20)
            {
                lbAutoAltitude.ForeColor = Color.Chartreuse;
            }
            else
            {
                lbAutoAltitude.ForeColor = Color.White;
            }
            //前后左右指示灯
            if ((NetPort.temp[5] & 0x02) == 0x02)
            {
                ledback.Value = true;
            }
            else
            {
                ledback.Value = false;
            }
            if ((NetPort.temp[5] & 0x04) == 0x04)
            {
                ledfront.Value = true;
            }
            else
            {
                ledfront.Value = false;
            }
            if ((NetPort.temp[5] & 0x08) == 0x08)
            {
                leddown.Value = true;
            }
            else
            {
                leddown.Value = false;
            }
            if ((NetPort.temp[5] & 0x10) == 0x10)
            {
                ledup.Value = true;
            }
            else
            {
                ledup.Value = false;
            }


            //水温
            // lbWaterTemperature .Text = ((NetPort.temp[12] * 256 + NetPort.temp[13]) / 10.0f).ToString();
            unchecked
            {
                int x = NetPort.temp[12] << 8 | NetPort.temp[13];
                float output = (x & 0x7FFF) / 10f;
                if ((NetPort.temp[12] & 0x80) > 0)
                {
                    lbWaterTemperature.Text = "-" + float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float).ToString();
                }
              
                else
                {
                    lbWaterTemperature.Text = "+" + float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float).ToString();
                }
            }
            //仓温
            unchecked
            {
                int x = NetPort.temp[14] << 8 | NetPort.temp[15];
                float output = (x & 0x7FFF) / 10f;
                if ((NetPort.temp[14] & 0x80) > 0)
                {
                    lbRoomTemperature.Text = "-" + float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float).ToString();
                }
                //else if((NetPort.temp[14] & 0x80) == 0)
                //{
                //    lbRoomTemperature.Text = "0";
                //}
                else
                {
                    lbRoomTemperature.Text = "+" + float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float).ToString();
                }
            }
            //电源仓温
            unchecked
            {
                int x = NetPort.temp[24] << 8 | NetPort.temp[25];
                float output = (x & 0x7FFF) / 10f;
                if ((NetPort.temp[24] & 0x80) > 0)
                {
                    lbPowerT.Text = "-" + float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float).ToString();
                }
                //else if (NetPort.temp[24] == 0)
                //{
                //    lbPowerT.Text = "0";
                //}
                else
                {
                    lbPowerT.Text = "+" + float.Parse(output.ToString("0.#"), System.Globalization.NumberStyles.Float).ToString();
                }
            }
            //深度
            lbWaterDepth.Text = ((NetPort.temp[16] * 256 + NetPort.temp[17]) / 100.0f).ToString();
            //湿度
            lbHumid.Text = Convert.ToInt32(NetPort.temp[18]).ToString();
            //云台反馈
            unchecked
            {
                if ((NetPort.temp[19] & 0x80) > 0)
                {
                    lbYuntai.Text = "-" + (NetPort.temp[19] - 0x80).ToString();
                }
                //else if((NetPort.temp[19] & 0x80) == 0)
                //{
                //    lbYuntai.Text = "0";
                //}
                else
                {
                    lbYuntai.Text = "+" + NetPort.temp[19].ToString();
                }
            }

            //灯反馈
            progressBar1.Value = NetPort.temp[20];
            if (progressBar1.Value != 0)
            {
                label47.ForeColor = Color.Chartreuse;
            }
            else
            {
                label47.ForeColor = Color.White;
            }
            // progressBar2 .Value = NetPort.temp[21];
            if (progressBar2.Value != 0)
            {
                label48.ForeColor = Color.Chartreuse;
            }
            else
            {
                label48.ForeColor = Color.White;
            }
            //总功率
            if ((NetPort.temp[22] * 256 + NetPort.temp[23]) == 0)
            {
                lbPower.Text = "<400";
            }
            else if((NetPort.temp[22] * 256 + NetPort.temp[23]) > 5000)
            {
                lbPower.Text = "N/A";
            }
            else
            {
                lbPower.Text = ((NetPort.temp[22] * 256 + NetPort.temp[23])).ToString();
            }

            //推进器转速
            lbyqsp.Text = ((NetPort .temp[26] * 256 + NetPort.temp[27])).ToString();
            lbyhsp.Text = ((NetPort.temp[28] * 256 + NetPort.temp[29])).ToString();
            lbzhsp.Text = ((NetPort.temp[30] * 256 + NetPort.temp[31])).ToString();
            lbzqsp.Text = ((NetPort.temp[32] * 256 + NetPort.temp[33])).ToString();
            lbyqcz.Text = ((NetPort.temp[34] * 256 + NetPort.temp[35])).ToString();
            lbyhcz.Text = ((NetPort.temp[36] * 256 + NetPort.temp[37])).ToString();
            lbzhcz.Text = ((NetPort.temp[38] * 256 + NetPort.temp[39])).ToString();
            lbzqcz.Text = ((NetPort.temp[40] * 256 + NetPort.temp[41])).ToString();
        }

        #endregion


        #region 摄像头相关函数
        //图像处理
        public void UpdateClientList(string strLogStatus, IntPtr lpDeviceInfo)
        {
            //列表新增报警信息
            labelLogin.Text = "登录状态（异步）：" + strLogStatus;
        }

        public void cbLoginCallBack(int lUserID, int dwResult, IntPtr lpDeviceInfo, IntPtr pUser)
        {
            string strLoginCallBack = "登录设备，lUserID：" + lUserID + "，dwResult：" + dwResult;

            if (dwResult == 0)
            {
                uint iErrCode = CHCNetSDK.NET_DVR_GetLastError();
                strLoginCallBack = strLoginCallBack + "，错误号:" + iErrCode;
            }

            //下面代码注释掉也会崩溃
            if (InvokeRequired)
            {
                object[] paras = new object[2];
                paras[0] = strLoginCallBack;
                paras[1] = lpDeviceInfo;
                labelLogin.BeginInvoke(new UpdateTextStatusCallback(UpdateClientList), paras);
            }
            else
            {
                //创建该控件的主线程直接更新信息列表 
                UpdateClientList(strLoginCallBack, lpDeviceInfo);
            }

        }

        
        /// <summary>
        /// 摄像头登陆按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
                            textBoxUserName.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID < 0)
            {

                struLogInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();

                //设备IP地址或者域名
                byte[] byIP = System.Text.Encoding.Default.GetBytes(textBoxIP.Text);
                struLogInfo.sDeviceAddress = new byte[129];
                byIP.CopyTo(struLogInfo.sDeviceAddress, 0);

                //设备用户名
                byte[] byUserName = System.Text.Encoding.Default.GetBytes(textBoxUserName.Text);
                struLogInfo.sUserName = new byte[64];
                byUserName.CopyTo(struLogInfo.sUserName, 0);

                //设备密码
                byte[] byPassword = System.Text.Encoding.Default.GetBytes(textBoxPassword.Text);
                struLogInfo.sPassword = new byte[64];
                byPassword.CopyTo(struLogInfo.sPassword, 0);

                struLogInfo.wPort = ushort.Parse(textBoxPort.Text);//设备服务端口号

                if (LoginCallBack == null)
                {
                    LoginCallBack = new CHCNetSDK.LOGINRESULTCALLBACK(cbLoginCallBack);//注册回调函数                    
                }
                struLogInfo.cbLoginResult = LoginCallBack;
                struLogInfo.bUseAsynLogin = false; //是否异步登录：0- 否，1- 是 

                DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();

                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V40(ref struLogInfo, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Login_V40 failed, error code= " + iLastErr; //登录失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //登录成功
                    //MessageBox.Show("Login Success!");
                    btnLogin.Text = "Logout";
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Logout failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lUserID = -1;
                btnLogin.Text = "Login";
            }
            return;
        }

        private void btnLogin1_Click(object sender, EventArgs e)
        {
            if (textBoxIP1.Text == "" || textBoxPort1.Text == "" ||
                textBoxUserName1.Text == "" || textBoxPassword1.Text == "")
            {
                MessageBox.Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID1 < 0)
            {

                struLogInfo1 = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();

                //设备IP地址或者域名
                byte[] byIP = System.Text.Encoding.Default.GetBytes(textBoxIP1.Text);
                struLogInfo1.sDeviceAddress = new byte[129];
                byIP.CopyTo(struLogInfo1.sDeviceAddress, 0);

                //设备用户名
                byte[] byUserName = System.Text.Encoding.Default.GetBytes(textBoxUserName1.Text);
                struLogInfo1.sUserName = new byte[64];
                byUserName.CopyTo(struLogInfo1.sUserName, 0);

                //设备密码
                byte[] byPassword = System.Text.Encoding.Default.GetBytes(textBoxPassword1.Text);
                struLogInfo1.sPassword = new byte[64];
                byPassword.CopyTo(struLogInfo1.sPassword, 0);

                struLogInfo1.wPort = ushort.Parse(textBoxPort1.Text);//设备服务端口号

                if (LoginCallBack1 == null)
                {
                    LoginCallBack1 = new CHCNetSDK.LOGINRESULTCALLBACK(cbLoginCallBack);//注册回调函数                    
                }
                struLogInfo1.cbLoginResult = LoginCallBack1;
                struLogInfo1.bUseAsynLogin = false; //是否异步登录：0- 否，1- 是 

                DeviceInfo1 = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();

                //登录设备 Login the device
                m_lUserID1 = CHCNetSDK.NET_DVR_Login_V40(ref struLogInfo1, ref DeviceInfo1);
                if (m_lUserID1 < 0)
                {
                    iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                    str1 = "NET_DVR_Login_V40 failed, error code= " + iLastErr1; //登录失败，输出错误号
                    MessageBox.Show(str1);
                    return;
                }
                else
                {
                    //登录成功
                    //MessageBox.Show("Login Success!");
                    btnLogin1.Text = "Logout";
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle1 >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID1))
                {
                    iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                    str1 = "NET_DVR_Logout failed, error code= " + iLastErr1;
                    MessageBox.Show(str1);
                    return;
                }
                m_lUserID1 = -1;
                btnLogin1.Text = "Login";
            }
            return;
        }

        private void btnLogin2_Click(object sender, EventArgs e)
        {
            if (textBoxIP2.Text == "" || textBoxPort2.Text == "" ||
                textBoxUserName2.Text == "" || textBoxPassword2.Text == "")
            {
                MessageBox.Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID2 < 0)
            {

                struLogInfo2 = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();

                //设备IP地址或者域名
                byte[] byIP = System.Text.Encoding.Default.GetBytes(textBoxIP2.Text);
                struLogInfo2.sDeviceAddress = new byte[129];
                byIP.CopyTo(struLogInfo2.sDeviceAddress, 0);

                //设备用户名
                byte[] byUserName = System.Text.Encoding.Default.GetBytes(textBoxUserName2.Text);
                struLogInfo2.sUserName = new byte[64];
                byUserName.CopyTo(struLogInfo2.sUserName, 0);

                //设备密码
                byte[] byPassword = System.Text.Encoding.Default.GetBytes(textBoxPassword2.Text);
                struLogInfo2.sPassword = new byte[64];
                byPassword.CopyTo(struLogInfo2.sPassword, 0);

                struLogInfo2.wPort = ushort.Parse(textBoxPort2.Text);//设备服务端口号

                if (LoginCallBack2 == null)
                {
                    LoginCallBack2 = new CHCNetSDK.LOGINRESULTCALLBACK(cbLoginCallBack);//注册回调函数                    
                }
                struLogInfo2.cbLoginResult = LoginCallBack2;
                struLogInfo2.bUseAsynLogin = false; //是否异步登录：0- 否，1- 是 

                DeviceInfo2 = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();

                //登录设备 Login the device
                m_lUserID2 = CHCNetSDK.NET_DVR_Login_V40(ref struLogInfo2, ref DeviceInfo2);
                if (m_lUserID2 < 0)
                {
                    iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                    str2 = "NET_DVR_Login_V40 failed, error code= " + iLastErr2; //登录失败，输出错误号
                    MessageBox.Show(str2);
                    return;
                }
                else
                {
                    //登录成功
                    //MessageBox.Show("Login Success!");
                    btnLogin2.Text = "Logout";
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle2 >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID2))
                {
                    iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                    str2 = "NET_DVR_Logout failed, error code= " + iLastErr2;
                    MessageBox.Show(str2);
                    return;
                }
                m_lUserID2 = -1;
                btnLogin2.Text = "Login";
            }
            return;
        }

        private void btnLogin3_Click(object sender, EventArgs e)
        {
            if (textBoxIP3.Text == "" || textBoxPort3.Text == "" ||
            textBoxUserName3.Text == "" || textBoxPassword3.Text == "")
            {
                MessageBox.Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID3 < 0)
            {

                struLogInfo3 = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();

                //设备IP地址或者域名
                byte[] byIP = System.Text.Encoding.Default.GetBytes(textBoxIP3.Text);
                struLogInfo3.sDeviceAddress = new byte[129];
                byIP.CopyTo(struLogInfo3.sDeviceAddress, 0);

                //设备用户名
                byte[] byUserName = System.Text.Encoding.Default.GetBytes(textBoxUserName3.Text);
                struLogInfo3.sUserName = new byte[64];
                byUserName.CopyTo(struLogInfo3.sUserName, 0);

                //设备密码
                byte[] byPassword = System.Text.Encoding.Default.GetBytes(textBoxPassword3.Text);
                struLogInfo3.sPassword = new byte[64];
                byPassword.CopyTo(struLogInfo3.sPassword, 0);

                struLogInfo3.wPort = ushort.Parse(textBoxPort3.Text);//设备服务端口号

                if (LoginCallBack3 == null)
                {
                    LoginCallBack3 = new CHCNetSDK.LOGINRESULTCALLBACK(cbLoginCallBack);//注册回调函数                    
                }
                struLogInfo3.cbLoginResult = LoginCallBack3;
                struLogInfo3.bUseAsynLogin = false; //是否异步登录：0- 否，1- 是 

                DeviceInfo3 = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();

                //登录设备 Login the device
                m_lUserID3 = CHCNetSDK.NET_DVR_Login_V40(ref struLogInfo3, ref DeviceInfo3);
                if (m_lUserID3 < 0)
                {
                    iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                    str3 = "NET_DVR_Login_V40 failed, error code= " + iLastErr3; //登录失败，输出错误号
                    MessageBox.Show(str3);
                    return;
                }
                else
                {
                    //登录成功
                    //MessageBox.Show("Login Success!");
                    btnLogin3.Text = "Logout";
                }

            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle3 >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID3))
                {
                    iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                    str3 = "NET_DVR_Logout failed, error code= " + iLastErr3;
                    MessageBox.Show(str3);
                    return;
                }
                m_lUserID3 = -1;
                btnLogin3.Text = "Login";
            }
            return;
        }

        /// <summary>
        /// 摄像头预览函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (m_lUserID < 0)
            {
                MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                if (textBoxID.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }


                if (RealData == null)
                {
                    RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }

                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview.Text = "Stop Live View";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "Live View";

            }
            return;
        }

        private void btnPreview1_Click(object sender, EventArgs e)
        {
            if (m_lUserID1 < 0)
            {
                MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle1 < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd1.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                if (textBoxID1.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID1.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }


                if (RealData1 == null)
                {
                    RealData1 = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }

                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle1 = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID1, ref lpPreviewInfo, null/*RealData1*/, pUser);
                if (m_lRealHandle1 < 0)
                {
                    iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                    str1 = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr1; //预览失败，输出错误号
                    MessageBox.Show(str1);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview1.Text = "Stop Live View";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle1))
                {
                    iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                    str1 = "NET_DVR_StopRealPlay failed, error code= " + iLastErr1;
                    MessageBox.Show(str1);
                    return;
                }
                m_lRealHandle1 = -1;
                btnPreview1.Text = "Live View";

            }
            return;
        }

        private void btnPreview2_Click(object sender, EventArgs e)
        {
            if (m_lUserID2 < 0)
            {
                MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle2 < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd2.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                if (textBoxID2.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID2.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }


                if (RealData2 == null)
                {
                    RealData2 = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }

                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle2 = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID2, ref lpPreviewInfo, null/*RealData1*/, pUser);
                if (m_lRealHandle2 < 0)
                {
                    iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                    str2 = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr2; //预览失败，输出错误号
                    MessageBox.Show(str2);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview2.Text = "Stop Live View";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle2))
                {
                    iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                    str2 = "NET_DVR_StopRealPlay failed, error code= " + iLastErr2;
                    MessageBox.Show(str2);
                    return;
                }
                m_lRealHandle2 = -1;
                btnPreview2.Text = "Live View";

            }
            return;
        }

        private void btnPreview3_Click(object sender, EventArgs e)
        {
            if (m_lUserID3 < 0)
            {
                MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle3 < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd3.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                if (textBoxID3.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID3.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }


                if (RealData3 == null)
                {
                    RealData3 = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }

                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                m_lRealHandle3 = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID3, ref lpPreviewInfo, null/*RealData1*/, pUser);
                if (m_lRealHandle3 < 0)
                {
                    iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                    str3 = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr3; //预览失败，输出错误号
                    MessageBox.Show(str3);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview3.Text = "Stop Live View";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle1))
                {
                    iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                    str3 = "NET_DVR_StopRealPlay failed, error code= " + iLastErr3;
                    MessageBox.Show(str3);
                    return;
                }
                m_lRealHandle3 = -1;
                btnPreview3.Text = "Live View";

            }
            return;
        }


        /// <summary>
        /// 读取图像数据流
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="pUser"></param>
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            if (dwBufSize > 0)
            {
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);

                string str = "实时流数据.ps";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();
            }
        }










        //点击开启摄像头按钮响应事件

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem1.Text == "摄像机连接")
            {
                try
                {
                    btnLogin_Click(null, null);
                    btnLogin1_Click(null, null);
                    btnLogin2_Click(null, null);
                    btnLogin3_Click(null, null);

                    btnPreview_Click(null, null);
                    btnPreview1_Click(null, null);
                    btnPreview2_Click(null, null);
                    btnPreview3_Click(null, null);
                    toolStripMenuItem1.Text = "摄像机断开";
                    MessageBox.Show("Connect Success!");
                }
                catch
                {

                }

            }
            else if (toolStripMenuItem1.Text == "摄像机断开")
            {
                //if (开始录像ToolStripMenuItem.Text == "结束录像")
                //{
                //    btnRecord_Click(null, null);
                //    btnRecord1_Click(null, null);
                //    开始录像ToolStripMenuItem.Text = "开始录像";
                //}
                btnPreview_Click(null, null);
                btnPreview1_Click(null, null);
                btnPreview2_Click(null, null);
                btnPreview3_Click(null, null);

                btnLogin_Click(null, null);
                btnLogin1_Click(null, null);
                btnLogin2_Click(null, null);
                btnLogin3_Click(null, null);
                toolStripMenuItem1.Text = "摄像机连接";
            }

        }


        #endregion




        #region 按钮

        //数据显示窗体按钮
        private void toolStripMenuItem3_Click(object sender, EventArgs e)     //数据
        {
            data FrmAdd = GenericSingleton<data>.CreateInstrance();
            FrmAdd.Show();
            FrmAdd.GetForm(this);
        }
        //PID参数配置窗体按钮
        private void pID参数配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PIDform pidform = PIDform.GetForm();//获取网络设置窗体

            pidform.Show(); //显示窗体
            pidform.Activate();//激活窗体获取焦点
        }

       
        //设置按钮
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem4.Text == "ROV连接")
            {
                if (netport1.Connect(SerConnectIP1, Convert.ToInt32(SerConnectPort1)))
                {
                    MessageBox.Show("ROV连接成功！");

                    toolStripMenuItem4.Text = "ROV断开";
                    TmSend.Start();
                }
                else
                {

                    toolStripMenuItem4.Text = "ROV连接";
                    TmSend.Stop();
                }


            }
            else
            {
                netport1.DisConnect();
                toolStripMenuItem4.Text = "ROV连接";
                TmSend.Stop();
            }
        }


        //连接设置窗体
        private void 连接设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmConnection FrmAdd = GenericSingleton<fmConnection>.CreateInstrance();
            FrmAdd.Show();
            FrmAdd.GetForm(this);
        }


        //录像按钮
        private void btnRecoder_Click(object sender, EventArgs e)
        {
            video_record();
            video_record1();
            video_record2();
            video_record3();
        }

        /// <summary>
        /// 录像函数
        /// </summary>
        public void video_record()
        {
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            sVideoFileName = @"D:\DCIM\vedio\摄像机1_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".mp4";

            if (m_bRecord == false)
            {
                //强制I帧 Make a I frame
                int lChannel = Int16.Parse(textBoxChannel.Text); //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);

                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_SaveRealData failed, error code= " + iLastErr;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    // btnRecord.Text = "前摄像机停止录像";
                    //btnRecoder .BackColor = Color.SpringGreen;
                    lbRecorder.ForeColor = Color.Chartreuse;
                    m_bRecord = true;
                }
            }
            else
            {
                //停止录像 Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr;
                    // MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    str = "Successful to stop recording and the saved file is " + sVideoFileName;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像成功";
                    tflag = 1;
                    //btnRecord.Text = "前摄像机开始录像";
                    // btnRecoder .BackColor = Color.SlateGray;
                    lbRecorder.ForeColor = Color.Yellow;
                    m_bRecord = false;
                }
            }
            return;
        }

        public void video_record1()
        {
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            sVideoFileName = @"D:\DCIM\vedio\摄像机2_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".mp4";

            if (m_bRecord1 == false)
            {
                //强制I帧 Make a I frame
                int lChannel = Int16.Parse(textBoxChannel1.Text); //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID1, lChannel);

                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle1, sVideoFileName))
                {
                    iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                    str1 = "NET_DVR_SaveRealData failed, error code= " + iLastErr1;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    // btnRecord.Text = "前摄像机停止录像";
                    //btnRecoder .BackColor = Color.SpringGreen;
                    lbRecorder.ForeColor = Color.Chartreuse;
                    m_bRecord1 = true;
                }
            }
            else
            {
                //停止录像 Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle1))
                {
                    iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                    str1 = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr1;
                    // MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    str1 = "Successful to stop recording and the saved file is " + sVideoFileName;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像成功";
                    tflag = 1;
                    //btnRecord.Text = "前摄像机开始录像";
                    // btnRecoder .BackColor = Color.SlateGray;
                    lbRecorder.ForeColor = Color.Yellow;
                    m_bRecord1 = false;
                }
            }
            return;
        }

        public void video_record2()
        {
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            sVideoFileName = @"D:\DCIM\vedio\摄像机3_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".mp4";

            if (m_bRecord2 == false)
            {
                //强制I帧 Make a I frame
                int lChannel = Int16.Parse(textBoxChannel2.Text); //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID2, lChannel);

                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle2, sVideoFileName))
                {
                    iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                    str2 = "NET_DVR_SaveRealData failed, error code= " + iLastErr2;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    // btnRecord.Text = "前摄像机停止录像";
                    //btnRecoder .BackColor = Color.SpringGreen;
                    lbRecorder.ForeColor = Color.Chartreuse;
                    m_bRecord2 = true;
                }
            }
            else
            {
                //停止录像 Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle2))
                {
                    iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                    str2 = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr2;
                    // MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    str2 = "Successful to stop recording and the saved file is " + sVideoFileName;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像成功";
                    tflag = 1;
                    //btnRecord.Text = "前摄像机开始录像";
                    // btnRecoder .BackColor = Color.SlateGray;
                    lbRecorder.ForeColor = Color.Yellow;
                    m_bRecord2 = false;
                }
            }
            return;
        }

        public void video_record3()
        {
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            sVideoFileName = @"D:\DCIM\vedio\摄像机4_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".mp4";

            if (m_bRecord3 == false)
            {
                //强制I帧 Make a I frame
                int lChannel = Int16.Parse(textBoxChannel3.Text); //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID3, lChannel);

                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle3, sVideoFileName))
                {
                    iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                    str3 = "NET_DVR_SaveRealData failed, error code= " + iLastErr3;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    // btnRecord.Text = "前摄像机停止录像";
                    //btnRecoder .BackColor = Color.SpringGreen;
                    lbRecorder.ForeColor = Color.Chartreuse;
                    m_bRecord3 = true;
                }
            }
            else
            {
                //停止录像 Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle3))
                {
                    iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                    str3 = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr3;
                    // MessageBox.Show(str);
                    lbTishi.Text = "录像失败";
                    tflag = 1;
                    return;
                }
                else
                {
                    str3 = "Successful to stop recording and the saved file is " + sVideoFileName;
                    //MessageBox.Show(str);
                    lbTishi.Text = "录像成功";
                    tflag = 1;
                    //btnRecord.Text = "前摄像机开始录像";
                    // btnRecoder .BackColor = Color.SlateGray;
                    lbRecorder.ForeColor = Color.Yellow;
                    m_bRecord3 = false;
                }
            }
            return;
        }


        //手柄操作说明
        private void 手柄操作说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "30kg级ROV手柄操作手册 - V2.0.docx";
            startInfo.Arguments = @"." + "\\";
            Process.Start(startInfo);
        }

       
        //系统参数辨识窗体
        private void 系统辨识参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemFrom systemform = SystemFrom.GetForm();//获取网络设置窗体

            systemform.Show(); //显示窗体
            systemform.Activate();//激活窗体获取焦点
        }

      

      

       
        //手柄连接
        private void btnShoubing_Click(object sender, EventArgs e)
        {
            if (_joystick_V.IsConnected && _joystick_V.IsCapture)
            {
                JoystickDispose();
                //  joySticksBtn.Text = "手柄未开启";
                // joySticksBtn.BackColor = Color.WhiteSmoke;
               // btnShoubing .BackColor = Color.SlateGray;
                lbshoubing.ForeColor  = Color.Yellow ;
            }
            else
            {
                JoystickInit();
                if (_joystick_V.IsCapture)
                    //joySticksBtn.Text = "手柄已开启";
                    //joySticksBtn.BackColor = Color.OrangeRed;
                  //  btnShoubing .BackColor = Color.SpringGreen;
                lbshoubing.ForeColor  = Color.Chartreuse;
            }
        }

        int time = 0;
        int tflag = 0;

   

        //拍照
        private void btnPhoto_Click(object sender, EventArgs e)
        {
            take_photo();
            take_photo1();
            take_photo2();
            take_photo3();
        }

        public void take_photo()
        {
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = @"D:\DCIM\photo\JPEG1_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";

            int lChannel = Int16.Parse(textBoxChannel.Text); //通道号 Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                //str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照失败";
                tflag = 1;
                return;
            }
            else
            {
                str = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照成功";
                tflag = 1;
            }
            return;
        }

        public void take_photo1()
        {
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = @"D:\DCIM\photo\JPEG2_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";

            int lChannel = Int16.Parse(textBoxChannel1.Text); //通道号 Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID1, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr1 = CHCNetSDK.NET_DVR_GetLastError();
                //str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照失败";
                tflag = 1;
                return;
            }
            else
            {
                str1 = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照成功";
                tflag = 1;
            }
            return;
        }

        public void take_photo2()
        {
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = @"D:\DCIM\photo\JPEG3_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";

            int lChannel = Int16.Parse(textBoxChannel2.Text); //通道号 Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID2, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr2 = CHCNetSDK.NET_DVR_GetLastError();
                //str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照失败";
                tflag = 1;
                return;
            }
            else
            {
                str2 = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照成功";
                tflag = 1;
            }
            return;
        }

        public void take_photo3()
        {
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = @"D:\DCIM\photo\JPEG4_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";

            int lChannel = Int16.Parse(textBoxChannel3.Text); //通道号 Channel number

            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档

            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID3, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr3 = CHCNetSDK.NET_DVR_GetLastError();
                //str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照失败";
                tflag = 1;
                return;
            }
            else
            {
                str3 = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                //MessageBox.Show(str);
                lbTishi.Text = "拍照成功";
                tflag = 1;
            }
            return;
        }

        #endregion

        #region 窗体缩放
        class CtlZooming : Attribute //属性类，用于帮助控制窗体缩放
        {
            #region 私有成员变量
            public float X { get; set; }
            public float Y { get; set; }
            #endregion

            #region 关于控件缩放的一些函数
            public void SetXY(float width, float height) //设置x y的值
            {
                X = width;
                Y = height;
            }
            public void InitTag(Control ctls) //设置标签值
            {
                //遍历窗体中的控件
                foreach (Control ctl in ctls.Controls)
                {
                    ctl.Tag = ctl.Width + ":" + ctl.Height + ":" + ctl.Left + ":" + ctl.Top + ":" + ctl.Font.Size; //标签
                    if (ctl.Controls.Count > 0) //递归问题，层序分析
                        InitTag(ctl);
                }
            }
            public void Resize(float zoomX, float zoomY, Control ctls) //窗体大小改变时的回调函数的辅助函数
            {
                foreach (Control ctl in ctls.Controls)
                {
                    if (null != ctl.Tag)
                    {
                        string[] myTag = ctl.Tag.ToString().Split(new char[] { ':' }); //获取控件的Tag属性值
                        float carry = Convert.ToSingle(myTag[0]) * zoomX;
                        ctl.Width = (int)carry; //设置宽度
                        carry = Convert.ToSingle(myTag[1]) * zoomY;
                        ctl.Height = (int)(carry);//设置高度
                        carry = Convert.ToSingle(myTag[2]) * zoomX;
                        ctl.Left = (int)(carry); //设置左边距
                        carry = Convert.ToSingle(myTag[3]) * zoomY;
                        ctl.Top = (int)(carry); //设置上边距
                        float currentSize = Convert.ToSingle(myTag[4]) * zoomY;
                        ctl.Font = new Font(ctl.Font.Name, currentSize, ctl.Font.Style, ctl.Font.Unit); //设置字号
                        if (ctl.Controls.Count > 0)
                        {
                            Resize(zoomX, zoomY, ctl);
                        }
                    }
                }
            }
            #endregion
        }
        private CtlZooming _ctlZooming = new CtlZooming();
        private void main_Load(object sender, EventArgs e)
        {
            _ctlZooming.SetXY(Width, Height); //设置宽度和高度初始值
            _ctlZooming.InitTag(this); //调用初始化标签值的函数
            //skinEngine1.SkinFile = Application.StartupPath + @"\Skins\WaveColor1.ssk"; //调用皮肤
        }


        private void main_Resize(object sender, EventArgs e)
        {
            _ctlZooming.Resize(Width / _ctlZooming.X, Height / _ctlZooming.Y, this);//随窗体改变控件大小
        }



        #endregion


        int Flag_Wnd = 0;
        int Flag_Wnd1 = 0;
        int Flag_Wnd2 = 0;
        int Flag_Wnd3 = 0;

        private void RealPlayWnd_Click(object sender, EventArgs e)
        {
            RealPlayWnd.BringToFront();
            if (Flag_Wnd==0)
            {
                RealPlayWnd.Size = RealPlayWnd.Size + RealPlayWnd.Size;
                Flag_Wnd = 1;
            }
            else
            {
                //RealPlayWnd.Location = Wnd;
                RealPlayWnd.Size = RealPlayWnd1.Size;
                Flag_Wnd = 0;
            }    
        }

        private void RealPlayWnd1_Click(object sender, EventArgs e)
        {
            RealPlayWnd1.BringToFront();
            if (Flag_Wnd1 == 0)
            {
                RealPlayWnd1.Location = RealPlayWnd.Location;
                RealPlayWnd1.Size = RealPlayWnd1.Size + RealPlayWnd1.Size;
                Flag_Wnd1 = 1;
            }
            else
            {
                RealPlayWnd1.Location = new Point(RealPlayWnd.Size.Width, RealPlayWnd.Location.Y);
                RealPlayWnd1.Size = RealPlayWnd.Size;
                Flag_Wnd1 = 0;
            }
        }

        private void RealPlayWnd2_Click(object sender, EventArgs e)
        {
            RealPlayWnd2.BringToFront();
            if (Flag_Wnd2 == 0)
            {
                RealPlayWnd2.Location = RealPlayWnd.Location;
                RealPlayWnd2.Size = RealPlayWnd2.Size + RealPlayWnd2.Size;
                Flag_Wnd2 = 1;
            }
            else
            {
                RealPlayWnd2.Location = new Point(RealPlayWnd.Location.X, RealPlayWnd.Location.Y + RealPlayWnd.Size.Height);
                RealPlayWnd2.Size = RealPlayWnd.Size;
                Flag_Wnd2 = 0;
            }
        }

        private void RealPlayWnd3_Click(object sender, EventArgs e)
        {
            RealPlayWnd3.BringToFront();
            if (Flag_Wnd3 == 0)
            {
                RealPlayWnd3.Location = RealPlayWnd.Location;
                RealPlayWnd3.Size = RealPlayWnd3.Size + RealPlayWnd3.Size;
                Flag_Wnd3 = 1;
            }
            else
            {
                RealPlayWnd3.Location = new Point(RealPlayWnd.Size.Width, RealPlayWnd.Location.Y +  RealPlayWnd.Size.Height);
                RealPlayWnd3.Size = RealPlayWnd.Size;
                Flag_Wnd3 = 0;
            }
        }
    }
}

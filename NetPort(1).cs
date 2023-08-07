using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using UnderwaterROV;
using UnderIceRov.FunctionFolder;

namespace UnderwaterROV
{
    class NetPort
    {
        //private DataCheck dataCheck;
        private static NetPort netPort;
        private static readonly object _Lock = new object();
        private DataCheck check = new DataCheck();

        public static bool isConnect;//判断是否连接成功以确定是否弹出主窗体
        public delegate void AnalysisDlg();//委托
        FromRovData myRovData = FromRovData.GetFromRovData();
        DataHandle dataHandle;
        /// <summary>
        /// 窗口单例设计模式 if双重锁方法，返回窗口对象，其他名字为对象的引用，只有一个实现
        /// </summary>
        /// <returns></returns>
        
   
        public static NetPort getNetPort()
        {
            if (netPort == null)
            {
                lock (_Lock)
                {
                    if (netPort == null)
                    {
                        netPort = new NetPort();
                       
                    }
                }
            }

            return netPort;
        }
        private string hostIpAdress; //远程主机IP
        private int hostPort;  //远程主机端口号
        public  bool isNetOpen = false;

        private TcpClient tcp = null;
        private NetworkStream workStream = null;

        //public string IPdizhi = "192.168.1.1";
        //public int duankou = 2001;

        private ManualResetEvent connectDone = new ManualResetEvent(false);

        public string HostIpAdress { get => hostIpAdress; set => hostIpAdress = value; }

        public int HostPort { get => hostPort; set => hostPort = value; }
        public bool  Connect(string hostip,int portnum)  //网口连接
        {
            bool a = false;
            try
            {
                if ((tcp == null) || (!tcp.Connected))
                {
                    try
                    {
                        tcp = new TcpClient();
                        tcp.ReceiveTimeout = 10;
                        connectDone.Reset();
                        
                        tcp.BeginConnect(hostip, portnum, new AsyncCallback(ConnectCallback), tcp);
                        connectDone.WaitOne();
                        if ((tcp != null) && (tcp.Connected))
                        {
                            Debug.WriteLine(1);
                            workStream = tcp.GetStream();
                          
                            asyncread(tcp);
                            Debug.WriteLine(2);

                            a = true;
                        }
                    }
                    catch (Exception se)
                    {
                        
                    }
                }
            }
            catch
            { }
            return a;
        }

        public void DisConnect() // 网口断开
        {
            if ((tcp != null) && (tcp.Connected))
            {
                workStream.Close();
                isNetOpen = false;
                tcp.Close();
                Debug.WriteLine("Tcp断开");
            }
        }

        private void asyncread(TcpClient sock)
        {
            StateObject state = new StateObject();
            state.client = sock;
            NetworkStream stream = sock.GetStream();

            if (stream.CanRead)
            {
                try
                {
                    IAsyncResult ar = stream.BeginRead(state.buffer, 0, StateObject.BufferSize,
                            new AsyncCallback(TCPReadCallBack), state);
                    Debug.WriteLine(" 读数 ");
                }
                catch (Exception e)
                {
                    isNetOpen = false;
                    Debug.WriteLine("Network IO problem " + e.ToString());
                }
            }
        }

        private Queue<byte> q_inputDate = new Queue<byte>();//数据筛选处理对列
        public static byte[] temp = new byte[64];

        private void TCPReadCallBack(IAsyncResult ar)
        {
            byte[] readtcpdata = new byte[255];
            StateObject state = (StateObject)ar.AsyncState;
            //主动断开时
            Debug.WriteLine("主动断开");
            if ((state.client == null) || (!state.client.Connected))
                return;
            try
            {
                int numberOfBytesRead;
                NetworkStream mas = state.client.GetStream();
                if (dataHandle == null)
                {
                    dataHandle = DataHandle.getDataHandle();
                }
                numberOfBytesRead = mas.EndRead(ar);

                state.totalBytesRead += numberOfBytesRead;


                if (numberOfBytesRead > 0)
                {
                    byte[] dd = new byte[numberOfBytesRead];

                    mas.BeginRead(state.buffer, 0, StateObject.BufferSize,
                            new AsyncCallback(TCPReadCallBack), state);

                    Array.Copy(state.buffer, 0, dd, 0, numberOfBytesRead);
                    OnGetData(dd, numberOfBytesRead);
                    dataHandle.DataAnalysis();
                    // AnalysisDlg ttc = new AnalysisDlg( dataHandle.DataAnalysis());
                    // ttc();
                    for (int j = 0; j < numberOfBytesRead; j++)  //1 缓存数据
                    {
                        try
                        {
                            q_inputDate.Enqueue(dd[j]);
                        }
                        catch
                        {

                        }
                        
                    }

                    while (q_inputDate.Count > 64)//数据匹配
                    {
                        byte tempbyte = q_inputDate.Dequeue();
                        if (tempbyte == 0xEB)
                        {
                            tempbyte = q_inputDate.Dequeue();
                            if (tempbyte == 0x90)
                            {
                                temp[0] = 0xEB;
                                temp[1] = 0x90;
                                for (int k = 2; k < 64; k++)
                                {
                                    temp[k] = q_inputDate.Dequeue();
                                }
                                if (temp[62] == 0x0d && temp[63] == 0x0a)
                                {
                                    check .RsCrc (temp, 60, out byte crcH, out byte crcL);
                                    if (temp[61] == crcL && temp[60] == crcH)
                                    {
                                        myRovData.RovDataLength = temp[2];
                                        myRovData.RovStateH = temp[3];
                                        myRovData.RovStateL = temp[4];
                                        myRovData.RovRunningFeedbak = temp[5];
                                        myRovData.RovHeading = (short)(temp[6] * 256 + temp[7]);
                                        myRovData.RovRoll = (short)(temp[8] * 256 + temp[9]);
                                        myRovData.RovPithc = (short)(temp[10] * 256 + temp[11]);
                                        myRovData.RovWaterTemperature = (short)(temp[12] * 256 + temp[13]);
                                        myRovData.RovTankTemperature = (short)(temp[14] * 256 + temp[15]);
                                        myRovData.RovDepth = (short)(temp[16] * 256 + temp[17]);
                                        myRovData.RovHumidity = temp[18];
                                        myRovData.CameraServoAngle = (sbyte)temp[19];
                                        myRovData.FrontLightNum = temp[20];
                                        myRovData.BackLightNum = temp[21];
                                        myRovData.PowerWatt = (ushort)(temp[22] * 256 + temp[23]);
                                        myRovData.PidFeedback = temp[42];
                                        myRovData.PnumH = temp[43];
                                        myRovData.PnumL = temp[44];
                                        myRovData.InumH = temp[45];
                                        myRovData.InumL = temp[46];
                                        myRovData.DnumH = temp[47];
                                        myRovData.DnumL = temp[48];
                                        DataToROV.Dangwei = temp[49];
                                        myRovData.SystemFeedback = temp[50];
                                        myRovData.Rovfrequent = temp[51];
                                        myRovData.Rovamplitude = temp[52];
                                        myRovData.RovCrcH = temp[60];
                                        myRovData.RovCrcL = temp[61];

                                    }
                                }
                            }
                        }
                    }


                }
                else
                {
                    //被动断开时
                    
                    mas.Close();
                    state.client.Close();
                    //SetText("Bytes read ------ " + numberOfBytesRead.ToString());
                    System.Windows.Forms.MessageBox.Show("网络被动断开，远程服务器断开，请检查远程服务器网络");
                    Debug.WriteLine("不读了");
                    isNetOpen = false;
                    mas = null;
                    state = null;

                    // setBtnStatus();
                }
            }

            catch(System.IO.IOException)
            {
                Debug.WriteLine("网络突然断开");
                
                state.client.Close();
                //SetText("Bytes read ------ " + numberOfBytesRead.ToString());
                Debug.WriteLine("不读了");
                isNetOpen = false;
                
                state = null;
                System.Windows.Forms.MessageBox.Show("远程服务器突然断开，请检查网络状况、关闭网络接连后重新连接");
            }
            
        }

        private byte[] buf = new byte[200];
        public  static byte[] buf1 = new byte[200];
       // private int PerCount = 10; //一帧大小
        Queue<byte> q_input = new Queue<byte>();
        public byte[] Getbuf                      //获取数据
        {
            get { return buf; }
        }

        /// <summary>
        /// 使用Tcpip 发送数据，发送35个字节
        /// </summary>
        /// <param name="a"></param>
        public void NetSendData(byte[]a)
        {
            
            if ((tcp == null) || (!tcp.Connected))
                return;
           
            //校验
            //byte crcL = 0, crcH = 0;
            
            //dataCheck.RsCrc(a,100, out crcH, out crcL);


            workStream.Write(a, 0, a.Length);
        }

       
        
        public static void OnGetData(byte[] data, int len) //数据接收
        {

            try
            {
                
                for (int i = 0; i < 53; i++)
                {
                    buf1[i] = data[i];
                }
                            
            }
            catch
            {
            }
        }
        

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            connectDone.Set();
            TcpClient t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    Debug.WriteLine ("连接成功");
                   
                    t.EndConnect(ar);
                    Debug.WriteLine("连接线程完成");
                    isConnect = true;
                    //System.Windows.Forms.MessageBox.Show("连接成功,连接线程完成！");
                }
                else
                {
                    Debug.WriteLine("连接失败");
                    System.Windows.Forms.MessageBox.Show("连接失败，请检查后重新尝试！");
                    t.EndConnect(ar);
                }
            }
            catch (SocketException se)
            {
                System.Windows.Forms.MessageBox.Show("连接发生错误ConnCallBack......"+ se.Message);
                Debug.WriteLine("连接发生错误ConnCallBack.......:" + se.Message);
            }
        }


       
     
      
    }

   
    internal class StateObject
    {
        public TcpClient client = null;
        public int totalBytesRead = 0;
        public const int BufferSize = 255;
        public string readType = null;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder messageBuffer = new StringBuilder();

    }
}

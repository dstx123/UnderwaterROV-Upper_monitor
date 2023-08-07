using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnderwaterROV;

namespace UnderwaterROV
{
    class DataHandle
    {
        public static string receivedata = "";
        public static string name = "receivedata" + DateTime.Now.ToString("yyyyMMddhhmmss");
        fmMain  form = new fmMain(); 
        //存储
        public static StringBuilder sb = new StringBuilder();
        public delegate int AddHandler(StringBuilder sbb);
        AddHandler handler;
      

        
        //异步调用
        public delegate void convertDlg(double a,double b);//委托
        convertDlg myAsyncDlg;//构造一个委托

        //private PictureBox pictureBox;
      
        static DataHandle dataHandle;
        private static readonly object _Lock = new object();

       

        //public DataHandle(PictureBox pic)
        //{
        //    pictureBox = pic;
        //}
        public static DataHandle getDataHandle()
        {
            
            if (dataHandle == null)
            {
                lock (_Lock)
                {
                    if (dataHandle == null)
                    {
                        dataHandle = new DataHandle();

                    }
                }
            }

            return dataHandle;
        }
       

        public void DataAnalysis()
        {            
            receivedata = DateTime.Now.ToString("yyyyMMddhhmmss:");
            receivedata += BitConverter.ToString(NetPort .buf1);
           
            receivedata += "\r\n";
                     
           // handler = new AddHandler(savefile.saveFile);
        
            //////IAsyncResult: 异步操作接口(interface)
            //////BeginInvoke: 委托(delegate)的一个异步方法的开始
           // IAsyncResult result = handler.BeginInvoke(sb, null, null);//异步调用，用来存储文件
            if (NetPort .buf1 [0]==0xeb&&NetPort .buf1 [1]==0x90)
            {
                
            }

          

       

          
        }

        

     

        
    }
}

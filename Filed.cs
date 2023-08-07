using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using UnderwaterROV;

namespace UnderwaterROV
{
   
    class Filed
    {

        
        /// <summary>
        ///将要写入的字符串写入指定位置
        /// </summary>
        /// <param name="s"></param>想要写入的字符串
        /// <param name="write"></param>是否写入，true 写入，false, 无操作
        /// <param name="path"></param>传入需要存储的txt地址
        public static void SaveStingToFile(string s, string path)
        {
            
                StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
            
                sw.WriteLine (s);
                sw.Close();
        

        }
        /// <summary>
        /// 读取文件目录下的数据，数据返回参数为读取的字符串
        /// </summary>
        /// <param name="s"></param>字符串形参
        /// <param name="path"></param>地址
        /// <returns></returns>
        //int countt;
        //public delegate void AnalysisDl();//委托
        public string ReadStringToFile(string s, string path)
        {
            StreamReader sw = new StreamReader(path);
            
            s = sw.ReadLine();
                         
                       
            sw.Close();


            return s;
            
        }
       
        /// <summary>
        /// stringbuilder数据存够一定大小再写入文件中，文件大小大于1M新建一个文件
        /// </summary>
        /// <param name="sbb"></param>
        /// <returns></returns>
        public static int SaveFile1(StringBuilder sb)
        {

            //string s = sb.AppendFormat("{0} {1}", "Skyer", "Chen").Remove(3, 2).Replace(' ', '-').ToString();
            sb.Append(fmMain .senddata);
            if (sb.Length > 104 * 300)
            {
                FileInfo fi = new FileInfo(@"D:\ROV data" + "\\" + fmMain.name1 + ".txt");
                Filed.SaveStingToFile(sb.ToString(), @"D:\ROV data" + "\\" + fmMain.name1 + ".txt");
               // FileInfo fi = new FileInfo(@"C:\ROV data" + "\\"+ fmMain.name1 + ".txt");
                fmMain. sbb.Remove(0, fmMain.sbb.Length);
                Int64 size = fi.Length; // 文件字节大小

                if (size > 405600)
                {
                    fmMain.name1 = "senddata" + DateTime.Now.ToString("yyyyMMddhhmmss");
                }
            }
            return sb.Length;
        }
        public static void SaveFile1Close(StringBuilder sbb)
        {
            sbb.Append(fmMain.senddata);
            Filed.SaveStingToFile(sbb.ToString(), @"D:\ROV data" + "\\" + fmMain.name1 + ".txt");
        }
        #region 存文件
        /// <summary>
        /// 存文件
        /// </summary>
        /// <param name="sbb"></param>
        /// <returns></returns>
        
        public static int SaveFile(StringBuilder sbb)
        {

            //string s = sb.AppendFormat("{0} {1}", "Skyer", "Chen").Remove(3, 2).Replace(' ', '-').ToString();
            sbb.Append(fmMain .receivedata);
            if (sbb.Length > 260 * 300)
            {
                Filed.SaveStingToFile(sbb.ToString(), @"D:\ROV data" + "\\" + fmMain.name + ".txt");
                FileInfo fi = new FileInfo(@"D:\ROV data" + "\\" + fmMain .name + ".txt");
                
              
                sbb.Remove(0, sbb.Length);
                Int64 size = fi.Length; // 文件字节大小
                if (size > 1014000)
                {

                    fmMain .name = "receivedata" + DateTime.Now.ToString("yyyyMMddhhmmss");

                }
                

            }
            return sbb.Length;
        }

        public static void SaveFileClose(StringBuilder sbb)
        {
            sbb.Append(fmMain .receivedata);
            Filed.SaveStingToFile(sbb.ToString(), @"D:\ROV data" + "\\" + fmMain .name + ".txt");
        }
        #endregion
    }
}

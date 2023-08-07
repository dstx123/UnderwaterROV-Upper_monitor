using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderwaterROV
{
    class DataToROV
    {
        private static DataToROV myDataToROV;
        private static readonly object _Lock = new object();

        private static byte _pidChangeCmd = 0b0;
        private static byte _pidInquireCmd = 0b0;
        private static byte   _pIntNum = 0;
        private static byte   _pDecimalNum = 0;

        private static byte   _iIntNum = 0;
        private static byte   _iDecimalNum = 0;

        private static byte _dIntNum = 0;
        private static byte _dDecimalNum = 0;

        private static byte _autoControlCmd = 0b0;

        private static short depthSetingNum = 0;
        private static short directSetingNum = 0;
        private static short pitchSetingNum = 0;
        private static short rollSetingNum = 0;
        private static byte _systemChangeCmd = 0b0;
        private static byte _systemInquireCmd = 0b0;
        private static byte _systemControlCmd = 0b0;
        private static byte frequent = 0;
        private static byte amplitude = 0;
        private static byte dangwei = 0;

        /// <summary>
        /// 上位机修改PID参数标志  0b0000   bit2：修改姿态pid  bit1:定向pid  bit0:定深pid 
        /// </summary>
        public static byte PidChangeCmd { get => _pidChangeCmd; set => _pidChangeCmd = value; }
        /// <summary>
        /// 上位机查询PID参数  0b0000   bit2:定姿态pithc  bit1:定向  bit0:定深 
        /// </summary>
        public static byte PidInquireCmd { get => _pidInquireCmd; set => _pidInquireCmd = value; }
        public static byte PIntNum { get => _pIntNum; set => _pIntNum = value; }
        public static byte PDecimalNum { get => _pDecimalNum; set => _pDecimalNum = value; }
        public static byte IIntNum { get => _iIntNum; set => _iIntNum = value; }
        public static byte IDecimalNum { get => _iDecimalNum; set => _iDecimalNum = value; }
        public static byte DIntNum { get => _dIntNum; set => _dIntNum = value; }
        public static byte DDecimalNum { get => _dDecimalNum; set => _dDecimalNum = value; }
        /// <summary>
        ///   bit2:定姿态使能 pitch   bit1:定向使能  bit0:定深使能 
        /// </summary>
        public static byte AutoControlCmd { get => _autoControlCmd; set => _autoControlCmd = value; }
        public static short DepthSetingNum { get => depthSetingNum; set => depthSetingNum = value; }
        public static short DirectSetingNum { get => directSetingNum; set => directSetingNum = value; }
        public static short PitchSetingNum { get => pitchSetingNum; set => pitchSetingNum = value; }
        public static short RollSetingNum { get => rollSetingNum; set => rollSetingNum = value; }
        public static byte SystemChangeCmd { get => _systemChangeCmd; set => _systemChangeCmd = value; }
        public static byte SystemInquireCmd { get => _systemInquireCmd; set => _systemInquireCmd = value; }
        public static byte SystemControlCmd { get => _systemControlCmd; set => _systemControlCmd = value; }
        public static byte Frequent { get => frequent; set => frequent = value; }
        public static byte Amplitude { get => amplitude; set => amplitude = value; }
        public static byte Dangwei { get => dangwei; set => dangwei = value; }


        /// <summary>
        /// 单例设计模式 if双重锁方法，返回窗口对象，其他名字为对象的引用，只有一个实现
        /// </summary>
        /// <returns></returns>
        public static DataToROV GetDataToROV()
        {
            if (myDataToROV == null)
            {
                lock (_Lock)
                {
                    if (myDataToROV == null)
                    {
                        myDataToROV = new DataToROV();
                       
                    }
                }
            }

            return myDataToROV;
        }

    }
}

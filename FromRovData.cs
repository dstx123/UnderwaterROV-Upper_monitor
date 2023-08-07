using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderwaterROV
{
    class FromRovData
    {
        private static FromRovData myRovData;
        private static readonly object _Lock = new object();

        public static FromRovData GetFromRovData()
        {
            if(myRovData == null)
            {
                lock(_Lock)
                {
                    if(myRovData == null)
                    {
                        myRovData = new FromRovData();
                    }
                }
            }

            return myRovData;
        }

        private byte rovDataLength;
        private byte rovStateH;
        private byte rovStateL;
        private byte rovRunningFeedbak;
        private Int16 rovHeading;
        private Int16 rovRoll;
        private Int16 rovPithc;
        private Int16 rovWaterTemperature;
        private Int16 rovTankTemperature;
        private Int16 rovDepth;
        private byte rovHumidity;
        private sbyte cameraServoAngle;
        private byte frontLightNum;
        private byte backLightNum;
       private UInt16 powerWatt;
        private byte pidFeedback;
        private byte pnumH;
        private byte pnumL;
        private byte inumH;
        private byte inumL;
        private byte dnumH;
        private byte dnumL;
        private byte rovCrcH;
        private byte rovCrcL;
        private byte systemFeedback;
        private byte rovfrequent;
        private byte rovamplitude;

        public byte RovDataLength { get => rovDataLength; set => rovDataLength = value; }
        public byte RovStateH { get => rovStateH; set => rovStateH = value; }
        public byte RovStateL { get => rovStateL; set => rovStateL = value; }
        public byte RovRunningFeedbak { get => rovRunningFeedbak; set => rovRunningFeedbak = value; }
        public short RovHeading { get => rovHeading; set => rovHeading = value; }
        public short RovRoll { get => rovRoll; set => rovRoll = value; }
        public short RovPithc { get => rovPithc; set => rovPithc = value; }
        public short RovWaterTemperature { get => rovWaterTemperature; set => rovWaterTemperature = value; }
        public short RovTankTemperature { get => rovTankTemperature; set => rovTankTemperature = value; }
        public short RovDepth { get => rovDepth; set => rovDepth = value; }
        public byte RovHumidity { get => rovHumidity; set => rovHumidity = value; }
        public sbyte CameraServoAngle { get => cameraServoAngle; set => cameraServoAngle = value; }
        public byte FrontLightNum { get => frontLightNum; set => frontLightNum = value; }
        public byte BackLightNum { get => backLightNum; set => backLightNum = value; }
        public ushort PowerWatt { get => powerWatt; set => powerWatt = value; }
        public byte PidFeedback { get => pidFeedback; set => pidFeedback = value; }
        public byte PnumH { get => pnumH; set => pnumH = value; }
        public byte PnumL { get => pnumL; set => pnumL = value; }
        public byte InumH { get => inumH; set => inumH = value; }
        public byte InumL { get => inumL; set => inumL = value; }
        public byte DnumH { get => dnumH; set => dnumH = value; }
        public byte DnumL { get => dnumL; set => dnumL = value; }
        public byte RovCrcH { get => rovCrcH; set => rovCrcH = value; }
        public byte RovCrcL { get => rovCrcL; set => rovCrcL = value; }
        public byte SystemFeedback { get => systemFeedback; set => systemFeedback = value; }
        public byte Rovfrequent { get => rovfrequent; set => rovfrequent = value; }
        public byte Rovamplitude { get => rovamplitude; set => rovamplitude = value; }

        public override string ToString()
        {
            if(CameraServoAngle>=0)
            {
                return "上扬" + CameraServoAngle.ToString();
            }
            else
            return "下垂" + CameraServoAngle.ToString();
        }
    }
}

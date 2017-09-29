using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RDTools.IDCard
{
    public class ReadCard_App
    {
        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int InitComm(int iPort);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseComm();

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int Authenticate();

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int Read_Content(int iActive);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleName(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleSex(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleNation(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleBirthday(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleAddress(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPeopleIDCode(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetStartDate(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetEndDate(Byte[] buf, int iLen);

        [DllImport("termb.DLL", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetPhotoBMP(Byte[] buf, int iLen);

    }
}

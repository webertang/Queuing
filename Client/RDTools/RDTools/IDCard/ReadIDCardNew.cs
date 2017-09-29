using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace RDTools.IDCard
{
    public class ReadIDCardNew
    {
        public ReadIDCardNew()
        {
            int i = ReadCard_App.InitComm(1001);
            i = ReadCard_App.Authenticate();
            i = ReadCard_App.Read_Content(1);
        }
        public string Name
        {
            get
            {
                return DllImportVal(new Func<Byte[], int, int>(ReadCard_App.GetPeopleName));
            }
        }

        public string Sex
        {
            get
            {
                return DllImportVal(new Func<Byte[], int, int>(ReadCard_App.GetPeopleSex));
            }
        }

        public string Nation
        {
            get
            {
                return DllImportVal(new Func<Byte[], int, int>(ReadCard_App.GetPeopleNation));
            }
        }

        public string Birthday
        {
            get
            {
                string _Birthday = DllImportVal(new Func<Byte[], int, int>(ReadCard_App.GetPeopleBirthday));
                if (_Birthday == "")
                    return "";
                return _Birthday.Substring(0, 4) + "-" + _Birthday.Substring(4, 2) + "-" + _Birthday.Substring(6, 2);
            }
        }

        public string Address
        {
            get
            {
                return DllImportVal(new Func<Byte[], int, int>(ReadCard_App.GetPeopleAddress));
            }
        }

        public string IDCode
        {
            get
            {
                return DllImportVal(new Func<Byte[], int, int>(ReadCard_App.GetPeopleIDCode));
            }
        }

        public Image PhotoBMP
        {
            get
            {
                Byte[] asciiBytes = new Byte[100 * 1024];
                int i = ReadCard_App.GetPhotoBMP(asciiBytes, 100 * 1024);
                if (i > 0)
                {
                    MemoryStream ms = new MemoryStream(asciiBytes);
                    return Image.FromStream(ms);
                }
                else
                {
                    return null;
                }
            }
        }

        private string DllImportVal(Func<Byte[], int, int> _ReadCard_App)
        {
            Byte[] asciiBytes = new Byte[100];

            int dwRet = _ReadCard_App(asciiBytes, 100);
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            char[] asciiChars = new char[gb2312.GetCharCount(asciiBytes, 0, dwRet)];
            gb2312.GetChars(asciiBytes, 0, dwRet, asciiChars, 0);
            return new string(asciiChars);
        }
    }
}

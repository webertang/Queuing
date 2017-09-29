using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateXML
{
    public class ICDBase
    {
        protected string _preNo = string.Empty;
        protected string _iCDCode = string.Empty;
        protected string _iCDName = string.Empty;

        /// <summary>
        /// 就诊唯一号
        /// </summary>
        public string PreNo
        {
            get { return _preNo; }
            set { _preNo = value ?? string.Empty; }
        }

        /// <summary>
        /// ICD代码
        /// </summary>
        public string ICDCode
        {
            get { return _iCDCode; }
            set { _iCDCode = value ?? string.Empty; }
        }

        /// <summary>
        /// ICD名称
        /// </summary>
        public string ICDName
        {
            get { return _iCDName; }
            set { _iCDName = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return string.Format("<ICDInfo PreNo=\"{0}\" ICDCode=\"{1}\" ICDName=\"{2}\"/>", _preNo, _iCDCode, _iCDName);
        }
         
    }
}

using System;

namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public abstract class DoctorInfoBase
    {
        protected string _job_number = string.Empty;
        protected string _date = string.Empty;

        /// <summary>
        /// 工号
        /// </summary>
        public string Job_number
        {
            get { return _job_number; }
            set { _job_number = value ?? string.Empty; }
        }

        public abstract DateTime? Date
        {
            get;
            set;
        }

        public string ConvertFunction()
        {
            return string.Format("<doctor_information job_number='{0}' date='{1}' />", _job_number, _date);
        }
    }
}

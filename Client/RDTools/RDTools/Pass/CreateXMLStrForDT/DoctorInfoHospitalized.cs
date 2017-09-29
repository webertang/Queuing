using System;

namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class DoctorInfoHospitalized : DoctorInfoBase
    {
        public DoctorInfoHospitalized()
        {
            Date = null;
        }

        /// <summary>
        /// 系统时间
        /// </summary>
        public override DateTime? Date
        {
            get
            {
                return Convert.ToDateTime(_date);
            }
            set
            {
                _date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}

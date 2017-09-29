using System;

/// <summary>
/// 大通pass创建xml
/// </summary>
namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 门诊医生信息
    /// </summary>
    public class DoctorInfoOutpatient : DoctorInfoBase
    {
        /// <summary>
        /// 处方日期
        /// </summary>
        public override DateTime? Date
        {
            get 
            {
                if (string.IsNullOrEmpty(_date))
                {
                    return null;
                }

                return Convert.ToDateTime(_date); 
            }
            set 
            {
                _date = value.HasValue ? _date = value.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            }
        }
    }
}

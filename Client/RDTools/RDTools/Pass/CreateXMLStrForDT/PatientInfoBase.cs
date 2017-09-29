using System;
using System.Collections.Generic;

namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public abstract class PatientInfoBase
    {
        protected string _weight = string.Empty;
        protected string _height = string.Empty;
        protected string _birth = string.Empty;
        protected string _patient_name = string.Empty;
        protected string _patient_sex = string.Empty;
        protected string _physiological_statms = string.Empty;
        protected string _boacterioscopy_effect = string.Empty;
        protected string _bloodpressure = string.Empty;
        protected string _liver_clean = string.Empty;

        /// <summary>
        /// 体重
        /// </summary>
        public int? Weight
        {
            get
            {
                if (string.IsNullOrEmpty(_weight))
                {
                    return null;
                }

                return int.Parse(_weight);
            }
            set
            {
                _weight = value.HasValue ? value.Value.ToString() : string.Empty;
            }
        }

        /// <summary>
        /// 身高
        /// </summary>
        public int? Height
        {
            get
            {
                if (string.IsNullOrEmpty(_height))
                {
                    return null;
                }

                return int.Parse(_height);
            }
            set
            {
                _height = value.HasValue ? value.Value.ToString() : string.Empty;
            }
        }

        /// <summary>
        /// 出生年月日
        /// </summary>
        public DateTime? Birth
        {
            get
            {
                if (string.IsNullOrEmpty(_birth))
                {
                    return null;
                }

                return Convert.ToDateTime(_birth);
            }
            set
            {
                _birth = value.HasValue ? value.Value.ToString("yyyy-MM-dd") : string.Empty;
            }
        }

        /// <summary>
        /// 病人名
        /// </summary>
        public string Patient_name
        {
            get { return _patient_name; }
            set { _patient_name = value ?? string.Empty; }
        }

        /// <summary>
        /// 病人性别
        /// </summary>
        public string Patient_sex
        {
            get { return _patient_sex; }
            set { _patient_sex = value ?? string.Empty; }
        }

        /// <summary>
        /// 生理情况
        /// </summary>
        public string Physiological_statms
        {
            get { return _physiological_statms; }
            set { _physiological_statms = value ?? string.Empty; }
        }

        /// <summary>
        /// 菌检结果
        /// </summary>
        public string Boacterioscopy_effect
        {
            get { return _boacterioscopy_effect; }
            set { _boacterioscopy_effect = value ?? string.Empty; }
        }

        /// <summary>
        /// 血压
        /// </summary>
        public string Bloodpressure
        {
            get { return _bloodpressure; }
            set { _bloodpressure = value ?? string.Empty; }
        }

        /// <summary>
        /// 肌酐清除率
        /// </summary>
        public string Liver_clean
        {
            get { return _liver_clean; }
            set { _liver_clean = value ?? string.Empty; }
        }

        /// <summary>
        /// 过敏史
        /// </summary>
        public List<AllergicHistory> Allergic_historyList { get; set; }

        public abstract string ConvertFunction();
    }
}

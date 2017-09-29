namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public abstract class SafeBase
    {
        protected string _doctor_name = string.Empty;
        protected string _doctor_type = string.Empty;
        protected string _department_code = string.Empty;
        protected string _department_name = string.Empty;
        protected string _case_id = string.Empty;
        protected string _inhos_code = string.Empty;
        protected string _bed_no = string.Empty;

        /// <summary>
        /// 医生名
        /// </summary>
        public string Doctor_name
        {
            get { return _doctor_name; }
            set { _doctor_name = value ?? string.Empty; }
        }

        /// <summary>
        /// 医生级别代码
        /// </summary>
        public string Doctor_type
        {
            get { return _doctor_type; }
            set { _doctor_type = value ?? string.Empty; }
        }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string Department_code
        {
            get { return _department_code; }
            set { _department_code = value ?? string.Empty; }
        }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string Department_name
        {
            get { return _department_name; }
            set { _department_name = value ?? string.Empty; }
        }

        /// <summary>
        /// 病历卡号
        /// </summary>
        public string Case_id
        {
            get { return _case_id; }
            set { _case_id = value ?? string.Empty; }
        }

        /// <summary>
        /// 门诊就诊号
        /// </summary>
        public string Inhos_code
        {
            get { return _inhos_code; }
            set { _inhos_code = value ?? string.Empty; }
        }

        /// <summary>
        /// 床号
        /// </summary>
        public string Bed_no
        {
            get { return _bed_no; }
            set { _bed_no = value ?? string.Empty; }
        }

        public abstract string ConvertFunction();
    }
}

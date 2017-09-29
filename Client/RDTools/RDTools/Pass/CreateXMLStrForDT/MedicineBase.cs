namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public abstract class MedicineBase
    {
        protected string _suspension = "false";
        protected string _judge = "true";
        protected string _group_number = string.Empty;
        protected string _general_name = string.Empty;
        protected string _license_number = string.Empty;
        protected string _medicine_name = string.Empty;
        protected string _single_dose = string.Empty;
        protected string _coef = "1";
        protected string _times = string.Empty;
        protected string _unit = string.Empty;
        protected string _administer_drugs = string.Empty;

        /// <summary>
        /// 组号
        /// </summary>
        public string Group_number
        {
            get { return _group_number; }
            set { _group_number = value ?? string.Empty; }
        }

        /// <summary>
        /// 通用名
        /// </summary>
        public string General_name
        {
            get { return _general_name; }
            set { _general_name = value.Replace('<', '(').Replace('>', ')') ?? string.Empty; }
        }

        /// <summary>
        /// 医院药品代码
        /// </summary>
        public string License_number
        {
            get { return _license_number; }
            set { _license_number = value ?? string.Empty; }
        }

        /// <summary>
        /// 商品名
        /// </summary>
        public string Medicine_name
        {
            get { return _medicine_name; }
            set { _medicine_name = value.Replace('<', '(').Replace('>', ')') ?? string.Empty; }
        }

        /// <summary>
        /// 单次量
        /// </summary>
        public decimal? Single_dose
        {
            get
            {
                if (string.IsNullOrEmpty(_single_dose))
                {
                    return null;
                }

                return decimal.Parse(_single_dose);
            }
            set
            {
                _single_dose = value.HasValue ? value.Value.ToString() : string.Empty;
            }
        }

        /// <summary>
        /// 频次代码
        /// </summary>
        public string Times
        {
            get { return _times; }
            set { _times = value ?? string.Empty; }
        }

        /// <summary>
        /// 单位（mg,g等）
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value ?? string.Empty; }
        }

        /// <summary>
        /// 用药途径
        /// </summary>
        public string Administer_drugs
        {
            get { return _administer_drugs; }
            set { _administer_drugs = value ?? string.Empty; }
        }

        public abstract string ConvertFunction();
    }
}

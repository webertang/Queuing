namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class DiagnoseHospitalized
    {
        private string _diagnose = string.Empty;
        private string _type = string.Empty;
        private string _name = string.Empty;

        /// <summary>
        /// 诊断代码
        /// </summary>
        public string Diagnose
        {
            get { return _diagnose; }
            set { _diagnose = value ?? string.Empty; }
        }

        /// <summary>
        /// 类型（0—诊断，1—病生理）
        /// </summary>
        public string Type
        {
            get { return _type; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    _type = string.Empty;
                }
                else if (value != "0")
                {
                    _type = "1";
                }
                else
                {
                    _type = value;
                }
            }
        }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return string.Format("<diagnose type='{0}' name='{1}' >{2}</diagnose>", _type, _name, _diagnose);
        }
    }
}

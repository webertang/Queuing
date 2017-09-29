/// <summary>
/// 大通pass创建xml
/// </summary>
namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 过敏史
    /// </summary>
    public class AllergicHistory
    {
        private string _case_code = string.Empty;
        private string _case_name = string.Empty;

        /// <summary>
        /// 过敏源代码，HIS药品代码需要在前面加上Y
        /// </summary>
        public string CaseCode
        {
            get { return _case_code; }
            set { _case_code = value ?? string.Empty; }
        }

        /// <summary>
        /// 过敏源名称
        /// </summary>
        public string CaseName
        {
            get { return _case_name; }
            set { _case_name = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return "<case><case_code>" + _case_code + "</case_code><case_name>" + _case_name + "</case_name></case>";
        }
    }
}

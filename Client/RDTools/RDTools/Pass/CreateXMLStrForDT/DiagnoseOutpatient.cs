namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class DiagnoseOutpatient
    {
        private string _diagnoseDiagnosis = string.Empty;
        private string _diagnosePhysiological = string.Empty;

        /// <summary>
        /// 诊断的ICD10集合
        /// </summary>
        public string DiagnoseDiagnosis 
        {
            get { return _diagnoseDiagnosis; }
            set { _diagnoseDiagnosis = value ?? string.Empty; }
        }

        /// <summary>
        /// 相应诊断的病生理情况集合
        /// </summary>
        public string DiagnosePhysiological
        {
            get { return _diagnosePhysiological; }
            set { _diagnosePhysiological = value ?? string.Empty; }
        }

        public string[] ConvertFunction()
        {
            return new string[]{
                "<diagnose>" + _diagnoseDiagnosis + "</diagnose>",
                "<diagnose>" + _diagnosePhysiological + "</diagnose>"};
        }
    }
}

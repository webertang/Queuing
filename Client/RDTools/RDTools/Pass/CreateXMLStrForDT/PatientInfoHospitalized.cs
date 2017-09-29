using System.Collections.Generic;

/// <summary>
/// 大通pass创建xml
/// </summary>
namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 住院病人信息
    /// </summary>
    public class PatientInfoHospitalized : PatientInfoBase
    {
        public PatientInfoHospitalized()
        {
            Allergic_historyList = new List<AllergicHistory>();
            DiagnoseHospitalizedList = new List<DiagnoseHospitalized>();
        }

        public List<DiagnoseHospitalized> DiagnoseHospitalizedList { get; set; }

        private string _pregnant = string.Empty;
        private string _pdw = string.Empty;

        /// <summary>
        /// 孕妇怀孕时间
        /// </summary>
        public string Pregnant
        {
            get { return _pregnant; }
            set { _pregnant = value ?? string.Empty; }
        }

        /// <summary>
        /// 怀孕时间计量单位
        /// </summary>
        public string Pdw
        {
            get { return _pdw; }
            set { _pdw = value ?? string.Empty; }
        }

        public override string ConvertFunction()
        {
            string xml = null;

            string patient_name = string.Format("<patient_name>{0}</patient_name>", _patient_name);
            string patient_sex = string.Format("<patient_sex>{0}</patient_sex>", _patient_sex);
            string physiological_statms = string.Format("<physiological_statms>{0}</physiological_statms>", _physiological_statms);
            string boacterioscopy_effect = string.Format("<boacterioscopy_effect>{0}</boacterioscopy_effect>", _boacterioscopy_effect);
            string bloodpressure = string.Format("<bloodpressure>{0}</bloodpressure>", _bloodpressure);
            string liver_clean = string.Format("<liver_clean>{0}</liver_clean>", _liver_clean);
            string pregnant = string.Format("<pregnant>{0}</pregnant>", _pregnant);
            string pdw = string.Format("<pdw>{0}</pdw>", _pdw);

            string allergic_history = "<allergic_history>";

            while (Allergic_historyList.Count < 3)
            {
                Allergic_historyList.Add(new AllergicHistory());
            }

            foreach (AllergicHistory allergicHistory in Allergic_historyList)
            {
                allergic_history += allergicHistory.ConvertFunction();
            }

            allergic_history += "</allergic_history>";

            string diagnoses = "<diagnoses>";

            while (DiagnoseHospitalizedList.Count < 3)
            {
                DiagnoseHospitalizedList.Add(new DiagnoseHospitalized());
            }

            string diagnose = string.Empty;

            for (int i = 0; i < 3;i++)
            {
                diagnose += DiagnoseHospitalizedList[i].ConvertFunction();
            }

            diagnoses += diagnose + "</diagnoses>";

            string context = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", patient_name, patient_sex, physiological_statms, boacterioscopy_effect, bloodpressure, liver_clean, pregnant, pdw,allergic_history, diagnoses);

            xml = string.Format("<patient_information weight='{0}' height='{1}' birth='{2}'>{3}</patient_information>", _weight, _height, _birth, context);

            return xml;
        }
    }
}

using System.Collections.Generic;

/// <summary>
/// 大通pass创建xml
/// </summary>
namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 门诊病人信息
    /// </summary>
    public class PatientInfoOutpatient : PatientInfoBase
    {
        public PatientInfoOutpatient()
        {
            Allergic_historyList = new List<AllergicHistory>();
            DiagnoseOutpatientList = new List<DiagnoseOutpatient>();
        }

        public List<DiagnoseOutpatient> DiagnoseOutpatientList { get; set; }

        public override string ConvertFunction()
        {
            string xml = null;

            string patient_name = string.Format("<patient_name>{0}</patient_name>", _patient_name);
            string patient_sex = string.Format("<patient_sex>{0}</patient_sex>", _patient_sex);
            string physiological_statms = string.Format("<physiological_statms>{0}</physiological_statms>", _physiological_statms);
            string boacterioscopy_effect = string.Format("<boacterioscopy_effect>{0}</boacterioscopy_effect>", _boacterioscopy_effect);
            string bloodpressure = string.Format("<bloodpressure>{0}</bloodpressure>", _bloodpressure);
            string liver_clean = string.Format("<liver_clean>{0}</liver_clean>", _liver_clean);

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

            while (DiagnoseOutpatientList.Count < 3)
            {
                DiagnoseOutpatientList.Add(new DiagnoseOutpatient());
            }

            string diagnoseDiagnosis = string.Empty;
            string diagnosePhysiological = string.Empty;

            foreach (DiagnoseOutpatient diagnoseOutpatient in DiagnoseOutpatientList)
            {
                string[] tmp = diagnoseOutpatient.ConvertFunction();
                diagnoseDiagnosis += tmp[0];
                diagnosePhysiological += tmp[1];
            }

            diagnoses += diagnoseDiagnosis + diagnosePhysiological + "</diagnoses>";

            string context = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", patient_name, patient_sex, physiological_statms, boacterioscopy_effect, bloodpressure, liver_clean, allergic_history, diagnoses);

            xml = string.Format("<patient_information weight='{0}' height='{1}' birth='{2}'>{3}</patient_information>", _weight, _height, _birth, context);

            return xml;
        }
    }
}

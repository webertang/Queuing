using System.Collections.Generic;

/// <summary>
/// 大通pass创建xml
/// </summary>
namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 门诊
    /// </summary>
    public class SafeOutpatient : SafeBase
    {
        public SafeOutpatient()
        {
            DoctorInfo = new DoctorInfoOutpatient();
            PatientInfo = new PatientInfoOutpatient();
            PrescriptionList = new List<PrescriptionOutpatient>();
        }

        public DoctorInfoOutpatient DoctorInfo { get; set; }
        public PatientInfoOutpatient PatientInfo { get; set; }
        public List<PrescriptionOutpatient> PrescriptionList { get; set; }

        public override string ConvertFunction()
        {
            string prescriptions = string.Empty;

            foreach (PrescriptionOutpatient prescription in PrescriptionList)
            {
                prescriptions += prescription.ConvertFunction();
            }

            return string.Format("<safe>{0}<doctor_name>{1}</doctor_name><doctor_type>{2}</doctor_type><department_code>{3}</department_code>"
                + "<department_name>{4}</department_name><case_id>{5}</case_id><inhos_code>{6}</inhos_code><bed_no>{7}</bed_no>{8}<prescriptions>{9}</prescriptions></safe>",
                DoctorInfo.ConvertFunction(), _doctor_name, _doctor_type, _department_code, _department_name, _case_id, _inhos_code, _bed_no, PatientInfo.ConvertFunction(), prescriptions);
        }
    }
}

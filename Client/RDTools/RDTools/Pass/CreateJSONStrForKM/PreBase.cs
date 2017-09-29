using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateJSONStrForKM
{
    #region Memo
    //hospitalId	医院id
    //clientId	His客户端编码
    //clientKey	His客户端访问加密key  
    //presCode	处方编码
    //presType	处方类型
    //outPatientType	门诊类型
    //deptCode	开单科室Id
    //deptName	开单科室
    //outDoctorCode	开单医生Id
    //outDoctor	开单医生
    //patientCode	病人ID
    //patientName	病人姓名
    //sex	病人性别
    //height	病人身高
    //weight	病人体重
    //phyState	生理状态
    //allergy	过敏史
    //medCareId	病人社保卡号
    //birthday	病人出生日期
    //presDate	处方日期
    //expense	病人费别
    ////isWorkInjury	是否工伤
    //diagnose	处方诊断
    //isSpecial	特管处方标识
    //isSlowDiagnose	是否慢病处方
    //isSkinTest	是否皮试
    ////drugs	处方中成药和西药品列表
    ////items	诊疗项目列表
    ////cmDrugs	处方中草药列表
    ////herbalDecoct	草药煎法
    ////herbalDosage	草药服法
    //type	审查类型
    //funcConf	功能启用配置
    ////reviewDispConf	所需审核规则
    ////tipInfo	提示语
    #endregion
    public class PreBase
    {
        private string hospitalId = string.Empty;
        private string clientId = string.Empty;
        private string clientKey = string.Empty;
        private string presCode = string.Empty;
        private string presType = "0";
        private string outPatientType = string.Empty;
        private string deptCode = string.Empty;
        private string deptName = string.Empty;
        private string outDoctorCode = string.Empty;
        private string outDoctor = string.Empty;
        private string patientCode = string.Empty;
        private string patientName = string.Empty;
        private string sex = "0";
        private string height = string.Empty;
        private string weight = string.Empty;
        private string phyState = string.Empty;
        private string allergy = string.Empty;
        private string medCareId = string.Empty;
        private string birthday = string.Empty;
        private string presDate = string.Empty;
        private string expense = string.Empty;
        private string diagnose = string.Empty;
        private string isSpecial = "0";
        private string isSlowDiagnose = "0";
        private string isSkinTest = "0";
        private string type = string.Empty;
        private string funcConf = string.Empty;

        public string HospitalId
        {
            get { return hospitalId; }
            set { hospitalId = value ?? string.Empty; }
        }

        public string ClientId
        {
            get { return clientId; }
            set { clientId = value ?? string.Empty; }
        }

        public string ClientKey
        {
            get { return clientKey; }
            set { clientKey = value ?? string.Empty; }
        }

        public string PresCode
        {
            get { return presCode; }
            set { presCode = value ?? string.Empty; }
        }

        public string PresType
        {
            get { return presType; }
            set { presType = value ?? "0"; }
        }

        public string OutPatientType
        {
            get { return outPatientType; }
            set { outPatientType = value ?? string.Empty; }
        }

        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value ?? string.Empty; }
        }

        public string DeptName
        {
            get { return deptName; }
            set { deptName = value ?? string.Empty; }
        }

        public string OutDoctorCode
        {
            get { return outDoctorCode; }
            set { outDoctorCode = value ?? string.Empty; }
        }

        public string OutDoctor
        {
            get { return outDoctor; }
            set { outDoctor = value ?? string.Empty; }
        }

        public string PatientCode
        {
            get { return patientCode; }
            set { patientCode = value ?? string.Empty; }
        }

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value ?? string.Empty; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value ?? "0"; }
        }

        public string Height
        {
            get { return height; }
            set { height = value ?? string.Empty; }
        }

        public string Weight
        {
            get { return weight; }
            set { weight = value ?? string.Empty; }
        }

        public string PhyState
        {
            get { return phyState; }
            set { phyState = value ?? string.Empty; }
        }

        public string Allergy
        {
            get { return allergy; }
            set { allergy = value ?? string.Empty; }
        }

        public string MedCareId
        {
            get { return medCareId; }
            set { medCareId = value ?? string.Empty; }
        }

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value ?? string.Empty; }
        }

        public string PresDate
        {
            get { return presDate; }
            set { presDate = value ?? string.Empty; }
        }

        public string Expense
        {
            get { return expense; }
            set { expense = value ?? string.Empty; }
        }

        public string Diagnose
        {
            get { return diagnose; }
            set { diagnose = value ?? string.Empty; }
        }

        public string IsSpecial
        {
            get { return isSpecial; }
            set { isSpecial = value ?? "0"; }
        }

        public string IsSlowDiagnose
        {
            get { return isSlowDiagnose; }
            set { isSlowDiagnose = value ?? "0"; }
        }

        public string IsSkinTest
        {
            get { return isSkinTest; }
            set { isSkinTest = value ?? "0"; }
        }

        public string Type
        {
            get { return type; }
            set { type = value ?? string.Empty; }
        }

        public string FuncConf
        {
            get { return funcConf; }
            set { funcConf = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return string.Format("\"hospitalId\":\"{0}\",\"clientId\":\"{1}\",\"clientKey\":\"{2}\"," +
                     "\"presCode\":\"{3}\",\"presType\":\"{4}\",\"outPatientType\":\"{5}\",\"deptCode\":\"{6}\"," +
                     "\"deptName\":\"{7}\",\"outDoctorCode\":\"{8}\",\"outDoctor\":\"{9}\",\"patientCode\":\"{10}\"," +
                     "\"patientName\":\"{11}\",\"sex\":\"{12}\",\"medCareId\":\"{13}\",\"presDate\":\"{14}\"," +
                     "\"birthday\":\"{15}\",\"expense\":\"{16}\",\"height\":\"{17}\",\"weight\":\"{18}\","+
                     "\"diagnose\":\"{19}\",\"phyState\":\"{20}\",\"allergy\":\"{21}\",\"isSpecial\":\"{22}\","+
                     "\"isSlowDiagnose\":\"{23}\",\"isSkinTest\":\"{24}\",\"type\":\"{25}\",\"funcConf\":[],", hospitalId,
                     clientId, clientKey, presCode, presType, outPatientType, deptCode, deptName, outDoctorCode, outDoctor,
                     patientCode, patientName, sex, medCareId, presDate, birthday, expense, height, weight, diagnose, phyState,
                     allergy, isSpecial, isSlowDiagnose, isSkinTest, type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateXML
{
    public class PreBase
    {
        protected string _preNo = string.Empty;
        protected string _preCode = string.Empty;
        protected string _preType = string.Empty;
        protected string _inDate = string.Empty;
        protected string _outDate = string.Empty;
        protected string _doctCode = string.Empty;
        protected string _deptCode = string.Empty;
        protected string _patientName = string.Empty;
        protected string _patientType = string.Empty;
        protected string _birthday = string.Empty;
        protected string _gender = string.Empty;
        protected string _liverStatus = string.Empty;
        protected string _kidneyStatus = string.Empty;
        protected string _womanStatus = string.Empty;
        protected string _allegeInfo = string.Empty;
        protected string _blCode = string.Empty;
        protected string _branch = string.Empty;

        /// <summary>
        /// 就诊唯一号
        /// </summary>
        public string PreNo
        {
            get { return _preNo; }
            set { _preNo = value ?? string.Empty; }
        }

        /// <summary>
        /// 处方号
        /// </summary>
        public string PreCode
        {
            get { return _preCode; }
            set { _preCode = value ?? string.Empty; }
        }

        /// <summary>
        /// 1为门诊，2为住院
        /// </summary>
        public string PreType
        {
            get { return _preType; }
            set { _preType = value ?? string.Empty; }
        }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public string InDate
        {
            get { return _inDate; }
            set { _inDate = value ?? string.Empty; }
        }

        /// <summary>
        /// 出院时间
        /// </summary>
        public string OutDate
        {
            get { return _outDate; }
            set { _outDate = value ?? string.Empty; }
        }

        /// <summary>
        /// 医生代码
        /// </summary>
        public string DoctCode
        {
            get { return _doctCode; }
            set { _doctCode = value ?? string.Empty; }
        }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value ?? string.Empty; }
        }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value ?? string.Empty; }
        }

        /// <summary>
        /// 病人类别
        /// </summary>
        public string PatientType
        {
            get { return _patientType; }
            set { _patientType = value ?? string.Empty; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value ?? string.Empty; }
        }

        /// <summary>
        /// 病人性别
        /// </summary>
        public string Gender
        {
            get { return _gender; }
            set { _gender = value ?? string.Empty; }
        }

        /// <summary>
        /// 肝功能状况
        /// </summary>
        public string LiverStatus
        {
            get { return _liverStatus; }
            set { _liverStatus = value ?? string.Empty; }
        }

        /// <summary>
        /// 肾功能状况
        /// </summary>
        public string KidneyStatus
        {
            get { return _kidneyStatus; }
            set { _kidneyStatus = value ?? string.Empty; }
        }

        /// <summary>
        /// 妊娠/哺乳
        /// </summary>
        public string WomanStatus
        {
            get { return _womanStatus; }
            set { _womanStatus = value ?? string.Empty; }
        }

        /// <summary>
        /// 过敏源代码
        /// </summary>
        public string AllegeInfo
        {
            get { return _allegeInfo; }
            set { _allegeInfo = value ?? string.Empty; }
        }

        /// <summary>
        /// 病历号
        /// </summary>
        public string BlCode
        {
            get { return _blCode; }
            set { _blCode = value ?? string.Empty; }
        }

        /// <summary>
        /// 医院分院
        /// </summary>
        public string Branch
        {
            get { return _branch; }
            set { _branch = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return string.Format("<Pre><PreInfo PreNo=\"{0}\" PreCode=\"{1}\" PreType=\"{2}\"" +
                                " InDate=\"{3}\" OutDate=\"{4}\" DoctCode=\"{5}\" DeptCode=\"{6}\"" +
                                " PatientName=\"{7}\" PatientType=\"{8}\" Birthday=\"{9}\" Gender=\"{10}\"" +
                                " LiverStatus=\"{11}\"  KidneyStatus=\"{12}\" WomanStatus=\"{13}\"" +
                                " AllegeInfo=\"{14}\" BlCode=\"{15}\" Branch=\"{16}\"/></Pre>",
                                _preNo, _preCode, _preType, _inDate, _outDate, _doctCode, _deptCode,
                                _patientName, _patientType, _birthday, _gender, _liverStatus,
                                _kidneyStatus, _womanStatus, _allegeInfo, _blCode, _branch);
        }
     
        
        #region 备注
//        0	PreNo	就诊唯一号
//(医嘱唯一号)	如果是门诊：在同一家医院内应保证不重复。一般可采用挂号时HIS产生的就诊流水号，若医院内此类流水号会被循环使用，无法确切保证长远唯一，则可在传入的流水号时右首拼入日期（格式YYYYMMDD）即可。
//大部分情况就诊唯一号就等于门诊挂号，但是在某些医院允许一个病人在一天内看同一个科的病只需要挂一个号，因此这种情况下就诊唯一号不等于门诊挂号，应该是等于门诊挂号+处方号（如果还不能保证本次就诊唯一，再加上时间）
//如果是住院：这里的医嘱唯一号是住院病人住院时开医嘱的唯一号码，若医院内此类号码会被循环使用，无法确切保证长远唯一，则可在传入的医嘱号时右首拼入日期（格式YYYYMMDD）即可
//1	PreCode	处方号	如果门诊就是处方号；如果是住院就是住院号
//2	PreType	区分门诊住院	当次分析的类别，1为门诊，2为住院
//3	InDate　	就诊时间	格式：YYYY-MM-DD hh:mm:ss(24h)住院医嘱的入院时间，门诊就是就诊日期
//4	OutDate　	出院时间	格式：YYYY-MM-DD hh:mm:ss(24h)住院医嘱的出院时间，门诊就是就诊日期
//5	DoctCode	医生代码	医生在HIS系统中的医生代码
//6	DeptCode	科室代码	医生在HIS系统中的科室代码
//7	PatientName	病人姓名	
//8	PatientType	病人类别	如自费，医保，绿色通道等
//9	Birthday	出生日期	格式：YYYY-MM-DD
//10	Gender	病人性别	0-男 1-女
//11	LiverStatus	肝功能状况	0-肝功能不全;2-严重肝功能不全
//12	KidneyStatus	肾功能状况	0-肾功能不全;2-严重肾功能不全
//13	WomanStatus	妊娠/哺乳	1-妊娠期;0-哺乳期
//14	AllegeInfo	过敏源代码	过敏源代码，如果有多个，用“；”隔开串起来。
//15	BlCode	病历号	或者叫病案号，是病人唯一标识
//16	Branch	医院分院	区分医院的分院标识(1,2,3表示)，默认填99表示全院

        #endregion
    }
}

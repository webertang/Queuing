using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateXML
{
    public class DrugBase
    {
        protected string _preNo = string.Empty;
        protected string _OrderCode = string.Empty;
        protected string _OrderType = string.Empty;
        protected string _OrderDate = string.Empty;
        protected string _OrderDoctor = string.Empty;
        protected string _IsCurrent = string.Empty;
        protected string _DrugCode = string.Empty;
        protected string _DrugName = string.Empty;
        protected string _DrugSpec = string.Empty;
        protected string _UsingType = string.Empty;
        protected string _Frequency = string.Empty;
        protected string _FreqTimes = string.Empty;
        protected string _Dcl = string.Empty;
        protected string _DclUnit = string.Empty;
        protected string _Qnty = string.Empty;
        protected string _QntyUnit = string.Empty;
        protected string _Price = string.Empty;
        protected string _GroupNo = string.Empty;
        protected string _BeginUseDate = string.Empty;
        protected string _EndUseDate = string.Empty;

        /// <summary>
        /// 就诊唯一号
        /// </summary>
        public string PreNo 
        {
            get { return _preNo; }
            set { _preNo = value ?? string.Empty; }
        }

        /// <summary>
        /// 医嘱唯一号
        /// </summary>
        public string OrderCode
        {
            get { return _OrderCode; }
            set { _OrderCode = value ?? string.Empty; }
        }

        /// <summary>
        /// 医嘱类型
        /// </summary>
        public string OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value ?? string.Empty; }
        }

        /// <summary>
        /// 开医嘱日期
        /// </summary>
        public string OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value ?? string.Empty; }
        }

        /// <summary>
        /// 医嘱医生代码
        /// </summary>
        public string OrderDoctor
        {
            get { return _OrderDoctor; }
            set { _OrderDoctor = value ?? string.Empty; }
        }

        /// <summary>
        /// 是否当前处方/医嘱
        /// </summary>
        public string IsCurrent
        {
            get { return _IsCurrent; }
            set { _IsCurrent = value ?? string.Empty; }
        }

        /// <summary>
        /// 药品唯一编码
        /// </summary>
        public string DrugCode
        {
            get { return _DrugCode; }
            set { _DrugCode = value ?? string.Empty; }
        }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName
        {
            get { return _DrugName; }
            set { _DrugName = value.Replace('<', '(').Replace('>', ')') ?? string.Empty; }
        }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string DrugSpec
        {
            get { return _DrugSpec; }
            set { _DrugSpec = value ?? string.Empty; }
        }

        /// <summary>
        /// 给药途径
        /// </summary>
        public string UsingType
        {
            get { return _UsingType; }
            set { _UsingType = value ?? string.Empty; }
        }

        /// <summary>
        /// 给药频次
        /// </summary>
        public string Frequency
        {
            get { return _Frequency; }
            set { _Frequency = value ?? string.Empty; }
        }

        /// <summary>
        /// 一天给药的次数
        /// </summary>
        public string FreqTimes
        {
            get { return _FreqTimes; }
            set { _FreqTimes = value ?? string.Empty; }
        }

        /// <summary>
        /// 单次剂量
        /// </summary>
        public string Dcl
        {
            get { return _Dcl; }
            set { _Dcl = value ?? string.Empty; }
        }

        /// <summary>
        /// 单次剂量单位
        /// </summary>
        public string DclUnit
        {
            get { return _DclUnit; }
            set { _DclUnit = value ?? string.Empty; }
        }

        /// <summary>
        /// 药品数量
        /// </summary>
        public string Qnty
        {
            get { return _Qnty; }
            set { _Qnty = value ?? string.Empty; }
        }

        /// <summary>
        /// 药品数量单位
        /// </summary>
        public string QntyUnit
        {
            get { return _QntyUnit; }
            set { _QntyUnit = value ?? string.Empty; }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public string Price
        {
            get { return _Price; }
            set { _Price = value ?? string.Empty; }
        }

        /// <summary>
        /// 组号
        /// </summary>
        public string GroupNo
        {
            get { return _GroupNo; }
            set { _GroupNo = value ?? string.Empty; }
        }

        /// <summary>
        /// 用药开始时间
        /// </summary>
        public string BeginUseDate
        {
            get { return _BeginUseDate; }
            set { _BeginUseDate = value ?? string.Empty; }
        }

        /// <summary>
        /// 用药结束时间
        /// </summary>
        public string EndUseDate
        {
            get { return _EndUseDate; }
            set { _EndUseDate = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return string.Format("<DrugInfo PreNo=\"{0}\" OrderCode=\"{1}\" OrderType=\"{2}\"" +
              " OrderDate=\"{3}\" OrderDoctor=\"{4}\" IsCurrent=\"{5}\" DrugCode=\"{6}\"" +
              " DrugName=\"{7}\" DrugSpec=\"{8}\" UsingType=\"{9}\" Frequency=\"{10}\"" +
              " FreqTimes=\"{11}\" Dcl=\"{12}\" DclUnit=\"{13}\" Qnty=\"{14}\" QntyUnit=\"{15}\"" +
              " Price=\"{16}\" GroupNo=\"{17}\" BeginUseDate=\"{18}\" EndUseDate=\"{19}\"/>",
              _preNo, _OrderCode, _OrderType, _OrderDate, _OrderDoctor, _IsCurrent, _DrugCode,
              _DrugName, _DrugSpec, _UsingType, _Frequency, _FreqTimes, _Dcl, _DclUnit, _Qnty,
              _QntyUnit, _Price, _GroupNo, _BeginUseDate, _EndUseDate);
        }

     

        #region 备注
//0	PreNo	同就诊唯一号	同Pre节点项目PreInfo
//1	OrderCode	医嘱唯一号	
//2	OrderType	医嘱类型	0为临时医嘱，1为长期医嘱。门诊时这里使用默认值0
//3	OrderDate	开医嘱日期	格式：YYYY-MM-DD hh:mm:ss(24h)
//4	OrderDoctor	医嘱医生代码	下嘱医生的代码
//5	IsCurrent	是否当前处方/医嘱	0当前处方；1历史处方。
//当前处方就是这个医生当前正在开的处方，有可能当前这个病人以前已经开过药（可能是其他科室），某些医院有要求N天之内同一个病人的药需要放在一起分析，这个时候就需要传历史的处方信息给我们，历史处方的IsCurrent= 1。历史处方本身的问题我们是不进行分析的，我们只分析历史处方中的药与当前处方中的药同时作用发生的问题
//6	DrugCode	药品唯一编码	如果HIS没有唯一码，可以把药品信息的几个字段并接组成
//7	DrugName	药品名称	
//8	DrugSpec	药品规格	
//9	UsingType　	给药途径	如：口服、静脉注射
//10	Frequency	给药频次	如qd、bid、tid、2/日等
//11	FreqTimes	一天给药的次数	指一日给药的次数,例如1日2次，就是2。如果小于1/日，都是1，例如2日1次，是1；3日1次也是1。
//12	Dcl	单次剂量	指一次给药的剂量
//13	DclUnit	单次剂量单位	一次给药的剂量单位,如：mg、g、ml
//14	Qnty	药品数量	处方开药的数量
//15	QntyUnit	药品数量单位	数量单位，例如：盒、瓶、袋等
//16	Price	单价	药品的单价，按数量单位算
//17	GroupNo	组号	相同“组号”是指在同一容器中的注射液，组号相同我们才进行配伍问题的判断。如果片剂之类没有组号。
//18	BeginUseDate	用药开始时间	格式：YYYY-MM-DD hh:mm:ss(24h)
//19	EndUseDate	用药结束时间	格式：YYYY-MM-DD hh:mm:ss(24h)

        #endregion
    }
}

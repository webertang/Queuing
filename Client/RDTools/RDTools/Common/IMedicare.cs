using System;
using System.Collections;
using System.Data;

namespace RDTools.MedicareInterface
{
	/// <summary>
	/// IMedicare 的摘要说明。
	/// 医保接口 继承打折的方法 
	/// </summary>
	public interface IMedicare
	{
		//初始化
		int InitHisDLL(ArrayList PrenoList);

		//释放
		int FreeHisDLL(ArrayList PrenoList);

		//读卡
		//patientType 1 
		int ReadCard(int TranType ,string PersonID,ref DataSet patientInfoData,ref ArrayList PrenoList);
		
		//挂号登记
		int Register(int TranType ,string PersonID,DataSet registerInfoData,ref ArrayList PrenoList);
				
		//门诊结算
		int ClinicBalance(int TranType,DataSet detailAccountData,DataSet ecipeMedicineData,ref BalanceInfo balanceInfo ,ref ArrayList PrenoList);


		//住院登记
		int InHosRegister(int TranType,int InHosState,string InHosID,int CurrentInHosMark,DataSet inHosRecordData ,DataSet leaveHosRecordData, ref ArrayList PrenoList );


		//住院划价收费
		int InHosCharge(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData ,DataSet inHosChargeDetailData,DataSet inHosLeechdomRecordData,ref  ArrayList PrenoList );

		//住院预交款
		int InHosAdvance(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData,DataSet advanceRecordData ,ref  ArrayList PrenoList);


		//住院转科
		int InHosChangeOffice(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData ,DataSet changeBedData, ref ArrayList PrenoList );

		//住院结算 TranType  0: 只预结算不处理费用    1：his调用传1   
		int InHosBalance(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData,ref BalanceInfo balanceInfo,ref ArrayList PrenoList);
		
		//住院换票
		int ChangeInvoice(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData,ref ArrayList PrenoList );

		//门诊his退费
		int HisRollBack(int ClinicType,int TranType,string PersonID,int CurrentInHosMark,ref ArrayList PrenoList );

		//his普通医保互相转换
		int HosTransformFeeType(int TranType,int InHosState,string PersonID,int CurrentInHosMark,ref ArrayList PrenoList);

		//是否初始化标识
		bool Init
		{
			get;
		}

		//数据库连接
        //IDataAccess Data
        //{
        //    get;
        //    set;
        //}

		//病人类型 0 打折病人 1 医保病人 -1 还未确定
		int PatientType
		{
			get;
		}
		//Sysconfig  sysconfig;

        /// <summary>
        /// 下载数据
        /// </summary>
        /// <param name="itemId">项目类别</param>
        /// <param name="beginTime">项目的更新时间</param>
        /// <returns></returns>
        DataSet DownLoadMedicareData(string itemId, DateTime beginTime);

        /// <summary>
        /// 上报对应关系数据
        /// </summary>
        /// <param name="medicareInfo">上报的数据</param>
        /// <param name="itemId">项目类别</param>
        /// <returns></returns>
        int UpLoadMedicareData(DataSet medicareInfo, string itemId);
	}

    public class PatientInfo
    {
        private string personalID = string.Empty;
        private string name = string.Empty;
        private string sex = string.Empty;
        private string age = string.Empty;
        private DateTime birthday;
        //帐户余额
        private double accountBalance = 0.00;
        //本年度挂号总次数
        private int registerTimes = 0;
        //本年度挂号个人支付总额
        private double registerPaySelf = 0.00;
        //本年度挂号统筹支付总额
        private double registerPayPlan = 0.00;
        //本年度门诊总次数
        private int clinicTimes = 0;
        //本年度门诊个人支付总额
        private double clinicPaySelf = 0.00;
        //本年度门诊统筹支付总额
        private double clinicPayPlan = 0.00;
        //本年度住院总次数
        private int inHosTimes = 0;
        //本年度住院个人支付总额
        private double inHosPaySelf = 0.00;
        //本年度住院统筹支付总额
        private double inHosPayPlan = 0.00;

        public string PersonalID
        {
            get { return personalID; }
            set { personalID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public double AccountBalance
        {
            get { return accountBalance; }
            set { accountBalance = value; }
        }

        public int RegisterTimes
        {
            get { return registerTimes; }
            set { registerTimes = value; }
        }

        public double RegisterPaySelf
        {
            get { return registerPaySelf; }
            set { registerPaySelf = value; }
        }

        public double RegisterPayPlan
        {
            get { return registerPayPlan; }
            set { registerPayPlan = value; }
        }

        public int ClinicTimes
        {
            get { return clinicTimes; }
            set { clinicTimes = value; }
        }

        public double ClinicPaySelf
        {
            get { return clinicPaySelf; }
            set { clinicPaySelf = value; }
        }

        public double ClinicPayPlan
        {
            get { return clinicPayPlan; }
            set { clinicPayPlan = value; }
        }

        public int InHosTimes
        {
            get { return inHosTimes; }
            set { inHosTimes = value; }
        }

        public double InHosPaySelf
        {
            get { return inHosPaySelf; }
            set { inHosPaySelf = value; }
        }

        public double InHosPayPlan
        {
            get { return inHosPayPlan; }
            set { inHosPayPlan = value; }
        }


    }

    public struct BalanceInfo
    {
        //统筹
        public double payPlan;
        //现金
        public double payCash;
        //账户
        public double payAccount;
        //帐户余额
        public double accountBalance;
    }
}

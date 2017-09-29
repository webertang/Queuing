using System;
using System.Collections;
using System.Data;

namespace RDTools.MedicareInterface
{
	/// <summary>
	/// IMedicare ��ժҪ˵����
	/// ҽ���ӿ� �̳д��۵ķ��� 
	/// </summary>
	public interface IMedicare
	{
		//��ʼ��
		int InitHisDLL(ArrayList PrenoList);

		//�ͷ�
		int FreeHisDLL(ArrayList PrenoList);

		//����
		//patientType 1 
		int ReadCard(int TranType ,string PersonID,ref DataSet patientInfoData,ref ArrayList PrenoList);
		
		//�ҺŵǼ�
		int Register(int TranType ,string PersonID,DataSet registerInfoData,ref ArrayList PrenoList);
				
		//�������
		int ClinicBalance(int TranType,DataSet detailAccountData,DataSet ecipeMedicineData,ref BalanceInfo balanceInfo ,ref ArrayList PrenoList);


		//סԺ�Ǽ�
		int InHosRegister(int TranType,int InHosState,string InHosID,int CurrentInHosMark,DataSet inHosRecordData ,DataSet leaveHosRecordData, ref ArrayList PrenoList );


		//סԺ�����շ�
		int InHosCharge(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData ,DataSet inHosChargeDetailData,DataSet inHosLeechdomRecordData,ref  ArrayList PrenoList );

		//סԺԤ����
		int InHosAdvance(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData,DataSet advanceRecordData ,ref  ArrayList PrenoList);


		//סԺת��
		int InHosChangeOffice(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData ,DataSet changeBedData, ref ArrayList PrenoList );

		//סԺ���� TranType  0: ֻԤ���㲻�������    1��his���ô�1   
		int InHosBalance(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData,ref BalanceInfo balanceInfo,ref ArrayList PrenoList);
		
		//סԺ��Ʊ
		int ChangeInvoice(int TranType,int InHosState,string PersonID,int CurrentInHosMark,DataSet inHosRecordData,ref ArrayList PrenoList );

		//����his�˷�
		int HisRollBack(int ClinicType,int TranType,string PersonID,int CurrentInHosMark,ref ArrayList PrenoList );

		//his��ͨҽ������ת��
		int HosTransformFeeType(int TranType,int InHosState,string PersonID,int CurrentInHosMark,ref ArrayList PrenoList);

		//�Ƿ��ʼ����ʶ
		bool Init
		{
			get;
		}

		//���ݿ�����
        //IDataAccess Data
        //{
        //    get;
        //    set;
        //}

		//�������� 0 ���۲��� 1 ҽ������ -1 ��δȷ��
		int PatientType
		{
			get;
		}
		//Sysconfig  sysconfig;

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="itemId">��Ŀ���</param>
        /// <param name="beginTime">��Ŀ�ĸ���ʱ��</param>
        /// <returns></returns>
        DataSet DownLoadMedicareData(string itemId, DateTime beginTime);

        /// <summary>
        /// �ϱ���Ӧ��ϵ����
        /// </summary>
        /// <param name="medicareInfo">�ϱ�������</param>
        /// <param name="itemId">��Ŀ���</param>
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
        //�ʻ����
        private double accountBalance = 0.00;
        //����ȹҺ��ܴ���
        private int registerTimes = 0;
        //����ȹҺŸ���֧���ܶ�
        private double registerPaySelf = 0.00;
        //����ȹҺ�ͳ��֧���ܶ�
        private double registerPayPlan = 0.00;
        //����������ܴ���
        private int clinicTimes = 0;
        //������������֧���ܶ�
        private double clinicPaySelf = 0.00;
        //���������ͳ��֧���ܶ�
        private double clinicPayPlan = 0.00;
        //�����סԺ�ܴ���
        private int inHosTimes = 0;
        //�����סԺ����֧���ܶ�
        private double inHosPaySelf = 0.00;
        //�����סԺͳ��֧���ܶ�
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
        //ͳ��
        public double payPlan;
        //�ֽ�
        public double payCash;
        //�˻�
        public double payAccount;
        //�ʻ����
        public double accountBalance;
    }
}

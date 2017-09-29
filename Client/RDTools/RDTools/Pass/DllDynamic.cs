using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RD.Pass
{	    
	/// <summary>
    /// ������Ҫ�õ���DLL��Դ�ļ�
	/// </summary>
	class DllDynamic
	{
		public DllDynamic()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		[DllImport("kernel32")] 
		public static extern long GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int Size,string filePath); 

		//		[DllImport("user32.dll")] 
		//		public static extern int GetWindowRect(int hwnd,ref str_windowrect lpRect);

		[DllImport("ShellRunAs.dll")] 
		public static extern int RegisterServer(); 
		
		[DllImport("DIFPassDll.dll")] 
		public static extern int PassInit(StringBuilder UserName,StringBuilder DepartMentName,int WorkStationType); 

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassGetState (string QueryItem); 

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetPatientInfo(
			string cPatientID,  //����ID
			string cVisitID,    //סԺ�������������������ϵͳ��Ϊ�ǡ�1��
			string cPatientName,//��������
			string cSex,		//�����Ա�
			string cBirthday,   //���˳������ڣ����봫�����������
			string cWeight,     //����
			string cHeight,		//���
			string cDeptName,	//��������
			string cDoctor,		//ҽ������
			//string cPatientType,//��Ժ����
			string cOutHospitalDate//��Ժ����
			//string cUseDate,	//������ڣ���ʱΪ���죩
			//int cSaveResultFlag,//�ݲ��ã�Ĭ��Ϊ0
			//int iIvorPass//��IV��黹��PASS��飺0:PASS��飬��); 
			);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassDoCommand(int iCommandNo); 

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetRecipeInfo(
			string cOrderUniqueCode , //ҽ��Ψһ��
			string cDrugCode ,        //ҩƷΨһ��
			string cDrugName ,        //ҩƷ����
			string cSingleDose ,      //ÿ�μ���
			string cDoseUnit   ,      //������λ
			string cFrequency  ,      //Ƶ�Σ���ʽҪ��n��m�Σ���"m/n"��
			string cStartDate ,       //��ʼ���ڣ���ʽҪ��"yyyy-mm-dd"��//��������������ڣ�ϵͳ��Ĭ��Ϊ���졣
			string cEndDate   ,       //ͣ�����ڸ�ʽҪ��"yyyy-mm-dd"��//��������������ڣ�ϵͳ��Ĭ��Ϊ���졣
			string cRouteName ,		  //��ҩ;������
			string cGroupTag  ,       //�����ǣ��������ҽ����Ǵ��գ�ϵͳ�Զ�//��"mdc" + �����±���Ϊ��һ��ı��
			string cOrderType ,       // 1 ��ʱҽ�� 0 ��ճ���ҽ�� 
			string cOrderDoctor       //����ҽ��ID\����ҽ������
			);      

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetControlParam(
			int SaveCheckResult,
			int AllowAllegen,
			int CheckMode,
			int DisqMode,
			int UseDiposeIdea
			); 
		
		//		[DllImport("DIFPassDll.dll")] 
		//		public static extern int PassSetAlleyInfo(
		//			string cPatientID ,  //����ID
		//			string cVisitID   ,  //סԺ�������������������ϵͳ��Ϊ�ǡ�1��
		//			string cPatientName, //��������
		//			string cSex,         //�����Ա�
		//			string cBirthday,    //���˳�������
		//			string cWeight ,     //����
		//			string cHeight ,     //���
		//			string cDoctor       //ҽ������
		//			);
			
		//		[DllImport("DIFPassDll.dll")] 
		//		public static extern int PassSetSearchdruginfo(
		//			string cDrugCode ,��  //ҩƷΨһ��
		//			string cDrugName ,��  //ҩƷ����
		//			string cDrugUnit ,����//ҩƷ��ҩ��λ
		//			string cRrouteName ,  //��ҩ;������
		//			int nWarn             //����ֵ
		//			);
		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetAllergenInfo(
			string AllergenIndex,
			string AllergenCode,
			string AllergenDesc,
			string AllergenType,
			string Reaction
			);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetMedCond(
			string MedCondIndex,
			string MedCondCode,
			string MedCondDesc,
			string MedCondType,
			string StartDate,
			string EndDate
			);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetQueryDrug(
			string DrugCode,
			string DrugName,
			string DoseUnit,
			string RouteName
			);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetFloatWinPos(
			int left,
			int top,
			int right,
			int bottome
			);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassGetWarn(string DrugUniqueCode);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetWarnDrug(string DrugUniqueCode);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassQuit();

        /// <summary>
        /// ��ͨdll
        /// </summary>
        /// <param name="nCode">���ܲ��������Ƶ��õĹ���</param>
        /// <param name="lParam">һ������¹̶�Ϊ��0����סԺ���ַ����������ʱ��Ϊ��1��</param>
        /// <param name="lpcszBuffer">�ַ���/����XML</param>
        /// <returns>��0������1������2�����ֱ����û�����⡱����һ�����⡱�͡��������⡱��</returns>
        [DllImport("dtywzxUI.dll")]
        public static extern int dtywzxUI(int _nCode, int _lParam,string _lpcszBuffer);

        #region ����
        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanInit(int Handle, string HospCode);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanInitRec();

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanGetDoctor(string DoctCode);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanPointHint(string DrugCode);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanAnaData(string PreType,string IsSave,string sData);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanGetRec(string PreType,string PreNo);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanCancel(string PreType,string PreNo);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanGetManaul(string DrugCode);

        [DllImport("vanxsd.dll", CharSet = CharSet.Ansi)]
        public static extern int VanExit();

        #endregion
    }
}

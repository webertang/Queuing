using System;
using System.Runtime.InteropServices;
using System.Text;

namespace RD.Pass
{	    
	/// <summary>
    /// 引入需要用到的DLL资源文件
	/// </summary>
	class DllDynamic
	{
		public DllDynamic()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
			string cPatientID,  //病人ID
			string cVisitID,    //住院次数，如果传“”，则系统认为是“1”
			string cPatientName,//病人姓名
			string cSex,		//病人性别
			string cBirthday,   //病人出生日期，必须传，才能审剂量
			string cWeight,     //体重
			string cHeight,		//身高
			string cDeptName,	//科室名称
			string cDoctor,		//医生姓名
			//string cPatientType,//入院日期
			string cOutHospitalDate//出院日期
			//string cUseDate,	//审查日期（空时为当天）
			//int cSaveResultFlag,//暂不用，默认为0
			//int iIvorPass//是IV审查还是PASS审查：0:PASS审查，非); 
			);

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassDoCommand(int iCommandNo); 

		[DllImport("DIFPassDll.dll")] 
		public static extern int PassSetRecipeInfo(
			string cOrderUniqueCode , //医嘱唯一码
			string cDrugCode ,        //药品唯一码
			string cDrugName ,        //药品名称
			string cSingleDose ,      //每次剂量
			string cDoseUnit   ,      //剂量单位
			string cFrequency  ,      //频次，格式要求：n天m次，传"m/n"）
			string cStartDate ,       //开始日期（格式要求"yyyy-mm-dd"）//如果不传开嘱日期，系统则默认为当天。
			string cEndDate   ,       //停嘱日期格式要求"yyyy-mm-dd"）//如果不传开嘱日期，系统则默认为当天。
			string cRouteName ,		  //给药途径名称
			string cGroupTag  ,       //成组标记，如果成组医嘱标记传空，系统自动//传"mdc" + 数组下标作为下一组的标记
			string cOrderType ,       // 1 临时医嘱 0 或空长期医嘱 
			string cOrderDoctor       //下嘱医生ID\下嘱医生姓名
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
		//			string cPatientID ,  //病人ID
		//			string cVisitID   ,  //住院次数，如果传“”，则系统认为是“1”
		//			string cPatientName, //病人姓名
		//			string cSex,         //病人性别
		//			string cBirthday,    //病人出生日期
		//			string cWeight ,     //体重
		//			string cHeight ,     //身高
		//			string cDoctor       //医生姓名
		//			);
			
		//		[DllImport("DIFPassDll.dll")] 
		//		public static extern int PassSetSearchdruginfo(
		//			string cDrugCode ,　  //药品唯一码
		//			string cDrugName ,　  //药品名称
		//			string cDrugUnit ,　　//药品给药单位
		//			string cRrouteName ,  //给药途径编码
		//			int nWarn             //警告值
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
        /// 大通dll
        /// </summary>
        /// <param name="nCode">功能参数，控制调用的功能</param>
        /// <param name="lParam">一般情况下固定为“0”，住院部分分析、保存的时候为“1”</param>
        /// <param name="lpcszBuffer">字符串/参照XML</param>
        /// <returns>“0”、“1”、“2”，分别代表“没有问题”、“一般问题”和“严重问题”。</returns>
        [DllImport("dtywzxUI.dll")]
        public static extern int dtywzxUI(int _nCode, int _lParam,string _lpcszBuffer);

        #region 创泽
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

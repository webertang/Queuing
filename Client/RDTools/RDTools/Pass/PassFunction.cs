using System.IO;
using System.Text;

namespace RD.Pass
{
	/// <summary>
	/// PassFunction 的摘要说明。
	/// </summary>
	public class PassFunction
	{
		public PassFunction()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		//判断PassFunction类是否初始化
		private static bool init=false; 

		//private int handle;

		/// <summary>
		/// pass初始化 
		/// </summary>
		/// <param name="UserName">用户名</param>
		/// <param name="DepartMentName">用户部门</param>
		/// <param name="WorkStationType">工作站类型 10 - 指住院医生工作站 、门诊医生工作站 、护士站 、PIVA静脉药物  20 - 指临床医学工作站</param>
		/// <returns>0:初始化成功 -1:DIFPassDll.dll文件不存在 -2:系统注册失败 -3:其它未知原因</returns>
		public static int PassInit(string UserName,string DepartMentName,int WorkStationType)
		{			
			try
			{				
				string path = ReadIni("WorkSpace","WorkPath",".\\ShellRunAs.ini");
				if(File.Exists("ShellRunAs.dll") == true)
				{
					DllDynamic.RegisterServer();
					
//					RDMessage.MsgInfo("DIFPassDll.dll文件不存在!");
//					return -1;
				}
				//RDMessage.MsgInfo("开始调用RegisterServer");
				
//				if(handle!=0)
//				{
//					init = false;
//					RDMessage.MsgInfo("系统注册失败!");
//					return -2;
//				}

				int handle;
//				if(File.Exists("DIFPassDll.dll") == true)
//				{
					StringBuilder userName = new StringBuilder(UserName);
					StringBuilder departMentName = new StringBuilder(DepartMentName);
					handle = DllDynamic.PassInit(userName,departMentName,WorkStationType);
					if(handle == 1)
					{
						DllDynamic.PassSetControlParam(1,2,0,2,1);
						init = true;
						return 0;
					}
					else
					{
						return -2;
					}
//				}
//				else
//				{
//					return -1;
//				}
                //RDMessage.MsgInfo("开始调用PassInit");
				
				//RDMessage.MsgInfo("结束调用PassInit");
				//RDMessage.MsgInfo(handle.ToString());
//				if(handle!=1)
//				{
//					return -3;
//				}
//				//RDMessage.MsgInfo("返回");
//				init = true;
				//RDMessage.MsgInfo("返回1");
//				return 0;		
			}
			catch
			{
				init = false;
				return -3;
			}
		}


		/// <summary>
		/// pass 系统功能有效性
		/// </summary>
		/// <param name="QueryItem">
		/// 0 PASSENABLE Pass连接是否可用 ;
		/// 6 DRUGWARN 单药警告是否可用;(审查相关命令状态)        ||
		/// 11 SYS-SET 系统参数设置; 
		/// 12 DISQUISITION 用药研究;
		/// 13 MATCH-DRUG 药品配对信息查询;
		/// 14 MATCH-ROUTE药品给药途径信息查询;(合理用药辅助功能)         ||
		/// 24 AlleyEnable病生状态/过敏史管理; (病生状态/过敏史管理)       ||
		/// 101 CPRRes/CPR 临床用药指南查询;
		/// 102 Directions 药品说明书查询;
		/// 103 CPERes.CPE 病人用药教育查询;
		/// 104 CheckRes/CHECKINFO 校验值查询;
		/// 105 HisDrugInfo 医院药品信息查询;
		/// 106 MEDInfo 药物信息查询中心;
		/// 107 Chp 中国药典;
		/// 501 DISPOSE 处理意见设置; (一级菜单查询)        ||
		/// 201 DDIM 药物与药物相互作用查询;
		/// 202 DFIM 药物与食物相互作用查询;
		/// 203 MatchRes/IV 国内注射剂体外配伍查询;
		/// 204 TriessRes/IVM 国外注射剂体外配伍查询;
		/// 205 DDCM 禁忌症查询;
		/// 206 SIDE 副作用查询;
		/// 207 GERI 老年人用药警告查询;
		/// 208 PEDI 儿童用药警告查询;
		/// 209 PREG 妊娠期用药警告查询;
		/// 210 LACT 哺乳期用药警告查询; (二级菜单查询状态)        ||     
		/// 302 HELP Pass帮助系统; (合理用药帮助系统状态)</param>
		/// <returns>0 可用 -1 不可用</returns>
		public static int PassGetState (string QueryItem)
		{
			if(init == false)
			{
				return -1;
			}
			int handle = DllDynamic.PassGetState(QueryItem);
			if(handle==0)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cPatientID"> 病人ID</param>
		/// <param name="cVisitID">住院次数，如果传“”，则系统认为是“1”</param>
		/// <param name="cPatientName">病人姓名</param>
		/// <param name="cSex">病人性别</param>
		/// <param name="cBirthday">病人出生日期，必须传，才能审剂量 yyyy-mm-dd</param>
		/// <param name="cWeight">体重</param>
		/// <param name="cHeight">身高</param>
		/// <param name="cDeptName">科室名称</param>
		/// <param name="cDoctor">医生姓名</param>
		//		/// <param name="cPatientType">入院日期</param>
		/// <param name="cOutHospitalDate">出院日期</param>
		//		/// <param name="cUseDate">审查日期（空时为当天）</param>
		//		/// <param name="cSaveResultFlag">暂不用，默认为0</param>
		//		/// <param name="iIvorPass">是IV审查还是PASS审查：0:PASS审查，非0：Iv审查，主要是传还是不传过敏史。</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 pass服务还没有启动 -3 其它原因错误</returns>
		public static int PassSetPatientInfo(string cPatientID, string cVisitID, string cPatientName,
			string cSex, string cBirthday, string cWeight, string cHeight,		
			string cDeptName,	string cDoctor, /*string cPatientType,*/string cOutHospitalDate)
			//string cUseDate,	int cSaveResultFlag, int iIvorPass)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}

				DllDynamic.PassSetPatientInfo(cPatientID,  //病人ID
					cVisitID,    //住院次数，如果传“”，则系统认为是“1”
					cPatientName,//病人姓名
					cSex,		//病人性别
					cBirthday,   //病人出生日期，必须传，才能审剂量
					cWeight,     //体重
					cHeight,		//身高
					cDeptName,	//科室名称
					cDoctor,		//医生姓名
					//					cPatientType,//入院日期
					cOutHospitalDate//出院日期
					//					cUseDate,	//审查日期（空时为当天）
					//					cSaveResultFlag,//暂不用，默认为0
					//					iIvorPass//是IV审查还是PASS审查：0:PASS审查，非0：Iv审查，主要是传还是不传过敏史; 
					);
			}
			catch
			{
				return -2;
			}
			return 0;
		}

		/// <summary>
		/// 传病人用药清单
		/// </summary>
		/// <param name="cOrderUniqueCode">医嘱唯一码</param>
		/// <param name="cDrugCode">药品唯一码</param>
		/// <param name="cDrugName">药品名称</param>
		/// <param name="cSingleDose">每次剂量</param>
		/// <param name="cDoseUnit">剂量单位</param>
		/// <param name="cFrequency">频次，格式要求：n天m次，传"m/n"</param>
		/// <param name="cStartDate">开始日期（格式要求"yyyy-mm-dd"）</param>
		/// <param name="cEndDate">停嘱日期格式要求"yyyy-mm-dd"）</param>
		/// <param name="cRouteName">给药途径名称</param>
		/// <param name="cGroupTag">成组标记，如果成组医嘱标记传空，系统自动//传"mdc" + 数组下标作为不一组的标记</param>
		/// <param name="cOrderType">1 临时医嘱 0 或空长期医嘱 </param>
		/// <param name="cOrderDoctor">下嘱医生ID\下嘱医生姓名 </param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetRecipeInfo(
			string cOrderUniqueCode , //医嘱唯一码
			string cDrugCode ,        //药品唯一码
			string cDrugName ,        //药品名称
			string cSingleDose ,      //每次剂量
			string cDoseUnit   ,      //剂量单位
			string cFrequency  ,      //频次，格式要求：n天m次，传"m/n"）
			string cStartDate ,       //开始日期（格式要求"yyyy-mm-dd"）//如果不传开嘱日期，系统则默认为当天。
			string cEndDate   ,       //停嘱日期格式要求"yyyy-mm-dd"）//如果不传开嘱日期，系统则默认为当天。
			string cRouteName ,		  //给药途径名称
			string cGroupTag  ,       //成组标记，如果成组医嘱标记传空，系统自动//传"mdc" + 数组下标作为不一组的标记
			string cOrderType,        //1 临时医嘱 0 或空长期医嘱 
			string cOrderDoctor		  //下嘱医生ID\下嘱医生姓名;  
			)       
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetRecipeInfo(
					cOrderUniqueCode , //医嘱唯一码
					cDrugCode ,        //药品唯一码
					cDrugName ,        //药品名称
					cSingleDose ,      //每次剂量
					cDoseUnit   ,      //剂量单位
					cFrequency  ,      //频次，格式要求：n天m次，传"m/n"）
					cStartDate ,       //开始日期（格式要求"yyyy-mm-dd"）//如果不传开嘱日期，系统则默认为当天。
					cEndDate   ,       //停嘱日期格式要求"yyyy-mm-dd"）//如果不传开嘱日期，系统则默认为当天。
					cRouteName ,	   //给药途径名称
					cGroupTag  ,       //成组标记，如果成组医嘱标记传空，系统自动//传"mdc" + 数组下标作为不一组的标记
					cOrderType ,       // 1 临时医嘱 0 或空长期医嘱 
					cOrderDoctor       //下嘱医生ID\下嘱医生姓名);  
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// 执行pass命令
		/// </summary>
		/// <param name="iCommandNo">命令代码</param>
		/// <returns></returns>
		public static int PassDoCommand(int iCommandNo)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassDoCommand(
					iCommandNo
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// pass应用模式设置 PassSetControlParam(1,2,0,2,1)默认值
		/// </summary>
		/// <param name="SaveCheckResult">是否需要保存pass监测结果 0 不采集 1 依赖系统设置</param>
		/// <param name="AllowAllegen">是否管理病人过敏史/病生状态 0 不管理 1 由用户传入 2 pass管理 3 pass强制管理</param> 
		/// <param name="CheckMode">审查模式 0 审查模式 1 IV模式</param>
		/// <param name="DisqMode">调用药研究时是否传入医嘱信息 0 不传 1 要传 2 提示</param>
		/// <param name="UseDiposeIdea">是否使用处理意见 0 不使用处理 1 根据设置</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetControlParam(
			int SaveCheckResult,
			int AllowAllegen,
			int CheckMode,
			int DisqMode,
			int UseDiposeIdea)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetControlParam(
					SaveCheckResult,
					AllowAllegen,
					CheckMode,
					DisqMode,
					UseDiposeIdea);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		//		/// <summary>
		//		/// PASS病人过敏史/病生状态管理传病人情况信息函数
		//		/// </summary>
		//		/// <param name="cPatientID">病人ID</param>
		//		/// <param name="cVisitID">住院次数，如果传“”，则系统认为是“1”</param>
		//		/// <param name="cPatientName">病人姓名</param>
		//		/// <param name="cSex">病人性别</param>
		//		/// <param name="cBirthday">病人出生日期</param>
		//		/// <param name="cWeight">体重</param>
		//		/// <param name="cHeight">身高</param>
		//		/// <param name="cDoctor">医生姓名</param>
		//		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		//		public int PassSetAlleyInfo(
		//			string cPatientID ,  //病人ID
		//			string cVisitID   ,  //住院次数，如果传“”，则系统认为是“1”
		//			string cPatientName, //病人姓名
		//			string cSex,         //病人性别
		//			string cBirthday,    //病人出生日期
		//			string cWeight ,     //体重
		//			string cHeight ,     //身高
		//			string cDoctor       //医生姓名
		//			)
		//		{
		//			try
		//			{
		//				if(init == false)
		//				{
		//					return -1;
		//				}
		//				DllDynamic.PassSetAlleyInfo(
		//					cPatientID ,  //病人ID
		//					cVisitID   ,  //住院次数，如果传“”，则系统认为是“1”
		//					cPatientName, //病人姓名
		//					cSex,         //病人性别
		//					cBirthday,    //病人出生日期
		//					cWeight ,     //体重
		//					cHeight ,     //身高
		//					cDoctor       //医生姓名
		//					);
		//				return 0;
		//			}
		//			catch
		//			{
		//				return -2;
		//			}
		//		}

		//		/// <summary>
		//		/// 信息查询传药品、给药途径函数
		//		/// </summary>
		//		/// <param name="cDrugCode">药品唯一码</param>
		//		/// <param name="cDrugName">药品名称</param>
		//		/// <param name="cDrugUnit">药品给药单位</param>
		//		/// <param name="cRrouteName">给药途径编码</param>
		//		/// <param name="nWarn">警告值</param>
		//		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		//		public int PassSetSearchdruginfo(
		//			string cDrugCode ,　  //药品唯一码
		//			string cDrugName ,　  //药品名称
		//			string cDrugUnit ,　　//药品给药单位
		//			string cRrouteName ,  //给药途径编码
		//			int nWarn             //警告值
		//			)
		//		{
		//			try
		//			{
		//				if(init == false)
		//				{
		//					return -1;
		//				}
		//				DllDynamic.PassSetSearchdruginfo(
		//					cDrugCode ,　  //药品唯一码
		//					cDrugName ,　  //药品名称
		//					cDrugUnit ,　　//药品给药单位
		//					cRrouteName ,  //给药途径编码
		//					nWarn          //警告值
		//					);
		//				return 0;
		//			}
		//			catch
		//			{
		//				return -2;
		//			}
		//		}

		/// <summary>
		/// 传入病人过敏史 (当病人过敏史信息由His管理,并传入到Pass系统进行审核时调用函数)
		/// </summary>
		/// <param name="AllergenIndex">过敏原在医嘱中的顺序编号</param>
		/// <param name="AllergenCode">过敏原编码值</param>
		/// <param name="AllergenDesc">过敏原名称</param>
		/// <param name="AllergenType">过敏原类型: AllergenGroup pass过敏组 ,User_Drug 用户药品 ,DrugName pass药物名称</param>
		/// <param name="Reaction">过敏症状(可卜传值)</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetAllergenInfo(
			string AllergenIndex,
			string AllergenCode,
			string AllergenDesc,
			string AllergenType,
			string Reaction
			)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetAllergenInfo(
					AllergenIndex,
					AllergenCode,
					AllergenDesc,
					AllergenType,
					Reaction
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// 传入病生状态
		/// </summary>
		/// <param name="MedCondIndex">医疗条件在医嘱中的顺序编号</param>
		/// <param name="MedCondCode">医疗条件编码</param>
		/// <param name="MedCondDesc">医疗条件名称</param>
		/// <param name="MedCondType">医疗条件类型: User_MedCond 用户医疗条件 , ICD ICD-9CM编码</param>
		/// <param name="StartDate">开始日期 (yyyy-mm-dd)</param>
		/// <param name="EndDate">结束日期 (yyyy-mm-dd)</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetMedCond(
			string MedCondIndex,
			string MedCondCode,
			string MedCondDesc,
			string MedCondType,
			string StartDate,
			string EndDate
			)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetMedCond(
					MedCondIndex,
					MedCondCode,
					MedCondDesc,
					MedCondType,
					StartDate,
					EndDate
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// 传入当前查询药品信息
		/// </summary>
		/// <param name="DrugCode">药品编码</param>
		/// <param name="DrugName">药品名称</param>
		/// <param name="DoseUnit">剂量单位</param>
		/// <param name="RouteName">用药途径中文名称</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetQueryDrug(
			string DrugCode,
			string DrugName,
			string DoseUnit,
			string RouteName
			)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetQueryDrug(
					DrugCode,
					DrugName,
					DoseUnit,
					RouteName
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// 传入当前浮动窗口的窗口显示位置
		/// </summary>
		/// <param name="left">编辑框左上角的 X 坐标</param>
		/// <param name="top">编辑框左上角的 Y 坐标</param>
		/// <param name="right">编辑框右下角的 X 坐标</param>
		/// <param name="bottome">编辑框右下角的 Y 坐标</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetFloatWinPos(
			int left,
			int top,
			int right,
			int bottome
			)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetFloatWinPos(
					left,
					top,
					right,
					bottome
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// 获取pass审查结果警示级别
		/// </summary>
		/// <param name="DrugUniqueCode">医嘱唯一编码</param>
		/// <returns>-3 无灯 表示pass出现错误,未进行审核 |  -2 无灯 表示该药品在处方传送时被过滤 | -1 表示未经Pass系统监测 | 0 蓝灯 Pass监测未提示相关用药问题 |
		/// 1 黄灯 危害较低或尚不明确，适度关注 | 2 红灯 不推荐或较严重危害,高度关注 | 4 橙灯 慎用或有一定危害 ,较高度关注 | 3 黑灯 绝对禁忌 , 错误或致死性危害 </returns>
		public static int PassGetWarn(string DrugUniqueCode)
		{
			try
			{
				if(init == false)
				{
					return -100;
				}
				return DllDynamic.PassGetWarn(DrugUniqueCode);
				//return 0;
			}
			catch
			{
				return -200;
			}
		}

		/// <summary>
		/// 设置需要进行单药警告的药品
		/// </summary>
		/// <param name="DrugUniqueCode">医嘱唯一编码</param>
		/// <returns>0:成功 -1: 还没有初始化 -2 其它原因错误</returns>
		public static int PassSetWarnDrug(string DrugUniqueCode)
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetWarnDrug(DrugUniqueCode);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// pass退出
		/// </summary>
		/// <returns></returns>
		public static int PassQuit()
		{
			if(init == false)
			{
				return -1;
			}
			DllDynamic.PassQuit();
			return 0;
		}

		//读ini
		private static string ReadIni(string section,string key,string filePath)
		{
			StringBuilder temp = new StringBuilder (500);
			DllDynamic.GetPrivateProfileString( section, key, "", temp, 500, filePath); 
			return temp.ToString ();
            
		}
	}
}

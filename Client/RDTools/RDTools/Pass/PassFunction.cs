using System.IO;
using System.Text;

namespace RD.Pass
{
	/// <summary>
	/// PassFunction ��ժҪ˵����
	/// </summary>
	public class PassFunction
	{
		public PassFunction()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		//�ж�PassFunction���Ƿ��ʼ��
		private static bool init=false; 

		//private int handle;

		/// <summary>
		/// pass��ʼ�� 
		/// </summary>
		/// <param name="UserName">�û���</param>
		/// <param name="DepartMentName">�û�����</param>
		/// <param name="WorkStationType">����վ���� 10 - ָסԺҽ������վ ������ҽ������վ ����ʿվ ��PIVA����ҩ��  20 - ָ�ٴ�ҽѧ����վ</param>
		/// <returns>0:��ʼ���ɹ� -1:DIFPassDll.dll�ļ������� -2:ϵͳע��ʧ�� -3:����δ֪ԭ��</returns>
		public static int PassInit(string UserName,string DepartMentName,int WorkStationType)
		{			
			try
			{				
				string path = ReadIni("WorkSpace","WorkPath",".\\ShellRunAs.ini");
				if(File.Exists("ShellRunAs.dll") == true)
				{
					DllDynamic.RegisterServer();
					
//					RDMessage.MsgInfo("DIFPassDll.dll�ļ�������!");
//					return -1;
				}
				//RDMessage.MsgInfo("��ʼ����RegisterServer");
				
//				if(handle!=0)
//				{
//					init = false;
//					RDMessage.MsgInfo("ϵͳע��ʧ��!");
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
                //RDMessage.MsgInfo("��ʼ����PassInit");
				
				//RDMessage.MsgInfo("��������PassInit");
				//RDMessage.MsgInfo(handle.ToString());
//				if(handle!=1)
//				{
//					return -3;
//				}
//				//RDMessage.MsgInfo("����");
//				init = true;
				//RDMessage.MsgInfo("����1");
//				return 0;		
			}
			catch
			{
				init = false;
				return -3;
			}
		}


		/// <summary>
		/// pass ϵͳ������Ч��
		/// </summary>
		/// <param name="QueryItem">
		/// 0 PASSENABLE Pass�����Ƿ���� ;
		/// 6 DRUGWARN ��ҩ�����Ƿ����;(����������״̬)        ||
		/// 11 SYS-SET ϵͳ��������; 
		/// 12 DISQUISITION ��ҩ�о�;
		/// 13 MATCH-DRUG ҩƷ�����Ϣ��ѯ;
		/// 14 MATCH-ROUTEҩƷ��ҩ;����Ϣ��ѯ;(������ҩ��������)         ||
		/// 24 AlleyEnable����״̬/����ʷ����; (����״̬/����ʷ����)       ||
		/// 101 CPRRes/CPR �ٴ���ҩָ�ϲ�ѯ;
		/// 102 Directions ҩƷ˵�����ѯ;
		/// 103 CPERes.CPE ������ҩ������ѯ;
		/// 104 CheckRes/CHECKINFO У��ֵ��ѯ;
		/// 105 HisDrugInfo ҽԺҩƷ��Ϣ��ѯ;
		/// 106 MEDInfo ҩ����Ϣ��ѯ����;
		/// 107 Chp �й�ҩ��;
		/// 501 DISPOSE �����������; (һ���˵���ѯ)        ||
		/// 201 DDIM ҩ����ҩ���໥���ò�ѯ;
		/// 202 DFIM ҩ����ʳ���໥���ò�ѯ;
		/// 203 MatchRes/IV ����ע������������ѯ;
		/// 204 TriessRes/IVM ����ע������������ѯ;
		/// 205 DDCM ����֢��ѯ;
		/// 206 SIDE �����ò�ѯ;
		/// 207 GERI ��������ҩ�����ѯ;
		/// 208 PEDI ��ͯ��ҩ�����ѯ;
		/// 209 PREG ��������ҩ�����ѯ;
		/// 210 LACT ��������ҩ�����ѯ; (�����˵���ѯ״̬)        ||     
		/// 302 HELP Pass����ϵͳ; (������ҩ����ϵͳ״̬)</param>
		/// <returns>0 ���� -1 ������</returns>
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
		/// <param name="cPatientID"> ����ID</param>
		/// <param name="cVisitID">סԺ�������������������ϵͳ��Ϊ�ǡ�1��</param>
		/// <param name="cPatientName">��������</param>
		/// <param name="cSex">�����Ա�</param>
		/// <param name="cBirthday">���˳������ڣ����봫����������� yyyy-mm-dd</param>
		/// <param name="cWeight">����</param>
		/// <param name="cHeight">���</param>
		/// <param name="cDeptName">��������</param>
		/// <param name="cDoctor">ҽ������</param>
		//		/// <param name="cPatientType">��Ժ����</param>
		/// <param name="cOutHospitalDate">��Ժ����</param>
		//		/// <param name="cUseDate">������ڣ���ʱΪ���죩</param>
		//		/// <param name="cSaveResultFlag">�ݲ��ã�Ĭ��Ϊ0</param>
		//		/// <param name="iIvorPass">��IV��黹��PASS��飺0:PASS��飬��0��Iv��飬��Ҫ�Ǵ����ǲ�������ʷ��</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 pass����û������ -3 ����ԭ�����</returns>
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

				DllDynamic.PassSetPatientInfo(cPatientID,  //����ID
					cVisitID,    //סԺ�������������������ϵͳ��Ϊ�ǡ�1��
					cPatientName,//��������
					cSex,		//�����Ա�
					cBirthday,   //���˳������ڣ����봫�����������
					cWeight,     //����
					cHeight,		//���
					cDeptName,	//��������
					cDoctor,		//ҽ������
					//					cPatientType,//��Ժ����
					cOutHospitalDate//��Ժ����
					//					cUseDate,	//������ڣ���ʱΪ���죩
					//					cSaveResultFlag,//�ݲ��ã�Ĭ��Ϊ0
					//					iIvorPass//��IV��黹��PASS��飺0:PASS��飬��0��Iv��飬��Ҫ�Ǵ����ǲ�������ʷ; 
					);
			}
			catch
			{
				return -2;
			}
			return 0;
		}

		/// <summary>
		/// ��������ҩ�嵥
		/// </summary>
		/// <param name="cOrderUniqueCode">ҽ��Ψһ��</param>
		/// <param name="cDrugCode">ҩƷΨһ��</param>
		/// <param name="cDrugName">ҩƷ����</param>
		/// <param name="cSingleDose">ÿ�μ���</param>
		/// <param name="cDoseUnit">������λ</param>
		/// <param name="cFrequency">Ƶ�Σ���ʽҪ��n��m�Σ���"m/n"</param>
		/// <param name="cStartDate">��ʼ���ڣ���ʽҪ��"yyyy-mm-dd"��</param>
		/// <param name="cEndDate">ͣ�����ڸ�ʽҪ��"yyyy-mm-dd"��</param>
		/// <param name="cRouteName">��ҩ;������</param>
		/// <param name="cGroupTag">�����ǣ��������ҽ����Ǵ��գ�ϵͳ�Զ�//��"mdc" + �����±���Ϊ��һ��ı��</param>
		/// <param name="cOrderType">1 ��ʱҽ�� 0 ��ճ���ҽ�� </param>
		/// <param name="cOrderDoctor">����ҽ��ID\����ҽ������ </param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
		public static int PassSetRecipeInfo(
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
			string cOrderType,        //1 ��ʱҽ�� 0 ��ճ���ҽ�� 
			string cOrderDoctor		  //����ҽ��ID\����ҽ������;  
			)       
		{
			try
			{
				if(init == false)
				{
					return -1;
				}
				DllDynamic.PassSetRecipeInfo(
					cOrderUniqueCode , //ҽ��Ψһ��
					cDrugCode ,        //ҩƷΨһ��
					cDrugName ,        //ҩƷ����
					cSingleDose ,      //ÿ�μ���
					cDoseUnit   ,      //������λ
					cFrequency  ,      //Ƶ�Σ���ʽҪ��n��m�Σ���"m/n"��
					cStartDate ,       //��ʼ���ڣ���ʽҪ��"yyyy-mm-dd"��//��������������ڣ�ϵͳ��Ĭ��Ϊ���졣
					cEndDate   ,       //ͣ�����ڸ�ʽҪ��"yyyy-mm-dd"��//��������������ڣ�ϵͳ��Ĭ��Ϊ���졣
					cRouteName ,	   //��ҩ;������
					cGroupTag  ,       //�����ǣ��������ҽ����Ǵ��գ�ϵͳ�Զ�//��"mdc" + �����±���Ϊ��һ��ı��
					cOrderType ,       // 1 ��ʱҽ�� 0 ��ճ���ҽ�� 
					cOrderDoctor       //����ҽ��ID\����ҽ������);  
					);
				return 0;
			}
			catch
			{
				return -2;
			}
		}

		/// <summary>
		/// ִ��pass����
		/// </summary>
		/// <param name="iCommandNo">�������</param>
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
		/// passӦ��ģʽ���� PassSetControlParam(1,2,0,2,1)Ĭ��ֵ
		/// </summary>
		/// <param name="SaveCheckResult">�Ƿ���Ҫ����pass����� 0 ���ɼ� 1 ����ϵͳ����</param>
		/// <param name="AllowAllegen">�Ƿ�����˹���ʷ/����״̬ 0 ������ 1 ���û����� 2 pass���� 3 passǿ�ƹ���</param> 
		/// <param name="CheckMode">���ģʽ 0 ���ģʽ 1 IVģʽ</param>
		/// <param name="DisqMode">����ҩ�о�ʱ�Ƿ���ҽ����Ϣ 0 ���� 1 Ҫ�� 2 ��ʾ</param>
		/// <param name="UseDiposeIdea">�Ƿ�ʹ�ô������ 0 ��ʹ�ô��� 1 ��������</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
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
		//		/// PASS���˹���ʷ/����״̬�������������Ϣ����
		//		/// </summary>
		//		/// <param name="cPatientID">����ID</param>
		//		/// <param name="cVisitID">סԺ�������������������ϵͳ��Ϊ�ǡ�1��</param>
		//		/// <param name="cPatientName">��������</param>
		//		/// <param name="cSex">�����Ա�</param>
		//		/// <param name="cBirthday">���˳�������</param>
		//		/// <param name="cWeight">����</param>
		//		/// <param name="cHeight">���</param>
		//		/// <param name="cDoctor">ҽ������</param>
		//		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
		//		public int PassSetAlleyInfo(
		//			string cPatientID ,  //����ID
		//			string cVisitID   ,  //סԺ�������������������ϵͳ��Ϊ�ǡ�1��
		//			string cPatientName, //��������
		//			string cSex,         //�����Ա�
		//			string cBirthday,    //���˳�������
		//			string cWeight ,     //����
		//			string cHeight ,     //���
		//			string cDoctor       //ҽ������
		//			)
		//		{
		//			try
		//			{
		//				if(init == false)
		//				{
		//					return -1;
		//				}
		//				DllDynamic.PassSetAlleyInfo(
		//					cPatientID ,  //����ID
		//					cVisitID   ,  //סԺ�������������������ϵͳ��Ϊ�ǡ�1��
		//					cPatientName, //��������
		//					cSex,         //�����Ա�
		//					cBirthday,    //���˳�������
		//					cWeight ,     //����
		//					cHeight ,     //���
		//					cDoctor       //ҽ������
		//					);
		//				return 0;
		//			}
		//			catch
		//			{
		//				return -2;
		//			}
		//		}

		//		/// <summary>
		//		/// ��Ϣ��ѯ��ҩƷ����ҩ;������
		//		/// </summary>
		//		/// <param name="cDrugCode">ҩƷΨһ��</param>
		//		/// <param name="cDrugName">ҩƷ����</param>
		//		/// <param name="cDrugUnit">ҩƷ��ҩ��λ</param>
		//		/// <param name="cRrouteName">��ҩ;������</param>
		//		/// <param name="nWarn">����ֵ</param>
		//		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
		//		public int PassSetSearchdruginfo(
		//			string cDrugCode ,��  //ҩƷΨһ��
		//			string cDrugName ,��  //ҩƷ����
		//			string cDrugUnit ,����//ҩƷ��ҩ��λ
		//			string cRrouteName ,  //��ҩ;������
		//			int nWarn             //����ֵ
		//			)
		//		{
		//			try
		//			{
		//				if(init == false)
		//				{
		//					return -1;
		//				}
		//				DllDynamic.PassSetSearchdruginfo(
		//					cDrugCode ,��  //ҩƷΨһ��
		//					cDrugName ,��  //ҩƷ����
		//					cDrugUnit ,����//ҩƷ��ҩ��λ
		//					cRrouteName ,  //��ҩ;������
		//					nWarn          //����ֵ
		//					);
		//				return 0;
		//			}
		//			catch
		//			{
		//				return -2;
		//			}
		//		}

		/// <summary>
		/// ���벡�˹���ʷ (�����˹���ʷ��Ϣ��His����,�����뵽Passϵͳ�������ʱ���ú���)
		/// </summary>
		/// <param name="AllergenIndex">����ԭ��ҽ���е�˳����</param>
		/// <param name="AllergenCode">����ԭ����ֵ</param>
		/// <param name="AllergenDesc">����ԭ����</param>
		/// <param name="AllergenType">����ԭ����: AllergenGroup pass������ ,User_Drug �û�ҩƷ ,DrugName passҩ������</param>
		/// <param name="Reaction">����֢״(�ɲ���ֵ)</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
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
		/// ���벡��״̬
		/// </summary>
		/// <param name="MedCondIndex">ҽ��������ҽ���е�˳����</param>
		/// <param name="MedCondCode">ҽ����������</param>
		/// <param name="MedCondDesc">ҽ����������</param>
		/// <param name="MedCondType">ҽ����������: User_MedCond �û�ҽ������ , ICD ICD-9CM����</param>
		/// <param name="StartDate">��ʼ���� (yyyy-mm-dd)</param>
		/// <param name="EndDate">�������� (yyyy-mm-dd)</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
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
		/// ���뵱ǰ��ѯҩƷ��Ϣ
		/// </summary>
		/// <param name="DrugCode">ҩƷ����</param>
		/// <param name="DrugName">ҩƷ����</param>
		/// <param name="DoseUnit">������λ</param>
		/// <param name="RouteName">��ҩ;����������</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
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
		/// ���뵱ǰ�������ڵĴ�����ʾλ��
		/// </summary>
		/// <param name="left">�༭�����Ͻǵ� X ����</param>
		/// <param name="top">�༭�����Ͻǵ� Y ����</param>
		/// <param name="right">�༭�����½ǵ� X ����</param>
		/// <param name="bottome">�༭�����½ǵ� Y ����</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
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
		/// ��ȡpass�������ʾ����
		/// </summary>
		/// <param name="DrugUniqueCode">ҽ��Ψһ����</param>
		/// <returns>-3 �޵� ��ʾpass���ִ���,δ������� |  -2 �޵� ��ʾ��ҩƷ�ڴ�������ʱ������ | -1 ��ʾδ��Passϵͳ��� | 0 ���� Pass���δ��ʾ�����ҩ���� |
		/// 1 �Ƶ� Σ���ϵͻ��в���ȷ���ʶȹ�ע | 2 ��� ���Ƽ��������Σ��,�߶ȹ�ע | 4 �ȵ� ���û���һ��Σ�� ,�ϸ߶ȹ�ע | 3 �ڵ� ���Խ��� , �����������Σ�� </returns>
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
		/// ������Ҫ���е�ҩ�����ҩƷ
		/// </summary>
		/// <param name="DrugUniqueCode">ҽ��Ψһ����</param>
		/// <returns>0:�ɹ� -1: ��û�г�ʼ�� -2 ����ԭ�����</returns>
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
		/// pass�˳�
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

		//��ini
		private static string ReadIni(string section,string key,string filePath)
		{
			StringBuilder temp = new StringBuilder (500);
			DllDynamic.GetPrivateProfileString( section, key, "", temp, 500, filePath); 
			return temp.ToString ();
            
		}
	}
}

using RDTools.NewSocketManager;
using System;

namespace RDTools.Common
{
	public class SysOperatorInfo
	{
		public SysOperatorInfo()
		{
		}

		/// <summary>
		/// ����Ա����
		/// </summary>
		public static string OperatorID;  
		/// <summary>
		/// ����Ա����
		/// </summary>
		public static string OperatorName;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public static string OperatorCode;
		/// <summary>
		/// ����Ա����
		/// </summary>
		public static string OperatorOffice; 
		/// <summary>
		/// Ĭ��ҩ��
		/// </summary>
		public static string OperatorDefaultDrugstore;
		/// <summary>
		/// ������Ϣ������
		/// </summary>
		public static string ReceiveMsgType;
		/// <summary>
		/// ����Ա����ID
		/// </summary>
		public static string OperatorOfficeID; 
		/// <summary>
		/// ����Ա��������
		/// </summary>
		public static string OperatorWorkkind;

		/// <summary>
		/// ҽԺ������
		/// </summary>
		public static string CustomerName;
		public static string UserID;

        public static NewClient NewClient { get; set; }

        /// <summary>
        /// ������ʾ����
        /// </summary>
        public static string ScreenOffice { get; set; }
	}
}

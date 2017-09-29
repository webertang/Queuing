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
		/// 操作员代码
		/// </summary>
		public static string OperatorID;  
		/// <summary>
		/// 操作员姓名
		/// </summary>
		public static string OperatorName;
		/// <summary>
		/// 操作员代码
		/// </summary>
		public static string OperatorCode;
		/// <summary>
		/// 操作员科室
		/// </summary>
		public static string OperatorOffice; 
		/// <summary>
		/// 默认药房
		/// </summary>
		public static string OperatorDefaultDrugstore;
		/// <summary>
		/// 接收消息的类型
		/// </summary>
		public static string ReceiveMsgType;
		/// <summary>
		/// 操作员科室ID
		/// </summary>
		public static string OperatorOfficeID; 
		/// <summary>
		/// 操作员工作性质
		/// </summary>
		public static string OperatorWorkkind;

		/// <summary>
		/// 医院的名称
		/// </summary>
		public static string CustomerName;
		public static string UserID;

        public static NewClient NewClient { get; set; }

        /// <summary>
        /// 大屏显示科室
        /// </summary>
        public static string ScreenOffice { get; set; }
	}
}

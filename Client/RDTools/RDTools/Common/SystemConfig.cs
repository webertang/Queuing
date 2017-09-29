using System;

namespace RDTools.Common
{
	[SerializableAttribute]
	public class SystemConfig
	{
		public SystemConfig()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _ConfigureNo;
		/// <summary>
		/// 配置编号
		/// </summary>
		public string ConfigureNo
		{
			get{return _ConfigureNo;}
			set{_ConfigureNo=value;}
		}
		private string _SetType;
		/// <summary>
		/// 设置类型
		/// </summary>
		public string SetType
		{
			get{return _SetType;}
			set{_SetType=value;}
		}
		private string _SetContent;
		/// <summary>
		/// 设置内容
		/// </summary>
		public string SetContent
		{
			get{return _SetContent;}
			set{_SetContent=value;}
		}
		private string _DefaultValue;
		/// <summary>
		/// 设置值
		/// </summary>
		public string DefaultValue
		{
			get{return _DefaultValue;}
			set{_DefaultValue=value;}
		}
		private string _Detail;
		/// <summary>
		/// 说明
		/// </summary>
		public string Detail
		{
			get{return _Detail;}
			set{_Detail=value;}
		}
		private string _OptionalPara;
		/// <summary>
		/// 可选参数
		/// </summary>
		public string OptionalPara
		{
			get{return _OptionalPara;}
			set{_OptionalPara=value;}
		}
	}
}

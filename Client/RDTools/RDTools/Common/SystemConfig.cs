using System;

namespace RDTools.Common
{
	[SerializableAttribute]
	public class SystemConfig
	{
		public SystemConfig()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private string _ConfigureNo;
		/// <summary>
		/// ���ñ��
		/// </summary>
		public string ConfigureNo
		{
			get{return _ConfigureNo;}
			set{_ConfigureNo=value;}
		}
		private string _SetType;
		/// <summary>
		/// ��������
		/// </summary>
		public string SetType
		{
			get{return _SetType;}
			set{_SetType=value;}
		}
		private string _SetContent;
		/// <summary>
		/// ��������
		/// </summary>
		public string SetContent
		{
			get{return _SetContent;}
			set{_SetContent=value;}
		}
		private string _DefaultValue;
		/// <summary>
		/// ����ֵ
		/// </summary>
		public string DefaultValue
		{
			get{return _DefaultValue;}
			set{_DefaultValue=value;}
		}
		private string _Detail;
		/// <summary>
		/// ˵��
		/// </summary>
		public string Detail
		{
			get{return _Detail;}
			set{_Detail=value;}
		}
		private string _OptionalPara;
		/// <summary>
		/// ��ѡ����
		/// </summary>
		public string OptionalPara
		{
			get{return _OptionalPara;}
			set{_OptionalPara=value;}
		}
	}
}

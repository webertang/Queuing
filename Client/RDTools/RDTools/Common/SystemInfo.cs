using System;

namespace RDTools.Common
{
	public class SystemInfo
	{
		public SystemInfo()
		{
		}

		private static SystemConfigCollection _SystemConfigs;
		/// <summary>
		/// ����������Ϣ�ļ���
		/// </summary>
		public static SystemConfigCollection SystemConfigs
		{
			get{return _SystemConfigs;}
			set{_SystemConfigs=value;}
		}
	}
}

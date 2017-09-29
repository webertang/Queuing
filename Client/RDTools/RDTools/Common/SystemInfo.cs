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
		/// 所有配置信息的集合
		/// </summary>
		public static SystemConfigCollection SystemConfigs
		{
			get{return _SystemConfigs;}
			set{_SystemConfigs=value;}
		}
	}
}

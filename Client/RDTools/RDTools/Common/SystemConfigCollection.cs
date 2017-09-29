using System;
using System.Collections;

namespace RDTools.Common
{
	public class SystemConfigCollection
	{
		private ArrayList items;
		public SystemConfigCollection()
		{
			items=new ArrayList();
		}
		/// <summary>
		/// 集合中对象的数量
		/// </summary>
		public int Count
		{
			get
			{
				return this.items.Count; 

			}
		}
		/// <summary>
		/// 添加一个系统配置对象
		/// </summary>
		/// <param name="config">系统配置对象</param>
		/// <returns>系统配置对象</returns>
		public SystemConfig Add(SystemConfig config)
		{
			items.Add(config);
			return config;
		}
		/// <summary>
		/// 按索引得到SystemConfig对象
		/// </summary>
		public SystemConfig this[int index]
		{
			get
			{
				//this.RangeCheck(index);
				int num1 = this.RangeCheck(index);
				return ((SystemConfig) this.items[num1]); 
			}

		}
        public SystemConfig this[string setContent]
        {
            get
            {
                int num1 = this.RangeCheck(setContent);
                return ((SystemConfig)this.items[num1]);

            }
        }
		/// <summary>
		/// 根据配置名称判断其是否存在
		/// </summary>
		/// <param name="setContent">配置名称</param>
		/// <returns>配置的索引</returns>
		private int RangeCheck(string setContent)
		{
			int num1;
			num1 = this.IndexOf(setContent);
			if (num1 < 0)
			{
                throw new IndexOutOfRangeException("请联系管理员:没有该配置对象！{" + setContent+"}"); 
			}
			return num1; 
		} 
		/// <summary>
		/// 检测索引值是不是超出集合的范围
		/// </summary>
		/// <param name="index">索引值</param>
		private int RangeCheck(int index)
		{
			int num1;
			num1 = this.IndexOf(index);
			if (num1 < 0)
			{
				throw new IndexOutOfRangeException("没有该配置对象！"); 
			}
			return num1; 
		} 
		/// <summary>
		/// 根据配置名称检测其索引
		/// </summary>
		/// <param name="setContent">配置名称</param>
		/// <returns>配置的索引值</returns>
		public int IndexOf(string setContent)
		{
			int index=-1;
			if (this.items != null)
			{
				for(int i=0;i<this.items.Count;i++)
				{
					if(((SystemConfig)items[i]).SetContent.Equals(setContent))
					{
						index=i;
						break;
					}
				}
			}
			return index; 
		}
		/// <summary>
		/// 根据编号检测其索引
		/// </summary>
		/// <param name="configureNo">配置编号</param>
		/// <returns>该编号的所用</returns>
		public int IndexOf(int configureNo)
		{
			int index=-1;
			if (this.items != null)
			{
				for(int i=0;i<this.items.Count;i++)
				{
					if(((SystemConfig)items[i]).ConfigureNo.Trim().Equals(configureNo.ToString().Trim()))
					{
						index=i;
						break;
					}
				}
			}
			return index; 
		}
		/// <summary>
		/// 清空集合
		/// </summary>
		public void Clear()
		{
			items.Clear();
		}
	}
}

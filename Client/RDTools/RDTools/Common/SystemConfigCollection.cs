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
		/// �����ж��������
		/// </summary>
		public int Count
		{
			get
			{
				return this.items.Count; 

			}
		}
		/// <summary>
		/// ���һ��ϵͳ���ö���
		/// </summary>
		/// <param name="config">ϵͳ���ö���</param>
		/// <returns>ϵͳ���ö���</returns>
		public SystemConfig Add(SystemConfig config)
		{
			items.Add(config);
			return config;
		}
		/// <summary>
		/// �������õ�SystemConfig����
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
		/// �������������ж����Ƿ����
		/// </summary>
		/// <param name="setContent">��������</param>
		/// <returns>���õ�����</returns>
		private int RangeCheck(string setContent)
		{
			int num1;
			num1 = this.IndexOf(setContent);
			if (num1 < 0)
			{
                throw new IndexOutOfRangeException("����ϵ����Ա:û�и����ö���{" + setContent+"}"); 
			}
			return num1; 
		} 
		/// <summary>
		/// �������ֵ�ǲ��ǳ������ϵķ�Χ
		/// </summary>
		/// <param name="index">����ֵ</param>
		private int RangeCheck(int index)
		{
			int num1;
			num1 = this.IndexOf(index);
			if (num1 < 0)
			{
				throw new IndexOutOfRangeException("û�и����ö���"); 
			}
			return num1; 
		} 
		/// <summary>
		/// �����������Ƽ��������
		/// </summary>
		/// <param name="setContent">��������</param>
		/// <returns>���õ�����ֵ</returns>
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
		/// ���ݱ�ż��������
		/// </summary>
		/// <param name="configureNo">���ñ��</param>
		/// <returns>�ñ�ŵ�����</returns>
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
		/// ��ռ���
		/// </summary>
		public void Clear()
		{
			items.Clear();
		}
	}
}

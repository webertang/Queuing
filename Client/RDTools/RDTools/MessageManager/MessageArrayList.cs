using System;
using System.Collections;

namespace RDTools.MsgService
{
	/// <summary>
	/// MessageArrayList ��ժҪ˵����
	/// </summary>
	public class MsgArrayList : ArrayList
	{
		public int Add(Msg value)
		{
			
			return base.Add(value);
		}
		public new Msg this[int index]
		{
			get
			{
				return (Msg)base[index];
			}
			set
			{
				base[index]=value;
			}
		}
		public override int Count
		{
			get
			{
				return base.Count;
				
			}
		}
	}
}

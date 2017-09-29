using System;

namespace RDTools.Common
{
	public class UnitToPack
	{
		public UnitToPack()
		{
		}

		/// <summary>
		/// 单位数量转换为包装数量
		/// </summary>
		/// <param name="OperationMarkName"></param>
		/// <returns></returns>
		public string GetPackAmount(int ChangeRatio ,int UnitAmount )
		{
			int tempvar1;
			int tempvar2;
			string packAmount;
			try
			{
				tempvar1=UnitAmount % ChangeRatio;
				tempvar2=UnitAmount / ChangeRatio;
				if(tempvar1==0)
					packAmount=tempvar2.ToString();
				else
					packAmount=(tempvar2+"/"+tempvar1).ToString();
				return packAmount ;
			}
			catch(Exception err)
			{
				throw new Exception("单位数量转换为包装数量失败！原因：\r\n" + err.Message,err);
			}
		}

		/// <summary>
		/// 单位数量转换为包装数量
		/// </summary>
		/// <param name="OperationMarkName"></param>
		/// <returns></returns>
		public string GetPackAmount(int ChangeRatio ,Decimal UnitAmount )
		{
			int tempvar1;
			int tempvar2;
			string packAmount;
			try
			{
				int iUnitAmount = Convert.ToInt32(UnitAmount);
				tempvar1=Convert.ToInt32(iUnitAmount % ChangeRatio);
				tempvar2=Convert.ToInt32(iUnitAmount / ChangeRatio);
				if(tempvar1==0)
					packAmount=tempvar2.ToString();
				else
					packAmount=(tempvar2+"/"+tempvar1).ToString();
				return packAmount ;
			}
			catch(Exception err)
			{
				throw new Exception("单位数量转换为包装数量失败！原因：\r\n" + err.Message,err);
			}
		}

		/// <summary>
		/// 包装数量转换为单位数量
		/// </summary>
		/// <param name="OperationMarkName"></param>
		/// <returns></returns>
		public  int GetUnitAmount(int ChangeRatio ,string PackAmount )
		{
			int tempvar1;
			int tempvar2;
			int pos;
			double unitAmount;
			try
			{
				pos = PackAmount.IndexOf("/");
				if(pos<0)
					unitAmount=Convert.ToDouble(PackAmount) * ChangeRatio;
				else
				{
					if(PackAmount.Substring(0,pos).ToString().Trim() == "")
						tempvar1 = 0;
					else
						tempvar1 = Convert.ToInt32(PackAmount.Substring(0,pos));
					tempvar2= Convert.ToInt32(PackAmount.Substring(pos+1,PackAmount.Length-pos-1));
					unitAmount = tempvar1 * ChangeRatio + tempvar2;
				}
				return (int)unitAmount ;
			}
			catch(Exception err)
			{
				throw new Exception("包装数量转换为单位数量失败！原因：\r\n" + err.Message,err);
			}
		}
		
		public string PackSubtraction(string minuend,string subtrahend,int changeRatio)
		{
			if(minuend == null||subtrahend == null)
				return null;
			string []min=minuend.Split('/');
			string []sub=subtrahend.Split('/');
			string packAmount=String.Empty;
			string unitAmount=String.Empty;
			string result=String.Empty;
			if(min.Length==1&&sub.Length==1)
			{
				packAmount=Convert.ToString(Convert.ToInt32(min[0])-Convert.ToInt32(sub[0]));
				result=packAmount;
			}
			if(min.Length==1&&sub.Length==2)
			{
				packAmount=Convert.ToString(Convert.ToInt32(min[0])-Convert.ToInt32(sub[0])-1);
				unitAmount=Convert.ToString(changeRatio-Convert.ToInt32(sub[1]));
				result=packAmount+"/"+unitAmount;
			}
			if(min.Length==2&&sub.Length==2)
			{
				packAmount=Convert.ToString(Convert.ToInt32(min[0])-Convert.ToInt32(sub[0]));
				unitAmount=Convert.ToString(Convert.ToInt32(min[1])-Convert.ToInt32(sub[1]));
				result=packAmount+"/"+unitAmount;
			}
			if(min.Length==2 && sub.Length==1)
			{
				packAmount=Convert.ToString(Convert.ToInt32(min[0])-Convert.ToInt32(sub[0]));
				unitAmount=min[1];
				result=packAmount+"/"+unitAmount;
			}
			return result;				
		}
	}
}

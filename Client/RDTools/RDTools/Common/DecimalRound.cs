using System;

namespace RDTools.Common
{
	public class DecimalRound
	{
		public DecimalRound()
		{
		}

		public static decimal Round(decimal d, int decimals)
		{
			decimal tenPow = Convert.ToDecimal(Math.Pow(10, decimals));
			decimal scrD = d * tenPow + 0.5m;
			return (Convert.ToDecimal(Math.Floor(Convert.ToDouble(scrD))) / tenPow);
		}

		public static double Round(double d, int decimals)
		{
			double tenPow = Convert.ToDouble(Math.Pow(10, decimals));
			double scrD = d * tenPow + 0.5d;
			return (Math.Floor(scrD) / tenPow);
		}

		/// <summary>
		/// ���С��λ�㹻��,Ҳ����Decimal.Round�������,����:
		/// Decimal.Round(1.25, 1) = 1.2
		/// ��������� Decimal.Round(1.251, 1) = 1.3
		/// ���Բ�������ķ���Ҳ�ܴﵽ���������Ч��
		/// </summary>
		/// <param name="d"></param>
		/// <param name="decimals"></param>
		/// <returns></returns>
		public static decimal MyDecimalRound(decimal d, int decimals)
		{
			d = d + 0.000000000000001m;
			return Decimal.Round(d, decimals);
		} 
	}
}

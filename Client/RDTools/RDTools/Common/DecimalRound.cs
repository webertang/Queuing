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
		/// 如果小数位足够长,也可以Decimal.Round这个方法,举例:
		/// Decimal.Round(1.25, 1) = 1.2
		/// 但是如果是 Decimal.Round(1.251, 1) = 1.3
		/// 所以采用下面的方法也能达到四舍五入的效果
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

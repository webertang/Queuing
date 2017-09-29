using System;
using System.Collections.Generic;

namespace RDTools.Common
{
	/// 类名：ChineseNum
	/// 功能：把金额数据从小写转换为汉字大写金额
	/// 限制条件：金额小于一万亿，且少于两位小数
    /// 用法：this.textBox2.Text = ChineseNum.ToChineseUpper(d);
	public class ChineseNum
	{
		public ChineseNum()
		{
	
        }

        public static string ToChineseUpper(decimal d)
        {
            if (d == 0)
                return "零元整";
			
            string je = d.ToString("####.00");
            if (je.Length > 15)
                return "";
            je = new String('0',15 - je.Length) + je;						//若小于15位长，前面补0

            string stry = je.Substring(0,4);								//取得'亿'单元
            string strw = je.Substring(4,4);								//取得'万'单元
            string strg = je.Substring(8,4);								//取得'元'单元
            string strf = je.Substring(13,2);								//取得小数部分
		
            string str1 = "",str2 = "",str3 = "";

            str1 = getupper(stry,"亿");								//亿单元的大写
            str2 = getupper(strw,"万");								//万单元的大写
            str3 = getupper(strg,"元");								//元单元的大写


            string str_y = "", str_w = "";									
            if (je[3] == '0' || je[4] == '0')								//亿和万之间是否有0
                str_y = "零";
            if (je[7] == '0' || je[8] == '0')								//万和元之间是否有0
                str_w = "零";



            string ret = str1 + str_y + str2 + str_w + str3;				//亿，万，元的三个大写合并

            for (int i = 0 ;i < ret.Length;i++)								//去掉前面的"零"			
            {
                if (ret[i] != '零')
                {
                    ret = ret.Substring(i);
                    break;
                }

            }
            for (int i = ret.Length - 1;i > -1 ;i--)						//去掉最后的"零"	
            {
                if (ret[i] != '零')
                {
                    ret = ret.Substring(0,i+1);
                    break;
                }
            }
			
            if (ret[ret.Length  - 1] != '元')								//若最后不位不是'元'，则加一个'元'字
                ret = ret + "元";

            if (ret == "零零元")											//若为零元，则去掉"元数"，结果只要小数部分
                ret = "";
			
            if (strf == "00")												//下面是小数部分的转换
            {
                ret = ret + "整";
            }
            else
            {
                string tmp = "";
                tmp = getint(strf[0]);
                if (tmp == "零")
                    ret = ret + tmp;
                else
                    ret = ret + tmp + "角";

                tmp = getint(strf[1]);
                if (tmp == "零")
                    ret = ret + "整";
                else
                    ret = ret + tmp + "分";
            }

            if (ret[0] == '零')
            {
                ret = ret.Substring(1);										//防止0.03转为"零叁分"，而直接转为"叁分"
            }

            return  ret;													//完成，返回

								
        }
        /// <summary>
        /// 把一个单元转为大写，如亿单元，万单元，个单元
        /// </summary>
        /// <param name="str">这个单元的小写数字（4位长，若不足，则前面补零）</param>
        /// <param name="strDW">亿，万，元</param>
        /// <returns>转换结果</returns>
        private static string getupper(string str,string strDW)
        {
            if (str == "0000")
                return "";

            string ret = "";
            string tmp1 = getint(str[0]) ;
            string tmp2 = getint(str[1]) ;
            string tmp3 = getint(str[2]) ;
            string tmp4 = getint(str[3]) ;
            if (tmp1 != "零")
            {
                ret = ret + tmp1 + "仟";
            }
            else
            {
                ret = ret + tmp1;
            }

            if (tmp2 != "零")
            {
                ret = ret + tmp2 + "佰";
            }
            else
            {
                if (tmp1 != "零")											//保证若有两个零'00'，结果只有一个零，下同
                    ret = ret + tmp2;
            }

            if (tmp3 != "零")
            {
                ret = ret + tmp3 + "拾";
            }
            else
            {
                if (tmp2 != "零")
                    ret = ret + tmp3;
            }

            if (tmp4 != "零")
            {
                ret = ret + tmp4 ;
            }
			
            if (ret[0] == '零')												//若第一个字符是'零'，则去掉
                ret = ret.Substring(1);
            if (ret[ret.Length - 1] == '零')								//若最后一个字符是'零'，则去掉
                ret = ret.Substring(0,ret.Length - 1);

            return ret + strDW;												//加上本单元的单位
			
        }
        /// <summary>
        /// 单个数字转为大写
        /// </summary>
        /// <param name="c">小写阿拉伯数字 0---9</param>
        /// <returns>大写数字</returns>
        private static string getint(char c)
        {
            string str = "";
            switch ( c )
            {
                case '0':
                    str = "零";
                    break;
                case '1':
                    str = "壹";
                    break;
                case '2':
                    str = "贰";
                    break;
                case '3':
                    str = "叁";
                    break;
                case '4':
                    str = "肆";
                    break;
                case '5':
                    str = "伍";
                    break;
                case '6':
                    str = "陆";
                    break;
                case '7':
                    str = "柒";
                    break;
                case '8':
                    str = "捌";
                    break;
                case '9':
                    str = "玖";
                    break;
            }
            return str;
        }

        /// <summary>
        /// 将阿拉伯数字转换为中文字符数组 
        /// </summary>
        /// <param name="num">待转的双精度阿拉伯数字</param>
        /// <returns>转换后的字符数组</returns>
        public static char[] ConvertNumToZhArray(double num)
        {
            List<string> listNum = new List<string>
            {
                "零","壹","贰","叁","肆","伍","陆","柒","捌","玖","拾"
            };

            string[] tmp = num.ToString().Split('.');
            char[] integerNum = tmp[0].ToCharArray();

            string value = string.Empty;

            for (int i = integerNum.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    value = (integerNum[i] == '-' ? "负" + value.PadLeft(6, '零') : " " + (listNum[Convert.ToInt32(integerNum[i].ToString())] + value).PadLeft(6, '零'));
                    break;
                }

                value = listNum[Convert.ToInt32(integerNum[i].ToString())] + value;
            }


            if (tmp.Length > 1)
            {
                char[] decimalNum = tmp[1].ToCharArray();

                for (int i = 0; i < (decimalNum.Length >= 2 ? 2 : decimalNum.Length); i++)
                {
                    value += listNum[Convert.ToInt32(decimalNum[i].ToString())];
                }
            }

            value = value.PadRight(9, '零');

            return value.ToCharArray();
        }
	}

	/// <summary>
	///0、不处理 1、四舍五入 2、见分进位 
	/// </summary>
	public class FormatMoney
	{
		public FormatMoney()
		{	
		}
		public static decimal GetFormatMoney(decimal money,string configure)
		{
			decimal formatMoney = 0;
			switch (configure)
			{
				case "0":
					formatMoney = Math.Round(money,2);
					break;
				case "1":
					formatMoney = Math.Round(money,1);
					break;
				case "2":
					formatMoney = Convert.ToDecimal(ToFormatMoney(money));
					break;
				default:
					formatMoney = Math.Round(money,2);
					break;
					
			}
			return formatMoney;
		}
		private static string ToFormatMoney(decimal money)
		{
			string str = money.ToString();
			string a = string.Empty;
			string c = string.Empty;
			int pos = 0;
			if(str.IndexOf(".") >= 0)
			{
				str = (money*10).ToString();
				pos = str.IndexOf(".");
				a = str.Substring(0,pos);
				if(pos>=0)
				{
					c = str.Substring(pos+1,str.Length-pos-1);
					if(Convert.ToDecimal(c)>0)
					{
						str = (Convert.ToDouble(a)/10+0.1).ToString();
					}
					else
					{
						str = (Convert.ToDouble(a)/10).ToString();
					}
				}
				else
				{
					str = (Convert.ToDouble(a)/10).ToString();
				}
			}
			return str;
		}
	}
}

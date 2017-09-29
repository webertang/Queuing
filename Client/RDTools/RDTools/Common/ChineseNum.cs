using System;
using System.Collections.Generic;

namespace RDTools.Common
{
	/// ������ChineseNum
	/// ���ܣ��ѽ�����ݴ�Сдת��Ϊ���ִ�д���
	/// �������������С��һ���ڣ���������λС��
    /// �÷���this.textBox2.Text = ChineseNum.ToChineseUpper(d);
	public class ChineseNum
	{
		public ChineseNum()
		{
	
        }

        public static string ToChineseUpper(decimal d)
        {
            if (d == 0)
                return "��Ԫ��";
			
            string je = d.ToString("####.00");
            if (je.Length > 15)
                return "";
            je = new String('0',15 - je.Length) + je;						//��С��15λ����ǰ�油0

            string stry = je.Substring(0,4);								//ȡ��'��'��Ԫ
            string strw = je.Substring(4,4);								//ȡ��'��'��Ԫ
            string strg = je.Substring(8,4);								//ȡ��'Ԫ'��Ԫ
            string strf = je.Substring(13,2);								//ȡ��С������
		
            string str1 = "",str2 = "",str3 = "";

            str1 = getupper(stry,"��");								//�ڵ�Ԫ�Ĵ�д
            str2 = getupper(strw,"��");								//��Ԫ�Ĵ�д
            str3 = getupper(strg,"Ԫ");								//Ԫ��Ԫ�Ĵ�д


            string str_y = "", str_w = "";									
            if (je[3] == '0' || je[4] == '0')								//�ں���֮���Ƿ���0
                str_y = "��";
            if (je[7] == '0' || je[8] == '0')								//���Ԫ֮���Ƿ���0
                str_w = "��";



            string ret = str1 + str_y + str2 + str_w + str3;				//�ڣ���Ԫ��������д�ϲ�

            for (int i = 0 ;i < ret.Length;i++)								//ȥ��ǰ���"��"			
            {
                if (ret[i] != '��')
                {
                    ret = ret.Substring(i);
                    break;
                }

            }
            for (int i = ret.Length - 1;i > -1 ;i--)						//ȥ������"��"	
            {
                if (ret[i] != '��')
                {
                    ret = ret.Substring(0,i+1);
                    break;
                }
            }
			
            if (ret[ret.Length  - 1] != 'Ԫ')								//�����λ����'Ԫ'�����һ��'Ԫ'��
                ret = ret + "Ԫ";

            if (ret == "����Ԫ")											//��Ϊ��Ԫ����ȥ��"Ԫ��"�����ֻҪС������
                ret = "";
			
            if (strf == "00")												//������С�����ֵ�ת��
            {
                ret = ret + "��";
            }
            else
            {
                string tmp = "";
                tmp = getint(strf[0]);
                if (tmp == "��")
                    ret = ret + tmp;
                else
                    ret = ret + tmp + "��";

                tmp = getint(strf[1]);
                if (tmp == "��")
                    ret = ret + "��";
                else
                    ret = ret + tmp + "��";
            }

            if (ret[0] == '��')
            {
                ret = ret.Substring(1);										//��ֹ0.03תΪ"������"����ֱ��תΪ"����"
            }

            return  ret;													//��ɣ�����

								
        }
        /// <summary>
        /// ��һ����ԪתΪ��д�����ڵ�Ԫ����Ԫ������Ԫ
        /// </summary>
        /// <param name="str">�����Ԫ��Сд���֣�4λ���������㣬��ǰ�油�㣩</param>
        /// <param name="strDW">�ڣ���Ԫ</param>
        /// <returns>ת�����</returns>
        private static string getupper(string str,string strDW)
        {
            if (str == "0000")
                return "";

            string ret = "";
            string tmp1 = getint(str[0]) ;
            string tmp2 = getint(str[1]) ;
            string tmp3 = getint(str[2]) ;
            string tmp4 = getint(str[3]) ;
            if (tmp1 != "��")
            {
                ret = ret + tmp1 + "Ǫ";
            }
            else
            {
                ret = ret + tmp1;
            }

            if (tmp2 != "��")
            {
                ret = ret + tmp2 + "��";
            }
            else
            {
                if (tmp1 != "��")											//��֤����������'00'�����ֻ��һ���㣬��ͬ
                    ret = ret + tmp2;
            }

            if (tmp3 != "��")
            {
                ret = ret + tmp3 + "ʰ";
            }
            else
            {
                if (tmp2 != "��")
                    ret = ret + tmp3;
            }

            if (tmp4 != "��")
            {
                ret = ret + tmp4 ;
            }
			
            if (ret[0] == '��')												//����һ���ַ���'��'����ȥ��
                ret = ret.Substring(1);
            if (ret[ret.Length - 1] == '��')								//�����һ���ַ���'��'����ȥ��
                ret = ret.Substring(0,ret.Length - 1);

            return ret + strDW;												//���ϱ���Ԫ�ĵ�λ
			
        }
        /// <summary>
        /// ��������תΪ��д
        /// </summary>
        /// <param name="c">Сд���������� 0---9</param>
        /// <returns>��д����</returns>
        private static string getint(char c)
        {
            string str = "";
            switch ( c )
            {
                case '0':
                    str = "��";
                    break;
                case '1':
                    str = "Ҽ";
                    break;
                case '2':
                    str = "��";
                    break;
                case '3':
                    str = "��";
                    break;
                case '4':
                    str = "��";
                    break;
                case '5':
                    str = "��";
                    break;
                case '6':
                    str = "½";
                    break;
                case '7':
                    str = "��";
                    break;
                case '8':
                    str = "��";
                    break;
                case '9':
                    str = "��";
                    break;
            }
            return str;
        }

        /// <summary>
        /// ������������ת��Ϊ�����ַ����� 
        /// </summary>
        /// <param name="num">��ת��˫���Ȱ���������</param>
        /// <returns>ת������ַ�����</returns>
        public static char[] ConvertNumToZhArray(double num)
        {
            List<string> listNum = new List<string>
            {
                "��","Ҽ","��","��","��","��","½","��","��","��","ʰ"
            };

            string[] tmp = num.ToString().Split('.');
            char[] integerNum = tmp[0].ToCharArray();

            string value = string.Empty;

            for (int i = integerNum.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    value = (integerNum[i] == '-' ? "��" + value.PadLeft(6, '��') : " " + (listNum[Convert.ToInt32(integerNum[i].ToString())] + value).PadLeft(6, '��'));
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

            value = value.PadRight(9, '��');

            return value.ToCharArray();
        }
	}

	/// <summary>
	///0�������� 1���������� 2�����ֽ�λ 
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

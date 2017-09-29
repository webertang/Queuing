using System;
using System.Runtime.InteropServices;

namespace RDTools.QuotedPrice
{
    /// <summary>
    /// 语音报价
    /// </summary>
    public class TDJCApplication
    {
        [DllImport("TdBjq.dll", EntryPoint = "dsbdll")]
        private static extern int TdBjq(int comPort, string input);

        public int dsbdll(int comPort, string input)
        {
            return TdBjq(comPort, input);
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="name">患者姓名</param>
        /// <param name="accounts">应收</param>
        /// <param name="paid">实收</param>
        //public void Charge(string name, decimal accounts, decimal paid)
        //{
        //    dsbdll("&Sc$");
        //    dsbdll("&C21  您好,请稍等!$");
        //    dsbdll("A1");
        //    dsbdll("&Sc$");
        //    dsbdll("&C11姓名：" + name + "$");
        //    dsbdll(accounts + "J");
        //    dsbdll(paid + "Y");
        //    dsbdll((paid - accounts) + "Z");
        //}


        //public void InitOperator()
        //{

        //}

        public void InitSystem(int comPort)
        {
            dsbdll(comPort, "&Sc$");
        }

        public void AccountPay(int comPort, decimal money)
        {
            dsbdll(comPort, "&Sc$");
            dsbdll(comPort, "&C11  你好,请付款,谢谢!$");
            dsbdll(comPort, money + "J");
        }

        public void CashPay(int comPort, decimal money)
        {
            dsbdll(comPort, "&Sc$");
            //dsbdll("&C21  收您" + money.ToString() + "元,谢谢!$");
            dsbdll(comPort, money + "Y");
        }

        public void DibsPay(int comPort, decimal money)
        {
            if (money > 0)
            {
                //dsbdll("&Sc$");
                //dsbdll("&C21  找零" + money.ToString() + "元,找零请当面点清、谢谢!$");
                dsbdll(comPort, money + "Z");
            }
            //else 
            //{
            //    dsbdll("&Sc$");
            //    dsbdll("&C21  谢谢!$");
            //    dsbdll("A1");
            //}
        }
    }
}

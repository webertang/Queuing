using System;

namespace RDTools.Common
{
    public class ValidateCode
    {
        public ValidateCode()
        {
        }
        
        /// <summary>
        /// 生成新的验证码5位数介于10000至99999之间
        /// </summary>
        /// <returns></returns>
        public string GenValidateCode()
        {
            Random random = new Random();
            int j = random.Next(10000, 99999);
            return j.ToString();
        }

        /// <summary>
        /// 生成新的验证码5位数介于10000至99999之间
        /// </summary>
        /// <param name="oldCode">如果卡号中有验证码调用时传入5位的验证码，如没有验证码传入string.Empty</param>
        /// <returns></returns>
        public string GenValidateCode(string oldCode)
        {
            int i = 0;
            if (int.TryParse(oldCode, out i) == false)
            {
                throw new Exception("无效的验证码！");
            }

            Random random = new Random();
            int j = random.Next(10000, 99999);
            while (i == j)
            {
                j = random.Next(10000, 99999);
            }
            return j.ToString();
        }
    }
}

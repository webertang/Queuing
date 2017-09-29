using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateJSONStrForKM
{
    #region Memo
    //presItemCode	项目明细编码	true	false
    //hospItemCode	医院项目Code	true	false
    //itemName	项目名称	true	false
    //spec	规格	false	false
    //manufacturer	生产厂家	false	True
    //sApvNO	注册号	false	true
    //unitPrice	单价	true	true
    //buyNum	数量	true	true
    //buyUnit	购买单位	true	true
    //medEncode	医保项目ID	true	true
    //isCharge	是否收费	true	true
    //freeReason	不收费原因	false	true
    //chargeTypeName	收费类型	false	true
    //chargesSubject	收费科目	false	true
    #endregion

    public class ItemBase
    {
        private string presItemCode = string.Empty;
        private string hospItemCode = string.Empty;
        private string itemName = string.Empty;
        private string spec = string.Empty;//
        private string manufacturer = string.Empty;//
        private string sApvNO = string.Empty;//
        private string unitPrice = string.Empty;
        private string buyNum = string.Empty;
        private string buyUnit = string.Empty;
        private string medEncode = string.Empty;
        private string isCharge = string.Empty;
        private string freeReason = string.Empty;//
        private string chargeTypeName = string.Empty;
        private string chargesSubject = string.Empty;

        public string PresItemCode
        {
            get { return presItemCode; }
            set { presItemCode = value ?? string.Empty; }
        }

        public string HospItemCode
        {
            get { return hospItemCode; }
            set { hospItemCode = value ?? string.Empty; }
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value ?? string.Empty; }
        }

        public string Spec
        {
            get { return spec; }
            set { spec = value ?? string.Empty; }
        }

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value ?? string.Empty; }
        }

        public string SApvNO
        {
            get { return sApvNO; }
            set { sApvNO = value ?? string.Empty; }
        }

        public string UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value ?? string.Empty; }
        }

        public string BuyNum
        {
            get { return buyNum; }
            set { buyNum = value ?? string.Empty; }
        }

        public string BuyUnit
        {
            get { return buyUnit; }
            set { buyUnit = value ?? string.Empty; }
        }

        public string MedEncode
        {
            get { return medEncode; }
            set { medEncode = value ?? string.Empty; }
        }

        public string IsCharge
        {
            get { return isCharge; }
            set { isCharge = value ?? string.Empty; }
        }

        public string FreeReason
        {
            get { return freeReason; }
            set { freeReason = value ?? string.Empty; }
        }

        public string ChargeTypeName
        {
            get { return chargeTypeName; }
            set { chargeTypeName = value ?? string.Empty; }
        }

        public string ChargesSubject
        {
            get { return chargesSubject; }
            set { chargesSubject = value ?? string.Empty; }
        }

        public string ConvertFunction()
        {
            return string.Format("\"presItemCode\":\"{0}\",\"hospItemCode\":\"{1}\",\"itemName\":\"{2}\"," +
                     "\"unitPrice\":\"{3}\",\"buyNum\":\"{4}\",\"buyUnit\":\"{5}\",\"medEncode\":\"{6}\"," +
                     "\"isCharge\":\"{7}\",\"chargeTypeName\":\"{8}\",\"chargesSubject\":\"{9}\"",
                     presItemCode, hospItemCode, itemName, unitPrice, buyNum, buyUnit, medEncode, isCharge,
                     chargeTypeName, chargesSubject);

        }
    }
}

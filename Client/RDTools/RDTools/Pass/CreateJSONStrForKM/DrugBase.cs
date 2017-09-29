using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateJSONStrForKM
{
    #region Memo
    //presDrugCode	处方明细编码	true	false
    //hospDrugCode	医院药品Code	true	false
    //drugName	药品名称	true	false
    //spec	规格	true	false
    //manufacturer	生产厂家	true	True
    //sApvNO	准字号	true	true
    //dosage	一次剂量	true	true
    //unit	一次单位	true	true
    //route	用药途径	true	true
    //frequence	频次	true	true
    //days	天数	true	true
    //unitPrice	单价	true	true
    //buyNum	发药数量	true	true
    //buyUnit	发药单位	true	true
    //groupId	注射药品配伍组号	true	true
    //medEncode	医保项目ID	true	true
    //isCharge	是否收费	true	true
    //freeReason	不收费原因	false	true
    //chargeTypeName	收费类型	false	true
    //chargesSubject	收费科目	false	true
    #endregion
    public class DrugBase
    {
        private string presDrugCode = string.Empty;
        private string hospDrugCode = string.Empty;
        private string drugName = string.Empty;
        private string spec = string.Empty;
        private string manufacturer = string.Empty;
        private string sApvNO = string.Empty;
        private string dosage = string.Empty;
        private string unit = string.Empty;
        private string route = string.Empty;
        private string frequence = string.Empty;
        private string days = string.Empty;
        private string unitPrice = string.Empty;
        private string buyNum = string.Empty;
        private string buyUnit = string.Empty;
        private string groupId = string.Empty;
        private string medEncode = string.Empty;
        private string isCharge = string.Empty;
        private string freeReason = string.Empty;
        private string chargeTypeName = string.Empty;
        private string chargesSubject = string.Empty;

        public string PresDrugCode
        {
            get { return presDrugCode; }
            set { presDrugCode = value ?? string.Empty; }
        }

        public string HospDrugCode
        {
            get { return hospDrugCode; }
            set { hospDrugCode = value ?? string.Empty; }
        }

        public string DrugName
        {
            get { return drugName; }
            set { drugName = value ?? string.Empty; }
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

        public string Dosage
        {
            get { return dosage; }
            set { dosage = value ?? string.Empty; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value ?? string.Empty; }
        }

        public string Route
        {
            get { return route; }
            set { route = value ?? string.Empty; }
        }

        public string Frequence
        {
            get { return frequence; }
            set { frequence = value ?? string.Empty; }
        }

        public string Days
        {
            get { return days; }
            set { days = value ?? string.Empty; }
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

        public string GroupId
        {
            get { return groupId; }
            set { groupId = value ?? string.Empty; }
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
            return string.Format("\"presDrugCode\":\"{0}\",\"groupId\":\"{1}\",\"hospDrugCode\":\"{2}\"," +
                                 "\"sApvNO\":\"{3}\",\"drugName\":\"{4}\",\"spec\":\"{5}\",\"dosage\":\"{6}\"," +
                                 "\"unit\":\"{7}\",\"unitPrice\":\"{8}\",\"route\":\"{9}\",\"frequence\":\"{10}\"," +
                                 "\"days\":\"{11}\",\"buyNum\":\"{12}\",\"buyUnit\":\"{13}\",\"manufacturer\":\"{14}\"," +
                                 "\"medEncode\":\"{15}\",\"isCharge\":\"{16}\",\"chargeTypeName\":\"{17}\",\"chargesSubject\":\"{18}\",\"freeReason\":\"{19}\"",
                                 presDrugCode, groupId, hospDrugCode, sApvNO, drugName, spec, dosage, unit, unitPrice, route,
                                 frequence, days, buyNum, buyUnit, manufacturer, medEncode, isCharge, chargeTypeName, chargesSubject, FreeReason);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDTools.Pass.CreateJSONStrForKM
{
    #region Memo
    //presChiMedCode	中草药明细编码	true	false
    //hospChiMedCode	中草药医院项目Code	true	false
    //chiMedName	中草药名称	true	false
    //isCHM	是否是饮片	true	false
    //spec	规格	false	false
    //manufacturer	生产厂家	false	True
    //sApvNO	准字号	false	true
    //dosage	一次剂量	true	true
    //unitPrice	单价	true	true
    //divNum	剂数	true	true
    //isCharge	是否收费	true	true
    //freeReason	不收费原因	false	true
    //itemTypeName	收费类型	false	true
    //chargesSubject	收费科目	false	true
    #endregion

    public class CMDrugBase
    {
        private string presChiMedCode = string.Empty;
        private string hospChiMedCode = string.Empty;
        private string chiMedName = string.Empty;
        private string isCHM = string.Empty;
        private string spec = string.Empty;
        private string manufacturer = string.Empty;
        private string sApvNO = string.Empty;
        private string dosage = string.Empty;
        private string unitPrice = string.Empty;
        private string divNum = string.Empty;
        private string isCharge = string.Empty;
        private string freeReason = string.Empty;
        private string chargeTypeName = string.Empty;
        private string chargesSubject = string.Empty;

        public string PresChiMedCode
        {
            get { return presChiMedCode; }
            set { presChiMedCode = value ?? string.Empty; }
        }

        public string HospChiMedCode
        {
            get { return hospChiMedCode; }
            set { hospChiMedCode = value ?? string.Empty; }
        }

        public string ChiMedName
        {
            get { return chiMedName; }
            set { chiMedName = value ?? string.Empty; }
        }

        public string IsCHM
        {
            get { return isCHM; }
            set { isCHM = value ?? string.Empty; }
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

        public string UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value ?? string.Empty; }
        }

        public string DivNum
        {
            get { return divNum; }
            set { divNum = value ?? string.Empty; }
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
            return string.Format("\"presChiMedCode\":\"{0}\",\"hospChiMedCode\":\"{1}\",\"chiMedName\":\"{2}\"," +
                     "\"dosage\":\"{3}\",\"unitPrice\":\"{4}\",\"divNum\":\"{5}\",\"isCharge\":\"{6}\"," +
                     "\"chargeTypeName\":\"{7}\",\"chargesSubject\":\"{8}\"",
                     presChiMedCode, hospChiMedCode, chiMedName, dosage, unitPrice, divNum, isCharge,
                     chargeTypeName, chargesSubject);

        }
    }
}

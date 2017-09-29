namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class PrescriptionHospitalized : PrescriptionBase
    {
        public PrescriptionHospitalized()
        {
            Medicine = new MedicineHospitalized();           
        }
        /// <summary>
        /// 医嘱类型（L——长期，T——临时）
        /// </summary>
        public override string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value == "T" ? value : "L";
            }
        }

        public MedicineHospitalized Medicine { get; set; }

        public override string ConvertFunction()
        {
            return string.Format("<prescription id='{0}' type='{1}' >{2}</prescription>", _id, _type, Medicine.ConvertFunction());
        }
    }
}

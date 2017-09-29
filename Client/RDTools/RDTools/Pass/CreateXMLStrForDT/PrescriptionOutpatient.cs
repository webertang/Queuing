namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class PrescriptionOutpatient : PrescriptionBase
    {
        public PrescriptionOutpatient()
        {
            Medicine = new MedicineOutpatient();
            Type = "";
        }

        private int _current = 1;

        public override string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = "mz";
            }
        }

        /// <summary>
        /// 1是当前处方，其余都是历史处方
        /// </summary>
        public int Current
        {
            get { return _current; }
            set { _current = value == 1 ? 1 : 0; }
        }

        public MedicineOutpatient Medicine { get; set; }

        public override string ConvertFunction()
        {
            return string.Format("<prescription id='{0}' type='{1}' current='{2}'>{3}</prescription>", _id, _type, _current, Medicine.ConvertFunction());
        }
    }
}

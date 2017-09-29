namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public abstract class PrescriptionBase
    {
        protected string _id = string.Empty;
        protected string _type = string.Empty;

        /// <summary>
        /// 处方号，医嘱号
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value ?? string.Empty; }
        }

        public abstract string Type
        {
            get;
            set;
        }

        public abstract string ConvertFunction();
    }
}

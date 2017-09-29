namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class MedicineOutpatient : MedicineBase
    {
        private string _days = string.Empty;

        /// <summary>
        /// 天数
        /// </summary>
        public int? Days
        {
            get
            {
                if (string.IsNullOrEmpty(_days))
                {
                    return null;
                }

                return int.Parse(_days);
            }
            set
            {
                _days = value.HasValue ? value.Value.ToString() : string.Empty;
            }
        }

        public override string ConvertFunction()
        {
            return string.Format("<medicine suspension='{0}' judge='{1}' ><group_number>{2}</group_number><general_name>{3}</general_name>"
                + "<license_number>{4}</license_number><medicine_name>{5}</medicine_name><single_dose coef='{6}'>{7}</single_dose><times>{8}</times>"
                + "<days>{9}</days><unit>{10}</unit><administer_drugs>{11}</administer_drugs></medicine>", _suspension, _judge, _group_number, _general_name, _license_number,
                _medicine_name, _coef, _single_dose, _times, _days, _unit, _administer_drugs);
        }
    }
}

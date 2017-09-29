using System;

namespace RD.Pass.CreateXML
{
    /// <summary>
    /// 大通pass创建xml
    /// </summary>
    public class MedicineHospitalized : MedicineBase
    {
        private string _frequency = string.Empty;
        private string _begin_time = string.Empty;
        private string _end_time = string.Empty;
        private string _prescription_time = string.Empty;

        /// <summary>
        /// 频次代码
        /// </summary>
        public string Frequency
        {
            get { return _frequency; }
            set { _frequency = value ?? string.Empty; }
        }

        /// <summary>
        /// 用药开始时间
        /// </summary>
        public DateTime? Begin_time
        {
            get
            {
                if (string.IsNullOrEmpty(_begin_time))
                {
                    return null;
                }

                return Convert.ToDateTime(_begin_time);
            }
            set
            {
                _begin_time = value.HasValue ? _begin_time = value.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            }
        }

        /// <summary>
        /// 用药结束时间
        /// </summary>
        public DateTime? End_time
        {
            get
            {
                if (string.IsNullOrEmpty(_end_time))
                {
                    return null;
                }

                return Convert.ToDateTime(_end_time);
            }
            set
            {
                _end_time = value.HasValue ? _end_time = value.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            }
        }

        /// <summary>
        /// 医嘱时间
        /// </summary>
        public DateTime? Prescription_time
        {
            get
            {
                if (string.IsNullOrEmpty(_prescription_time))
                {
                    return null;
                }

                return Convert.ToDateTime(_prescription_time);
            }
            set
            {
                _prescription_time = value.HasValue ? _prescription_time = value.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            }
        }

        public override string ConvertFunction()
        {
            return string.Format("<medicine suspension='{0}' judge='{1}' ><group_number>{2}</group_number><general_name>{3}</general_name>"
                 + "<license_number>{4}</license_number><medicine_name>{5}</medicine_name><single_dose coef='{6}'>{7}</single_dose><frequency>{8}</frequency><times>{9}</times>"
                 + "<unit>{10}</unit><administer_drugs>{11}</administer_drugs><begin_time>{12}</begin_time><end_time>{13}</end_time><prescription_time>{14}</prescription_time></medicine>", _suspension, _judge, _group_number, _general_name, _license_number,
                _medicine_name, _coef, _single_dose, _frequency, _times, _unit, _administer_drugs, _begin_time, _end_time, _prescription_time);
        }
    }
}

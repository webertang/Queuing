using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RD.Pass
{
    /// <summary>
    /// 合理用药——大通
    /// </summary>
    public class PassFunctionForDT
    {
        public PassFunctionForDT()
        { }

        /// <summary>
        /// 大通Pass初始化
        /// </summary>
        /// <returns>0_表示服务未运行；1_表示服务正在运行中</returns>
        public static int InitializeDT()
        {
            //string getVal = ReadIni("interface", "interface_ver", ".\\dtver.ini");
            //if (getVal != "2.6")
            //{
            //    return -1;
            //}

            int i = DllDynamic.dtywzxUI(0, 0, "");
            return i;
        }

        /// <summary>
        /// 记录医生工号
        /// </summary>
        /// <param name="_doctorID">医生工号</param>
        /// <returns></returns>
        public static int RecordDoctorID(string _doctorID)
        {
            int i = DllDynamic.dtywzxUI(768, 0, _doctorID);
            return i;
        }

        /// <summary>
        /// 新处方开始/新医嘱开始
        /// </summary>
        /// <returns></returns>
        public static int BeginNew()
        {
            int i = DllDynamic.dtywzxUI(3, 0, "");
            return i;
        }

        /// <summary>
        /// 显示要点提示
        /// </summary>
        /// <param name="_points"></param>
        /// <returns></returns>
        public static int PointstoNote(string _StrPoints)
        {
            int i = DllDynamic.dtywzxUI(12, 0, _StrPoints);
            return i;
        }

        /// <summary>
        /// 显示要点提示重载
        /// </summary>
        /// <param name="_LEECHDOMName">药品名称</param>
        /// <param name="_LEECHDOMNO">药品代码</param>
        /// <returns></returns>
        public static int PointstoNote(string _LEECHDOMName, string _LEECHDOMNO)
        {
            string _StrPoints = "<safe><general_name>" + _LEECHDOMName + "</general_name><license_number>" + _LEECHDOMNO + "</license_number><type>0</type></safe>";
            int i = PointstoNote(_StrPoints);
            return i;
        }

        /// <summary>
        /// 重新显示要点提示
        /// </summary>
        /// <param name="_points"></param>
        /// <returns></returns>
        public static int RePointstoNote(string _StrPoints)
        {
            int i = DllDynamic.dtywzxUI(4108, 0, _StrPoints);
            return i;
        }

        /// <summary>
        /// 重新显示要点提示重载
        /// </summary>
        /// <param name="_LEECHDOMName">药品名称</param>
        /// <param name="_LEECHDOMNO">药品代码</param>
        /// <returns></returns>
        public static int RePointstoNote(string _LEECHDOMName, string _LEECHDOMNO)
        {
            string _StrPoints = "<safe><general_name>" + _LEECHDOMName + "</general_name><license_number>" + _LEECHDOMNO + "</license_number></safe>";
            int i = RePointstoNote(_StrPoints);
            return i;
        }

        /// <summary>
        /// 门诊处方配伍分析
        /// </summary>
        /// <param name="_BufferXML">XML</param>
        /// <returns></returns>
        public static int ClinicPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.dtywzxUI(4,0, _BufferXML);
            return i;
        }

        /// <summary>
        /// 住院医嘱配伍分析
        /// </summary>
        /// <param name="_BufferXML">XML</param>
        /// <returns></returns>
        public static int InpatientPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.dtywzxUI(28676, 1, _BufferXML);
            return i;
        }

        /// <summary>
        /// 保存门诊处方配伍分析
        /// </summary>
        /// <param name="_BufferXML">XML</param>
        /// <returns></returns>
        public static int Save_ClinicPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.dtywzxUI(13, 0, _BufferXML);
            return i;
        }

        /// <summary>
        /// 保存住院医嘱配伍分析
        /// </summary>
        /// <param name="_BufferXML">XML</param>
        /// <returns></returns>
        public static int Save_InpatientPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.dtywzxUI(28685, 1, _BufferXML);
            return i;
        }

        /// <summary>
        /// 保存XML（检查参数使用）
        /// </summary>
        /// <param name="_BufferXML">XML</param>
        /// <returns></returns>
        public static int Save_PrescriptionXML(string _BufferXML)
        {
            int i = DllDynamic.dtywzxUI(4109, 0, _BufferXML);
            return i;
        }

        /// <summary>
        /// 退出大通Pass
        /// </summary>
        /// <returns></returns>
        public static int ExitDT()
        {
            int i = DllDynamic.dtywzxUI(1, 0, "");
            return i;
        }

     

        /// <summary>
        ///    //读ini
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string ReadIni(string section, string key, string filePath)
        {
            StringBuilder temp = new StringBuilder(500);
            DllDynamic.GetPrivateProfileString(section, key, "", temp, 500, filePath);
            return temp.ToString();

        }
    }
}

using RD.Pass;
using System;
using System.Text;

namespace RD.Pass
{
    /// <summary>
    /// 创泽合理用药
    /// </summary>
    public class PassFunctionForCZ
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static int InitializeCZ(int Handle)
        {
            int i = DllDynamic.VanInit(Handle, "0000");
            return i;
        }

        /// <summary>
        /// 记录医生工号
        /// </summary>
        /// <param name="_doctorID"></param>
        /// <returns></returns>
        public static int RecordDoctorID(string _doctorID)
        {
            int i = DllDynamic.VanGetDoctor(_doctorID);
            return i;
        }

        /// <summary>
        /// 清空分析结果
        /// </summary>
        /// <returns></returns>
        public static int BeginNew()
        {
            int i = DllDynamic.VanInitRec();
            return i;
        }

        /// <summary>
        /// 单个药品要点提示
        /// </summary>
        /// <param name="_StrPoints"></param>
        /// <returns></returns>
        public static int PointstoNote(string DrugCode)
        {
            int i = DllDynamic.VanPointHint(DrugCode);
            return i;
        }

        /// <summary>
        /// 获取指定药品的详细说明书
        /// </summary>
        /// <param name="_StrPoints"></param>
        /// <returns></returns>
        public static int RePointstoNote(string DrugCode)
        {
            int i = DllDynamic.VanGetManaul(DrugCode);
            return i;
        }

        /// <summary>
        /// 门诊处方分析
        /// </summary>
        /// <param name="_BufferXML"></param>
        /// <returns></returns>
        public static int ClinicPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.VanAnaData("1", "0", _BufferXML);
            return i;
        }

        /// <summary>
        /// 住院处方分析
        /// </summary>
        /// <param name="_BufferXML"></param>
        /// <returns></returns>
        public static int InpatientPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.VanAnaData("2", "0", _BufferXML);
            return i;
        }

        /// <summary>
        /// 保存门诊处方分析
        /// </summary>
        /// <param name="_BufferXML"></param>
        /// <returns></returns>
        public static int Save_ClinicPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.VanAnaData("1", "1", _BufferXML);
            return i;
        }

        /// <summary>
        /// 保存住院处方分析
        /// </summary>
        /// <param name="_BufferXML"></param>
        /// <returns></returns>
        public static int Save_InpatientPrescription_Analysis(string _BufferXML)
        {
            int i = DllDynamic.VanAnaData("2", "1", _BufferXML);
            return i;
        }

        /// <summary>
        /// 取消门诊之前分析的结果
        /// </summary>
        /// <param name="PreType"></param>
        /// <param name="PreNo"></param>
        /// <returns></returns>
        public static int ClinicVanCancel(string PreType, string PreNo)
        {
            int i = DllDynamic.VanCancel("1", PreNo);
            return i;
        }

        /// <summary>
        /// 取消住院之前分析的结果
        /// </summary>
        /// <param name="PreType"></param>
        /// <param name="PreNo"></param>
        /// <returns></returns>
        public static int InpatientVanCancel(string PreType, string PreNo)
        {
            int i = DllDynamic.VanCancel("2", PreNo);
            return i;
        }

        /// <summary>
        /// 接口退出及释放资源
        /// </summary>
        /// <returns></returns>
        public static int ExitCZ()
        {
            int i = DllDynamic.VanExit();
            return i;
        }
    }


}

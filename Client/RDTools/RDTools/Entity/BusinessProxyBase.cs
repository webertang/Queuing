using System;
using System.Collections;
using System.Data;

namespace RDTools.Entity
{
    /// <summary>
    /// Facade基类
    /// </summary>
    public class BusinessProxyBase : MarshalByRefObject
    {
        #region Exec

        /// <summary>
        /// 执行任意sql语句返回DataSet
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>结果集</returns>
        public DataSet ExecuteDataset(string sqlText, int? otherConnectionStringIndex)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecuteDataset(sqlText, otherConnectionStringIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行任意sql语句返回DataSet
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <returns>结果集</returns>
        public DataSet ExecuteDataset(string sqlText)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecuteDataset(sqlText);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行任意sql语句返回受影响行数
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string sqlText, int? otherConnectionStringIndex)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecuteNonQuery(sqlText, otherConnectionStringIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行任意sql语句返回受影响行数
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string sqlText)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecuteNonQuery(sqlText);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="keys">参数名</param>
        /// <param name="inParamList">输入参数值</param>
        /// <param name="outParamList">输出参数值</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecStoredProcDataSet(string storedProc, Hashtable keys, Hashtable inParamList, Hashtable outParamList, int? otherConnectionStringIndex)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcDataSet(storedProc, keys, inParamList, outParamList, otherConnectionStringIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="keys">参数名</param>
        /// <param name="inParamList">输入参数值</param>
        /// <param name="outParamList">输出参数值</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecStoredProcDataSet(string storedProc, Hashtable keys, Hashtable inParamList, Hashtable outParamList)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcDataSet(storedProc, keys, inParamList, outParamList);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecStoredProcDataSet(string storedProc)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcDataSet(storedProc);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecStoredProcDataSet(string storedProc, int? otherConnectionStringIndex)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcDataSet(storedProc, otherConnectionStringIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="keys">参数名</param>
        /// <param name="inParamList">输入参数值</param>
        /// <param name="outParamList">输出参数值</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>受影响行数</returns>
        public int ExecStoredProcNonQuery(string storedProc, ArrayList keys, Hashtable inParamList, Hashtable outParamList, int? otherConnectionStringIndex)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcNonQuery(storedProc, keys, inParamList, outParamList, otherConnectionStringIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <returns>受影响行数</returns>
        public int ExecStoredProcNonQuery(string storedProc)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcNonQuery(storedProc);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="keys">参数名</param>
        /// <param name="inParamList">输入参数值</param>
        /// <param name="outParamList">输出参数值</param>
        /// <returns>受影响行数</returns>
        public int ExecStoredProcNonQuery(string storedProc, ArrayList keys, Hashtable inParamList, Hashtable outParamList)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcNonQuery(storedProc, keys, inParamList, outParamList);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>受影响行数</returns>
        public int ExecStoredProcNonQuery(string storedProc, int? otherConnectionStringIndex)
        {
            try
            {
                BusinessRulesBase rule = new BusinessRulesBase();
                return rule.ExecStoredProcNonQuery(storedProc, otherConnectionStringIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        #endregion Exec
    }
}

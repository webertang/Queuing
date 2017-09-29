using System;
using System.Collections;
using System.Data;
using RD.Data;

namespace RDTools.Entity
{
    /// <summary>
    /// Rules基类
    /// </summary>
    public class BusinessRulesBase
    {
        /// <summary>
        /// 获取数据库操作类实例
        /// </summary>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>数据库操作实例</returns>
        public IDataAccess GetDataAccessByConnectionStringIndex(int? otherConnectionStringIndex)
        {
            return DataAccessFactory.instance.CreateDataAccess();
        }

        /// <summary>
        /// 执行任意sql语句返回DataSet
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>结果集</returns>
        public DataSet ExecuteDataset(string sqlText,int? otherConnectionStringIndex)
        {
            IDataAccess data = GetDataAccessByConnectionStringIndex(otherConnectionStringIndex);

            try
            {
                return data.ExecuteDataset(CommandType.Text, sqlText);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 执行任意sql语句返回DataSet
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <returns>结果集</returns>
        public DataSet ExecuteDataset(string sqlText)
        {
            return ExecuteDataset(sqlText,null);
        }

        /// <summary>
        /// 执行任意sql语句返回受影响行数
        /// </summary>
        /// <param name="sqlText">TSQL语句或存储过程名</param>
        /// <param name="isProcedure">是否是存储过程</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string sqlText,int? otherConnectionStringIndex)
        {
            IDataAccess data = GetDataAccessByConnectionStringIndex(otherConnectionStringIndex);

            data.Open();
            //SingleTransaction tran = new SingleTransaction(data);
            //tran.Begin();

            try
            {
                int i = data.ExecuteNonQuery(CommandType.Text, sqlText);

                //tran.Commit();
                return i;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
            finally
            {
                data.Close();
            }
        }

        /// <summary>
        /// 执行任意sql语句返回受影响行数
        /// </summary>
        /// <param name="sqlText">TSQL语句</param>
        /// <returns>受影响行数</returns>
        public int ExecuteNonQuery(string sqlText)
        {
            return ExecuteNonQuery(sqlText,null);
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
            IDataAccess data = GetDataAccessByConnectionStringIndex(otherConnectionStringIndex);

            data.Open();

            try
            {
                DataSet ds = new DataSet();
                if (keys == null)
                {
                    data.ExecuteDataset(CommandType.StoredProcedure, storedProc,ds);
                }
                else
                {
                    QueryParameterCollection qpc = new QueryParameterCollection(inParamList.Count + outParamList.Count);
                    QueryParameter qp = null;
                    //IEnumerator keyEnum = keys.GetEnumerator();
                    foreach (DictionaryEntry de in keys)
                    {
                        //Console.WriteLine("Key = {0}, Value = {1}", de.Key, de.Value);
                        if (inParamList[de.Key] != null)
                        {
                            qp = new QueryParameter(de.Key.ToString(), inParamList[de.Key],de.Value.ToString(), ParameterDirection.Input);
                        }
                        else
                        {
                            //MSSQL2000的存储过程Output参数必须未int型，才能配合return返回；
                            qp = new QueryParameter(de.Key.ToString(), outParamList[de.Key],de.Value.ToString(), ParameterDirection.Output);
                        }
                        qpc.Add(qp);
                    }

                    //while (keyEnum.MoveNext())
                    //{
                    //    if (inParamList[keyEnum.Current] != null)
                    //    {
                    //        qp = new QueryParameter(keyEnum.Current.ToString(), inParamList[keyEnum.Current], "NVARCHAR", ParameterDirection.Input);
                    //    }
                    //    else
                    //    {
                    //        //MSSQL2000的存储过程Output参数必须未int型，才能配合return返回；
                    //        qp = new QueryParameter(keyEnum.Current.ToString(), outParamList[keyEnum.Current], "INTEGER", ParameterDirection.Output);
                    //    }
                    //    qpc.Add(qp);
                    //}
                   
                    data.ExecuteDataset(CommandType.StoredProcedure, storedProc, qpc, ds);
                    foreach (QueryParameter qryParam in qpc)
                    {
                        if (outParamList[qryParam.ParameterName] != null)
                        {
                            outParamList[qryParam.ParameterName] = qryParam.Value;
                        }
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                data.Close();
            }
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecStoredProcDataSet(string storedProc)
        {
            return ExecStoredProcDataSet(storedProc, null, null, null, null);
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
            return ExecStoredProcDataSet(storedProc, keys, inParamList, outParamList, null);
        }

        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>返回DataSet</returns>
        public DataSet ExecStoredProcDataSet(string storedProc, int? otherConnectionStringIndex)
        {
            return ExecStoredProcDataSet(storedProc, null, null, null, otherConnectionStringIndex);
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
            IDataAccess data = GetDataAccessByConnectionStringIndex(otherConnectionStringIndex);

            data.Open();

            try
            {
                int returnValue = 0;

                if (keys == null)
                {
                    returnValue = data.ExecuteNonQuery(CommandType.StoredProcedure, storedProc);
                }
                else
                {
                    QueryParameterCollection qpc = new QueryParameterCollection(inParamList.Count + outParamList.Count);
                    QueryParameter qp = null;
                    IEnumerator keyEnum = keys.GetEnumerator();

                    while (keyEnum.MoveNext())
                    {
                        if (inParamList[keyEnum.Current] != null)
                        {
                            qp = new QueryParameter(keyEnum.Current.ToString(), inParamList[keyEnum.Current]);
                        }
                        else
                        {
                            qp = new QueryParameter(keyEnum.Current.ToString(), outParamList[keyEnum.Current], outParamList[keyEnum.Current].ToString(), ParameterDirection.Output);
                        }
                        qpc.Add(qp);
                    }

                    returnValue = data.ExecuteNonQuery(CommandType.StoredProcedure, storedProc, qpc);

                    foreach (QueryParameter qryParam in qpc)
                    {
                        if (outParamList[qryParam.ParameterName] != null)
                        {
                            outParamList[qryParam.ParameterName] = qryParam.Value;
                        }
                    }
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                data.Close();
            }
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <returns>受影响行数</returns>
        public int ExecStoredProcNonQuery(string storedProc)
        {
            return ExecStoredProcNonQuery(storedProc, null, null, null, null);
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
            return ExecStoredProcNonQuery(storedProc, keys, inParamList, outParamList, null);
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <param name="storedProc">存储过程名</param>
        /// <param name="otherConnectionStringIndex">副数据库连接串Index</param>
        /// <returns>受影响行数</returns>
        public int ExecStoredProcNonQuery(string storedProc, int? otherConnectionStringIndex)
        {
            return ExecStoredProcNonQuery(storedProc, null, null, null, otherConnectionStringIndex);
        }
    }
}

using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DBExecute
{
    public class DBAct
    {
        /// <summary>
        ///     连接字符串
        /// </summary>
        public static string ConnectionStr { get; private set; }

        /// <summary>
        ///     连接对象
        /// </summary>
        public static OracleConnection OrclConnect { get; private set; }

        public static DBAct InitDBAct(string connectstring)
        {
            ConnectionStr = connectstring;
            return new DBAct();
        }
        /// <summary>
        ///     创建并打开connection
        /// </summary>
        private OracleConnection ConnenctOpen()
        {
            if (!string.IsNullOrEmpty(ConnectionStr))
            {
                try
                {
                    if (OrclConnect == null)
                    {
                        var orclcon = new OracleConnection(ConnectionStr);
                        OrclConnect = orclcon;
                    }

                    if (OrclConnect.State != ConnectionState.Open) OrclConnect.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return OrclConnect;
            }

            throw new Exception("数据库连接字符串未设置！");
        }

        /// <summary>
        ///     查询 返回dataset
        /// </summary>
        /// <param name="SQL">查询sql</param>
        /// <returns>dataset</returns>
        public DataSet DBSelectDS(string SQL)
        {
            var dataSet = new DataSet();
            using (OrclConnect = ConnenctOpen())
            {
                try
                {
                    var cmd = new OracleCommand(SQL, OrclConnect);
                    var oda = new OracleDataAdapter();
                    oda.SelectCommand = cmd;
                    oda.Fill(dataSet);
                    OrclConnect.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return dataSet;
        }

        /// <summary>
        ///     查询 返回datatable
        /// </summary>
        /// <param name="SQL">查询sql</param>
        /// <returns>datatable</returns>
        public DataTable DBSelectDT(string SQL)
        {
            var dataSet = new DataSet();
            using (OrclConnect = ConnenctOpen())
            {
                try
                {
                    var cmd = new OracleCommand(SQL, OrclConnect);
                    var oda = new OracleDataAdapter();
                    oda.SelectCommand = cmd;
                    oda.Fill(dataSet);
                    OrclConnect.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            var datatable = new DataTable();
            datatable = dataSet.Tables.Count > 0 ? dataSet.Tables[0] : datatable;
            return datatable;
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="SQL">更新SQL语句</param>
        /// <returns>更新条数</returns>
        public int DBUpdata(string SQL)
        {
            var num = 0;
            using (OrclConnect = ConnenctOpen())
            {
                try
                {
                    var cmd = new OracleCommand(SQL, OrclConnect);
                    num = cmd.ExecuteNonQuery();
                    OrclConnect.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return num;
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="SQL">插入SQL语句</param>
        /// <returns>拆入条数</returns>
        public int DBInsert(string SQL)
        {
            var num = 0;
            using (OrclConnect = ConnenctOpen())
            {
                try
                {
                    var cmd = new OracleCommand(SQL, OrclConnect);
                    num = cmd.ExecuteNonQuery();
                    OrclConnect.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return num;
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="SQL">删除SQL语句</param>
        /// <returns>删除条数</returns>
        public int DBDelete(string SQL)
        {
            var num = 0;
            using (OrclConnect = ConnenctOpen())
            {
                try
                {
                    var cmd = new OracleCommand(SQL, OrclConnect);
                    num = cmd.ExecuteNonQuery();
                    OrclConnect.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return num;
        }

        /// <summary>
        ///     执行分页的存储过程
        /// </summary>
        /// <param name="tup">1:datasql 2:pagesize页大小 3:curpage当前页</param>
        /// <returns>1.curpage当前页 2.pagenum页数 3.总条数 4.分页数据表</returns>
        public Tuple<int, int, int, DataTable> DBProcPage(Tuple<string, int, int> tup)
        {
            using (OrclConnect = ConnenctOpen())
            {
                var pagenum = 0;
                var numcount = 0;
                var dt = new DataTable();
                try
                {
                    var cmd = new OracleCommand("Pager", OrclConnect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter[] pars =
                    {
                        new OracleParameter
                            ("datasql", OracleDbType.Varchar2, ParameterDirection.Input),
                        new OracleParameter
                            ("pagesize", OracleDbType.Int32, ParameterDirection.Input),
                        new OracleParameter
                            ("currtpage", OracleDbType.Int32, ParameterDirection.Input),
                        new OracleParameter("pagenum", OracleDbType.Int32, ParameterDirection.Output),
                        new OracleParameter("numcount", OracleDbType.Int32, ParameterDirection.Output),
                        new OracleParameter("v_cur", OracleDbType.RefCursor, ParameterDirection.Output)
                    };
                    pars[0].Value = tup.Item1; //datasql
                    pars[1].Value = tup.Item2; //pagesize 页大小
                    pars[2].Value = tup.Item3; //curpage 当前页
                    cmd.Parameters.AddRange(pars);
                    cmd.ExecuteNonQuery();

                    foreach (OracleParameter parameter in cmd.Parameters)
                    {
                        if (parameter.ParameterName == "pagenum") pagenum = int.Parse(parameter.Value.ToString());
                        if (parameter.ParameterName == "numcount") numcount = int.Parse(parameter.Value.ToString());
                        if (parameter.ParameterName == "v_cur") dt = parameter.Value as DataTable;
                    }

                    OrclConnect.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return new Tuple<int, int, int, DataTable>(tup.Item3, pagenum, numcount, dt);
            }
        }
    }
}
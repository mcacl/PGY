using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace PGYShopingSystem.Common
{
    public class ComProcParam
    {
        public string ProcName { get; set; }
        public ComOracleParam[] Param { get; set; }
        public bool IsRetTable { get; set; }

        public OracleParameter[] GetOracleParam()
        {
            List<OracleParameter> oplist = new List<OracleParameter>();
            if (this.Param != null && Param.Length > 0)
            {
                foreach (ComOracleParam comOracleParam in this.Param)
                {
                    OracleDbType oracledbtype = OracleDbType.Varchar2;
                    switch (comOracleParam.DbType)
                    {
                        case "int32":
                            oracledbtype = OracleDbType.Int32; break;
                        case "double":
                            oracledbtype = OracleDbType.Double; break;
                        case "cursor":
                            oracledbtype = OracleDbType.RefCursor;
                            break;
                        case "date":
                            oracledbtype = OracleDbType.Date;
                            break;
                    }
                    ParameterDirection oracledirection = ParameterDirection.Input;
                    switch (comOracleParam.Direction)
                    {
                        case "out":
                            oracledirection = ParameterDirection.Output; break;
                        case "inout":
                            oracledirection = ParameterDirection.InputOutput; break;
                        case "return":
                            oracledirection = ParameterDirection.ReturnValue; break;
                    }
                    OracleParameter oracleparam = new OracleParameter(comOracleParam.ParamName, oracledbtype, oracledirection);
                    oracleparam.Value = comOracleParam.Value;
                    oplist.Add(oracleparam);
                }
            }
            return oplist.ToArray();
        }
    }

    public class ComOracleParam
    {
        /// <summary>
        /// proc参数名称
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// proc参数值的类型int32.double.cursor.date 默认varchar2
        /// </summary>
        public string DbType { get; set; }
        /// <summary>
        /// proc参数类型out输出inout输入输出return返回值
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }
    }
}
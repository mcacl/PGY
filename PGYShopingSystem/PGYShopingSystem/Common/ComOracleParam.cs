using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace PGYShopingSystem.Common
{
    public class ComProcParam
    {
        public string ProcName { get; set; }
        public OracleParameter[] Param { get; set; }
        public bool IsRetTable { get; set; }
    }

    public class ComOracleParam
    {
        public string ParamName { get; set; }
         
    }
}
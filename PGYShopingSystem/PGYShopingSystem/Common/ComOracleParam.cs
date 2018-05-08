using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace PGYShopingSystem.Common
{
    public class ComOracleParam
    {
        public string ProcName { get; set; }
        public OracleParameter[] Param { get; set; }
        public bool IsReturnTable { get; set; }
    }
}
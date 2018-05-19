using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExecute
{
    public class DBTypeEnum
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DBType
        {
            [Description("未知数据库")]
            NoKnow = 0,
            [Description("Oracle数据库")]
            Oracle = 1,
            [Description("SQLServer数据库")]
            SqlServer = 2,
            [Description("Access数据库")]
            Access = 3,
            [Description("Sqllite数据库")]
            Sqlite = 4
        }
    }
}

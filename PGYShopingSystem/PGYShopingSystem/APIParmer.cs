using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using PGYShopingSystem.Common;

namespace PGYShopingSystem
{
    public class APIParmer
    {
        public int Type { get; set; }

        public object Data { get; set; }

        public object GetAct()
        {
            var type = (ComEnum.ActEnum)Type;
            object obj = null;
            Data = ComTool.CustomDecryption(Data.ToString(), ComWebSetting.AppSetingConfig["Key"], true, Data.ToString().Length % (ComWebSetting.KEY.Length % 5));
            var SQl = Data.ToString().Replace("~", "\\\"").Replace("^", "\\\'");//~双引号 ^单引号
            switch (type)
            {
                case ComEnum.ActEnum.Select:
                    //查尽量带t {"Type":"1","Data":"{'tablename':'xs_zdjbxx t','fields':'zl,ysdm','WhereT':'t.zddm=^610821104200GB00039^','Orderby':'t.bsm'}"}
                    obj = JsonConvert.DeserializeObject<ActSelect>(SQl);
                    break;
                case ComEnum.ActEnum.Insert:
                    //增values不加引号 {"Type":"2","Data":"{'tablename':'xs_zdjbxx','fields':'zl,ysdm','values':'神木县大柳塔镇光明路66666,666666'}"}
                    obj = JsonConvert.DeserializeObject<ActInsert>(SQl);
                    break;
                case ComEnum.ActEnum.Update:
                    //更新values不加引号 {"Type":"3","Data":"{'tablename':'xs_zdjbxx t','fields':'zl,ysdm','values':'神木县大柳塔镇光明路,6001010001','WhereT':'t.zddm=^610821104200GB00039^'}"}
                    obj = JsonConvert.DeserializeObject<ActUpdate>(SQl);
                    break;
                case ComEnum.ActEnum.Delete:
                    //删除 {"Type":"4","Data":"{'tablename':'xs_zdjbxx','wheret':'ysdm=^666666^'}"}
                    obj = JsonConvert.DeserializeObject<ActDelete>(SQl);
                    break;
                case ComEnum.ActEnum.Other:
                    //多条不用begin不行 {"Type":"5","Data":"begin update xs_zdjbxx set zl=zl||'1' where zddm='610821104200GB00030';update xs_zdjbxx set zl=zl||'2' where zddm='610821104200GB00031'; end;"}
                    obj = Data.ToString();
                    break;
                case ComEnum.ActEnum.SelectPageProc:
                    obj = JsonConvert.DeserializeObject<PageParam>(SQl);
                    break;
                case ComEnum.ActEnum.Proc:
                    //注意sql用select *时  一定要带别名 t.*
                    /*{"Type":"6","Data":{"ProcName":"Pager","Param":[{"ParamName":"datasql","DbType":"varchar2","Direction":"in","Value":"select t.* from manage_user t"},{"ParamName":"pagesize","DbType":"int32","Direction":"in","Value":"5"},{"ParamName":"currtpage","DbType":"int32","Direction":"in","Value":"1"},{"ParamName":"pagenum","DbType":"int32","Direction":"out","Value":""},{"ParamName":"numcount","DbType":"int32","Direction":"out","Value":""},{"ParamName":"v_cur","DbType":"cursor","Direction":"out","Value":""}],"IsRetTable":"True"}}*/
                    obj = JsonConvert.DeserializeObject<ComProcParam>(SQl);
                    break;
            }

            return obj;
        }
    }

    public abstract class Action
    {
        public string TableName { get; set; }
        public abstract string ToSQL();
    }

    public class ActSelect : Action
    {
        public string Fields { get; set; }
        public string WhereT { get; set; }
        public string Orderby { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(Fields))
            {
                SQL = " select " + Fields + " from " + TableName;
                if (!string.IsNullOrEmpty(WhereT))
                    SQL = SQL + " where " + WhereT;
                if (!string.IsNullOrEmpty(Orderby))
                    SQL = SQL + " order by " + Orderby;
            }

            return SQL;
        }
    }

    public class ActInsert : Action
    {
        public string Fields { get; set; }
        public string Values { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields != null && Values != null &&
                Fields.Split(',').Length == Values.Split(',').Length)
            {
                var valuesarr = Values.Split(',');
                Values = "";
                foreach (var v in valuesarr)
                    Values += "'" + v + "',";

                Values = Values.TrimEnd(',');
                SQL = " insert into " + TableName + "(" + Fields + ") values(" + Values + ")";
            }

            return SQL;
        }
    }

    public class ActUpdate : Action
    {
        public string Fields { get; set; }
        public string Values { get; set; }
        public string WhereT { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields != null && Values != null &&
                Fields.Split(',').Length == Values.Split(',').Length)
            {
                SQL = " update " + TableName + " set ";
                var fileval = "";
                var fieldarr = Fields.Split(',');
                var valuearr = Values.Split(',');
                for (var i = 0; i < fieldarr.Length; i++)
                    fileval += fieldarr[i] + "='" + valuearr[i] + "',";
                SQL = SQL + fileval.TrimEnd(',');
                if (!string.IsNullOrEmpty(WhereT))
                    SQL = SQL + " where " + WhereT;
            }

            return SQL;
        }
    }

    public class ActDelete : Action
    {
        public string WhereT { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName))
            {
                SQL = " delete from " + TableName;
                if (!string.IsNullOrEmpty(WhereT))
                    SQL += " where " + WhereT;
            }

            return SQL;
        }
    }

    public class ActResult
    {
        /// <summary>
        /// -1:异常 0:失败 1:成功
        /// </summary>
        public ComEnum.EnumActResult Code { get; set; }
        /// <summary>
        /// 操作消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// data是否加密 默认false
        /// </summary>
        public bool IsEncrypt { get; set; }
        /// <summary>
        /// 请求的数据
        /// </summary>
        public string Data { get; set; }
    }
}
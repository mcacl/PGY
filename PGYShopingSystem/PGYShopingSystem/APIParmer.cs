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
            var SQl = Data.ToString().Replace("~", "\\\"").Replace("^", "\\\'");//~双引号 ^单引号
            switch (type)
            {
                case ComEnum.ActEnum.Select:
                    //{"Type":"1","Data":"{'tablename':'manager_user t','field':'t.*','Wheret':'t.name=\\'qq5\\'','Orderby':'id'}"} 多表
                    //{"Type":"1","Data":"{'tablename':'manager_user t','fields':'name,sex,age,phone,!~666~!','values':'^qq10^,^0^,^10^,^1234567896666^','Wheret':'t.name=\\'qq5\\'','Orderby':'id'}"} 单表
                    obj = JsonConvert.DeserializeObject<ActSelect>(SQl);
                    break;
                case ComEnum.ActEnum.Insert:
                    //{"Type":"2","Data":"{'tablename':'manager_user','field':'name,sex,age,phone','values':'^qq10^,^0^,^10^,\\'1234567896666\\''}"}
                    obj = JsonConvert.DeserializeObject<ActInsert>(SQl);
                    break;
                case ComEnum.ActEnum.Update:
                    //{"Type":"3","Data":"{'tablename':'manager_user','fields':'name,sex,age','values':'qq11,11,11','WhereT':'name=^qq10^'}"}
                    obj = JsonConvert.DeserializeObject<ActUpdate>(SQl);
                    break;
                case ComEnum.ActEnum.Delete:
                    ////{"Type":"4","Data":"{'tablename':'manager_user','WhereT':'name=^qq11^'}"}
                    obj = JsonConvert.DeserializeObject<ActDelete>(SQl);
                    break;
                case ComEnum.ActEnum.Other:
                    obj = Data.ToString();
                    break;
                case ComEnum.ActEnum.SelectPageProc:
                    obj = JsonConvert.DeserializeObject<PageParam>(SQl);
                    break;
                case ComEnum.ActEnum.Proc:
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
                if (!string.IsNullOrEmpty(WhereT)) SQL = SQL + " where " + WhereT;
                if (!string.IsNullOrEmpty(Orderby)) SQL = SQL + " order by " + Orderby;
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
                foreach (var v in valuesarr) Values += "'" + v + "',";

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
                for (var i = 0; i < fieldarr.Length; i++) fileval += fieldarr[i] + "='" + valuearr[i] + "',";
                SQL = SQL + fileval.TrimEnd(',');
                if (!string.IsNullOrEmpty(WhereT)) SQL = SQL + " where " + WhereT;
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
                SQL = " delete " + TableName;
                if (!string.IsNullOrEmpty(WhereT)) SQL += " where " + WhereT;
            }

            return SQL;
        }
    }

    public class ActResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public string Data { get; set; }
    }
}
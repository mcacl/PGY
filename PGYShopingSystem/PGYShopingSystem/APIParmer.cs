using Newtonsoft.Json;
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
            switch (type)
            {
                case ComEnum.ActEnum.Select:
                    //{"Type":"1","Data":"{'tablename':'manager_user t','field':'t.*','Wheret':'t.name=\\'qq5\\'','Orderby':'id'}"}
                    obj = JsonConvert.DeserializeObject<ActSelect>(Data.ToString());
                    break;
                case ComEnum.ActEnum.Insert:
                    //{"Type":"2","Data":"{'tablename':'manager_user','field':'-name,sex,age,phone-','values':'-qq10-,-0-,-10-,\\'1234567896666\\''}"}
                    obj = JsonConvert.DeserializeObject<ActInsert>(Data.ToString());
                    break;
                case ComEnum.ActEnum.Update:
                    obj = JsonConvert.DeserializeObject<ActUpdate>(Data.ToString());
                    break;
                case ComEnum.ActEnum.Delete:
                    obj = JsonConvert.DeserializeObject<ActDelete>(Data.ToString());
                    break;
                case ComEnum.ActEnum.Other:
                    obj = Data.ToString();
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
        public string Field { get; set; }
        public string WhereT { get; set; }
        public string Orderby { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && !string.IsNullOrEmpty(Field))
            {
                SQL = " select " + Field + " from " + TableName;
                if (!string.IsNullOrEmpty(WhereT)) SQL = SQL + " where " + WhereT;
                if (!string.IsNullOrEmpty(Orderby)) SQL = SQL + " order by " + Orderby;
            }

            return SQL;
        }
    }

    public class ActUpdate : Action
    {
        public string[] Fields { get; set; }
        public string[] Values { get; set; }
        public string WhereT { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields != null && Values != null && Fields.Length == Values.Length)
            {
                SQL = " update " + TableName + " set ";
                var fileval = "";
                for (var i = 0; i < Fields.Length; i++) fileval += Fields[i] + "='" + Values[i] + "',";
                SQL = SQL + fileval.TrimEnd(',');
                if (!string.IsNullOrEmpty(WhereT)) SQL += SQL + " where " + WhereT;
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
            if (!string.IsNullOrEmpty(TableName) && Fields != null && Values != null && Fields.Split(',').Length == Values.Split(',').Length)
            {
                SQL = " insert into " + TableName + "(" + Fields + ") values(" + Values + ")";
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
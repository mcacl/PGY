using Newtonsoft.Json;

namespace PGYShopingSystem
{
    public class APIParmer
    {
        public enum MyEnum
        {
            Select = 1,
            Insert = 2,
            Update = 3,
            Delete = 4
        }

        public int Type { get; set; }

        public object Data { get; set; }

        public object GetAct()
        {
            var type = (MyEnum)Type;
            object obj = null;
            switch (type)
            {
                case MyEnum.Select:
                    obj = JsonConvert.DeserializeObject<ActSelect>(Data.ToString());
                    break;
                case MyEnum.Insert:
                    obj = JsonConvert.DeserializeObject<ActInsert>(Data.ToString());
                    break;
                case MyEnum.Update:
                    obj = JsonConvert.DeserializeObject<ActUpdate>(Data.ToString());
                    break;
                case MyEnum.Delete:
                    obj = JsonConvert.DeserializeObject<ActDelete>(Data.ToString());
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
            if (!string.IsNullOrEmpty(TableName))
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
            if (!string.IsNullOrEmpty(TableName) && Fields.Length == Values.Length)
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
        public string[] Fields { get; set; }
        public string[] Values { get; set; }
        public string WhereT { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields.Length == Values.Length)
            {
                SQL = " insert into " + TableName + "(";
                var field = ",";
                var value = ",";
                for (var i = 0; i < Fields.Length; i++)
                {
                    field += Fields[i] + ",";
                    value += "'" + Values[i] + "',";
                }

                SQL = SQL + field.Trim(',') + ") values(" + value.Trim(',') + ")";
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
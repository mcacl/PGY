using System;

namespace PGYShopingSystem
{
    public class APIParmer
    {
        public Enum Type { get; set; }

        public object Data { get; set; }
    }

    public abstract class Action
    {
        public string TableName { get; set; }
        public abstract string ToSQL();
    }

    public class ActSelect : Action
    {
        public string Field { get; set; }
        public string WhereTerm { get; set; }
        public string Orderby { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName))
            {
                SQL = " select " + Field + " from " + TableName;
                if (!string.IsNullOrEmpty(WhereTerm))
                {
                    SQL = SQL + " where " + WhereTerm;
                }
                if (!string.IsNullOrEmpty(Orderby))
                {
                    SQL = SQL + " order by " + Orderby;
                }
            }
            return SQL;
        }
    }

    public class ActUpdate : Action
    {
        public string[] Fields { get; set; }
        public string[] Values { get; set; }
        public string WhereTerm { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields.Length == Values.Length)
            {
                SQL = " update " + TableName + " set ";
                var fileval = "";
                for (var i = 0; i < Fields.Length; i++)
                {
                    fileval += Fields[i] + "='" + Values[i] + "',";
                }
                SQL = SQL + fileval.TrimEnd(',');
                if (!string.IsNullOrEmpty(WhereTerm))
                {
                    SQL += SQL + " where " + WhereTerm;
                }
            }
            return SQL;
        }
    }

    public class ActInsert : Action
    {
        public string[] Fields { get; set; }
        public string[] Values { get; set; }
        public string WhereTerm { get; set; }

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
        public string WhereTerm { get; set; }

        public override string ToSQL()
        {
            var SQL = "";
            if (!string.IsNullOrEmpty(TableName))
            {
                SQL = " delete " + TableName;
                if (!string.IsNullOrEmpty(WhereTerm))
                {
                    SQL += " where " + WhereTerm;
                }
            }
            return SQL;
        }
    }
}
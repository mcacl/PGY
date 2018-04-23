using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PGYShopingSystem
{
    public class APIParmer
    {
        public int Type { get; set; }

        public Object Data
        {
            get { return this; }
            set
            {
                var type = (MyEnum)Type;
                switch (type)
                {
                    case MyEnum.Select:
                        value = JsonConvert.DeserializeObject(value.ToString()); break;
                    case MyEnum.Insert:
                        value = JsonConvert.DeserializeObject(value.ToString()); break;
                    case MyEnum.Update:
                        value = JsonConvert.DeserializeObject(value.ToString()); break;
                    case MyEnum.Delete:
                        value = JsonConvert.DeserializeObject(value.ToString()); break;
                }
            }
        }

        public enum MyEnum
        {
            Select = 1,
            Insert = 2,
            Update = 3,
            Delete = 4
        }
    }

    public abstract class Action
    {
        public static string TableName { get; set; }
        public abstract string ToSQL();
    }

    public class ActSelect : Action
    {
        public string Field { get; set; }
        public string WhereT { get; set; }
        public string Orderby { get; set; }

        public override string ToSQL()
        {
            string SQL = "";
            if (!string.IsNullOrEmpty(TableName))
            {
                SQL = " select " + Field + " from " + TableName;
                if (!string.IsNullOrEmpty(WhereT))
                {
                    SQL = SQL + " where " + WhereT;
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
        public string WhereT { get; set; }

        public override string ToSQL()
        {
            string SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields.Length == Values.Length)
            {
                SQL = " update " + TableName + " set ";
                string fileval = "";
                for (int i = 0; i < Fields.Length; i++)
                {
                    fileval += Fields[i] + "='" + Values[i] + "',";
                }
                SQL = SQL + fileval.TrimEnd(',');
                if (!string.IsNullOrEmpty(WhereT))
                {
                    SQL += SQL + " where " + WhereT;
                }
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
            string SQL = "";
            if (!string.IsNullOrEmpty(TableName) && Fields.Length == Values.Length)
            {
                SQL = " insert into " + TableName + "(";
                string field = ",";
                string value = ",";
                for (int i = 0; i < Fields.Length; i++)
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
        public static string WhereT { get; set; }

        public override string ToSQL()
        {
            string SQL = "";
            if (!string.IsNullOrEmpty(TableName))
            {
                SQL = " delete " + TableName;
                if (!string.IsNullOrEmpty(WhereT))
                {
                    SQL += " where " + WhereT;
                }
            }
            return SQL;
        }
    }
}
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using Oracle.ManagedDataAccess.Client;

namespace DBExecute
{
    public class DBActBase
    {
        /// <summary>
        ///     连接字符串
        /// </summary>
        public string ConnectionStr { get; private set; }
        public string DBType { get; private set; }

        private object ObjConnect { get; set; }
        private object ObjCommand { get; set; }
        private object ObjAdapter { get; set; }
        private int DBTypeValue { get; set; }
        public DBActBase(DBTypeEnum.DBType dbtype, string connectstr)
        {
            this.ConnectionStr = connectstr;
            this.DBType = Enum.GetName(dbtype.GetType(), dbtype);
            this.DBTypeValue = (int)dbtype;
        }
        public DBActBase(string connectstr)
        {
            this.ConnectionStr = connectstr;
        }

        public Tuple<OracleConnection, OracleCommand, OracleDataAdapter> GetOracleConnnect(string SQL = "")
        {
            Tuple<OracleConnection, OracleCommand, OracleDataAdapter> tuple = null;
            if (!string.IsNullOrEmpty(ConnectionStr))
            {
                OracleConnection conn = new OracleConnection(ConnectionStr);
                conn.Open();
                OracleCommand cmd = new OracleCommand(SQL, conn);
                OracleDataAdapter adapt = new OracleDataAdapter(cmd);
                tuple = new Tuple<OracleConnection, OracleCommand, OracleDataAdapter>(conn, cmd, adapt);
            }
            return tuple;
        }
        public Tuple<OleDbConnection, OleDbCommand, OleDbDataAdapter> GetAccessConnnect(string SQL = "")
        {
            Tuple<OleDbConnection, OleDbCommand, OleDbDataAdapter> tuple = null;
            if (!string.IsNullOrEmpty(ConnectionStr))
            {
                OleDbConnection conn = new OleDbConnection(ConnectionStr);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(SQL, conn);
                OleDbDataAdapter adapt = new OleDbDataAdapter(cmd);
                tuple = new Tuple<OleDbConnection, OleDbCommand, OleDbDataAdapter>(conn, cmd, adapt);
            }
            return tuple;
        }
        public Tuple<SqlConnection, SqlCommand, SqlDataAdapter> GetSqlServerConnnect(string SQL = "")
        {
            Tuple<SqlConnection, SqlCommand, SqlDataAdapter> tuple = null;
            if (!string.IsNullOrEmpty(ConnectionStr))
            {
                SqlConnection conn = new SqlConnection(ConnectionStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL, conn);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                tuple = new Tuple<SqlConnection, SqlCommand, SqlDataAdapter>(conn, cmd, adapt);
            }
            return tuple;
        }
        public Tuple<SQLiteConnection, SQLiteCommand, SQLiteDataAdapter> GetSqliteConnnect(string SQL = "")
        {
            Tuple<SQLiteConnection, SQLiteCommand, SQLiteDataAdapter> tuple = null;
            if (!string.IsNullOrEmpty(ConnectionStr))
            {
                SQLiteConnection conn = new SQLiteConnection(ConnectionStr);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(SQL, conn);
                SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
                tuple = new Tuple<SQLiteConnection, SQLiteCommand, SQLiteDataAdapter>(conn, cmd, adapt);
            }
            return tuple;
        }
        
        public T GetConnection<T>(string SQL) where T : DbConnection
        {
            object obj = null;
            dynamic dyn;
            switch (this.DBTypeValue)
            {
                case (int)DBTypeEnum.DBType.Oracle:
                    dyn = new OracleConnection(ConnectionStr);
                    obj = this.ObjConnect = dyn;
                    dyn = new OracleCommand(SQL, dyn);
                    this.ObjCommand = dyn;
                    dyn = new OracleDataAdapter();
                    this.ObjAdapter = dyn;
                    break;
                case (int)DBTypeEnum.DBType.Access:
                    dyn = new OleDbConnection(ConnectionStr);
                    obj = this.ObjConnect = dyn;
                    dyn = new OleDbCommand(SQL, dyn);
                    this.ObjCommand = dyn;
                    dyn = new OleDbDataAdapter();
                    this.ObjAdapter = dyn;
                    break;
                case (int)DBTypeEnum.DBType.SqlServer:
                    dyn = new SqlConnection(ConnectionStr);
                    obj = this.ObjConnect = dyn;
                    dyn = new SqlCommand(SQL, dyn);
                    this.ObjCommand = dyn;
                    dyn = new SqlDataAdapter();
                    this.ObjAdapter = dyn;
                    break;
            }
            this.ObjConnect = obj;
            return obj as T;
        }

        public T GetCommand<T>() where T : DbCommand
        {
            object obj = null;
            if (this.ObjCommand != null)
            {
                switch (this.DBTypeValue)
                {
                    case (int)DBTypeEnum.DBType.Oracle:
                        obj = this.ObjCommand as OracleCommand;
                        break;
                    case (int)DBTypeEnum.DBType.Access:
                        obj = this.ObjCommand as OleDbCommand;
                        break;
                    case (int)DBTypeEnum.DBType.SqlServer:
                        obj = this.ObjCommand as SqlCommand;
                        break;
                }
            }
            return obj as T;
        }
        public T GetAdapter<T>() where T : DbDataAdapter
        {
            object obj = null;
            if (this.ObjAdapter != null)
            {
                switch (this.DBTypeValue)
                {
                    case (int)DBTypeEnum.DBType.Oracle:
                        obj = this.ObjAdapter as OracleDataAdapter;
                        break;
                    case (int)DBTypeEnum.DBType.Access:
                        obj = this.ObjAdapter as OleDbDataAdapter;
                        break;
                    case (int)DBTypeEnum.DBType.SqlServer:
                        obj = this.ObjAdapter as SqlDataAdapter;
                        break;
                }
            }
            return obj as T;
        }
    }
}
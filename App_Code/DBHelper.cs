using System;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public static class DBHelper
    {
        public static string ConnStr => ConfigurationManager.ConnectionStrings["IWPSOra"].ConnectionString;

        public static OracleConnection GetConnection()
        {
            var conn = new OracleConnection(ConnStr);
            conn.Open();
            return conn;
        }

        public static DataTable ExecuteQuery(string sql, params OracleParameter[] prms)
        {
            var dt = new DataTable();
            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                if (prms != null) foreach (var p in prms) cmd.Parameters.Add(p);
                using (var da = new OracleDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        public static int ExecuteNonQuery(string sql, params OracleParameter[] prms)
        {
            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                if (prms != null) foreach (var p in prms) cmd.Parameters.Add(p);
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params OracleParameter[] prms)
        {
            using (var conn = GetConnection())
            using (var cmd = new OracleCommand(sql, conn))
            {
                cmd.BindByName = true;
                if (prms != null) foreach (var p in prms) cmd.Parameters.Add(p);
                return cmd.ExecuteScalar();
            }
        }

        public static int GetNextSeq(string tableName, string codeColumn, string prefix = "")
        {
            string sql = string.IsNullOrEmpty(prefix)
                ? $"SELECT NVL(MAX(TO_NUMBER(REGEXP_SUBSTR({codeColumn},'[0-9]+$'))),0)+1 FROM MATPASS.{tableName}"
                : $"SELECT NVL(MAX(TO_NUMBER(REGEXP_SUBSTR({codeColumn},'[0-9]+$'))),0)+1 FROM MATPASS.{tableName} WHERE {codeColumn} LIKE '{prefix}%'";
            object val = ExecuteScalar(sql);
            return val == null || val == DBNull.Value ? 1 : Convert.ToInt32(val);
        }

        public static OracleParameter P(string name, object value)
            => new OracleParameter(name, value ?? DBNull.Value);

        public static OracleParameter PDate(string name, DateTime? value)
        {
            var p = new OracleParameter(name, OracleDbType.Date);
            p.Value = value.HasValue ? (object)value.Value : DBNull.Value;
            return p;
        }
    }
}

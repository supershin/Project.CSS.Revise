using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Project.CSS.Revise.Web.Library.DAL
{
    public class DataAccess
    {
        private string _connectionString = "";
        private bool _enableCaching = true;
        private int _cacheDuration = 0;

        protected string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        protected bool EnableCaching
        {
            get { return _enableCaching; }
            set { _enableCaching = value; }
        }

        protected int CacheDuration
        {
            get { return _cacheDuration; }
            set { _cacheDuration = value; }
        }

        public DataAccess(IConfiguration configuration)
        {

            var connStr = configuration.GetConnectionString("CSSStrings");
            if (connStr == null)
            {
                throw new InvalidOperationException("Connection string 'CSSStrings' not found in configuration.");
            }
            _connectionString = connStr;
        }

        protected string ExecuteNonQuerystr(DbCommand cmd)
        {
            return Convert.ToString(cmd.ExecuteNonQuery());
        }

        protected int ExecuteNonQuery(DbCommand cmd)
        {
            return cmd.ExecuteNonQuery();
        }

        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.Default);
        }

        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }
    }
}

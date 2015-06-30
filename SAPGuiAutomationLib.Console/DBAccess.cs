using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPGuiAutomationLib.Console
{
    public class DBAccess
    {
        private DbConnection _dbCn;
        private DbCommand _cmd;
        public DBAccess(DbConnection dbConnection, DbCommand cmd)
        {
            _dbCn = dbConnection;
            _cmd = cmd;
            _cmd.Connection = _dbCn;
        }

        public bool TestConnection()
        {
            try
            {
                _dbCn.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _dbCn.Close();
            }
        }

        public int ExecuteNonQuery(string CommandText, CommandType cmdType, DbParameter[] parameters = null)
        {
            int result = 0;
            _dbCn.Open();
            _cmd.CommandText = CommandText;
            _cmd.CommandType = cmdType;
            if (parameters != null)
            {
                _cmd.Parameters.AddRange(parameters);
            }
            result = _cmd.ExecuteNonQuery();
            _cmd.Dispose();
            _dbCn.Close();
            return result;
        }

        public DataSet GetData(DbDataAdapter adapter, string CommandText, CommandType cmdType, DbParameter[] parameters = null)
        {
            DataSet ds = new DataSet();
            _cmd.CommandText = CommandText;
            _cmd.CommandType = cmdType;
            if (parameters != null && parameters.Count() > 0)
            {
                _cmd.Parameters.AddRange(parameters);
            }
            adapter.SelectCommand = _cmd;
            adapter.Fill(ds);
            return ds;
        }
    }
}

using ShmffPortal.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShmffPortal.DAL
{
    public class DataAccessCls
    {

        private SqlConnection conn;
  
        //This Constructor Inisialize the connection object
        public DataAccessCls()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionSMS"].ConnectionString);
        }
        public void Open()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        //Method to close the connection
        public void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public DataTable ExcuteQuery(string query)
        {
             LogExceptions help = new LogExceptions();
            Open();
             try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader Reader = cmd.ExecuteReader();
                DataTable Data = new DataTable();
                Data.Load(Reader);
                Close();
                return Data;
            }
            catch (Exception ex)
            {
                Close();
                help.LogError(ex);
                return null;
            }
        }


        public int ExcuteQueryCUD(string query)
        {

            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                Open();
                int success = cmd.ExecuteNonQuery();
                //OracleDataReader Reader = cmd.ExecuteReader();
                //DataTable Data = new DataTable();
                //Data.Load(Reader);
                // return Data;
                Close();
                return success;
            }
            catch (Exception ex)
            {
                Close();
                return 0;
            }
        }

        //Method To Read Data From Database
        public DataTable SelectData(string stored_procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = conn;
             if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

    }
}
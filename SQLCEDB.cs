using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;

namespace DataLoader
{
    public class SQLDBCE
    {
        #region Members
        private static String m_ErrMsg = "";
        private static SqlCeConnection con = null;
        private static SqlCeCommand com = null;
        private static SqlCeDataReader dr = null;
        
        #endregion

        #region Error Methods

        public static String GetErrorMessage()
        {
            return m_ErrMsg;
        }

        private static void ClearErrorMessage()
        {
            m_ErrMsg = "";
            if (dr != null)
                dr.Close();
            if (con != null && con.State == ConnectionState.Open)
                con.Close();
            dr = null;
        }

        #endregion

        #region Database Connection Setting Methods

        public static void SetConnectString(String sConStr)
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
            if (com != null)
                com.Dispose();

            con = new SqlCeConnection();
            com = new SqlCeCommand();

            con.ConnectionString = sConStr;
            com.Connection = con;
        }

        #endregion

        #region Database Operation Methods

        public static SqlCeDataReader ExecuteDataReader(String sSQLQuery)
        {
            ClearErrorMessage();

            try
            {
                com.CommandText = sSQLQuery;
                con.Open();
                dr = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                m_ErrMsg = e.Message;
                if (con != null && con.State == ConnectionState.Open)
                    con.Close();
                dr = null;
            }

            return dr;
        }

        public static DataTable GetDataTable(String sSQLQuery)
        {
            ClearErrorMessage();
            ExecuteDataReader(sSQLQuery);
            DataTable dt = null;
            if (dr != null)
            {
                //if (dr.Depth)
                //{
                    dt = new DataTable();
                    dt.Load(dr);
                //}
                dr.Close();
                dr.Dispose();
            }

            return dt;
        }

        public static String ExecuteQuery(String sSQLQuery)
        {
            ClearErrorMessage();
           

            try
            {
                com.CommandText = sSQLQuery;
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                m_ErrMsg = e.Message;
            }
           
            return m_ErrMsg;
        }

        public static String ExecuteQuery(String sSQLQuery,ref long iReturn)
        {
            ClearErrorMessage();
                      
            try
            {
                com.CommandText = sSQLQuery;
                SqlCeParameter parameterReturnValue = new SqlCeParameter();
                parameterReturnValue.Direction = ParameterDirection.ReturnValue;
                com.Parameters.Add(parameterReturnValue);
                con.Open();
                com.ExecuteNonQuery();
                //Get the return value
                iReturn = Convert.ToInt64(parameterReturnValue.Value);
            }
            catch (Exception e)
            {
                m_ErrMsg = e.Message;
            }
            
            return m_ErrMsg;
        }

       
        #endregion

    }
}

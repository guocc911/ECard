
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using MySql.Data.Common;
using MySql.Data;

namespace COMM
{

    public class MySqlDBHelper
    {
        // Fields
        public static string Conn = "Database='cbitracesysdb';Data Source='localhost';User Id='root';Password='87775236';charset='gb2312';pooling=true";


        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static bool TestCoonnectin(string datasource, string database, string user, string pwd)
        {
            bool result = false;

            StringBuilder strInfo = new StringBuilder();
            strInfo.Append("Database='" + database + "';");
            strInfo.Append("Data Source='" + datasource + "';");
            strInfo.Append("User Id='" + user + "';");
            strInfo.Append("Password='" + pwd + "';charset='gb2312';pooling=true");


            MySqlConnection conn = new MySqlConnection(strInfo.ToString());


            try
            {
                conn.Open();

                result = true;
            }
            catch
            {
                return result;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static bool ModifyConnectionInfo(string datasource,string database,string user,string pwd)
        {

            bool result = false;

            StringBuilder strInfo=new StringBuilder();
                            strInfo.Append("Database='"+database+"';");
                            strInfo.Append("Data Source='"+datasource+"';");
                            strInfo.Append("User Id='"+user+"';");
                            strInfo.Append("Password='"+pwd+"';charset='gb2312';pooling=true");


                            MySqlConnection conn = new MySqlConnection(strInfo.ToString());

             try
             {
               conn.Open();

               result = true;
             }
             catch 
             {
                 return result;
             }
             finally
             {
                 conn.Close();

                 if (result)
                 {
                     Conn = strInfo.ToString();
                 }
             }

             return result;
        }

        // Methods
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            DataSet set2;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                cmd.Parameters.Clear();
                conn.Close();
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return set2;
        }

        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int num = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int num = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int num = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return num;
            }
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlDataReader reader2;
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {
                conn.Close();
                throw;
            }
            return reader2;
        }

        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object obj2 = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object obj2 = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return obj2;
            }
        }

        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] parameterArray = (MySqlParameter[])parmCache[cacheKey];
            if (parameterArray == null)
            {
                return null;
            }
            MySqlParameter[] parameterArray2 = new MySqlParameter[parameterArray.Length];
            int index = 0;
            int length = parameterArray.Length;
            while (index < length)
            {
                parameterArray2[index] = (MySqlParameter)((ICloneable)parameterArray[index]).Clone();
                index++;
            }
            return parameterArray2;
        }

        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 0xbb8;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }
}

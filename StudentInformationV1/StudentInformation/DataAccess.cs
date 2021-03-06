﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace StudentInformation
{
    public class DataAccess
    {
        public static string CONNECTION_STRING = @"";

        //This returns the connection string  
        private static string _connectionString = string.Empty;

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == string.Empty)
                {
                    _connectionString = CONNECTION_STRING;
                }

                return _connectionString;
            }
        }

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataAccess()
        {
            string dbPath =
                   Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName +
                   @"\Student.mdf";
            CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ dbPath + ";Integrated Security=True;Connect Timeout=30";
        }

        /// <summary>
        /// Returns a SqlCommand object to add some parameters in it. After you send this to Execute method.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlCommand GetCommand(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(sql, conn);
            return sqlCmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable Execute(string sql)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = GetCommand(sql);
            
            cmd.Connection.Open();
            dt.Load(cmd.ExecuteReader());
            cmd.Connection.Close();
            return dt;
        }

        /// <summary>
        /// Returns DataTable
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public DataTable Execute(SqlCommand command)
        {
            DataTable dt = new DataTable();
            try
            {

                command.Connection.Open();
                dt.Load(command.ExecuteReader());
            }
            catch (Exception ex){}
            finally
            {
                command.Connection.Close();
            }

            return dt;
        }

        /// <summary>
        /// returns affected row count
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = GetCommand(sql);
            int result = 0;
            try
            {
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(SqlCommand command)
        {
            int result = 0;
            try
            {
                command.Connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                command.Connection.Close();
            }
            
            return result;
        }

    }
}
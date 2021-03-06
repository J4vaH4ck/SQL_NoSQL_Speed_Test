﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLTestApplication.Statistic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SQLTestApplication.Exceptions;

namespace SQLTestApplication.SQLDatabase
{
    class MSSQLTestClass
    {
        private static DataSet DataSetObject = new DataSet();
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
        private static SqlConnection SqlConnectionObject = new SqlConnection(ConnectionString);
        private static SqlDataAdapter SqlDataAdapterObject = new SqlDataAdapter();


        public SQLStatistic selectAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Read", Types.SQLActions.Olvasás, Types.SQLType.MSSQL);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                SqlDataReader dataReader = null;
                string query = "SELECT * FROM test";
                string result = "";
                SqlDataAdapterObject.SelectCommand = new SqlCommand(query, SqlConnectionObject);
                stat.Start();
                dataReader = SqlDataAdapterObject.SelectCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    result += dataReader["ID"] + "," + dataReader["Name"] + "," + dataReader["NeptunCode"] + "\n";
                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MSSQLException("(MSSQL) Hiba történt az adat(ok) olvasása során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic insertRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Insert", Types.SQLActions.Beszúrás, Types.SQLType.MSSQL);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < rows; i++)
                {
                    string query = "INSERT INTO test(ID,Name,NeptunCode) VALUES (@ID,@Name,@NeptunCode)";
                    SqlDataAdapterObject.InsertCommand = new SqlCommand(query, SqlConnectionObject);
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@ID", SqlDbType.Int).Value = i;
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@Name", SqlDbType.VarChar, 30).Value = "testUser" + i;
                    SqlDataAdapterObject.InsertCommand.Parameters.Add("@NeptunCode", SqlDbType.VarChar, 6).Value = "B" + i;
                    stat.Start();
                    SqlDataAdapterObject.InsertCommand.ExecuteNonQuery();
                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MSSQLException("(MSSQL) Hiba történt az adat(ok) beszúrása során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic deleteAllRows()
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Delete", Types.SQLActions.Törlés, Types.SQLType.MSSQL);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                string query = "DELETE FROM test";
                SqlDataAdapterObject.DeleteCommand = new SqlCommand(query, SqlConnectionObject);
                stat.Start();
                SqlDataAdapterObject.DeleteCommand.ExecuteNonQuery();
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MSSQLException("(MSSQL) Hiba történt az adat(ok) törlése során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

        public SQLStatistic updateRows(int rows)
        {
            SQLStatistic stat = new SQLStatistic("MSSQL Update", Types.SQLActions.Frissítés, Types.SQLType.MSSQL);
            try
            {
                DataObject obj = new DataObject();
                SqlConnectionObject.Open();
                //------------------------------------------------------------------------------------------------------------
                for (int i = 0; i < rows; i++)
                {
                    string query = "UPDATE test SET NeptunCode=@code WHERE Name=@name";
                    SqlDataAdapterObject.UpdateCommand = new SqlCommand(query, SqlConnectionObject);
                    SqlDataAdapterObject.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar, 30).Value = "testUser" + i;
                    SqlDataAdapterObject.UpdateCommand.Parameters.Add("@code", SqlDbType.VarChar, 6).Value = "HS8GZ9";
                    stat.Start();
                    SqlDataAdapterObject.UpdateCommand.ExecuteNonQuery();
                }
                //------------------------------------------------------------------------------------------------------------
                stat.End();
            }
            catch (Exception ex)
            {
                throw new MSSQLException("(MSSQL) Hiba történt az adat(ok) frissítése során\n" + ex.Message);
            }
            finally
            {
                SqlConnectionObject.Close();
            }
            return stat;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace deTestWebForm0509
{
    public class Unity
    {
        string ConnectStr = "";
        string sqlStr = "";

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=GSSWEB;Integrated Security=True");
          
        }

        public int exeNonQuery(string sql, List<ParamatsWithValueClass> paramatsList)
        {
            int ifSuccess = -1;

            using (SqlConnection con = GetSqlConnection())
            {

                SqlCommand scomm = new SqlCommand(sql);
                scomm.Connection = con;
                con.Open();
                SqlTransaction transactionMan = con.BeginTransaction();
                scomm.Transaction = transactionMan;

                try
                {
                    if (paramatsList != null)
                    {
                        foreach (ParamatsWithValueClass item in paramatsList)
                        {
                            scomm.Parameters.AddWithValue(item.key, item.value);
                        }
                    }


                    ifSuccess = scomm.ExecuteNonQuery();
                    transactionMan.Commit();
                }
                catch (Exception ex)
                {
                    transactionMan.Rollback();
                    Console.WriteLine(sql+ "。(前面為sql) exeNonQuery 失敗。(錯誤訊息) " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
            return ifSuccess;

        }
        public string exeScalar(string sql, List<ParamatsWithValueClass> paramatsList)
        {

            string ifSuccess = "";

            using (SqlConnection con = GetSqlConnection())
            {

                SqlCommand scomm = new SqlCommand(sql);
                scomm.Connection = con;
                con.Open();

                SqlTransaction transactionMan = con.BeginTransaction();
                scomm.Transaction = transactionMan;
                try
                {
                    if (paramatsList != null)
                    {
                        foreach (ParamatsWithValueClass item in paramatsList)
                        {
                            scomm.Parameters.AddWithValue(item.key, item.value);
                        }
                    }
                    var ifSuccessVar = scomm.ExecuteScalar();

                    if (ifSuccessVar == null) { ifSuccess = "0"; } else { ifSuccess = ifSuccessVar.ToString(); }

                    transactionMan.Commit();
                }
                catch (Exception ex)
                {
                    transactionMan.Rollback();
                    Console.WriteLine(sql + "。(前面為sql) exeScalar 失敗。(錯誤訊息) " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
            return ifSuccess;

        }
        public DataTable exeReader(string sql, List<ParamatsWithValueClass> paramatsList)
        {

            DataTable schemaTable = new DataTable();
            using (SqlConnection con = GetSqlConnection())
            {
                SqlCommand scomm = new SqlCommand(sql);
                scomm.Connection = con;
                con.Open();
                SqlTransaction transactionMan = con.BeginTransaction();
                scomm.Transaction = transactionMan;
                SqlDataReader sr = null;
                try
                {
                    if (paramatsList != null)
                    {
                        foreach (ParamatsWithValueClass item in paramatsList)
                        {
                            scomm.Parameters.AddWithValue(item.key, item.value);
                        }
                    }
                    sr = scomm.ExecuteReader();
                    schemaTable.Load(sr);
                    sr.Close(); 
                    transactionMan.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        sr.Close();
                        transactionMan.Rollback();
                    }
                    catch (Exception eex)
                    {
                      
                        Console.WriteLine(sql + "。(前面為sql) exeReader Rollback 失敗。(錯誤訊息) " + eex.Message);
                        return null;
                    }


                    Console.WriteLine(sql + "。(前面為sql) exeReader 失敗。(錯誤訊息) " + ex.Message);
                }
                finally
                {
                    sr.Close();
                    con.Close();
                }
            }
            return schemaTable;
        }

    }
}
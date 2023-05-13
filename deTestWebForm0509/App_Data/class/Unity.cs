using NPOI.OpenXmlFormats;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static string ExceptionWrong = "";
        public static string ExceptionWrong2 = "";

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=GSSWEB;Integrated Security=True");

        }

        public int exeNonQuery(string sql, List<ParamatsWithValueClass> paramatsList)
        {
            int ifSuccess = -1;
            ExceptionWrong = "";

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

                    ExceptionWrong = sql + "。(前面為sql) exeNonQuery 失敗。(錯誤訊息) " + ex.Message;
                    Console.WriteLine(ExceptionWrong);
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
            ExceptionWrong = "";
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
                    ExceptionWrong = sql + "。(前面為sql) exeScalar 失敗。(錯誤訊息) " + ex.Message;
                    Console.WriteLine(ExceptionWrong);
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
            ExceptionWrong = "";
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
                        ExceptionWrong = sql + "。(前面為sql) exeReader Rollback 失敗。(錯誤訊息) " + eex.Message;
                        Console.WriteLine(ExceptionWrong);
                        return null;
                    }

                    ExceptionWrong = sql + "。(前面為sql) exeReader 失敗。(錯誤訊息) " + ex.Message;
                    Console.WriteLine(ExceptionWrong);
                }
                finally
                {
                    sr.Close();
                    con.Close();
                }
            }
            return schemaTable;
        }

        public void ImpotExcelCsv(DataTable dt)
        {
            ExceptionWrong2 = "";
            string showNo = "";
            IEnumerable<DataRow> dtAll = new Unity().exeReader(@"select * from [dbo].[deTestWebFormMember]", null).AsEnumerable();
            int count = 0;
            List<string>  waitDelete = new List<string>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dtAll.Any(_ => _.Field<string>("id").Trim() == dt.Rows[i]["id"].ToString()))
                {
                    waitDelete.Add(i.ToString());
                    showNo += dt.Rows[i]["id"] + ",";
                    count++;
                }
            }

            //foreach (DataRow item in dt.Rows)
            //{
            //    if (dtAll.Any(_ => _.Field<string>("id").Trim() == item["id"].ToString()))
            //    {

            //        waitDelete.Rows.Add(item);
            //    };
            //}
            waitDelete.Reverse();
            foreach (string waitDeleteItem in waitDelete)
            {

                dt.Rows.RemoveAt(Convert.ToInt32( waitDeleteItem));
            }
            ExceptionWrong2 += "發現有 " + count + " 筆重覆" + showNo + "，已剔除。";
            count = 0;
            string sqlString =
                @"INSERT INTO [dbo].[deTestWebFormMember]
                ([id],[name] ,[sex]  ,[phone] ,[address])
                VALUES
                (@K_id,@K_name ,@K_sex,@K_phone,@K_address)";

            foreach (DataRow row in dt.Rows)
            {

                List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_id", value = row["id"].ToString() });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_name", value = row["name"].ToString() });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_sex", value = row["sex"].ToString() });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_phone", value = row["phone"].ToString() });
                paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_address", value = row["address"].ToString() });

                new Unity().exeNonQuery(sqlString, paramatsWithValueClasses);
                count++;
            }
            ExceptionWrong2 += "成功加入了" + count + "筆";
        }


    }
}
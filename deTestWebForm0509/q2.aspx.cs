using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;


namespace deTestWebForm0509
{
    public partial class q2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Repeater1.DataSource = new Unity().exeReader(@"select * from [dbo].[deTestWebFormMember]", null);
                Repeater1.DataBind();
            }

        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            string sqlString =
                @"INSERT INTO [dbo].[deTestWebFormMember]
                ([name] ,[sex]  ,[phone] ,[address])
                VALUES
                (@K_name ,@K_sex,@K_phone,@K_address)";

            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_name", value = nameTextBox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_sex", value = sexTextBox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_phone", value = phoneTextBox.Text });
            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_address", value = addressTextBox.Text });

            new Unity().exeNonQuery(sqlString, paramatsWithValueClasses);

            Response.Redirect("q2.aspx");
        }

        protected void searchIDButton_Click(object sender, EventArgs e)
        {
            searchIDResult.Text = "-";
            string sqlString = @"select * from [dbo].[deTestWebFormMember] where id = @K_id";

            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();

            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_id", value = searchIDTextBox.Text });

            DataTable dt = new Unity().exeReader(sqlString, paramatsWithValueClasses);

            if (dt.Rows.Count <= 0)
            {
                searchIDResult.Text = "查無資料";
                Repeater1.Dispose();
                Repeater1.DataBind();
                nameTextBox.Text = "查無資料";
                sexTextBox.Text = "查無資料";
                phoneTextBox.Text = "查無資料";
                addressTextBox.Text = "查無資料";

                return;
            };

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            foreach (DataRow item in dt.Rows)
            {
                nameTextBox.Text = item[1].ToString();
                sexTextBox.Text = item[2].ToString();
                phoneTextBox.Text = item[3].ToString();
                addressTextBox.Text = item[4].ToString();
            }

        }



        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("q2.aspx");
        }
        private string testID()
        {
            string sqlString = @"select * from [dbo].[deTestWebFormMember] where id = @K_id";

            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();

            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_id", value = searchIDTextBox.Text });

            return new Unity().exeScalar(sqlString, paramatsWithValueClasses);

        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (testID() == "0")
            {
                searchIDResult.Text = "請正確輸入員編";
                return;
            }

            string sqlStringUpdate =
               @"UPDATE [dbo].[deTestWebFormMember]
                     SET [name] = @K_name
                            ,[sex] = @K_sex
                            ,[phone] = @K_phone
                            ,[address] = @K_address
                                WHERE id= @K_id";

            List<ParamatsWithValueClass> paramatsWithValueClassesUpdate = new List<ParamatsWithValueClass>();
            paramatsWithValueClassesUpdate.Add(new ParamatsWithValueClass() { key = "K_name", value = nameTextBox.Text });
            paramatsWithValueClassesUpdate.Add(new ParamatsWithValueClass() { key = "K_sex", value = sexTextBox.Text });
            paramatsWithValueClassesUpdate.Add(new ParamatsWithValueClass() { key = "K_phone", value = phoneTextBox.Text });
            paramatsWithValueClassesUpdate.Add(new ParamatsWithValueClass() { key = "K_address", value = addressTextBox.Text });
            paramatsWithValueClassesUpdate.Add(new ParamatsWithValueClass() { key = "K_id", value = searchIDTextBox.Text });

            new Unity().exeNonQuery(sqlStringUpdate, paramatsWithValueClassesUpdate);

            Response.Redirect("q2.aspx");

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            if (testID() == "0")
            {
                searchIDResult.Text = "請正確輸入員編";
                return;
            }

            string sqlStringDelete = @"DELETE FROM [dbo].[deTestWebFormMember] WHERE id = @K_id";

            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();

            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_id", value = searchIDTextBox.Text });

            new Unity().exeNonQuery(sqlStringDelete, paramatsWithValueClasses);

            Response.Redirect("q2.aspx");
        }

        protected void NpoiButton_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet u_sheet = workbook.CreateSheet("會員資料_Sheet1");
            MemoryStream MS = new MemoryStream();
            try
            {

                IRow u_row0 = u_sheet.CreateRow(0);
                u_row0.CreateCell(0).SetCellValue("員編");
                u_row0.CreateCell(1).SetCellValue("姓名");
                u_row0.CreateCell(2).SetCellValue("性別");
                u_row0.CreateCell(3).SetCellValue("電話");
                u_row0.CreateCell(4).SetCellValue("住址");
            
                DataTable dt = new Unity().exeReader(@"select * from [dbo].[deTestWebFormMember]", null);

                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    IRow u_row = u_sheet.CreateRow(i);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        u_row.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                    }
                }

                u_sheet.Header.Center = @"&F";
                u_sheet.Header.Right = @"&D";
                u_sheet.Footer.Center = @"&P";


                workbook.Write(MS);

             
                Response.AddHeader("Content-Disposition", "attachment; filename=會員資料.xlsx");
                Response.BinaryWrite(MS.ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            finally
            {

                //== 釋放資源
                workbook = null;   //== VB為 Nothing
                MS.Close();
                MS.Dispose();

                Response.Flush();
                Response.End();
            }
        }

      

    }
}
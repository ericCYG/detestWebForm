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
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using static NPOI.HSSF.Util.HSSFColor;
using NPOI.SS.Formula.Functions;

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

        protected void iTextSharpButton_Click(object sender, EventArgs e)
        {
            
            DataTable dt = new Unity().exeReader(@"select * from [dbo].[deTestWebFormMember]", null);   //撈資料

            using (MemoryStream stream = new MemoryStream())
            {
                // 字型設定==============vvv
                BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font ChFont = new Font(bfChinese, 12);
                //Font ChFont_blue = new Font(bfChinese, 40, Font.NORMAL, new BaseColor(51, 0, 153));
                //Font ChFont_msg = new Font(bfChinese, 12, Font.ITALIC, BaseColor.RED);
                //==========================^^^

                Document doc = new Document(PageSize.A4);

                PdfWriter writer = PdfWriter.GetInstance(doc, stream);
                doc.Open();

                var table1 = new PdfPTable(dt.Columns.Count);
                //設置列寬比例=========================vvv
                float[] a = new float[dt.Columns.Count];

                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = (float)Math.Ceiling((float)dt.Rows[0][i].ToString().Length / 2);
                }
                a[0]++;
                table1.SetWidths(a);
                //==================================^^^

                table1.AddCell(new Phrase("員編", ChFont));
                table1.AddCell(new Phrase("姓名", ChFont));
                table1.AddCell(new Phrase("性別", ChFont));
                table1.AddCell(new Phrase("電話", ChFont));
                table1.AddCell(new Phrase("住址", ChFont));
                table1.HeaderRows = 1;

                foreach (DataRow item in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        table1.AddCell(new Phrase(item[i].ToString(), ChFont));
                    }

                }
                doc.AddHeader("rick", "會員資料");
                doc.Add(table1);
                doc.AddTitle("會員資料pdf測試");//文件標題
                doc.AddAuthor("rick");//文件作者
                doc.Close();

                iTextSharp.text.Font blackFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                using (var ms2 = new MemoryStream())
                {
                    PdfReader reader = new PdfReader(stream.ToArray());
                    using (PdfStamper stamper = new PdfStamper(reader, ms2))
                    {
                        int pages = reader.NumberOfPages;
                        for (int i = 1; i <= pages; i++)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString(), blackFont), 568f, 15f, 0);
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase( DateTime.Now.ToShortDateString(), blackFont), 568f, 820f, 0);
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase(@"會員資料", ChFont), 260f, 820f, 0);
                        }
                    }
                    ms2.ToArray();
                    Response.Clear();

                    Response.OutputStream.Write(ms2.GetBuffer(), 0, ms2.GetBuffer().Length);
                }


                string filename = Server.UrlPathEncode("會員資料iTextSharp.pdf");

                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                Response.ContentType = "application/octet-stream";
                //Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.End();
            }
        }

        protected void CrystalReportButton_Click(object sender, EventArgs e)
        {
            this.Response.AddHeader("Content-Type", "application/pdf");
            this.Response.AddHeader("Content-Disposition", "attachment; filename=SampleGuide.pdf");
            this.Response.Flush();
            //this.Response.BinaryWrite(documentContent);
            //DocumentContent is a Byte[] of the data to be written in PDF
            this.Response.Flush();
            this.Response.End();
        }
    }
}
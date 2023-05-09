using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        private string testID() {
            string sqlString = @"select * from [dbo].[deTestWebFormMember] where id = @K_id";

            List<ParamatsWithValueClass> paramatsWithValueClasses = new List<ParamatsWithValueClass>();

            paramatsWithValueClasses.Add(new ParamatsWithValueClass() { key = "K_id", value = searchIDTextBox.Text });

            return new Unity().exeScalar(sqlString, paramatsWithValueClasses);
            
        }
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if ( testID()== "0")
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

            new Unity().exeNonQuery(sqlStringDelete,paramatsWithValueClasses);

            Response.Redirect("q2.aspx");
        }
    }
}
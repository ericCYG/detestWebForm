using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace deTestWebForm0509
{
    public partial class ajaxTest : System.Web.UI.Page
    {
        Dictionary<string, object> dic;

        protected void Page_Load(object sender, EventArgs e)
        {
            dic = new Dictionary<string, object>();

            dic.Add("", "");
            dic.Add("pokemon", new member());
            dic.Add("pokemon-color", new ajaxTestBodys());
            if (!IsPostBack)
            {


                

                DropDownList_apiSwitch.DataSource = dic;
                DropDownList_apiSwitch.DataValueField = "Key";
                DropDownList_apiSwitch.DataTextField = "Key";

                DropDownList_apiSwitch.DataBind();
            }
        }

        protected void DropDownList_apiSwitch_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            string temp = DropDownList_apiSwitch.SelectedValue;
            if (dic.ContainsKey(temp))
            {

                foreach (System.Reflection.PropertyInfo item in dic[temp].GetType().GetProperties())
                {
                    //var tempLiteral = new Literal();
                    //tempLiteral.Text = "<div>";
                    //Panel_ajaxBodys.Controls.Add(tempLiteral);
                    Label lblDynamic = new Label();
                    lblDynamic.Text = item.Name+@"：";
                    lblDynamic.Attributes["style"] = @"float: left;width:5%;text-align: right;";

                    // 創建一個新的TextBox控件
                    TextBox txtDynamic = new TextBox();
                    txtDynamic.ID = item.Name;
                    txtDynamic.Attributes["placeholder"] = item.Name;

                    // 將TextBox添加到Panel中
                    Panel_ajaxBodys.Controls.Add(lblDynamic);
                    Panel_ajaxBodys.Controls.Add(txtDynamic);
                    Literal tempLiteral = new Literal();
                    tempLiteral.Text = "<br/>";
                    Panel_ajaxBodys.Controls.Add(tempLiteral);
                }
            }
        }
    }
}
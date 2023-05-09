<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="q2.aspx.cs" Inherits="deTestWebForm0509.q2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="nameLabel" runat="server" Text="姓名："></asp:Label><asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox><br/>
            <asp:Label ID="sexLabel" runat="server" Text="性別："></asp:Label><asp:TextBox ID="sexTextBox" runat="server"></asp:TextBox><br/>
            <asp:Label ID="phoneLabel" runat="server" Text="電話："></asp:Label><asp:TextBox ID="phoneTextBox" runat="server"></asp:TextBox><br/>
            <asp:Label ID="addressLabel" runat="server" Text="住址："></asp:Label><asp:TextBox ID="addressTextBox" runat="server"></asp:TextBox><br/>
            <asp:Button ID="InsertButton" runat="server" Text="新增" /><asp:Button ID="UpdateButton" runat="server" Text="修改" /><asp:Button ID="DeleteButton" runat="server" Text="刪除" />
        </div>
        <div>

        </div>
    </form>
</body>
</html>

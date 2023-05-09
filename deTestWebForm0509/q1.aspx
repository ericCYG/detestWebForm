<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="q1.aspx.cs" Inherits="deTestWebForm0509.q1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="resourceString" runat="server" Text="打一串數字用逗號隔開"></asp:Label>
            <asp:TextBox ID="resourceStringTextBox" runat="server">11,7,23,18,22,90</asp:TextBox><asp:Button ID="resourceStringButton" runat="server" Text="開始" onclick="sortStart"/>
            <br/>
            <asp:Label ID="sortAns" runat="server" Text="排序後"></asp:Label>
            
        </div>
    </form>
</body>
</html>

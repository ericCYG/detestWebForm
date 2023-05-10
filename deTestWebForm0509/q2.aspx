<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="q2.aspx.cs" Inherits="deTestWebForm0509.q2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>

            #customers * {
                border: 1px solid #ddd;
                padding: 8px;
            }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="nameLabel" runat="server" Text="姓名："></asp:Label><asp:TextBox ID="nameTextBox" runat="server">rick</asp:TextBox><br />
            <asp:Label ID="sexLabel" runat="server" Text="性別："></asp:Label><asp:TextBox ID="sexTextBox" runat="server">男</asp:TextBox><br />
            <asp:Label ID="phoneLabel" runat="server" Text="電話："></asp:Label><asp:TextBox ID="phoneTextBox" runat="server">0988777888</asp:TextBox><br />
            <asp:Label ID="addressLabel" runat="server" Text="住址："></asp:Label><asp:TextBox ID="addressTextBox" runat="server">台灣</asp:TextBox><br />
            <br />
            <asp:Button ID="InsertButton" runat="server" Text="新增" OnClick="InsertButton_Click" />
            <br />
            <br />
            <asp:Label ID="searchID" runat="server" Text="依員編"></asp:Label><asp:TextBox ID="searchIDTextBox" runat="server"></asp:TextBox><asp:Button ID="searchIDButton" runat="server" Text="搜尋" OnClick="searchIDButton_Click" /><asp:Label ID="searchIDResult" runat="server" Text="-"></asp:Label><br />
             
            <asp:Button ID="UpdateButton" runat="server" Text="修改" OnClick="UpdateButton_Click" />
            <asp:Button ID="DeleteButton" runat="server" Text="刪除" OnClick="DeleteButton_Click" />
        </div>
        <div>
            <asp:Repeater ID="Repeater1" runat="server">

                <HeaderTemplate>
                    <table id="customers">
                        <thead>
                            <tr>
                                <th>員編</th>
                                <th>姓名</th>
                                <th>性別</th>
                                <th>電話</th>
                                <th>住址</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval(" id") %></td>
                        <td><%# Eval(" name") %></td>
                        <td><%# Eval(" sex") %></td>
                        <td><%# Eval(" phone") %></td>
                        <td><%# Eval(" address") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>

            </asp:Repeater>
            <asp:Button runat="server" Text="回列表" OnClick="Unnamed1_Click"></asp:Button><br/>
            <asp:Label ID="Label1" runat="server" Text="Excel"></asp:Label>
            <asp:Button ID="NpoiButton" runat="server" Text="NPOI" OnClick="NpoiButton_Click" /><br/>
            <asp:Label ID="Label2" runat="server" Text="PDF"></asp:Label>
            <asp:Button ID="iTextSharpButton" runat="server" Text="iTextSharp" OnClick="iTextSharpButton_Click" />
            <asp:Button ID="CrystalReportButton" runat="server" Text="Crystal Report" OnClick="CrystalReportButton_Click" />
        </div>
    </form>
</body>
</html>

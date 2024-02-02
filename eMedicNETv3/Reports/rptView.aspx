<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptView.aspx.cs" Inherits="emedic3.Reports.rptView" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Reports</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p><asp:Button runat="server" ID="btn" Text="Download Excel" OnClick="btn_Click" /></p>
            <p><%= _strValue %></p>
        </div>
    </form>
</body>
</html>

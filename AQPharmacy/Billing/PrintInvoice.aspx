<%@ Page Language="C#" AutoEventWireup="true" Inherits="Billing_PrintInvoice" Codebehind="PrintInvoice.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frm" runat="server">
        <%=getInvoiceData() %>
    </form>
</body>
</html>

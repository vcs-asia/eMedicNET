<%@ Page Language="C#" AutoEventWireup="true" Inherits="Billing_invoice_1" Codebehind="invoice_1.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .tbl{
            font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size:70%;
            width:100%;
        }
    </style>
</head>
<body>
    <form id="frm" runat="server">
        <%=getInvoiceData() %>
    </form>
</body>
</html>

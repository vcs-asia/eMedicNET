<%@ Page Language="C#" AutoEventWireup="true" Inherits="report_preview" Codebehind="report-preview.aspx.cs" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .tbl{
            font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size:70%;
            width:100%;
        }
        .tblx{
            font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size:50%;
            width:100%;
        }
        .foot{
            font-family:'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
            font-size:30%;
            width:100%;
        }
    </style>
    <title></title>
</head>
<body onload="javascript:window.print();">
    <form id="frm" runat="server">
        <%=dataReport(Request.QueryString[0].ToString(), Request.QueryString[1].ToString()) %>
    </form>
</body>
</html>

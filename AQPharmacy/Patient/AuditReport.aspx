<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Audit Report" AutoEventWireup="true" Inherits="Patient_AuditReport" Codebehind="AuditReport.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Receipts</h4>
    <div class="well">
        <div class="control-group">
            <label class="control-label">Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtDate" CssClass="dt" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <p>
            <Button type="button" class="btn btn-success" onclick="openRpt()">Process</Button>
        </p>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function openRpt() {
            var fdate = document.getElementById("txtDate").value;
            var winRpt = window.open("<%:ResolveUrl("~/report-preview.aspx")%>?_d=" + fdate.split("/").join('') + fdate.split("/").join("") + "&_t=ar", "_blank");
            return false;
        }
    </script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/eMedicNET.master" Title="Login Details" Inherits="Manage_LoginDetails" Codebehind="LoginDetails.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Login Details</h4>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>Oh snap!</strong> Error in saving data. Please check and save again.
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>Oh Yeah!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <div class="well">
        <div class="control-group">
            <label class="control-label">From Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtFromDate" CssClass="dt" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">To Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtToDate" CssClass="dt" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
    </div>
    <p>
        <button type="button" class="btn btn-primary" onclick="generateReport()">Generate</button>
        <button type="button" class="btn" onclick="goBack()">Back</button>
    </p>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        function generateReport() {
            var fdate = $("#txtFromDate").val();
            var tdate = $("#txtToDate").val();

            window.open("<%:ResolveUrl("~/report-preview.aspx?param=")%>" + fdate.split('/').join('') + tdate.split('/').join('') + "&_mD=LOGIN");
        }
    </script>
</asp:Content>

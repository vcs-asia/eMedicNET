<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Manage_Reporting" Codebehind="Reporting.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Reports - MIS</h4>
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
            <label class="control-label" for="lstReport">Choose Report</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstReport" ClientIDMode="Static">
                    <asp:ListItem Value="SALES">Sales</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">Month</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtMonth" ClientIDMode="Static">
                    <asp:ListItem Value="01">JANUARY</asp:ListItem>
                    <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                    <asp:ListItem Value="03">MARCH</asp:ListItem>
                    <asp:ListItem Value="04">APRIL</asp:ListItem>
                    <asp:ListItem Value="05">MAY</asp:ListItem>
                    <asp:ListItem Value="06">JUNE</asp:ListItem>
                    <asp:ListItem Value="07">JULY</asp:ListItem>
                    <asp:ListItem Value="08">AUGUST</asp:ListItem>
                    <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                    <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                    <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                    <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">Year</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtYear" Required="true" ClientIDMode="Static"></asp:TextBox>
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
            var rpt = $("#lstReport").val();
            var mn = $("#txtMonth").val();
            var yr = $("#txtYear").val();

            window.open("<%:ResolveUrl("~/report-preview.aspx?param=")%>" + mn + yr + "&_mD=" + rpt);
        }
        function goBack() {
            window.history.back();
        }
    </script>
</asp:Content>

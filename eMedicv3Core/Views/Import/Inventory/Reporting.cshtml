<%@ Page Language="C#" Title="Reporting" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_Reporting" Codebehind="Reporting.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Reports - Inventory</h4>
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
                    <asp:ListItem Value="GRNDetailsList">List of GRN</asp:ListItem>
                    <asp:ListItem Value="STK">Current Stock</asp:ListItem>
                    <asp:ListItem Value="STKVAL">Current Stock - Value</asp:ListItem>
                    <asp:ListItem Value="STKB">Current Stock - Batch</asp:ListItem>
                    <asp:ListItem Value="DRUGLIST">Drug List - Selling Price</asp:ListItem>
                    <asp:ListItem Value="ISSVAL">Issues with Value</asp:ListItem>
                    <asp:ListItem Value="RORDER">Stock to Order</asp:ListItem>
                    <asp:ListItem Value="EXPIRE">Stock Expiring</asp:ListItem>
                    <asp:ListItem Value="DMARKUP">Drugs Markup Report</asp:ListItem>
                    <asp:ListItem Value="IMARKUP">Items Markup Report</asp:ListItem>
                    <asp:ListItem Value="OSTKVAL">Outlets - Current Stock Value</asp:ListItem>
                    <asp:ListItem Value="OUTPO">Outstanding PO List</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">Outlets</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstOutlets" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">Days</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtDays" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtFromDate">From Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtFromDate" CssClass="dt" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtToDate">To Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtToDate" CssClass="dt" ClientIDMode="Static"></asp:TextBox>
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
        $('#lstDrugs').attr('disabled', true);
        $('#lstReport').change(function () {
            if ($('#lstReport').val() == "4") {
                $('#lstDrugs').removeAttr('disabled');
            }
            else if ($('#lstReport').val() == "STK" || $('#lstReport').val() == "STKB") {
                $('#lstDrugs').attr('disabled', true);
                $('#txtFromDate').attr('disabled', true);
                $('#txtToDate').attr('disabled', true);
                //$('#txtToDate').removeAttr('disabled');
            }
            else {
                //$('#lstDrugs').attr('disabled',true);
                //$('#lstDrugs').attr('disabled', true);
                $('#txtFromDate').removeAttr('disabled');
                $('#txtToDate').removeAttr('disabled');
            }
        });
        function generateReport() {
            var rpt = $("#lstReport").val();
            var fdate = $("#txtFromDate").val();
            var tdate = $("#txtToDate").val();
            var days = $("#txtDays").val();
            var outlet = $("#lstOutlets").val();
            var outletname = $("#lstOutlets option:selected").text();

            if (rpt == 'EXPIRE') {
                window.open("<%:ResolveUrl("~/report-preview.aspx")%>?param=" + days + "&mod=" + rpt);
            }
            else if (rpt == "OSTKVAL") {
                window.open("<%:ResolveUrl("~/report-preview.aspx")%>?param=" + outlet + "." + outletname + "&mod=" + rpt);
            }
            else {
                window.open("<%:ResolveUrl("~/report-preview.aspx")%>?param=" + fdate.split('/').join('') + tdate.split('/').join('') + "." + days + "&mod=" + rpt);
            }
    }
    function goBack() {
        window.history.back();
    }
    </script>
</asp:Content>

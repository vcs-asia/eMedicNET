<%@ Page Language="C#" Title="Reporting" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Patient_Reporting" Codebehind="Reporting.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Reports - Patient Data</h4>
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
                    <asp:ListItem Value="VISITS">Visit Summary</asp:ListItem>
                    <asp:ListItem Value="PANELS">Panel Summary</asp:ListItem>
                    <asp:ListItem Value="PANELV">Panel Visit Summary</asp:ListItem>
                    <asp:ListItem Value="AUDIT">Audit Reporting</asp:ListItem>
                    <asp:ListItem Value="TRAFFIC">Daily Traffic Report</asp:ListItem>
                    <asp:ListItem Value="DRUGSDISPENSE">Drugs Dispensing Report</asp:ListItem>
                    <asp:ListItem Value="DRUGDISPENSE">Drug Dispensing Report</asp:ListItem>
                    <asp:ListItem Value="PANELR">Patient Payment Details(Panel)</asp:ListItem>
                    <asp:ListItem Value="CASHR">Patient Payment Details</asp:ListItem>
                    <asp:ListItem Value="PSY">Psy. Drug Report</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="lstReport">Drugs</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstDrugs" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">Pay Mode</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstPMode" ClientIDMode="Static">
                </asp:DropDownList>
            </div>
        </div>
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
        // VISITS PANELS PANELV AUDIT TRAFFIC DRUGSDISPENSE PANELR CASHR CARDR 
        $('#lstDrugs').attr('disabled', true);
        $('#lstPMode').attr('disabled', true);
        $('#lstReport').change(function (event) {
            event.preventDefault();
            if ($('#lstReport').val() == "DRUGDISPENSE") {
                $('#lstDrugs').prop('disabled', false);
            }
            else if ($('#lstReport').val() == "CASHR") {
                $('#lstPMode').prop('disabled', false);
            }
            else if ($('#lstReport').val() == "PSY") {
                $('#lstDrugs').prop('disabled', false);
            }
            else {
                $('#lstDrugs').prop('disabled', true);
                $('#lstPMode').prop('disabled', true);

            }
        });
        function generateReport() {
            var rpt = $("#lstReport").val();
            var fdate = $("#txtFromDate").val();
            var tdate = $("#txtToDate").val();
            var drug = $("#lstDrugs").val();
            var mode = $("#lstPMode").val();
            var tmde = $("#lstPMode :selected").text();

            if ($('#lstReport').val() == "DRUGDISPENSE")
                window.open("<%:ResolveUrl("~/report-preview.aspx?param=")%>" + fdate.split('/').join('') + tdate.split('/').join('') + "." + drug + ".1&_mD=" + rpt);
            else if ($('#lstReport').val() == "CASHR")
                window.open("<%:ResolveUrl("~/report-preview.aspx?param=")%>" + fdate.split('/').join('') + tdate.split('/').join('') + "." + mode + "." + tmde + "&_mD=" + rpt);
            else if ($('#lstReport').val() == "PSY")
                window.open("<%:ResolveUrl("~/report-preview.aspx?param=")%>" + fdate.split('/').join('') + tdate.split('/').join('') + ".1." + drug + "&_mD=" + rpt);
            else
                window.open("<%:ResolveUrl("~/report-preview.aspx?param=")%>" + fdate.split('/').join('') + tdate.split('/').join('') + "&_mD=" + rpt);
        }
        function goBack() {
            window.history.back();
        }
    </script>
</asp:Content>

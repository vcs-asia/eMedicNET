<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Test List" AutoEventWireup="true" Inherits="Patient_patList" Codebehind="patList.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Patients List</h4>
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
    <div class="well">
        <table id="myTable">
            <thead>
                <tr>
                    <th>Patient Name</th>
                    <th>IC No.</th>
                    <th>Folder No.</th>
                    <th>Birth Date</th>
                    <th>Regn. Date</th>
                    <th>Panel</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                </tr>
            </thead>
        </table>
    </div>
    <link href="jquery.dataTables.min.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script src="jquery.dataTables.mis.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#myTable').DataTable({
                "processing": true,
                "serverSide": true,
                "filter": false,
                "orderMulti": false,
                "ajax": {
                    "url": "/home/loadData",
                    "type": "POST",
                    "dataType": "json",
                },
                "columns": [
                    { "data": "ContactName", "name": "ContactName", "autoWidth": true },
                    { "data": "CompanyName", "name": "CompanyName", "autoWidth": true },
                    { "data": "Phone", "name": "Phone", "autoWidth": true },
                    { "data": "Country", "name": "Country", "autoWidth": true },
                    { "data": "City", "name": "City", "autoWidth": true },
                    { "data": "PostalCode", "name": "PostalCode", "autoWidth": true }
                ]
            });
        });
    </script>
</asp:Content>
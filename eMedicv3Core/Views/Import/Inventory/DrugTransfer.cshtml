<%@ Page Language="C#" Title ="Transfer" MasterPageFile="~/eMedicNET.master" AutoEventWireup ="true" Inherits="Inventory_DrugTransfer" Codebehind="DrugTransfer.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Stock Transfer</h3>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Ref No."></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtRefNo" ClientIDMode="Static"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdnFlag"  ClientIDMode="Static" Value="0"/>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="From"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="txtOutlet" CssClass="input-xxlarge" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="btnOK"></asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="To"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstOutlet" CssClass="input-xxlarge"></asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Transfer Date"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtIssueDate" Required="true" ClientIDMode="Static" CssClass="dt"></asp:TextBox>
        </div>
    </div>
    <div class="bs-docs-example">
        <ul id="myDrugTab" class="nav nav-tabs">
        </ul>
        <div id="myTabContent" class="tab-content">
        </div>
    </div>
    <div class="control-group">
        <table id="tblDrugs" border="1" style="width:100%;border-collapse:separate;border-spacing:1px;">
            <tbody>
                <tr>
                    <th style="width:70%;padding:5px;">Drug Name</th>
                    <th style="width:30%;padding:5px;" colspan="4">Transfer Quantity</th>
                </tr>
            </tbody>
        </table>
    </div>
    <Button type="button" class="btn  btn-inverse" onclick="javascript:operer.location.reload();winT.close();">Close</Button>
    <Button type="button" class="btn btn-success" onclick="saveTransfer()">Save</Button>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function getDrugDtls(rowID) {
            var outID = document.getElementById("txtOutlet");
            var medID = document.getElementsByName("drug[]")[rowID];

            $.ajax({
                type: 'POST',
                url: 'DrugTransfer.aspx/getDrugDetails',
                data: '{medID:"' + medID.value + '",outletID:"' + outID.value + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (respond) {
                    var retVal = respond.d;
                    document.getElementsByName("avlq[]")[rowID].value = retVal.split('-')[0];
                    document.getElementsByName("suom[]")[rowID].value = retVal.split('-')[1];
                },
                error: function () {
                    alert("AJAX calling error");
                }
            });
        }
        function saveTransfer() {
            var data = $('#frm').serializeArray();
            $.ajax({
                type: 'POST',
                url: 'DrugTransfer.aspx/saveInfo',
                data: JSON.stringify({ frmValues: data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d!='') {
                        alert(response.d);
                    }
                    else {
                        opener.location.reload();
                        window.close();
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.resposeText);
                }
            });
        }
    </script>
</asp:Content>


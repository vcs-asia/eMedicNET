<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_Item" Codebehind="Item.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Item</h3>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <p>
        <button class="btn btn-inverse" onClick="javascript:unlockRecord('<%=(Request.QueryString["id"]==null?"0":Request.QueryString["id"].ToString())%>','MEDICINE_MST');">Close</button>
    </p>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Stock Code"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtCode" Required="true" ClientIDMode="Static" placeholder="019900901"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdnID" Value="0" />
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Cost Category"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstCostCat">

            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Description"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtDesc" Required="true" ClientIDMode="Static" placeholder="Drug/Item Name" CssClass="input-xxlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text=""></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtDescE" ClientIDMode="Static" placeholder="Generic Name" CssClass="input-xxlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Category"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstCategory"></asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Form"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstForm">

            </asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Cost Price [RM]"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtCostPrice" Required="true" ClientIDMode="Static" CssClass="input-mini" placeholder="5.00"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtBUOM" Required="true" ClientIDMode="Static" placeholder="BOX" CssClass="input-mini"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Packing"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPacking" Required="true" ClientIDMode="Static" placeholder="1000" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtPSUOM" ClientIDMode="Static" placeholder="TAB" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtPBUOM" ClientIDMode="Static" placeholder="BOX" CssClass="input-mini" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Unit Cost"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtUnitCost" ClientIDMode="Static" placeholder="0.10" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtUSUOM" ClientIDMode="Static" placeholder="TAB" CssClass="input-mini" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Selling (In) [RM]"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtSellingIn" Required="true" ClientIDMode="Static" placeholder="0.40" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtSUOMI" Required="true" ClientIDMode="Static" placeholder="TAB" CssClass="input-mini" ReadOnly="true"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtIMarkup" Required="true" ClientIDMode="Static" placeholder="100%" CssClass="input-mini"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Selling (Out) [RM]"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtSellingOut" Required="true" ClientIDMode="Static" placeholder="0.50" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtSUOMO" ClientIDMode="Static" ReadOnly="true" placeholder="TAB" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtOMarkup" Required="true" ClientIDMode="Static" placeholder="90%" CssClass="input-mini"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Caution"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtCaution" ClientIDMode="Static" placeholder="ANTI-BIOTIC" CssClass="input-xxlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Min. Order"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtMinLevel" ClientIDMode="Static" placeholder="1000" CssClass="input-mini"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtSUOMM" ClientIDMode="Static" ReadOnly="true" placeholder="TAB" CssClass="input-mini"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Active"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="actFlag">
                <asp:ListItem Value="0">NO</asp:ListItem>
                <asp:ListItem Value="1">YES</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <p>
        <button type="button" class="btn" onclick="saveInfo()">Save</button>
        <button class="btn btn-inverse" onClick="javascript:unlockRecord('<%=(Request.QueryString["id"]==null?"0":Request.QueryString["id"].ToString())%>','MEDICINE_MST');">Close</button>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $("#txtBUOM").blur(function () { $("#txtPBUOM").val($("#txtBUOM").val()); });
        $("#txtPSUOM").blur(function () { $("#txtSUOMI").val($("#txtPSUOM").val()); $("#txtSUOMO").val($("#txtPSUOM").val()); $("#txtUSUOM").val($("#txtPSUOM").val()); $("#txtSUOMM").val($("#txtPSUOM").val()); });
        $("#txtPacking").blur(function () { $("#txtUnitCost").val(($("#txtCostPrice").val() / $("#txtPacking").val()).toFixed(2)) });
        $("#txtIMarkup").blur(function () { $("#txtSellingIn").val((parseFloat($("#txtUnitCost").val()) + (parseFloat($("#txtUnitCost").val()) * parseFloat($("#txtIMarkup").val())) / 100).toFixed(2)); });
        $("#txtOMarkup").blur(function () { $("#txtSellingOut").val((parseFloat($("#txtUnitCost").val()) + (parseFloat($("#txtUnitCost").val()) * parseFloat($("#txtOMarkup").val())) / 100).toFixed(2)); });
        $("#txtSellingIn").blur(function () { $("#txtIMarkup").val((((parseFloat($("#txtSellingIn").val()) - parseFloat($("#txtUnitCost").val())) / parseFloat($("#txtUnitCost").val())) * 100).toFixed(2)); });
        $("#txtSellingOut").blur(function () { $("#txtOMarkup").val((((parseFloat($("#txtSellingOut").val()) - parseFloat($("#txtUnitCost").val())) / parseFloat($("#txtUnitCost").val())) * 100).toFixed(2)); });
        function unlockRecord(qID, tblName) {
            /*
            $.ajax({
                type: 'POST',
                url: 'PatientVisit.aspx/unlockRecord',
                data: '{qID:"' + qID + '",tblName:"' + tblName + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (response.d.substring(0, 5) == 'ERROR') {
                        alert('Please try clicking close again.');
                    }
                    else {
                        opener.location.reload(); window.close();
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });*/
            opener.location.reload(); window.close();
        }
        function saveInfo() {
            var Data = $("#frm").serializeArray();
            $.ajax({
                type: 'POST',
                url: 'Item.aspx/saveInfo',
                data: JSON.stringify({ frmV: Data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d != '') {
                        alert(response.d);
                    }
                    else {
                        opener.location.reload();
                        window.close();
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>

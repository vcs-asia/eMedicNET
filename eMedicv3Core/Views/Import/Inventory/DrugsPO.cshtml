<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugsPO" Codebehind="DrugsPO.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calculateAmount() {
            var grandTotal = 0;
            var amt = 0;
            for (var row = 0; row < $('input[name^="ordQ"]').length; row++) {
                if ($('input[name^="ordQ"').eq(row).val() != "") {
                    amt = ($('input[name^="ordQ"]').eq(row).val() * $('input[name^="cost"]').eq(row).val());
                    $('input[name^="amt"]').eq(row).val(amt.toFixed(2));
                }
                else {
                    amt = 0;
                }
                grandTotal += amt;
            }
            $("#txtAmount").val(grandTotal.toFixed(2));
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Purchase Order</h3>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <p>
        <button class="btn btn-inverse" onClick="javascript:unlockRecord('<%=(Request.QueryString["n"]==null?"0":Request.QueryString["n"].ToString())%>','PURCHASE_ORDER_INFO');">Close</button>
    </p>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="PO No."></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPONo" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Supplier"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="txtSupplier" CssClass="input-xxlarge"></asp:DropDownList>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Order Date"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtOrderDate" Required="true" ClientIDMode="Static" CssClass="dt"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Terms"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtTerms" ClientIDMode="Static"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Amount"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" Required="true"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdnPF" ClientIDMode="Static" Value="0"/>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Remarks"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtRemarks" CssClass="input-xxlarge" TextMode="MultiLine" Rows="3" ClientIDMode="Static"></asp:TextBox>
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
                    <th style="width:30%;padding:5px;">Drug Name</th>
                    <th style="width:32%;padding:5px;">Description</th>
                    <th style="width:15%;padding:5px;text-align:center">Packing</th>
                    <th style="width:5%;padding:5px;">Qty</th>
                    <th style="width:8%;padding:5px;text-align:right">Cost</th>
                    <th style="width:5%;padding:5px;text-align:right">Amount</th>
                    <th style="width:5%;padding:5px;">&nbsp;</th>
                </tr>
            </tbody>
        </table>
    </div>
    <p>
        <button type="button" class="btn btn-primary" onclick="saveInfo()">Save</button>
        <button type="button" class="btn btn-inverse" onclick="javascript:unlockRecord('<%=(Request.QueryString["n"]==null?"0":Request.QueryString["n"].ToString())%>','PURCHASE_ORDER_INFO');">Close</button>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function getDrugDtls(rowID) {
            var medID = $('select[name^="drug"]').eq(rowID).val();
            $.ajax({
                type: 'POST',
                url: 'DrugsPO.aspx/getDrugDetails',
                data: '{medID:"' + medID + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (respond) {
                    document.getElementsByName('pack[]')[rowID].value = respond.d.split('|')[0];
                    document.getElementsByName('cost[]')[rowID].value = respond.d.split('|')[1];
                    document.getElementsByName('uom[]')[rowID].value = respond.d.split('|')[3] + "/" + respond.d.split('|')[2];
                    document.getElementsByName('ddsc[]')[rowID].value = document.getElementsByName("drug[]")[rowID].options[document.getElementsByName("drug[]")[rowID].selectedIndex].text;
                },
                error: function () {
                    alert("AJAX calling error");
                }
            });
        }
        function saveInfo() {
            var Data = $("#frm").serializeArray();
            alert(Data);
            $.ajax({
                type: 'POST',
                url: 'DrugsPO.aspx/saveInfo',
                data: JSON.stringify({ formVars: Data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function(response){
                    if (response.d!='')
                        alert(response.d);
                    else{
                        window.close();
                        opener.location.reload();
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
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
    </script>
</asp:Content>

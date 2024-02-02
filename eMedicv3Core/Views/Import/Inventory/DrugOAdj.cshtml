<%@ Page Language="C#" Title="Outlet Stock Adjustment" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugOAdj" Codebehind="DrugOAdj.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Stock Adjustment</h3>
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
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Date"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtIssueDate" ClientIDMode="Static" CssClass="dt"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Outlet"></asp:Label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstOutlet" OnSelectedIndexChanged="fillDrugs" AutoPostBack="true" ClientIDMode="Static">

            </asp:DropDownList>
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
                    <th style="width:40%;padding:5px;">Drug Name</th>
                    <th style="width:30%;padding:5px;" colspan="4">Phy. Qty</th>
                    <th style="width:15%;padding:5px;">Variance</th>
                    <th style="width:15%;padding:5px;">Exp. Date</th>
                </tr>
            </tbody>
        </table>
    </div>
    <Button type="button" class="btn  btn-inverse" onclick="javascript:operer.location.reload();winT.close();">Close</Button>
    <Button type="button" class="btn btn-success" onclick="saveDetails()">Save</Button>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function getVQty(Qty, Pack, BUom, SUom) {
            var Stock;

            var BStock = parseInt(Qty / Pack);
            var SStock = Qty % Pack;

            if (BStock != 0 && SStock != 0) {
                Stock = BStock + ' ' + BUom + ' ' + SStock + ' ' + SUom;
            }
            else if (BStock == 0 && SStock != 0) {
                Stock = SStock + ' ' + SUom;
            }
            else if (BStock != 0 && SStock == 0) {
                Stock = BStock + ' ' + BUom;
            }

            return Stock;
        }
        function getVariance(rowID) {
            var drug = document.getElementsByName("drug")[rowID];
            var isbq = document.getElementsByName("isbq")[rowID];
            var issq = document.getElementsByName("issq")[rowID];
            var vqty = document.getElementsByName("vqty")[rowID];

            //var qty = parseInt(drug.value.split('.')[2]) - ((parseInt(drug.value.split('.')[1]) * parseInt(isbq.value)) + parseInt(issq.value));
            var qty = ((parseInt(drug.value.split('.')[1]) * parseInt(isbq.value)) + parseInt(issq.value)) - parseInt(drug.value.split('.')[2]);
            vqty.value = qty;
            document.getElementsByName("varq")[rowID].value = getVQty(qty, (parseInt(drug.value.split('.')[1])), document.getElementsByName("buom")[rowID].value, document.getElementsByName("suom")[rowID].value);
        }
        function getDrugDtls(rowID) {
            var medID = document.getElementsByName("drug")[rowID];
            var outID = $('#lstOutlet').val();

            $.ajax({
                type: 'POST',
                url: 'DrugOAdj.aspx/getDrugDetails',
                data: '{medID:"' + medID.value + '", outletID:"' + outID + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (respond) {
                    var retVal = respond.d;
                    //alert(retVal);
                    document.getElementsByName("avlq")[rowID].value = retVal.split('|')[0];
                    document.getElementsByName("buom")[rowID].value = retVal.split('|')[1];
                    document.getElementsByName("suom")[rowID].value = retVal.split('|')[2];
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
        function saveDetails() {
            if (document.getElementById("txtIssueDate").value == '') {
                alert('Date cannot be null');
                document.getElementById("txtIssueDate").focus();
                return;
            }
            var data = $('#frm').serializeArray();
            $.ajax({
                type: 'POST',
                url: 'DrugOAdj.aspx/saveInfo',
                data: JSON.stringify({ frmV: data }),
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
                    alert("Ajax Error:" + xhr.resposeText);
                }
            });
        }
    </script>
</asp:Content>

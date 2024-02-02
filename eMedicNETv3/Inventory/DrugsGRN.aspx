<%@ Page Title="Drug GRN" Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_DrugsGRN" Codebehind="DrugsGRN.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function calculateAmount() {
            var drugArray = document.getElementsByName("drug[]");
            var ordQty = document.getElementsByName("ordq[]");
            var recQty = document.getElementsByName("recb[]");
            var drgAmt = document.getElementsByName("amt[]");
            var gstAmt = document.getElementsByName("gst[]");
            var drgCst = document.getElementsByName("cost[]");
            var recsq = document.getElementsByName("recs[]");
            var drgpk = document.getElementsByName("pack[]");

            var grandTotal = 0;
            for (var row = 0; row < drugArray.length; row++) {
                var ucost = parseFloat(drgCst[row].value) / parseInt(drgpk[row].value);
                drgAmt[row].value = parseFloat(ucost) * ((parseInt(recQty[row].value) * parseInt(drgpk[row].value)) + parseInt(recsq[row].value)) ;
                drgAmt[row].value = (parseFloat(drgAmt[row].value) + parseFloat(gstAmt[row].value)).toFixed(2);
                grandTotal += parseFloat(drgAmt[row].value);
            }
            $("#txtTotal").val(grandTotal.toFixed(2));
        }
        function removeDrug(row) {
            var result = confirm("Are you sure want to REMOVE the drug?");
            if (result == true) {
                row.remove();
                if ($("#Lst > tbody >tr").length > 1) {
                    $("#X").val($("#Lst > tr").length);
                }
                else {
                    $("#X").val("");
                }
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Goods Received Note</h3>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <p>
        <button class="btn btn-inverse" onClick="javascript:unlockRecord('<%=(Request.QueryString["n"]==null?"0":Request.QueryString["n"].ToString())%>','GRN_INFO');">Close</button>
    </p>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Invoice No."></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtInvoiceNo" Required="true" ClientIDMode="Static"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdnPOFlag" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdnGRNID" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdnFlag" ClientIDMode="Static" />
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Invoice Date"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtInvDate" Required="true" ClientIDMode="Static"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Invoice Amount"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtInvoiceAmount" ClientIDMode="Static" Required="true" style="text-align:right"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Recd. Date."></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtRecDate" ClientIDMode="Static" Required="true"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="PO No."></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPONo" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
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
                    <th style="width:31%;padding:5px;">Drug Name</th>
                    <th style="width:12%;padding:5px;">Quantity Due</th>
                    <th style="width:4%;padding:5px;text-align:right">Pack</th>
                    <th style="width:20%;padding:5px;">Received</th>
                    <th style="width:7%;padding:5px;text-align:right">Cost</th>
                    <th style="width:7%;padding:5px;text-align:right">GST</th>
                    <th style="width:5%;padding:5px;text-align:right">Amount</th>
                    <th style="width:6%;padding:5px;text-align:right">Batch</th>
                    <th style="width:10%;padding:5px;text-align:right">Expires</th>
                </tr>
            </tbody>
        </table>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Total Amount"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtTotal" ReadOnly="true" ClientIDMode="Static" style="text-align:right"></asp:TextBox>
        </div>
    </div>
    <p>
        <button type="button" class="btn btn-success" onclick="javascript:saveRecord();">Save GRN</button>
        <!--<button type="button" class="btn btn-success" onclick="javascript:savePost();">Save & Post</button>-->
        <button type="button" class="btn btn-inverse" onClick="javascript:unlockRecord('<%=(Request.QueryString["n"]==null?"0":Request.QueryString["n"].ToString())%>','GRN_INFO');">Close</button>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $("#txtRecDate").datepicker({ format: 'dd/mm/yyyy' });
        $("#txtInvDate").datepicker({ format: 'dd/mm/yyyy' });
        $(".dp").datepicker({ format: 'dd/mm/yyyy' });
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
        function saveRecord() {
            var data = $('#frm').serializeArray();
            $.ajax({
                type: 'POST',
                url : 'DrugsGRN.aspx/saveGRN',
                data: JSON.stringify({ frmValues: data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d!=''){
                        alert(response.d);
                    }
                    else {
                        opener.location.reload(); window.close();
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
        function savePost() {
            var data = $('#frm').serializeArray();
            $.ajax({
                type: 'POST',
                url: 'DrugsGRN.aspx/saveGRN',
                data: JSON.stringify({ frmValues: data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d.substring(0, 5) == "ERROR") {
                        alert(response.d);
                    }
                    else {
                        $.ajax({
                            type: 'POST',
                            url: 'DrugsGRN.aspx/postGRN',
                            data: '',
                            dataType: 'json',
                            contentType: 'application/json;charset=utf-8',
                            success: function (response) {
                                if (response.d.substring(0, 5) == "ERROR") {
                                    alert(response.d);
                                }
                                else {
                                    opener.location.reload(); window.close();
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                alert(xhr.responseText);
                            }
                        });
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" Title="Receipt" MasterPageFile="~/eMedicNET.master" Inherits="Patient_ReceiptList" Codebehind="ReceiptList.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Receipts</h4>
    <div class="well">
        <div class="control-group">
            <label class="control-label">Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtDate" CssClass="dt"></asp:TextBox>
            </div>
        </div>
        <p>
            <asp:Button runat="server" ID="btn" CssClass="btn btn-success" Text="Process" OnClick="processList"/>
        </p>
    </div>
    <asp:GridView runat="server" ID="LstVisit" AutoGenerateColumns="false" DataKeyNames="VISIT_ID, PAT_ID, RECEIPT_NO" Width="100%" GridLines="None" AlternatingRowStyle-BackColor="Beige" OnRowEditing="OnRowEditing">
        <Columns>
            <asp:BoundField HeaderText="Rec No" DataField="RECEIPT_NO" ItemStyle-Width="10%" />
            <asp:BoundField HeaderText="Date" DataField="REC_DATE" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField HeaderText="Patient Name" DataField="PAT_NAME" ItemStyle-Width="40%" DataFormatString="{0:dd/MM/yyyy}"/>
            <asp:BoundField HeaderText="Total" DataField="TOT_AMT" ItemStyle-Width="13%" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Paid" DataField="PAID_AMT" ItemStyle-Width="13%"  DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right"/>
            <asp:BoundField HeaderText="Mode" DataField="PMODE" ItemStyle-Width="10%" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:editReceipt(" + Eval("RECEIPT_NO") + ", " + Eval("VISIT_ID") + ")") %>' ToolTip="Edit Receipt"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:deleteReceipt({0})", HttpUtility.UrlEncode(Eval("RECEIPT_NO").ToString())) %>' ToolTip="Delete Receipt"><i class="icon-ban-circle"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:printReceipt({0})", HttpUtility.UrlEncode(Eval("VISIT_ID").ToString())) %>' ToolTip="Print Receipt"><i class="icon-print"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--Receipt Starts-->
    <div class="modal hide fade" id="myReceipt" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myReceiptLabel">Receipt</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField runat="server" ID="hdnRecNo" ClientIDMode="Static" />
            <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
                <button type="button" class="close" data-dismiss="alert">x</button>
                <asp:Label runat="server" ID="lblError"></asp:Label>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
                <button type="button" class="close" data-dismiss="alert">x</button>
                <strong>SUCCESS!</strong>
                <asp:Label runat="server" ID="lblSuccess"></asp:Label>
            </asp:Panel>
            <div class="control-group">
                <label class="control-label" for="txtPatName">Patient Name</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPatName" ClientIDMode="Static" CssClass="input-xlarge" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Date</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtRecDate" CssClass="dt" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Total Amount</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtAmt" ClientIDMode="Static" CssClass="input-small" Text="0"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txtPaid">Paid</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPaid" ClientIDMode="Static" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Mode of Pay</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="payMode" ClientIDMode="Static">
                        <asp:ListItem Value="0">CASH</asp:ListItem>
                        <asp:ListItem Value="1">CHEQUE</asp:ListItem>
                        <asp:ListItem Value="2">CARD</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Remarks</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtRemarks" ClientIDMode="Static" CssClass="input-xlarge" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <asp:Button runat="server" CssClass="btn btn-success" Text="Save & Close" OnClick="saveReceipt" />
        </div>
    </div>
    <!--Receipt Ends-->
    <!--Receipt Deletion Starts-->
    <div class="modal hide fade" id="myDReceipt" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myDReceiptLabel">Receipt</h3>
        </div>
        <div class="modal-body">
            Are you sure want to delete the Receipt?
            <asp:HiddenField runat="server" ID="hdnDRecNo" ClientIDMode="Static" />
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button type="button" class="btn btn-danger" onclick="delReceipt()">Delete</button>
        </div>
    </div>
    <!--Receipt Ends-->
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function editReceipt(_qD, _vID) {
            $('#hdnRecNo').val(_qD);
            getReceiptDetails(_qD, _vID);
            $('#myReceipt').modal('show');
        }
        function getReceiptDetails(_recNo, _vID) {
            $.ajax({
                type: 'POST',
                url: 'ReceiptList.aspx/getReceiptDetails',
                data: '{recID:"' + _recNo + '",vID:"' + _vID + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (typeof response.d != 'undefined') {
                        document.getElementById("txtRecDate").value = response.d[0];
                        document.getElementById("txtAmt").value = response.d[1];
                        document.getElementById("txtPaid").value = response.d[2];
                        document.getElementById("payMode").value = response.d[3];
                        document.getElementById("txtRemarks").value = response.d[4];
                        document.getElementById("txtPatName").value = response.d[5];
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
        function printReceipt(_rNo) {
            var winP = window.open("<%:ResolveUrl("~/report-preview.aspx")%>?_v=" + _rNo + "&_m=RECEIPT", "_blank");
            return false;
        }
        function deleteReceipt(_rNo) {
            $('#hdnDRecNo').val(_rNo);
            $('#myDReceipt').modal('show');
        }
        function delReceipt() {
            $.ajax({
                type: 'POST',
                url: 'ReceiptList.aspx/deleteReceipt',
                data: '{rNo:"' + document.getElementById("hdnDRecNo").value + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d.substring(0, 5) == 'ERROR') {
                        alert(response.d);
                    }
                    else {
                        location.reload();
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>
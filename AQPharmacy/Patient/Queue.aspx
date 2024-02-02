<%@ Page Language="C#" AutoEventWireup="true" Title="Queue" MasterPageFile="~/eMedicNET.master" Inherits="Patient_Queue" Codebehind="Queue.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Patients Queue</h4>
    <div class="row-fluid">
        <div class="span3">
            <div class="control-group">
                <label class="control-label">Discipline</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="drpDiscipline" CssClass="input-medium">
                    </asp:DropDownList>
                    <asp:CheckBox runat="server" ID="chkDiscipline" Checked="false"/>
                </div>
            </div>
        </div>
        <div class="span3">
            <div class="control-group">
                <label class="control-label">Date</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="dt input-medium" Required="true" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span3">
            <div class="control-group">
                <label class="control-label">Status</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="queueStatus" CssClass="input-medium">
                        <asp:ListItem Value="Waiting">Waiting</asp:ListItem>
                        <asp:ListItem Value="Pharmacy">Pharmacy</asp:ListItem>
                        <asp:ListItem Value="Cash">Cash</asp:ListItem>
                        <asp:ListItem Value="Over">Over</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox runat="server" ID="chkStatus" Checked="false" CssClass="input-small" />
                </div>
            </div>
        </div>
        <div class="span3">
            <div class="control-group">
                <div class="controls">
                    <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Show" OnCommand="btnSearch_Command" />
                </div>
            </div>
        </div>
    </div>
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="PAT_ID, PAT_QUEUE_STATUS, VISIT_ID, PAT_DISC, PAT_QID" GridLines="Vertical" AutoGenerateColumns="false" CellPadding="1" CellSpacing="1" Width="100%"
         OnSorting="Sorting" AllowSorting="true" AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small" OnRowDataBound="RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText = "#" ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="PAT_REG_NO" SortExpression="PAT_REG_NO" ItemStyle-Width="5%" HeaderText="Folder" />
            <asp:BoundField DataField="PAT_QUEUE_DATE" SortExpression="PAT_QUEUE_DATE" ItemStyle-Width="5%" HeaderText="Time"   DataFormatString="{0:HH:mm}" />
            <asp:BoundField DataField="PAT_QUEUE_NEW_OLD" SortExpression="PAT_QUEUE_NEW_OLD" ItemStyle-Width="5%" HeaderText="New/Old" />
            <asp:TemplateField ItemStyle-Width="15%" SortExpression="COMP_NAME">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:changeDiscipline(" + Eval("PAT_QID") + "," + Eval("PAT_DISC") + "," + Eval("VISIT_ID") + ")") %>' ToolTip="Change discipline"><i class="icon-edit"></i></asp:HyperLink>
                    &nbsp;
                    <asp:Label runat="server"><%#Eval("COMP_NAME") %></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PAT_NAME" SortExpression="PAT_NAME" ItemStyle-Width="20%" HeaderText="Name of Patient" />
            <asp:BoundField DataField="PAT_IC_NO" SortExpression="PAT_IC_NO" ItemStyle-Width="13%"  HeaderText="IC No"/>
            <asp:BoundField DataField="PAT_AGE" SortExpression="PAT_AGE" ItemStyle-Width="8%"  HeaderText="Age"/>
            <asp:BoundField DataField="CUSER" SortExpression="CUSER" ItemStyle-Width="8%"  HeaderText="User"/>
            <asp:BoundField DataField="PAT_QUEUE_STATUS" SortExpression="PAT_QUEUE_STATUS" ItemStyle-Width="7%" HeaderText="Status" />
            <asp:TemplateField ItemStyle-Width="12%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format(ResolveUrl("~/Patient/Registration.aspx?PatID={0}").ToString(),HttpUtility.UrlEncode(Eval("PAT_ID").ToString()))%>' Target="_blank" ToolTip="Edit/View Patient Details"><i class="icon-edit"></i></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openVisit({0},{1},{2},{3},{4},\"{5}\")",HttpUtility.UrlEncode(Eval("PAT_ID").ToString()),HttpUtility.UrlEncode(Eval("PAT_QID").ToString()),HttpUtility.UrlEncode(Eval("VISIT_ID").ToString()), HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["vType"].ToString()),HttpUtility.UrlEncode(Eval("PAT_DISC").ToString()), "")%>' ToolTip="Today Visit Entry"><i class="icon-ok-sign"></i></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:newReceipt({0},{1})", HttpUtility.UrlEncode(Eval("VISIT_ID").ToString()), HttpUtility.UrlEncode(Eval("PAT_ID").ToString()))%>' ToolTip="Today Receipt" Text="$"></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:deleteFromQueue(" + Eval("PAT_QID") + ")") %>' ToolTip="Delete from Queue"><i class="icon-trash"></i></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:getDrugsOfAPatient(" + Eval("VISIT_ID") + ")") %>' ToolTip="Print Drugs Label"><i class="icon-print"></i></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:printNormalLabel(" + Eval("PAT_ID") + ")") %>' ToolTip="Print Label"><i class="icon-list-alt"></i></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:medicalExam(" + Eval("VISIT_ID") + ")") %>' ToolTip="Medical Exam"><i class="icon-file"></i></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:printReceipt(" + Eval("VISIT_ID") + ")") %>' ToolTip="Print Receipt" Text="P"></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:printInvoice(" + Eval("VISIT_ID") + ")") %>' ToolTip="Print Invoice" Text="I"></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:printME(" + Eval("VISIT_ID") + ")") %>' ToolTip="Print ME" Text="M"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <div class="row-fluid">
        <div class="span3">
            <div class="control-group">
                <div class="controls">
                    <button type="button" class="btn" onclick="javascript:location.reload();">Reload</button>
                </div>
            </div>
        </div>
    </div>
    <!--Patient queue deletion starts -->
    <div class="modal hide fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myModalLabel">Patient Queue Deletion</h3>
        </div>
        <div class="modal-body">
            <p>Are you sure want to delete from Queue?</p>
            <asp:HiddenField runat="server" ID="hdnQID" ClientIDMode="Static" />
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">No</button>
            <asp:Button runat="server" CssClass="btn btn-danger" Text="Yes. Confirm" OnClick="deleteQueue" />
        </div>
    </div>
    <!-- Patient queue deletion ends-->
    <!--Drugs label printing starts-->
    <div class="modal hide fade" id="myDrugLabel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myDrgLabel">Drugs Labels Printing</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField runat="server" ID="hdnDVisitID" ClientIDMode="Static" />
            <table id="myDrugTable">
                <thead>
                    <tr>
                        <th>&nbsp;</th>
                        <th>Drug Name</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <Button type="button" class="btn btn-inverse" onclick="printDrugLabel()">Print</Button>
        </div>
    </div>
    <!--Drugs label printing ends-->
    <!--Reception label printing starts-->
    <div class="modal hide fade" id="myLbl" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myLblLabel">Labels Printing</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField runat="server" ID="hdnNLabel" ClientIDMode="Static" />
            <div class="control-group">
                <label class="control-label">No. of Copies</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtCopies" ClientIDMode="Static" CssClass="input-small" Text="6"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <Button type="button" class="btn btn-inverse" onclick="printNLabel()">Print</Button>
        </div>
    </div>
    <!--Reception label printing ends-->
    <!--Change discipline starts-->
    <div class="modal hide fade" id="myDisc" tabindex="-1" role="dialog" aria-labelledby="myDiscLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myDiscLabel">Change Discipline</h3>
        </div>
        <div class="modal-body">
            <div class="control-group">
                <label class="control-label">Select Discipline</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstDiscipline" ClientIDMode="Static">
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="hdnPatQID" ClientIDMode="Static" />
                    <asp:HiddenField runat="server" ID="hdnPVisitID" ClientIDMode="Static" />
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button class="btn btn-inverse" onclick="saveDiscipline()">Change</button>
        </div>
    </div>
    <!--change discipline ends-->
    <!--Receipt Starts-->
    <div class="modal hide fade" id="myReceipt" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myReceiptLabel">Receipt</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField runat="server" ID="hdnRecNo" ClientIDMode="Static" />
            <div class="control-group">
                <label class="control-label" for="txtPatName">Patient Name</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPatName" ClientIDMode="Static" CssClass="input-xlarge" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txtPatName">Receipt No</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtRecNo" ClientIDMode="Static" CssClass="input-small" ReadOnly="true"></asp:TextBox>
                    <asp:HiddenField ID="hdnPatID" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnVisitID" runat="server" ClientIDMode="Static" />
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
                    <asp:TextBox runat="server" ID="txtAmt" ClientIDMode="Static" CssClass="input-small" Text="0"></asp:TextBox>&nbsp;<a href="#" onclick="javascript:getLatestAmount()">Refresh</a>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="txtPaid">Paid [RM]</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPaid" ClientIDMode="Static" CssClass="input-small" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <table id="tblPayMode" cellspacing="1" cellpadding="1" border="1">
                        <thead>
                            <tr><th width="10%">#</th><th width="30%">Mode</th><th width="20%">Amount[RM]</th><th width="40%">Remarks</th></tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button type="button" class="btn btn-success" onclick='javascript:saveReceipt()'>Save</button>
        </div>
    </div>
    <!--Receipt Ends-->
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function callMe(hyperLink) {
            var myWindow = window.open(hyperLink, "_blank", "toolbar=yes, scrollbar=yes, resizable=yes, fullscreen=yes");
        }
        function deleteFromQueue(_qD) {
            $('#hdnQID').val(_qD);
            $('#myModal').modal('show');
        }
        function changeDiscipline(_qD, _dD, _vD) {
            $('#hdnPatQID').val(_qD);
            $('#hdnPVisitID').val(_vD);
            $('#lstDiscipline').val(_dD);
            $('#myDisc').modal('show');
        }
        function saveDiscipline() {
            var qid = document.getElementById("hdnPatQID").value;
            var vid = document.getElementById("hdnPVisitID").value;
            var did = document.getElementById("lstDiscipline").value;

            $.ajax({
                type: 'POST',
                url: 'Queue.aspx/saveDiscipline',
                data: '{qid:"' + qid + '", vid:"' + vid + '", did:"' + did + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    location.reload();
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
        }
        function printDrugLabel() {
            var drugID = document.getElementsByName("chkid[]");
            var drugArray = new Array(drugID.length);
            for (var inc = 0; inc < drugID.length; inc++) {
                if (drugID[inc].checked == true) {
                    drugArray[inc] = drugID[inc].value;
                }
                else {
                    drugArray[inc] = '0';
                }
            }
            $.ajax({
                type:'POST',
                url: 'Queue.aspx/printDLabel',
                data: '{id:"' + document.getElementById("hdnDVisitID").value + '",drugid:"' + drugArray + '"}',
                success: function (respond) {
                    if (respond.d.substring(0, 5) == 'ERROR') {
                        alert(respond.d);
                    }
                    else {
                        alert('Label(s) for printing has been sent.');
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                },
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                traditional: true
            });
        }
        function printNormalLabel(_qD) {
            $('#hdnNLabel').val(_qD);
            $('#myLbl').modal('show');
        }
        function printNLabel() {
            $.ajax({
                type: 'POST',
                url: 'Queue.aspx/printNLabel',
                data: '{patID:"' + document.getElementById('hdnNLabel').value + '", nCopies:"'+ document.getElementById('txtCopies').value +'"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    if (respond.d.substring(0, 5) == 'ERROR') {
                        alert(respond.d);
                    }
                    else {
                        $('#myLbl').modal('hide');
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
        }
        function saveReceipt() {
            var Data = $("#frm").serializeArray();
            $.ajax({
                type: 'POST',
                url: 'Queue.aspx/saveReceipt',
                data: JSON.stringify({formVars:Data}),
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (response.d!='') {
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
        function newReceipt(_vD, _pD) {
            $.ajax({
                type: 'POST',
                url: 'Queue.aspx/getReceiptDetails',
                data: '{vID:"' + _vD + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (typeof response.d != 'undefined') {
                        document.getElementById("txtRecDate").value = response.d[0];
                        document.getElementById("txtAmt").value = response.d[1];
                        document.getElementById("txtPaid").value = response.d[2];
                        document.getElementById("txtPatName").value = response.d[3];
                        document.getElementById("txtRecNo").value = response.d[4];
                        document.getElementById("hdnPatID").value = _pD;
                        document.getElementById("hdnVisitID").value = _vD;
                        getPayModes(response.d[2], response.d[4]);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
            $('#myReceipt').modal('show');
        }
        function getPayModes(_amt, _recn) {
            $('#tblPayMode > tbody').html("");
            $.ajax({
                type: 'POST',
                url: 'Queue.aspx/getPaymentModes',
                data: '{recNo:"' + _recn + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    for (var inc = 0; inc < response.d.length; inc++) {
                        if (_recn == 'NEW RECEIPT') {
                            if (inc == 0) {
                                $('#tblPayMode > tbody:last').append('<tr><td>' + (inc + 1) + '<input type=\"hidden\" id=\"pmode[]\" name=\"pmode[]\" value=\"' + response.d[inc].split('|')[0] + '\" style=\"width:90%\"/></td><td>' + response.d[inc].split('|')[1] + '</td><td><input type=\"text\" id=\"pdamt[]\" name=\"pdamt[]\" value=\"' + _amt + '\" style=\"width:90%\" onblur=\"calculatePAmt()\"/></td><td><input type=\"text\" id=\"rmrks[]\" name=\"rmrks[]\" value=\"\" style=\"width:90%\"/></tr>');
                            }
                            else {
                                $('#tblPayMode > tbody:last').append('<tr><td>' + (inc + 1) + '<input type=\"hidden\" id=\"pmode[]\" name=\"pmode[]\" value=\"' + response.d[inc].split('|')[0] + '\" style=\"width:90%\"/></td><td>' + response.d[inc].split('|')[1] + '</td><td><input type=\"text\" id=\"pdamt[]\" name=\"pdamt[]\" value=\"' + 0 + '\" style=\"width:90%\" onblur=\"calculatePAmt()\"/></td><td><input type=\"text\" id=\"rmrks[]\" name=\"rmrks[]\" value=\"\" style=\"width:90%\"/></td></tr>');
                            }
                        }
                        else {
                            $('#tblPayMode > tbody:last').append('<tr><td>' + (inc + 1) + '<input type=\"hidden\" id=\"pmode[]\" name=\"pmode[]\" value=\"' + response.d[inc].split('|')[0] + '\" style=\"width:90%\"/></td><td>' + response.d[inc].split('|')[1] + '</td><td><input type=\"text\" id=\"pdamt[]\" name=\"pdamt[]\" value=\"' + response.d[inc].split('|')[2] + '\" style=\"width:90%\" onblur=\"calculatePAmt()\"/></td><td><input type=\"text\" id=\"rmrks[]\" name=\"rmrks[]\" value=\"' + response.d[inc].split('|')[3] + '\" style=\"width:90%\"/></td></tr>');
                        }
                    }
                },
                error: function(xhr, ajaxOptions, thrownerror){
                    alert(xhr.responseText);
                }
            });
        }
        function calculatePAmt() {
            var tAmt = 0;
            for (row = 0; row < document.getElementsByName('pdamt[]').length; row++) {
                if (document.getElementsByName('pdamt[]')[row].value != '') {
                    var amt = parseFloat(document.getElementsByName('pdamt[]')[row].value);
                    document.getElementsByName('pdamt[]')[row].value = amt.toFixed(2);
                    tAmt += amt;
                }
            }
            $('#txtPaid').val(tAmt.toFixed(2));
        }
        function printReceipt(_vD) {
            var winrec = window.open('<%:ResolveUrl("~/report-preview.aspx?_v=")%>' + _vD + '&_m=RECEIPT', '_blank');
            return false;
        }
        function printInvoice(_vD) {
            var winrec = window.open('<%:ResolveUrl("~/report-preview.aspx?_v=")%>' + _vD + '&_m=PINVOICE', '_blank');
            return false;
        }
        function printME(_vD) {
            var winrec = window.open('<%:ResolveUrl("~/report-preview.aspx?_v=")%>' + _vD + '&_m=ME', '_blank');
            return false;
        }
        $('#txtPaid').blur(function () {
            if (parseFloat($('#txtAmt').val()) < parseFloat($('#txtPaid').val())) {
                $('#txtPaid').val($('#txtAmt').val());
            }
        });
        function medicalExam(_vD) {
            if (_vD != '0') {
                var winME = window.open("<%:ResolveUrl("~/Patient/ME.aspx?_vD=")%>" + _vD, "_blank");
                return false;
            }
        }
        function getLatestAmount() {
            var _vD = document.getElementById("hdnVisitID").value;
            $.ajax({
                type: 'POST',
                url: "Queue.aspx/getLatestAmount",
                data: '{vD:"' + _vD + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    document.getElementById("txtAmt").value = response.d;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
        function openVisit(_pD, _qD, _vD, _op, _pS, _uI) {
            $.ajax({
                type: "POST",
                url: "Queue.aspx/lockQueue",
                data: '{qID:"' + _qD + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (response.d == 'N') {
                        if (_op == "1")
                        {
                            var myWindow1 = window.open('<%:ResolveUrl("~/Patient/PatientVisit.aspx")%>' + '?PatID=' + _pD + '&_vD=' + _vD + '&_qD=' + _qD + '&_pS=' + _pS + '&_uI=' + _uI, '_blank');
                        }
                        else if (_op=="2")
                        {
                            var myWindow2 = window.open('<%:ResolveUrl("~/Patient/Visit_1.aspx")%>' + '?PatID=' + _pD + '&_vD=' + _vD + '&_qD=' + _qD + '&_pS=' + _pS + '&_uI=' + _uI, '_blank');
                        }
                        return false;
                    }
                    else
                        alert("The record is being accessed by another user. Please wait until it is unlocked.");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
        function getDrugsOfAPatient(visitID) {
            $.ajax({
                type: 'POST',
                url: 'Queue.aspx/getDrugsOfAPatient',
                data: '{visitID:"' + visitID + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d.substr(0, 5) == "ERROR") {
                        alert(response.d);
                    }
                    else {
                        $('#hdnDVisitID').val(visitID);
                        $('#myDrugTable').find("tr:not(:first)").remove();
                        $('#myDrugTable > tbody:last').append(response.d);
                        $('#myDrugLabel').modal('show');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>
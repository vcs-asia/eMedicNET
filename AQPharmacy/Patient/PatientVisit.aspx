<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Patient_PatientVisit" Codebehind="PatientVisit.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        function scal(row) {
            var serID = document.getElementsByName("service[]")[row].value;
            document.getElementsByName("amt[]")[row].value = (parseFloat(document.getElementsByName("charge[]")[row].value) - parseFloat(document.getElementsByName("discount[]")[row].value)).toFixed(2);
            calculate();
        }
        function calculate() {
            var grandTotal = 0; var tAmt = 0; var tDsc = 0; var tChr = 0;
            for (row = 0; row < document.getElementsByName('service[]').length; row++) {
                if (document.getElementsByName('service[]')[row].value != '') {
                    var chr = parseFloat(document.getElementsByName('charge[]')[row].value);
                    var dsc = parseFloat(document.getElementsByName('discount[]')[row].value);
                    var amt = parseFloat(document.getElementsByName('amt[]')[row].value);
                    document.getElementsByName('amt[]')[row].value = amt.toFixed(2);
                    grandTotal += amt;
                    tAmt += amt; tChr += chr; tDsc += dsc;
                }
            }
            $('#txtAmount').val(grandTotal.toFixed(2));
            $('#txtTCharges').val(tChr.toFixed(2));
            $('#txtTDiscount').val(tDsc.toFixed(2));
            $('#txtTSAmount').val(tAmt.toFixed(2));
        }   
        function drugcal(row) {
            document.getElementsByName("mamt[]")[row].value = document.getElementsByName("scost[]")[row].value * document.getElementsByName("mqty[]")[row].value;
            calculateDrugs();
        }
        function calculateDrugs() {
            var tAmt = 0; var tDsc = 0;
            for (row = 0; row < document.getElementsByName('drug[]').length; row++) {
                if (document.getElementsByName('drug[]')[row].value != '') {
                    var amt = parseFloat(document.getElementsByName('scost[]')[row].value) * parseFloat(document.getElementsByName('mqty[]')[row].value);
                    var dsc = parseFloat(document.getElementsByName('mdisc[]')[row].value);
                    document.getElementsByName('mamt[]')[row].value = amt.toFixed(2);
                    tAmt += amt; tDsc += dsc;
                }
            }
            $('#txtTDAmount').val(tAmt.toFixed(2));
            $('#txtTDDiscount').val(tDsc.toFixed(2));
            calculateServiceChargesExceptDrugs(tAmt.toFixed(2));
        }
        function calculateServiceChargesExceptDrugs(drugsAmount) {
            var grandTotal = 0;
            for (row = 0; row < document.getElementsByName('service[]').length; row++) {
                if (document.getElementsByName('service[]')[row].value != '' && document.getElementsByName('service[]')[row].value != '283') {
                    var amt = parseFloat(document.getElementsByName('amt[]')[row].value);
                    document.getElementsByName('amt[]')[row].value = amt.toFixed(2);
                    grandTotal += amt;
                }
            }
            $('#txtAmount').val((parseFloat(drugsAmount) + parseFloat(grandTotal)).toFixed(2));
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Patient Visit</h4>
    <asp:Panel runat="server" ID="pnlDeleteAlert" CssClass="alert alert-block alert-error fade in" Visible="false">
        <a class="close" data-dismiss="alert">x</a>
        <h4 class="alert-heading">Oh! You are trying to delete</h4>
        <p>
            Are you sure want to delete the selected record?
        </p>
        <p>
            <asp:Button runat="server" CssClass="btn btn-danger" Text="Yes. Delete" OnClick="deleteMe" />
            <asp:Button runat="server" CssClass="btn" Text="No. I dont want to Delete" OnClick="cancelDelete"/>
        </p>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong>
        <asp:Label runat="server" ID="lblSuccess"></asp:Label>
    </asp:Panel>
    <div class="row-fluid">
        <div class="span4">
            <div class="control-group">
                <label class="control-label">Patient Name<br />IC No.<br />Date of Birth<br />Age</label>
                <asp:HiddenField runat="server" ID="pID" ClientIDMode="Static"/>
                <asp:HiddenField runat="server" ID="qID" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="vID" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="pDS" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="uID" ClientIDMode="Static" />
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPatName" ClientIDMode="Static" CssClass="input-xlarge" ReadOnly="true" TextMode="MultiLine" Rows="4"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span4 offset2">
            <div class="control-group">
                <label class="control-label">Allergies</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPAllergies" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="control-group">
        <label class="control-label">Panel</label>
        <div class="controls">
            <asp:DropDownList runat="server" ID="txtCompany">
            </asp:DropDownList>
            <asp:HiddenField runat="server" ID="hdnCompanyID" />
        </div>
    </div>
    <div class="control-group">
        <label class="control-label">Previous History</label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPrevHistory" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="3"></asp:TextBox>
        </div>
    </div>
    <div class="btn-group">
        <button class="btn btn-primary">Previous Visits</button>
        <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></button>
        <ul class="dropdown-menu">
            <%
            if (Request.QueryString["PatID"] != null)
            { %>
                <!--<li><a href="#"><i class="icon-pencil"></i> Edit</a></li>-->
                <%=getVisitLinks(Request.QueryString["PatID"].ToString()) %>
            <%
            }
            %>
            <!--<li class="divider"></li>
            <li><a href="#"><i class="i"></i> Make admin</a></li>-->
        </ul>
	</div>
    <hr />
	<ul id="myTab" class="nav nav-tabs">
		<li class="active"><a href="#hminfo" data-toggle="tab">Main Info</a></li>
		<li><a href="#mdinfo" data-toggle="tab">Medical Info</a></li>
		<li><a href="#chinfo" data-toggle="tab">Charges</a></li>
		<li><a href="#dginfo" data-toggle="tab">Drugs</a></li>
		<li><a href="#clinfo" data-toggle="tab">Child Info</a></li>
	</ul>
    <div class="tab-content">
        <div class="tab-pane fade in active" id="hminfo">
            <div class="well">
                <div class="row-fluid">
                    <div class="span4">
                        <div class="control-group">
                            <label class="control-label" for="txtVisitDate">Date</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtVisitDate" CssClass="dt" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="span4 offset2">
                        <div class="control-group">
                            <label class="control-label" for="txtVisitTime">Time</label>
                            <div class="controls">
                                <div id="tm" class="input-append">
                                    <asp:TextBox runat="server" ID="txtVisitTime" data-format="hh:mm:ss" ClientIDMode="Static"></asp:TextBox>
                                    <span class="add-on">
                                        <i data-time-icon="icon-time" data-date-icon="icon-calendar"></i>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span4">
                        <div class="control-group">
                            <label class="control-label" for="txtBillNo">Bill No</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtBillNo" ClientIDMode="Static" CssClass="input-small" Text="0"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="span4 offset2">
                        <div class="control-group">
                            <label class="control-label" for="txtDoctor">Doctor</label>
                            <div class="controls">
                                <asp:DropDownList runat="server" ID="txtDoctor" ClientIDMode="Static" CssClass="input-xlarge"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span4">
                        <div class="control-group">
                            <label class="control-label" for="txtAmount">Amount</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtAmount" ClientIDMode="Static" CssClass="input-small" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="span4 offset2">
                        <div class="control-group">
                            <label class="control-label" for="txtDiscount">Discount</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtVisitDiscount" ClientIDMode="Static" CssClass="input-small" Text="0"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span4">
                        <div class="control-group">
                            <label class="control-label" for="txtMCDate">MC Date</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtMCDate" ClientIDMode="Static" CssClass="dt" Text=""></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="span4 offset2">
                        <div class="control-group">
                            <label class="control-label" for="txtMCDays">MC</label>
                            <div class="controls">
                                <asp:TextBox runat="server" ID="txtMCDays" ClientIDMode="Static" CssClass="input-small" Text="0"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">Next Appt.</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtNApptDate" CssClass="dt" ClientIDMode="Static"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="mdinfo">
            <div class="row-fluid">
                <div class="span4">
                    <div class="control-group">
                        <label class="control-label">Height</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtPHeight" CssClass="input-small" placeholder="cms" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="span4 offset2">
                    <div class="control-group">
                        <label class="control-label">Weight</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtPWeight" CssClass="input-small" placeholder="Kgs" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span4">
                    <div class="control-group">
                        <label class="control-label">BMI</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtBMI" CssClass="input-small" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span4">
                    <div class="control-group">
                        <label class="control-label" for="txtSigns">Signs & Symptoms</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtSigns" CssClass="input-xlarge" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="span4 offset2">
                    <div class="control-group">
                        <label class="control-label">General Findings</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtFindings" CssClass="input-xlarge" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span4">
                    <div class="control-group">
                        <label class="control-label">Treatment Notes</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtTreatmentNotes" CssClass="input-xlarge" TextMode="MultiLine" Rows="3" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="span4 offset2">
                    <div class="control-group">
                        <label class="control-label">Surgery Notes</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtSurgeryNotes" CssClass="input-xlarge" TextMode="MultiLine" Rows="3" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span4">
                    <div class="control-group">
                        <label class="control-label" for="txtExam">Health Complaints</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtExam" CssClass="input-xlarge" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="span4 offset2">
                    <div class="control-group">
                        <label class="control-label" for="txtDiagnosis">Diagnosis<%if(Session["usertype"].ToString()=="2") { %><a href="#" onclick="newDiagnosis()">+</a><%} %></label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtDiagnosis" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span4">
                    <div class="control-group">
                        <label class="control-label" for="txtRemarks">Remarks</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="5" CssClass="input-xlarge" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="span4 offset2">
                    <div class="control-group">
                        <label class="control-label" for="txtAllergies">Allergies</label>
                        <div class="controls">
                            <asp:TextBox runat="server" ID="txtAllergies" TextMode="MultiLine" Rows="5" CssClass="input-xlarge"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="tab-pane fade" id="chinfo">
            <h5>Services/ Charges</h5>
            <div class="control-group">
                <table id="tblServices" border="1" style="width:100%;border-collapse:separate;border-spacing:1px;">
                    <tbody>
                        <tr>
                            <th style="width:55%;padding:5px;">Service Description</th>
                            <th style="width:15%;padding:5px;text-align:right">Charges [RM]</th>
                            <th style="width:15%;padding:5px;text-align:right">Discount [RM]</th>
                            <th style="width:15%;padding:5px;text-align:right">Amount [RM]</th>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td><b>Total Amount</b></td>
                            <td><input type="text" id="txtTCharges" name="txtTCharges" readonly style="width:90%;text-align:right"/></td>
                            <td><input type="text" id="txtTDiscount" name="txtTDiscount" readonly style="width:90%;text-align:right"/></td>
                            <td><input type="text" id="txtTSAmount" name="txtTSAmount" readonly style="width:90%;text-align:right"/></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="tab-pane fade" id="dginfo">
            <h5>Medication/ Drugs</h5>
            <div class="control-group">
                <table id="tblDrugs" border="1" style="width:100%;border-collapse:separate;border-spacing:1px;">
                    <tbody>
                        <tr>
                            <th style="width:25%;padding:5px;">Drug Description</th>
                            <th style="width:25%;padding:5px;">Instructions</th>
                            <!--<th style="width:10%;padding:5px;text-align:right">Unit [RM]</th>-->
                            <th style="width:10%;padding:5px;text-align:right">Selling [RM]</th>
                            <th style="width:10%;padding:5px;text-align:right">Qty</th>
                            <th style="width:10%;padding:5px;text-align:right">Amt [RM]</th>
                            <th style="width:10%;padding:5px;text-align:right">Disc [RM]</th>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="4"><b>Total Amount</b></td>
                            <td><input type="text" id="txtTDAmount" name="txtTDAmount" readonly style="width:90%;text-align:right"/></td>
                            <td><input type="text" id="txtTDDiscount" name="txtTDDiscount" readonly style="width:90%;text-align:right"/></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <div class="tab-pane fade" id="clinfo">
            <h5>For Children</h5>
            <div class="control-group">
                <label class="control-label">Age (months)</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtAge" ClientIDMode="Static" Text="0"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Length (cms)</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtLength" ClientIDMode="Static" Text="0"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Weight (kgs)</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtWeight" ClientIDMode="Static" Text="0"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Head Circumference</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtCirc" ClientIDMode="Static" Text="0"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <p>
        <button class="btn btn-inverse" onClick="javascript:unlockRecord('<%=(Request.QueryString["_qD"]==null?"0":Request.QueryString["_qD"].ToString())%>','PAT_QUEUE');">Close</button>
        <% if (Session["usertype"].ToString() == "6" || Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "0")
           { %>
        <asp:Button runat="server" CssClass="btn btn-danger" Text="Delete Visit" OnClick="deleteVisit" />
        <% } %>
        <button type="button" class="btn btn-warning" onclick="saveVisit()">Save & Close</button>
    </p>
    <!--New Diagnosis starts-->
    <div class="modal hide fade" id="myDiagnosis" tabindex="-1" role="dialog" aria-labelledby="myDiscLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myDiscLabel">New Diagnosis</h3>
        </div>
        <div class="modal-body">
            <div class="control-group">
                <label class="control-label">Description</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtNewDiagnosis" CssClass="input-xlarge" placeholder="Eg: URTI" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button type="button" class="btn btn-inverse" onclick="addDiagnosis()">Add</button>
        </div>
    </div>
    <!--change discipline ends-->
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        function getServiceDtls(row) {
            var control = document.getElementsByName("service[]")[row];
            var index = control.selectedIndex;
            var serviceID = control.options[index].value;
            $.ajax({
                type: 'POST',
                url: 'PatientVisit.aspx/getServiceDtls',
                data:'{serviceID:"' + serviceID + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (typeof response.d.split("|")[1] == 'undefined') {
                        document.getElementsByName("charge[]")[row].value = 0;
                    }
                    else {
                        document.getElementsByName("charge[]")[row].value = parseFloat(response.d.split("|")[1]).toFixed(2);
                    }
                    if (typeof response.d.split("|")[2] == 'undefined') {
                        document.getElementsByName("stype[]")[row].value = 0;
                    }
                    else {
                        document.getElementsByName("stype[]")[row].value = parseInt(response.d.split("|")[2]);
                    }
                },
                error: function () {
                    alert("Ajax failure.");
                }
            });
        }
        function getDrugDtls(row) {
            var control = document.getElementsByName("drug[]")[row];
            var index = control.selectedIndex;
            var medID = control.options[index].value;
            $.ajax({
                type: 'POST',
                url: 'PatientVisit.aspx/getDrugDtls',
                data: '{medID:"' + medID + '"}',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (typeof response.d.split("|")[0]=='undefined'){
                        document.getElementsByName("ucost[]")[row].value = 0;
                        document.getElementsByName("scost[]")[row].value = 0;
                    }
                    else {
                        document.getElementsByName("ucost[]")[row].value = parseFloat(response.d.split("|")[0]).toFixed(2);
                        document.getElementsByName("scost[]")[row].value = parseFloat(response.d.split("|")[1]).toFixed(2);
                    }
                },
                error: function () {
                    alert("Ajax failure.");
                }
            });
        }
        $(function () {
            $('#tm').datetimepicker({
                language: 'en',
                pick12HourFormat: true,
                pickDate: false
            });
        });
        function saveVisit() {
            var Data = $('#frm').serializeArray();
            $.ajax({
                type: 'POST',
                url: 'PatientVisit.aspx/saveVisit',
                data: JSON.stringify({formVars:Data}),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d != '') {
                        alert(response.d);
                    }
                    else {
                        unlockRecord($('#qID').val(), "PAT_QUEUE");
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
        }
        function unlockRecord(qID, tblName) {
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
                error: function(xhr, ajaxOptions, thrownError){
                    alert(xhr.responseText);
                }
            });
        }
        function newDiagnosis() {
            $('#myDiagnosis').modal('show');
        }
        function addDiagnosis() {
            $.ajax({
                type: 'POST',
                url: 'PatientVisit.aspx/saveDiagnosis',
                data: '{nm:"' + document.getElementById("txtNewDiagnosis").value + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d.substring(0, 5) == 'ERROR') {
                        alert(response.d);
                    }
                    else 
                    {
                        $('#myDiagnosis').modal('hide');
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }

            });
        }
        $('#txtPWeight').blur(function() {
            var height = document.getElementById("txtPHeight").value;
            var weight = document.getElementById("txtPWeight").value;
            var mheigh = parseInt(height) / 100;

            var bmi = parseFloat(parseInt(weight) / (parseFloat(mheigh) * 2)).toFixed(2);

            document.getElementById("txtBMI").value = bmi;
        });
    </script>
</asp:Content>


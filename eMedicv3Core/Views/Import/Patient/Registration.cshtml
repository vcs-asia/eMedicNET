<%@ Page Language="C#" Title="Registration" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Patient_Registration" Codebehind="Registration.aspx.cs" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        $('#txtCompany').change(function () {
            $('#txtOutlet').val($('#txtCompany').val());
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentForm">
    <h4>Patient Registration</h4>
    <p><strong>MyKAD reading function works only on Internet Explorer. Make sure it is up to date version.</strong></p>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" Visible="false" ClientIDMode="Static">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved.
    </asp:Panel>
    <p>
        <button type="button" class="btn btn-success" onclick="saveInfo()">Save</button>
    </p>
    <div class="well">
        <h5>Profile</h5>
        <div class="control-group">
            <label class="control-label">Name</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtPatientName" CssClass="input-xxlarge" Required="true" ClientIDMode="Static" oninvalid="setCustomValidity('Please enter the Patient Name')"></asp:TextBox>
                <asp:HiddenField runat="server" ID="hdnPatID" Value="0" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtICNo">IC/Passport No.<a href="#" onclick="javascript:getMyKadInfo(frm)"><i class="icon-barcode"></i></a></label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtICNo" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtRegnDate">Regn. Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtRegnDate" CssClass="dt" Required="true"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="Birth Date">Birth Date</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtBirthDate" CssClass="dt" Required="true" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtFolderNo">Folder No.</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtFolderNo" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <!--
        <div class="control-group">
            <label class="control-label" for="txtAge">Age</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAge" CssClass="input-small"></asp:TextBox>
            </div>
        </div>
        -->
        <div class="control-group">
            <label class="control-label" for="lstSex">Sex</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstSex" ClientIDMode="Static">
                    <asp:ListItem Value="0">Male</asp:ListItem>
                    <asp:ListItem Value="1">Female</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="lstStatus">Status</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstStatus">
                    <asp:ListItem Value="0">Single</asp:ListItem>
                    <asp:ListItem Value="1">Married</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtNationality">Nationality</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtNationality" CssClass="input-xlarge" ClientIDMode="Static"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtRace">Race</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtRace" Required="true" ClientIDMode="Static"></asp:DropDownList>
            </div>
        </div>
        <!--
        <div class="control-group">
            <label class="control-label" for="lstType">Type</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstType">
                    <asp:ListItem Value="0">In Patient</asp:ListItem>
                    <asp:ListItem Value="1">Out Patient</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtReligion">Religion</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtReligion" ClientIDMode="Static"/>
            </div>
        </div>
        -->
        <div class="control-group">
            <label class="control-label" for="lstBloodGroup">Blood Group</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="lstBloodGroup">
                    <asp:ListItem Value="1">Unknown</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtRegnNo">Regn No</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtRegnNo" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <h5>Contact Information</h5>
        <div class="control-group">
            <label class="control-label" for="txtAddr1">Address</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAddr1" CssClass="input-xxlarge" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtAddr2">&nbsp;</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAddr2" CssClass="input-xxlarge" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtAddr3">&nbsp;</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAddr3" CssClass="input-xxlarge" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtPhones">Phones</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtPhones" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtHandPhone">Hand Phone</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtHandPhone" CssClass="input-xlarge" Required="true" Text="012"></asp:TextBox>
            </div>
        </div>
        <h5>Panel Information</h5>
        <div class="control-group">
            <label class="control-label" for="txtCompany">Company</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtCompany" CssClass="input-xxlarge" ClientIDMode="Static"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtOutlet">Outlet</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtOutlet" CssClass="input-large" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtStatus">Status</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtStatus" CssClass="input-large"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtRelation">Relation</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtRelation" CssClass="input-large" ClientIDMode="Static" Required="true"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtEmployee">Employee</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtEmployee" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtEmpNo">Emp. No.</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtEmpNo" CssClass="input-large"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtCostCentre">Cost Centre</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtCostCentre" CssClass="input-large"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtDept">Department</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtDept" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtEmpRemarks">Remarks</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtEmpRemarks" TextMode="MultiLine" Rows="3" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <h5>Next of Kin</h5>
        <div class="control-group">
            <label class="control-label" for="txtNextKin">Next of Kin</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtNextKin" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtNKICNo">IC No.</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtNKICNo"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtNKRelation">Relation</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtNKRelation" ClientIDMode="Static"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtNKOccupation">Occupation</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtNKOccupation" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtNKPhones">Phones</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtNKPhones" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtNKAddress">Address</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtNKAddress" TextMode="MultiLine" CssClass="input-xxlarge" Rows="5"></asp:TextBox>
            </div>
        </div>
        <h5>Guarantor</h5>
        <div class="control-group">
            <label class="control-label" for="txtGuarantor">Guarantor</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtGuarantor" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtGICNo">IC No.</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtGICNo"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtGRelation">Relation</label>
            <div class="controls">
                <asp:DropDownList runat="server" ID="txtGRelation" ClientIDMode="Static"></asp:DropDownList>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtGOccupation">Occupation</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtGOccupation" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtGPhones">Phones</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtGPhones" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtGAddress">Address</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtGAddress" TextMode="MultiLine" CssClass="input-xxlarge" Rows="5"></asp:TextBox>
            </div>
        </div>
        <h5>Others</h5>
        <div class="control-group">
            <label class="control-label" for="txtRemarks">Remarks</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="5" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label" for="txtAllergies">Allergies</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtAllergies" TextMode="MultiLine" Rows="5" CssClass="input-xxlarge"></asp:TextBox>
            </div>
        </div>
    </div>
    <p>
        <button type="button" class="btn btn-success" onclick="saveInfo()">Save</button>
    </p>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        $("#txtICNo").blur(function () {
            var patLetter = $("#txtPatientName").val();
            var icLetter = $("#txtICNo").val();
            var dob = $("#txtBirthDate").val();
            if (icLetter.length == 14) {
                if (parseInt(icLetter.substr(0,2)) >= 0 && parseInt(icLetter.substr(0,2)) <= 16)
                    $("#txtBirthDate").val(icLetter.substr(4, 2) + '/' + icLetter.substr(2, 2) + '/20' + icLetter.substr(0, 2));
                else
                    $("#txtBirthDate").val(icLetter.substr(4, 2) + '/' + icLetter.substr(2, 2) + '/19' + icLetter.substr(0, 2));
            }
            else if (icLetter.length == 12) {
                if (parseInt(icLetter.substr(0,2)) >= 0 && parseInt(icLetter.substr(0,2)) <= 16)
                    $("#txtBirthDate").val(icLetter.substr(4, 2) + '/' + icLetter.substr(2, 2) + '/20' + icLetter.substr(0, 2));
                else
                    $("#txtBirthDate").val(icLetter.substr(4, 2) + '/' + icLetter.substr(2, 2) + '/19' + icLetter.substr(0, 2));
                    //$("#txtBirthDate").val(icLetter.substr(4, 2) + '/' + icLetter.substr(2, 2) + '/19' + icLetter.substr(0, 2));
            }
        });
        $("#txtBirthDate").blur(function () {
            var patLetter = $("#txtPatientName").val();
            var icLetter = $("#txtICNo").val();
            var dob = $("#txtBirthDate").val();
            if (<%:System.Configuration.ConfigurationManager.AppSettings["regtype"].ToString()%>=="1"){
                if (dob!="") {
                    $("#txtFolderNo").val(icLetter.substr(10,4)); //720715-06-5437
                }
                else if (icLetter.length == 12) {
                    $("#txtFolderNo").val(icLetter.substr(8, 4)); //720715065437
                }
                else if (icLetter.length != '') {
                    $("#txtFolderNo").val(icLetter.substr($("#txtICNo").val().length-4, 4)); //J2232666
                }
            }
            else if (<%:System.Configuration.ConfigurationManager.AppSettings["regtype"].ToString()%>=="2"){
                if (dob!="") {
                    $("#txtFolderNo").val(patLetter.substr(0, 1) + '-' + icLetter.substr(10, 4)); //720715-06-5437
                }
                else if (icLetter.length == 12) {
                    $("#txtFolderNo").val(patLetter.substr(0, 1) + '-' + icLetter.substr(8, 4)); //720715065437
                }
                else if (icLetter.length != '') {
                    $("#txtFolderNo").val(patLetter.substr(0, 1) + '-' + icLetter.substr(8, 4)); //J2232666
                }
            }
            else if (<%:System.Configuration.ConfigurationManager.AppSettings["regtype"].ToString()%>=="3"){
                if (dob != "") {
                    $("#txtFolderNo").val(dob.substr(3, 2) + dob.substr(0, 2)); //01/10/2015
                }
            }
        });
        
        function getMyKadInfo(frm){
            try{
                var obj = new ActiveXObject("mykadproweb.mykadproweb.jpn");
                var strRet = obj.BeginJPN("Feitian SCR301 0");
                if (strRet=="0"){
                    document.getElementById("txtPatientName").value = obj.getKPTName();
                    if (obj.getKPTName()=='')
                        document.getElementById("txtPatientName").value = obj.getGMPCName();
                    document.getElementById("txtICNo").value = obj.getIDNum();
                    if (obj.getGender()=='L')
                        document.getElementById("lstSex").value = '0';
                    else
                        document.getElementById("lstSex").value = '1';
                    document.getElementById("txtAddr1").value = obj.getAddress().split("\n")[0];
                    document.getElementById("txtAddr2").value = obj.getAddress().split("\n")[1];
                    document.getElementById("txtAddr3").value = obj.getAddress().split("\n")[2];
                    document.getElementById("txtBirthDate").value = obj.getBirthDate().substr(8,2) + '/' + obj.getBirthDate().substr(5,2) + '/' + obj.getBirthDate().substr(0,4);
                }
                else{
                    alert("MyKad reader returns" + strRet);
                }
            }
            catch(e){
                alert("Please contact vendor. Your device might be having a problem " + e.message);
            }
        }
        function saveInfo(){
            var Data = $("#frm").serializeArray();

            $.ajax({
                type: 'POST',
                url: 'Registration.aspx/saveInfo',
                data: JSON.stringify({ frmV:Data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function(response){
                    if (response.d!=''){
                        alert(response.d);
                    }
                    else {
                        parent.location.reload();
                        window.close();
                    }
                },
                error: function(xhr, ajaxOptions, thrownError){
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>

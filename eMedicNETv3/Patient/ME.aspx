<%@ Page Language="C#" AutoEventWireup="true" Title="ME" MasterPageFile="~/eMedicNET.master" Inherits="Patient_ME" Codebehind="ME.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h3 id="headRecord">Medical Exam</h3>
    <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" ClientIDMode="Static" Visible="false">
        <button type="button" class="close" data-dismiss="alert">x</button>
        <strong>SUCCESS!</strong> The Record has been saved Successfully.
    </asp:Panel>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Occupation"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtOccupation" Required="true" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hdnVisitID" Value="" ClientIDMode="Static" />
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Family History"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtFamilyHistory" Required="true" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Past History"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPastHistory" Required="true" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Allergy History"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtAllergyHistory" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Present Complaints"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPresentComplaints" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Height"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtHeight" ClientIDMode="Static" Text="0" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Weight"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtWeight" ClientIDMode="Static" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="BMI"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtBMI" ClientIDMode="Static" Text="0" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Blood Pressure (mmHg)"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtBP" ClientIDMode="Static" placeholder= "110/70" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span4 offset2">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Pulse Rate /min"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtPulse" ClientIDMode="Static" placeholder="88" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="VISION"></asp:Label>
                <div class="controls">
                    <asp:Label runat="server" Text="RIGHT"></asp:Label>
                </div>
            </div>
        </div>
        <div class="span4 offset2">
            <div class="control-group">
                <div class="controls">
                    <asp:Label runat="server" Text="LEFT"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Corrected"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtLCorrected" ClientIDMode="Static" placeholder="6/9" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span4">
            <div class="control-group">
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtRCorrected" ClientIDMode="Static" placeholder="6/9" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span4">
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Uncorrected"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtLUncorrected" ClientIDMode="Static" placeholder="6/9" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="span4">
            <div class="control-group">
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtRUncorrected" ClientIDMode="Static" placeholder="6/9" CssClass="input-small"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Colour Vision"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtColourVision" ClientIDMode="Static" Text="Normal" CssClass="input-small"></asp:TextBox>
        </div>
    </div>
    <!--To be dynamic //Starts here//-->
    <div id="divPhy">
        <!--
        <div class="row-fluid">
            <div class="span4">
                <div class="control-group">
                    <label class="control-label">Test 1</label>
                    <div class="controls">
                        <input type="checkbox" id="chk[]" name="chk[]"/>
                    </div>
                </div>
            </div>
            <div class="span4 offset2">
                <div class="control-group">
                    <label class="control-label">Test 2</label>
                    <div class="controls">
                        <input type="checkbox" id="chk[]" name="chk[]"/>
                    </div>
                </div>
            </div>
        </div>
        -->
    </div>
    <!--//Ends here-->
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Remarks"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtRemarks" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="3"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Protein"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtProtein" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="pH"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtPH" ClientIDMode="Static" CssClass="input-small"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Sugar"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtSugar" ClientIDMode="Static" CssClass="input-smalle"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="SG"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtSG" ClientIDMode="Static" CssClass="input-small"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Micro"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtMicro" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Drug Screen [Qualitative]"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtDrugScreen" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Other Test"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtOtherTest" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Chest X-Ray Report"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtChestXRay" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Findings/ Recommendations"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtFindings" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <div class="control-group">
        <asp:Label runat="server" CssClass="control-label" Text="Conclusion"></asp:Label>
        <div class="controls">
            <asp:TextBox runat="server" ID="txtConclusion" ClientIDMode="Static" CssClass="input-xxlarge" TextMode="MultiLine" Rows="4"></asp:TextBox>
        </div>
    </div>
    <p>
        <Button type="button" id="btnSave" name="btnSave" class="btn btn-success" onclick="saveInfo()">Save</Button>
        <Button type="button" id="btnPrint" name="btnPrint" class="btn btn-invert" onclick="printInfo()">Print</Button>
    </p>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            loadPhysicalExams();
        });
        function loadPhysicalExams() {
            var col1 = '';
            var col2 = '';
            var val1 = '0';
            var val2 = '0'
            var str = '';
            $.ajax({
                type: 'POST',
                url: 'ME.aspx/getPhysicalExams',
                data: '',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d[0].substr(0, 5) == 'ERROR') {
                        alert(response.d[0]);
                    }
                    else {
                        str = '';
                        for (var inc = 0; inc < response.d.length; inc++) {
                            col1 = response.d[inc].split('|')[0];
                            val1 = response.d[inc].split('|')[1];
                            if ((inc + 1) == response.d.length){
                                col2 = ""; val2 = '0';
                            }
                            else{
                                col2 = response.d[inc + 1].split('|')[0];
                                val2 = response.d[inc + 1].split('|')[1];
                            }
                            str += '<div class=\"row-fluid\">';
                            str += '<div class=\"span4\">';
                            str += '<div class=\"control-group\">';
                            str += '<label class=\"control-label\">' + col1 + '</label>';
                            str += '<div class=\"controls\">';
                            str += '<input type=\"checkbox\" id=\"chk[]\" name=\"chk[]\" value=\"' + val1 + '\"/>';
                            str += '</div>';
                            str += '</div>';
                            str += '</div>';
                            if (col2 != "") {
                                str += '<div class=\"span4 offset2\">';
                                str += '<div class=\"control-group\">';
                                str += '<label class=\"control-label\">' + col2 + '</label>';
                                str += '<div class=\"controls\">';
                                str += '<input type=\"checkbox\" id=\"chk[]\" name=\"chk[]\" value=\"' + val2 + '\"/>';
                                str += '</div>';
                                str += '</div>';
                                str += '</div>';
                            }
                            str += '</div>';

                            inc += 1;
                        }
                        $('#divPhy').append(str);
                    }
                },
                error:function(xhr, ajaxOptions, thrownerror){
                    alert(xhr.responseText);
                }
            });
        }
        function saveInfo() {
            var data = $("#frm").serializeArray();
            $.ajax({
                type: 'POST',
                url: 'ME.aspx/saveInfo',
                data: JSON.stringify({ formVars: data }),
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d.substr(0, 5) == 'ERROR') {
                        alert(response.d);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>


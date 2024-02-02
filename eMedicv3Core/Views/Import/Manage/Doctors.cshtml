<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Doctors" AutoEventWireup="true" Inherits="Manage_Doctors" Codebehind="Doctors.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Doctors</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="DOC_NAME">Doctor Name</asp:ListItem>
                <asp:ListItem Value="COMP_NAME">Discipline</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onClick="javascript:openDoctor(0);">New</button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="DOC_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="DOC_NAME" SortExpression="DOC_NAME" ItemStyle-Width="30%" HeaderText="Name of Doctor" />
            <asp:BoundField DataField="COMP_NAME" SortExpression="COMP_NAME" ItemStyle-Width="20%" HeaderText="Discipline" />
            <asp:BoundField DataField="DOC_HANDPHONE" SortExpression="DOC_HANDPHONE" ItemStyle-Width="10%" HeaderText="H/Phone" />
            <asp:BoundField DataField="DOC_EMAIL" SortExpression="DOC_EMAIL" ItemStyle-Width="20%" HeaderText="Email" />
            <asp:BoundField DataField="DOC_QUALIFICATION" SortExpression="DOC_QUALIFICATION" ItemStyle-Width="18%" HeaderText="Qualification" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openDoctor({0})", HttpUtility.UrlEncode(Eval("DOC_ID").ToString())) %>' ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument="" CommandName="delete" ToolTip="Delete"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--Doctor Editing/New Starts-->
    <div class="modal hide fade" id="myDoctor" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myReceiptLabel">Doctor</h3>
        </div>
        <div class="modal-body">
            <asp:HiddenField runat="server" ID="hdnID" ClientIDMode="Static" />
            <div class="control-group">
                <label class="control-label">Name</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Sex</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstSex" ClientIDMode="Static">
                        <asp:ListItem Value="0">MALE</asp:ListItem>
                        <asp:ListItem Value="1">FEMALE</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Qualification</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtQualification" CssClass="input-xlarge" ClientIDMode="Static"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Email</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" CssClass="input-large" placeholder="someone@domain.com"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Specialization</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstSpec" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Type</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstType" ClientIDMode="Static">
                        <asp:ListItem Value="0">IN HOUSE</asp:ListItem>
                        <asp:ListItem Value="1">CONSULTANT</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Mobile</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtHp" ClientIDMode="Static" CssClass="input-xsmall" placeholder="0123456789"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button type="button" class="btn btn-success" onclick='javascript:saveDoctor()'>Save</button>
        </div>
    </div>
    <!--Doctor Editing/New Ends-->
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function openDoctor(_docID) {
            $.ajax({
                type: 'POST',
                url: 'Doctors.aspx/getInfo',
                data: '{docID:"' + _docID + '"}',
                dataType: 'JSON',
                contentType: 'application/json;charset=utf-8',
                success: function (response) {
                    document.getElementById("txtName").value = response.d[0];
                    document.getElementById("lstSex").value = response.d[1];
                    document.getElementById("txtQualification").value = response.d[2];
                    document.getElementById("txtEmail").value = response.d[3];
                    document.getElementById("lstSpec").value = response.d[4];
                    document.getElementById("lstType").value = response.d[5];
                    document.getElementById("txtHp").value = response.d[6];
                    document.getElementById("hdnID").value = _docID;
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
            $("#myDoctor").modal('show');
        }
        function saveDoctor() {
            var _docID = document.getElementById("hdnID").value;
            var _docNm = document.getElementById("txtName").value;
            var _docSx = document.getElementById("lstSex").value;
            var _docQn = document.getElementById("txtQualification").value;
            var _docEm = document.getElementById("txtEmail").value;
            var _docSp = document.getElementById("lstSpec").value;
            var _docTp = document.getElementById("lstType").value;
            var _docHp = document.getElementById("txtHp").value;

            $.ajax({
                type: 'POST',
                url: 'Doctors.aspx/saveDoctor',
                data: '{docID:"' + _docID + '" ,docName: "' + _docNm + '",docSex: "' + _docSx + '", docQualification: "' + _docQn + '", docEmail: "' + _docEm + '",docSpecialization: "' + _docSp + '", docType:"' + _docTp + '", docHP:"' + _docHp + '"}',
                dataType: 'json',
                contentType:'application/json;charset=utf-8',
                success: function (response) {
                    if (response.d.substring(0, 5) != 'ERROR')
                        location.reload();
                    else
                        alert(response.d);
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>
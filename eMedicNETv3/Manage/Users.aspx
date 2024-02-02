<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Account_Users" Codebehind="Users.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Users List</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="USER_NAME">User Name</asp:ListItem>
                <asp:ListItem Value="USER_LOGIN_ID">Login ID</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="bSearch" CssClass="btn" Text="Search" OnClick="OnSearch" UseSubmitBehavior="false"/>
            <Button type="button" class="btn" onclick="javascript:openRecord('0')">New</Button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="USER_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="USER_LOGIN_ID" SortExpression="USER_LOGIN_ID" ItemStyle-Width="20%" HeaderText="Login ID" />
            <asp:BoundField DataField="USER_NAME" SortExpression="USER_NAME" ItemStyle-Width="30%" HeaderText="User Name" />
            <asp:BoundField DataField="USER_SEX" SortExpression="USER_SEX" ItemStyle-Width="10%" HeaderText="User Sex" />
            <asp:BoundField DataField="USER_TYPE" SortExpression="USER_TYPE" ItemStyle-Width="10%" HeaderText="User Type" />
            <asp:BoundField DataField="USER_EMAIL" SortExpression="USER_EMAIL" ItemStyle-Width="30%" HeaderText="Email" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openRecord({0})", HttpUtility.UrlEncode(Eval("USER_ID").ToString())) %>' ToolTip="Edit User"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ToolTip="Deactivate User" CommandArgument="" CommandName="delete"><i class="icon-ban-circle"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" ToolTip="Reset Password" NavigateUrl='<%# String.Format("javascript:resetPassword({0})",HttpUtility.UrlEncode(Eval("USER_ID").ToString())) %>'><i class="icon-remove-circle"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--User Editing/New Starts-->
    <div class="modal hide fade" id="myUser" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myModalLabel">User</h3>
        </div>
        <div class="modal-body">
            <asp:Panel runat="server" ID="pnlError" CssClass="alert alert-error error-icon" ClientIDMode="Static" Visible="false">
                <button type="button" class="close" data-dismiss="alert">x</button>
                <asp:Label runat="server" ID="lblError" ClientIDMode="Static"></asp:Label>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlSuccess" CssClass="alert alert-success success-icon" ClientIDMode="Static" Visible="false">
                <button type="button" class="close" data-dismiss="alert">x</button>
                <strong>SUCCESS!</strong> The Record has been saved Successfully.
            </asp:Panel>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="User Name"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtUserName" Required="true" ClientIDMode="Static" CssClass="input-xlarge" placeholder="NAME"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hdnID" Value="0" ClientIDMode="Static" />
                </div>
            </div>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Login ID"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtLoginID" Required="true" ClientIDMode="Static" CssClass="input-large" placeholder="NAME"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Sex"></asp:Label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstSex" ClientIDMode="Static">
                        <asp:ListItem Value="1">MALE</asp:ListItem>
                        <asp:ListItem Value="2">FEMALE</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="User Type"></asp:Label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstUType" ClientIDMode="Static">
                        <asp:ListItem Value="1">Administrator</asp:ListItem>
                        <asp:ListItem Value="2">Doctor</asp:ListItem>
                        <asp:ListItem Value="3">Cashier</asp:ListItem>
                        <asp:ListItem Value="4">Pharmacy</asp:ListItem>
                        <asp:ListItem Value="5">Reception</asp:ListItem>
                        <asp:ListItem Value="6">Office</asp:ListItem>
                        <asp:ListItem Value="7">Inventory Full</asp:ListItem>
                        <asp:ListItem Value="8">Billing</asp:ListItem>
                        <asp:ListItem Value="9">Patient</asp:ListItem>
                        <asp:ListItem Value="10">Inventory Store</asp:ListItem>
                        <asp:ListItem Value="11">Inventory Outlet</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Email"></asp:Label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtUEmail" Required="true" ClientIDMode="Static" CssClass="input-xlarge"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Doctor"></asp:Label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstDoctor" ClientIDMode="Static">

                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <asp:Label runat="server" CssClass="control-label" Text="Status"></asp:Label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstStatus" ClientIDMode="Static">
                        <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                        <asp:ListItem Value="0">IN-ACTIVE</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button type="button" class="btn btn-success" onclick='javascript:saveRecord()'>Save</button>
        </div>
    </div>
    <!--Service Editing/New Ends-->
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function openRecord(_id) {
            $.ajax({
                type: 'POST',
                url: 'Users.aspx/getDetails',
                data: '{id:"' + _id + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    document.getElementById("txtUserName").value = respond.d[0];
                    document.getElementById("txtLoginID").value = respond.d[1];
                    document.getElementById("lstSex").value = respond.d[2];
                    document.getElementById("lstUType").value = respond.d[3];
                    document.getElementById("txtUEmail").value = respond.d[4];
                    document.getElementById("lstDoctor").value = respond.d[5];
                    document.getElementById("lstStatus").value = respond.d[6];
                    document.getElementById("hdnID").value = _id;
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.respondText);
                }
            });
            if (_id == '0') {
                document.getElementById("txtUserName").value = '';
                document.getElementById("txtLoginID").value = '';
                document.getElementById("lstSex").value = '1';
                document.getElementById("lstUType").value = '2';
                document.getElementById("txtUEmail").value = '';
                document.getElementById("lstDoctor").value = '0';
                document.getElementById("lstStatus").value = '0';
                document.getElementById("hdnID").value = '0';
            }
            $('#myUser').modal('show');
        }

        function saveRecord() {
            var _nm = document.getElementById("txtUserName").value;
            var _ld = document.getElementById("txtLoginID").value;
            var _sx = document.getElementById("lstSex").value;
            var _tp = document.getElementById("lstUType").value;
            var _em = document.getElementById("txtUEmail").value = '';
            var _dc = document.getElementById("lstDoctor").value;
            var _st = document.getElementById("lstStatus").value;
            var _id = document.getElementById("hdnID").value;
            $.ajax({
                type: 'POST',
                url: 'Users.aspx/saveDetails',
                data: '{id:"' + _id + '", nm: "' + _nm + '", ld: "' + _ld + '", sx:"' + _sx + '", tp:"' + _tp + '", em:"' + _em + '", dc:"' + _dc + '", st:"' + _st + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    if (respond.d.substring(0, 5) == 'ERROR') {
                        alert(respond.d);
                    }
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.respondText);
                }
            });
        }
        function resetPassword(_id) {
            $.ajax({
                type: 'POST',
                url: 'Users.aspx/resetPassword',
                data: '{userid:"' + _id + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    if (respond.d.substring(0, 5) == 'ERROR')
                        alert(respond.d);
                    else
                        alert('The password has been reset successfully to 12345678.');
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>
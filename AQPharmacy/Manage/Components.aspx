<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Components" AutoEventWireup="true" Inherits="Manage_Components" Codebehind="Components.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Components</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="COMP_NAME">Name</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onclick="javascript:openRecord('0');">New</button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="COMP_ID, COMP_TYPE" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight" OnRowDeleting="Lst_RowDeleting">
        <Columns>
            <asp:BoundField DataField="COMPTYPE" SortExpression="COMPTYPE" ItemStyle-Width="38%" HeaderText="Type" />
            <asp:BoundField DataField="COMP_NAME" SortExpression="COMP_NAME" ItemStyle-Width="70%" HeaderText="Name" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openRecord({0})", HttpUtility.UrlEncode(Eval("COMP_ID").ToString())) %>' ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument= "" CommandName="delete" ToolTip="Delete"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--Component Editing/New Starts-->
    <div class="modal hide fade" id="myComponent" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myReceiptLabel">Component</h3>
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
                <label class="control-label">Type</label>
                <div class="controls">
                    <asp:DropDownList runat="server" ID="lstType" ClientIDMode="Static">
                        <asp:ListItem Value="0">DIAGNOSIS</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">DISCIPLINE</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">NATIONALITY</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn" data-dismiss="modal" aria-hidden="true">Cancel</button>
            <button type="button" class="btn btn-success" onclick='javascript:saveRecord()'>Save</button>
        </div>
    </div>
    <!--Component Editing/New Ends-->
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function openRecord(_id) {
            $.ajax({
                type: 'POST',
                url: 'Components.aspx/getDetails',
                data: '{id:"' + _id + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    document.getElementById("txtName").value = respond.d[0];
                    document.getElementById("lstType").value = respond.d[1];
                    document.getElementById("hdnID").value = _id;
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.respondText);
                }
            });
            if (_id == '0') {
                document.getElementById("txtName").value = '';
                document.getElementById("lstType").value = '1';
                document.getElementById("hdnID").value = '0';
            }
            $('#myComponent').modal('show');
        }

        function saveRecord() {
            var _nm = document.getElementById("txtName").value;
            var _tp = document.getElementById("lstType").value;
            var _id = document.getElementById("hdnID").value;
            $.ajax({
                type: 'POST',
                url: 'Components.aspx/saveDetails',
                data: '{id:"' + _id + '", nm: "' + _nm + '", tp: "' + _tp + '"}',
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
    </script>
</asp:Content>
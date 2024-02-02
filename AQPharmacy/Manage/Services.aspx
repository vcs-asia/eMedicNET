<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" Title="Services" AutoEventWireup="true" Inherits="Manage_Services" Codebehind="Services.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Services</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="SERVICE_NAME">Service Name</asp:ListItem>
                <asp:ListItem Value="SERVICE_TYPE">Service Type</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onclick="javascript:openRecord('0');">New</button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="SERVICE_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="SERVICE_NAME" SortExpression="SERVICE_NAME" ItemStyle-Width="25%" HeaderText="Name of Service" />
            <asp:BoundField DataField="SERVICE_DESC" SortExpression="SERVICE_DESC" ItemStyle-Width="30%" HeaderText="Description" />
            <asp:BoundField DataField="SERVICE_CHARGE" SortExpression="SERVICE_CHARGE" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Right" HeaderText="Charges [RM]" DataFormatString="{0:0.00}" />
            <asp:BoundField DataField="SERVICE_TYPE" SortExpression="SERVICE_TYPE" ItemStyle-Width="25%" HeaderText="Service Type" />
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("javascript:openRecord({0})", HttpUtility.UrlEncode(Eval("SERVICE_ID").ToString())) %>' ToolTip="Edit"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="1%">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument="" CommandName="delete" ToolTip="Delete"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!--Service Editing/New Starts-->
    <div class="modal hide fade" id="myService" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myReceiptLabel">Service</h3>
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
                        <asp:ListItem Value="0">CONSULTATION</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label">Charges</label>
                <div class="controls">
                    <asp:TextBox runat="server" ID="txtCharges" ClientIDMode="Static" CssClass="input-xsmall"></asp:TextBox>
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
                url: 'Services.aspx/getDetails',
                data: '{id:"' + _id + '"}',
                dataType: 'json',
                contentType: 'application/json;charset=utf-8',
                success: function (respond) {
                    document.getElementById("txtName").value = respond.d[0];
                    document.getElementById("lstType").value = respond.d[1];
                    document.getElementById("txtCharges").value = respond.d[2];
                    document.getElementById("hdnID").value = _id;
                },
                error: function (xhr, ajaxOptions, thrownerror) {
                    alert(xhr.respondText);
                }
            });
            if (_id == '0') {
                document.getElementById("txtName").value = '';
                document.getElementById("lstType").value = '1';
                document.getElementById("txtCharges").value = '0';
                document.getElementById("hdnID").value = '0';
            }
            $('#myService').modal('show');
        }

        function saveRecord() {
            var _nm = document.getElementById("txtName").value;
            var _tp = document.getElementById("lstType").value;
            var _ch = document.getElementById("txtCharges").value;
            var _id = document.getElementById("hdnID").value;
            $.ajax({
                type: 'POST',
                url: 'Services.aspx/saveDetails',
                data: '{id:"' + _id + '", nm: "' + _nm + '", tp: "' + _tp + '", ch:"' + _ch + '"}',
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
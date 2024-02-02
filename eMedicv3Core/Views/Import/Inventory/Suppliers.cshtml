<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_Suppliers" Codebehind="Suppliers.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Suppliers List</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="SUPPLIER_NAME">Name</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button type="button" class="btn" onclick="openSupplier(0)">New</button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="SUPPLIER_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDeleting="OnRowDeleting" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight" Font-Size="Small">
        <Columns>
            <asp:BoundField DataField="SUPPLIER_NAME" SortExpression="SUPPLIER_NAME" ItemStyle-Width="40%" HeaderText="Name of Supplier" />
            <asp:BoundField DataField="SUPPLIER_CONT_PERSON" SortExpression="SUPPLIER_CONT_PERSON" ItemStyle-Width="20%"  HeaderText="Contact Person"/>
            <asp:BoundField DataField="SUPPLIER_PHONE1" SortExpression="SUPPLIER_PHONE1" ItemStyle-Width="20%"  HeaderText="Phone"/>
            <asp:BoundField DataField="SUPPLIER_FAX" SortExpression="SUPPLIER_FAX" ItemStyle-Width="20%"  HeaderText="Fax"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%#String.Format("javascript:openSupplier({0})",Eval("SUPPLIER_ID")) %>' ToolTip="Edit Supplier Details"><i class="icon-edit"></i></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="delete" CommandArgument='<%#Eval("SUPPLIER_ID") %>' ToolTip="Delet Supplier"><i class="icon-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        $("[src*=expand]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "<%:ResolveUrl("~/Content/img/icon-collapse.gif")%>");
        });
        $("[src*=collapse]").live("click", function () {
            $(this).attr("src", "<%:ResolveUrl("~/Content/img/icon-expand.gif")%>");
            $(this).closest("tr").next().remove();
        });
        function openSupplier(_id) {
            var win = window.open("<%:ResolveUrl("~/Inventory/Supplier.aspx")%>?_id=" + _id);
            return false;
        }
    </script>
</asp:Content>
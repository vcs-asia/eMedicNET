<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Inventory_Outlets" Codebehind="Outlets.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Outlets</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="OUTLET_NAME">Outlet</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="OUTLET_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="med_stock_code" SortExpression="med_stock_code" ItemStyle-Width="10%" HeaderText="Code" />
            <asp:BoundField DataField="med_name" SortExpression="med_name" ItemStyle-Width="20%" HeaderText="Name of Drug" />
            <asp:BoundField DataField="med_generic_name" SortExpression="med_generic_name" ItemStyle-Width="20%"  HeaderText="Generic Name"/>
            <asp:BoundField DataField="BAL" SortExpression="BAL" ItemStyle-Width="10%" HeaderText="Balance Stock" />
            <asp:TemplateField ItemStyle-Width="5%" HeaderText="Option">
                <ItemTemplate>
                    <div class='btn-group'>
                        <asp:Button runat="server" CssClass='btn btn-mini' Text='<%# (Convert.ToInt16(Eval("MED_FLAG"))==1) ? "Active" : "In-Active" %>'/>
						<button class='btn btn-mini dropdown-toggle' data-toggle='dropdown'>
						    <span class='caret'></span>
						</button>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:HyperLink runat="server" ID="linkActive" Text="Active" NavigateUrl="#"></asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink runat="server" ID="linkInActive" Text="In-Active" NavigateUrl="#"></asp:HyperLink>
                            </li>
                        </ul>
					</div>
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
    </script>
</asp:Content>
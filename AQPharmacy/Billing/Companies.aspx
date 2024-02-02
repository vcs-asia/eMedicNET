<%@ Page Language="C#" MasterPageFile="~/eMedicNET.master" AutoEventWireup="true" Inherits="Billing_Companies" Codebehind="Companies.aspx.cs" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentForm" runat="server">
    <h4>Companies List</h4>
    <div class="control-group">
        <div class="controls">
            <asp:DropDownList runat="server" ID="lstFields">
                <asp:ListItem Value="COMPANY_NAME">Name</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox runat="server" ID="txtKeyword" placeholder="Search"></asp:TextBox>
            <asp:Button runat="server" ID="btnSearch" CssClass="btn" Text="Search" OnCommand="searchKeyword"/>
            <button class="btn" onclick='javascript:var wincmp = window.open("<%:ResolveUrl("~/Billing/Company.aspx?_id=0")%>", "_blank"); return false;'>New</button>
        </div>
    </div>
    
    <asp:GridView runat="server" ID="Lst" CssClass="" DataKeyNames="COMPANY_ID" AutoGenerateColumns="false" GridLines="None" CellPadding="1" CellSpacing="1" AllowPaging="true" Width="100%" PageSize="15"
        OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" OnSorting="Sorting" AllowSorting="true"  AlternatingRowStyle-BackColor="ControlLightLight">
        <Columns>
            <asp:BoundField DataField="COMPANY_NAME" SortExpression="COMPANY_NAME" ItemStyle-Width="40%" HeaderText="Name of Company" />
            <asp:BoundField DataField="COMPANY_CONT_PERSON" SortExpression="COMPANY_CONT_PERSON" ItemStyle-Width="20%"  HeaderText="Contact Person"/>
            <asp:BoundField DataField="COMPANY_PHONE1" SortExpression="COMPANY_PHONE1" ItemStyle-Width="20%"  HeaderText="Phone"/>
            <asp:BoundField DataField="COMPANY_FAX" SortExpression="COMPANY_FAX" ItemStyle-Width="20%"  HeaderText="Fax"/>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%#String.Format("~/Billing/Company.aspx?_id={0}",Eval("COMPANY_ID")) %>' Target="_blank" ToolTip="Edit Company Details"><i class="icon-edit"></i></asp:HyperLink>
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